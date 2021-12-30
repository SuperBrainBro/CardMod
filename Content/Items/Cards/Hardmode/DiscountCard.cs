using CardMod.Core;
using Terraria;

namespace CardMod.Content.Items.Cards.Hardmode
{
    public class DiscountCard : BaseCard
    {
        public DiscountCard() : base(CardRarity.Rare,
            "Rewards Card",
            "DISCOUNT",
                "Shops prices are reduced")
        {
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals) => player.Card()._cardDiscount = true;
    }
}
