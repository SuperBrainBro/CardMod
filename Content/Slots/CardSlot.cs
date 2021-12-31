using CardMod.Core;
using Terraria;
using Terraria.ModLoader;

namespace CardMod.Content.Slots
{
    public class CardSlot : ModAccessorySlot
    {
        public override bool DrawVanitySlot => false;
        public override bool DrawDyeSlot => false;

        public override string FunctionalBackgroundTexture => "Terraria/Images/Inventory_Back15";
        public override string FunctionalTexture => "CardMod/Assets/CardSlot";
        public override string DyeBackgroundTexture => "Terraria/Images/Inventory_Back15";

        private static bool Acceptable(Item item)
        {
            BaseCard card = item.ModItem as BaseCard;
            return card.isCard || item.GetGlobalItem<CardItem>().isCard;
        }

        public override bool CanAcceptItem(Item checkItem, AccessorySlotType context) => Acceptable(checkItem);

        public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo) => Acceptable(item);

        public override bool IsVisibleWhenNotEnabled() => false;

        public override bool IsEnabled() => true;

        public override void OnMouseHover(AccessorySlotType context)
        {
            switch (context)
            {
                case AccessorySlotType.FunctionalSlot:
                    Main.hoverItemName = "Card";
                    break;
                case AccessorySlotType.DyeSlot:
                    Main.hoverItemName = "Dye";
                    break;
            }
        }
    }
}
