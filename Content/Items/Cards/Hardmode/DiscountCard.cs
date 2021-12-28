using CardMod.Core;
using Terraria;

namespace CardMod.Content.Items.Cards.Hardmode
{
    public class DiscountCard : BaseCard
    {
        public DiscountCard() : base(CardRarity.Rare)
        {
        }

        public override void SetStaticDefaults2() => Tooltip.SetDefault(@"Ability: Discount
Shops prices lowered by 10%
Stackable with Discount Card");

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals) => player.Card()._cardDiscount = true;
    }
}
