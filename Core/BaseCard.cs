using CardMod.Content.Slots;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CardMod.Core
{
    public abstract class BaseCard : ModItem
    {
        public readonly float alpha = Main.mouseTextColor / 255f;

        public BaseCard(int cardRarity = CardRarity.Common, string name = "Base Card", string ability = "Ability", string abilityDescription = "Description", string weakness = "Weakness")
        {
            this.cardRarity = cardRarity;
            cardName = name;
            cardAbility = ability + "!";
            cardAbilityDescription = abilityDescription;
            cardWeakness = weakness;
        }

        public int cardRarity;
        public bool isCard;
        public string cardName;
        public string cardAbility;
        public string cardAbilityDescription;
        public string cardWeakness;

        public virtual bool CanBeBoughtFromCardNPC() => true;

        public override ModItem Clone(Item item)
        {
            BaseCard clone = (BaseCard)base.Clone(item);
            clone.isCard = isCard;
            clone.cardRarity = cardRarity;
            return clone;
        }

        public sealed override void SetStaticDefaults()
        {
            DisplayName.SetDefault(cardName);
            Tooltip.SetDefault($"Ability: {cardAbility}" +
                $"\n{cardAbilityDescription}" +
                $"\nWeakness:" +
                $"\n{cardWeakness}");
        }

        public sealed override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 24;

            SafeSetDefaults();

            Item.rare = cardRarity switch
            {
                CardRarity.Uncommon => ModContent.RarityType<CardMod.Rarities.Uncommon>(),
                CardRarity.Rare => ModContent.RarityType<CardMod.Rarities.Rare>(),
                CardRarity.Epic => ModContent.RarityType<CardMod.Rarities.Epic>(),
                CardRarity.Legendary => ModContent.RarityType<CardMod.Rarities.Legendary>(),
                CardRarity.Mythical => ModContent.RarityType<CardMod.Rarities.Mythic>(),
                _ => ModContent.RarityType<CardMod.Rarities.Common>(),
            };

            Item.Card().isCard = isCard;
            Item.accessory = isCard;

            Item.value = cardRarity switch
            {
                CardRarity.Uncommon => Item.sellPrice(silver: 50),
                CardRarity.Rare => Item.sellPrice(gold: 2, silver: 25),
                CardRarity.Epic => Item.sellPrice(gold: 5),
                CardRarity.Legendary => Item.sellPrice(gold: 12, silver: 50),
                CardRarity.Mythical => Item.sellPrice(gold: 25),
                >= CardRarity.Count => (int)Math.Pow(cardRarity, Math.Sqrt(5)) * 10000,
                _ => Item.sellPrice(silver: 10),
            };

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public virtual void SafeSetDefaults()
        {
        }

        public sealed override string Texture => ModContent.RequestIfExists<Texture2D>((GetType().Namespace + "." + Name).Replace('.', '/').Replace("Content", "Assets"), out _)
                    ? (GetType().Namespace + "." + Name).Replace('.', '/').Replace("Content", "Assets")
                    : $"CardMod/Assets/Items/Cards/Card{cardRarity}";

        public sealed override void UpdateAccessory(Player player, bool hideVisual) => CardEffects(player, hideVisual);

        public sealed override void UpdateInventory(Player player)
        {
            if (Item.prefix != 0)
            {
                Item.prefix = 0;
                Item.Prefix(0);
            }

            UpdateInventory2(player);
        }

        public virtual void UpdateInventory2(Player player)
        {
        }

        public virtual void CardEffects(Player player, bool hideVisuals)
        {
        }

        public sealed override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            for (int n = 0; n < Main.HoverItem.ToolTip.Lines; n++)
            {
                string line = Main.HoverItem.ToolTip.GetLine(n);
                string tooltipName = "Tooltip" + n.ToString();
                tooltips.Add(new TooltipLine(Mod, tooltipName, line));
            }

            TooltipLine abilityLine = tooltips.Find(x => x.mod == Mod.Name && x.text.StartsWith("Ability:"));
            if (abilityLine != null)
            {
                abilityLine.overrideColor = ModContent.GetInstance<CardMod.Rarities.Legendary>().RarityColor;
            }
            abilityLine = tooltips.Find(x => x.mod == Mod.Name && x.text.StartsWith("Weakness:"));
            if (abilityLine != null)
            {
                abilityLine.overrideColor = new Color(255, 101, 85);
            }

            if ((Item.tooltipContext == 0 || Item.tooltipContext == 2 || Item.tooltipContext == 1 || Item.tooltipContext == 3 || (Item.tooltipContext == 4 || Item.tooltipContext == 15) ? 1 : (Item.tooltipContext == 6 ? 1 : 0)) != 0 && Main.LocalPlayer.difficulty == 3 && CreativeItemSacrificesCatalog.Instance.TryGetSacrificeCountCapToUnlockInfiniteItems(Item.type, out int amountNeeded))
            {
                int sacrificeCount = Main.LocalPlayerCreativeTracker.ItemSacrifices.GetSacrificeCount(Item.type);
                if (amountNeeded - sacrificeCount > 0)
                {
                    tooltips.Add(new TooltipLine(Mod, "CreativeSacrifice", Language.GetTextValue("CommonItemTooltip.CreativeSacrificeNeeded",
                        amountNeeded - sacrificeCount))
                    { overrideColor = Colors.JourneyMode });
                }
            }

            TooltipLine line0 = tooltips.Find(x => x.mod == "Terraria" && x.Name == "Price");
            if (line0 != null)
            {
                TooltipLine line1 = new(Mod, line0.Name, line0.text)
                {
                    overrideColor = line0.overrideColor,
                    isModifier = line0.isModifier,
                    isModifierBad = line0.isModifierBad
                }; /// Save Price line

                tooltips.Remove(line0); /// Remove vanilla Price line

                tooltips.Add(line1); /// Add modded Price line
            }

            SafeModifyTooltips(ref tooltips);
            tooltips.RemoveAll(x => x.mod == "Terraria" && x.Name != "ItemName" && x.Name != "Price");
        }

        public virtual void SafeModifyTooltips(ref List<TooltipLine> tooltips)
        {
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded) => isCard && modded && slot == ModContent.GetInstance<CardSlot>().Type;

        public override void SaveData(TagCompound tag) => tag["isCard"] = isCard;

        public override void LoadData(TagCompound tag) => isCard = tag.GetBool("isCard");
    }
}
