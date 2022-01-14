using CardMod.Core;
using Terraria;

namespace CardMod.Content.Items.Cards.PreHardmode
{
    public class BirdCard : BaseCard
    {
        public BirdCard() : base(CardRarity.Common,
            "Bird Card",
            "Genetically Enhanced Wings",
                "20% increased wing duration",
            "10% increased shop prices")
        {
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals)
        {
            player.wingTimeMax = (int)(player.wingTimeMax * 1.2f * CardPlayer.GetCardMultiplier(player));
            player.Card()._cardBird = true;
        }
    }
}
