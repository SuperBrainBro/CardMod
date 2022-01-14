using CardMod.Core;
using Microsoft.Xna.Framework;
using Terraria;

namespace CardMod.Content.Items.Cards.PreHardmode
{
    public class HarpyCard : BaseCard
    {
        public HarpyCard() : base(CardRarity.Common,
            "Harpy Card",
            "Space is your Home",
            "All stats increase depending on" +
            "\nhow close you to space!",
            "If there's no sunlight, you start dying")
        {
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals)
        {
            player.Card()._cardHarpy = true;
            player.AllStatIncrease((int)MathHelper.SmoothStep(-5, 15, CardUtils.InverseLerp(Main.maxTilesY * 16, 0, player.Center.Y, true) * CardPlayer.GetCardMultiplier(player)),
                MathHelper.SmoothStep(-0.5f, 0.5f, CardUtils.InverseLerp(Main.maxTilesY * 16, 0, player.Center.Y, true)) * CardPlayer.GetCardMultiplier(player));
        }
    }
}
