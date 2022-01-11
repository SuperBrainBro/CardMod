using CardMod.Core;
using Terraria;

namespace CardMod.Content.Items.Cards.PreHardmode
{
    public class SnowFlinxCard : BaseCard
    {
        public SnowFlinxCard() : base(CardRarity.Common,
            "Snow Flinx Card",
            "Cozy Zone",
            "Spawn Rates decreased in Snow biome",
            "Your movement speed is decreased outside Snow biome")
        {
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals)
        {
            if (player.ZoneSnow)
                player.Card()._cardSnowFlinx = true;
            else
                player.moveSpeed -= 0.08f;
        }
    }
}
