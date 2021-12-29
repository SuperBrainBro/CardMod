using CardMod.Core;
using Terraria;

namespace CardMod.Content.Items.Cards.PreHardmode
{
    public class UmbrellaSlimeCard : BaseCard
    {
        public UmbrellaSlimeCard() : base(CardRarity.Rare, "Umbrella Slime Card", "Umbrella", "Allows slowfall and removes fall damage.")
        {
        }


        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals)
        {
            player.noFallDmg = true;
            if (!player.pulley)
            {
                player.fallStart = (int)(player.position.Y / 16.0);
                if (player.gravDir == -1.0)
                {
                    if (player.velocity.Y < -2.0)
                        player.velocity.Y = -2f;
                }
                else if (player.velocity.Y > 2.0)
                    player.velocity.Y = 2f;
            }
        }
    }
}
