using CardMod.Core;
using Terraria;

namespace CardMod.Content.Items.Cards.PreHardmode
{
    public class BunnyCard : BaseCard
    {
        public BunnyCard() : base(CardRarity.Common,
            "Bunny Card",
            "Bunny Utilization",
                "Increased jump height",
            "5% reduced HP")
        {
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals)
        {
            Player.jumpHeight += (int)(8 * CardPlayer.GetCardMultiplier(player));
            Player.jumpSpeed += 3f * CardPlayer.GetCardMultiplier(player);
            player.statLifeMax2 = (int)(player.statLifeMax2 * 0.95f);
        }
    }
}
