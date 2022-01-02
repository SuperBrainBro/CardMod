using CardMod.Core;
using Terraria;

namespace CardMod.Content.Items.Cards.Hardmode
{
    public class RedDevilCard : BaseCard
    {
        public RedDevilCard() : base(CardRarity.Rare,
            "Red Devil Card",
            "Orgasmic Ashes",
                "Greatly increased damage infliction" +
              "\nupon enemies when \"On Fire!\"",
            "You receive massive damage when" +
          "\ntouching water")
        {
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals) => player.Card()._cardRedDevil = true;
    }
}
