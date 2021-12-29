using CardMod.Core;
using Terraria;

namespace CardMod.Content.Items.Cards.PreHardmode
{
    public class UmbrellaSlimeCard : BaseCard
    {
        public UmbrellaSlimeCard() : base(CardRarity.Common)
        {
        }

        public override void SetStaticDefaults2() => Tooltip.SetDefault(@"Ability: Umbrella!
Allows slowfall and removes fall damage");

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals)
        {
            player.noFallDmg = true;
            if (!player.pulley)
            {
                player.fallStart = (int)(player.position.Y / 16f);
                if (player.gravDir == -1f)
                {
                    if (player.velocity.Y < -2f)
                        player.velocity.Y = -2f;
                }
                else if (player.velocity.Y > 2f)
                    player.velocity.Y = 2f;
            }
        }
    }
}
