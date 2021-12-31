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
            Player.jumpHeight += 8;
            Player.jumpSpeed += 3f;
            player.statLifeMax2 = (int)(player.statLifeMax2 * 0.95f);
        }
    }
}
