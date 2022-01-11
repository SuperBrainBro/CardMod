using CardMod.Core;
using Terraria;

namespace CardMod.Content.Items.Cards.PreHardmode
{
    public class UmbrellaSlimeCard : BaseCard
    {
        public UmbrellaSlimeCard() : base(CardRarity.Uncommon,
            "Umbrella Slime Card",
            "Umbrella",
                "Allows slowfall and removes fall damage",
            "Your movement speed decreased when it's not windy")
        {
        }


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

            if (!Main.WindyEnoughForKiteDrops)
                player.moveSpeed -= 0.04f;
        }
    }
}
