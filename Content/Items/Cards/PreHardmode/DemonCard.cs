using CardMod.Core;
using Terraria;

namespace CardMod.Content.Items.Cards.PreHardmode
{
    public class DemonCard : BaseCard
    {
        public DemonCard() : base(CardRarity.Uncommon,
            "Demon Card",
            "Pleasant Burning",
                "Increased damage infliction upon" +
              "\nenemies when \"On Fire!\"",
            "You receive damage when touching water")
        {
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals)
        {
            player.Card()._cardDemon = true;
        }
    }
}
