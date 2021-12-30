using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CardMod.Core
{
    public class CardGlobalItem : GlobalItem
    {
        public bool isCard = false;

        public override bool InstancePerEntity => true;
        public override GlobalItem Clone(Item item, Item itemClone)
        {
            CardGlobalItem myClone = (CardGlobalItem)base.Clone(item, itemClone);
            myClone.isCard = isCard;
            return myClone;
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if ((ItemID.Sets.Torches[item.type] || ItemID.Sets.WaterTorches[item.type]) && player.Card()._cardTorchGod)
                return false;
            return base.CanUseItem(item, player);
        }

        public override void SaveData(Item item, TagCompound tag)
        {
            tag["CardMod:isCard"] = isCard;
        }

        public override void LoadData(Item item, TagCompound tag)
        {
            isCard = tag.GetBool("CardMod:isCard");
        }
    }
}
