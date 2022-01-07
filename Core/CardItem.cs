using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CardMod.Core
{
    public class CardItem : GlobalItem
    {
        public bool isCard = false;
        public bool dedicatedItem = false;

        public override bool InstancePerEntity => true;
        public override GlobalItem Clone(Item item, Item itemClone)
        {
            CardItem myClone = (CardItem)base.Clone(item, itemClone);
            myClone.isCard = isCard;
            myClone.dedicatedItem = dedicatedItem;
            return myClone;
        }

        public override void SetDefaults(Item item)
        {
            if (CardLists.DedicatedItems.Keys.ToList().Contains(item.type))
                dedicatedItem = true;

            if (dedicatedItem)
                item.rare = ModContent.GetInstance<CardMod.Rarities.Dedicated>().Type;
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if ((ItemID.Sets.Torches[item.type] || ItemID.Sets.WaterTorches[item.type]) && player.Card()._cardTorchGod)
                return false;
            return base.CanUseItem(item, player);
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            int index = tooltips.FindIndex(tt => tt.mod.Equals("Terraria") && tt.Name.Equals("ItemName"));
            if (index != -1 && CardLists.DedicatedItems.Keys.ToList().Contains(item.type))
                tooltips.Insert(index + 1, new TooltipLine(Mod, "DedicatedTag", "-Dedicated Item-")
                {
                    overrideColor = new Color?(Color.Lerp(ModContent.GetInstance<CardMod.Rarities.Dedicated>().RarityColor, Color.White, 0.667f))
                });

            if (CardLists.DedicatedItems.Keys.ToList().Contains(item.type))
            {
                int num = CardLists.DedicatedItems.Keys.ToList().IndexOf(item.type);

                index = tooltips.FindLastIndex(tt => tt.Name.Contains("Tooltip"));
                if (index != -1)
                {
                    Color color = Color.Lerp(
                        Color.Lerp(ModContent.GetInstance<CardMod.Rarities.Dedicated>().RarityColor,
                        Color.White, 0.667f),
                        Color.Gold, 0.12f);
                    tooltips.Insert(index + 1, new TooltipLine(Mod, "DedicatedToTag", "Dedicated To: " +
                        $"[c/{color.Hex3()}:{CardLists.DedicatedItems.Values.ToList()[num]}]")
                    {
                        overrideColor = new Color?(Color.Lerp(ModContent.GetInstance<CardMod.Rarities.Dedicated>().RarityColor, Color.White, 0.667f))
                    });
                }
            }
        }

        public override void SaveData(Item item, TagCompound tag)
        {
            tag["CardMod:isCard"] = isCard;
            tag["CardMod:dedicatedItem"] = dedicatedItem;
        }

        public override void LoadData(Item item, TagCompound tag)
        {
            isCard = tag.GetBool("CardMod:isCard");
            dedicatedItem = tag.GetBool("CardMod:dedicatedItem");
        }
    }
}
