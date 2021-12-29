using CardMod.Content.Slots;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CardMod.Core
{
    public abstract class CardItem : ModItem
    {
        public readonly float alpha = Main.mouseTextColor / 255f;

        protected int cardRarity;
        public bool isCard;

        public override ModItem Clone(Item item)
        {
            CardItem clone = (CardItem)base.Clone(item);
            clone.isCard = isCard;
            clone.cardRarity = cardRarity;
            return clone;
        }

        public CardItem(int cardRarity = CardRarity.Common) => this.cardRarity = cardRarity;

        public sealed override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 24;
            SafeSetDefaults();
            SetDefaults2();
        }

        public virtual void SafeSetDefaults()
        {
        }

        private void SetDefaults2()
        {
            Item.rare = cardRarity switch
            {
                CardRarity.Uncommon => ModContent.RarityType<CardMod.Rarities.Uncommon>(),
                CardRarity.Rare => ModContent.RarityType<CardMod.Rarities.Rare>(),
                CardRarity.Epic => ModContent.RarityType<CardMod.Rarities.Epic>(),
                CardRarity.Legendary => ModContent.RarityType<CardMod.Rarities.Legendary>(),
                CardRarity.Mythical => ModContent.RarityType<CardMod.Rarities.Mythic>(),
                _ => ModContent.RarityType<CardMod.Rarities.Common>(),
            };

            Item.GetGlobalItem<CardGlobalItem>().isCard = isCard;
            if (isCard)
                Item.accessory = true;
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
                abilityLine.overrideColor = new Color(255, 161, 99) * alpha;
            }

            if ((Item.tooltipContext == 0 || Item.tooltipContext == 2 || Item.tooltipContext == 1 || Item.tooltipContext == 3 || (Item.tooltipContext == 4 || Item.tooltipContext == 15) ? 1 : (Item.tooltipContext == 6 ? 1 : 0)) != 0 && Main.LocalPlayer.difficulty == 3 && CreativeItemSacrificesCatalog.Instance.TryGetSacrificeCountCapToUnlockInfiniteItems(Item.type, out int amountNeeded))
            {
                int sacrificeCount = Main.LocalPlayerCreativeTracker.ItemSacrifices.GetSacrificeCount(Item.type);
                if (amountNeeded - sacrificeCount > 0)
                {
                    string text = Language.GetTextValue("CommonItemTooltip.CreativeSacrificeNeeded", amountNeeded - sacrificeCount);
                    tooltips.Add(new TooltipLine(Mod, "CreativeSacrifice", text) { overrideColor = Colors.JourneyMode });
                }
            }

            SafeModifyTooltips(ref tooltips);
            tooltips.RemoveAll(x => x.mod == "Terraria" && x.Name != "ItemName");
        }

        public virtual void SafeModifyTooltips(ref List<TooltipLine> tooltips)
        {
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded) => isCard && modded && slot == ModContent.GetInstance<CardSlot>().Type;

        public override void SaveData(TagCompound tag) => tag["isCard"] = isCard;

        public override void LoadData(TagCompound tag) => isCard = tag.GetBool("isCard");
    }
}
