using CardMod.Core;
using Terraria;

namespace CardMod.Content.Items.Cards.Hardmode
{
    public class ShopDiscountCard : BaseCard
    {
        public ShopDiscountCard() : base(CardRarity.Rare,
            "Shop Discount Card",
            "DISCOUNT",
                "Shops prices are reduced",
            "Enemies drop less coins")
        {
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals) => player.Card()._cardDiscount = true;
    }
}
