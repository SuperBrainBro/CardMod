using CardMod.Core;
using Terraria;

namespace CardMod.Content.Items.Cards.PreHardmode
{
    public class SquirrelCard : BaseCard
    {
        public SquirrelCard() : base(CardRarity.Common,
            "Squirrel Card",
            "Squirrel",
                "No fall damage",
            "Slightly reduced jump height")
        {
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals)
        {
            player.noFallDmg = true;
            Player.jumpHeight -= 4;
            Player.jumpSpeed -= 1.5f;
        }
    }
}
