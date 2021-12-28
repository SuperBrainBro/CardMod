using Terraria;
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
