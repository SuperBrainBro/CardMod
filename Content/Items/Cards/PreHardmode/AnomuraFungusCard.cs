using CardMod.Core;
using Terraria;

namespace CardMod.Content.Items.Cards.PreHardmode
{
    public class AnomuraFungusCard : BaseCard
    {
        public AnomuraFungusCard() : base(CardRarity.Uncommon,
            "Anomura Fungus Card",
            "Critical Health",
            "Crit hits slightly heal you",
            "When enemies crit you they slightly heal")
        {
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals) => player.Card()._cardAnomuraFungus = true;
    }
}
