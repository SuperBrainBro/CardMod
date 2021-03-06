using CardMod.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace CardMod.Content.Items.Cards.Boss
{
    public class QueenSlimeCard : BaseCard
    {
        public QueenSlimeCard() : base(CardRarity.Epic,
            "Queen Slime Card",
            "Volatile Bloons",
                "You project volatile ballons periodically while moving",
            "Damage from slimes will be increased, except if you in Hallow biome")
        {
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals)
        {
            player.Card()._cardQueenSlime = true;
            if (player.Moving())
            {
                if (player.Card()._volatileCD == 0)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Vector2 vector = new(0, 0);
                        if (i == 0)
                            vector = new(10, 0);
                        else if (i == 1)
                            vector = new(-10, 0);
                        else if (i == 2)
                            vector = new(0, 10);
                        else if (i == 3)
                            vector = new(0, -10);
                        else if (i == 4)
                            vector = new(10, 10);
                        else if (i == 5)
                            vector = new(-10, -10);
                        else if (i == 6)
                            vector = new(-10, 10);
                        else if (i == 7)
                            vector = new(10, -10);
                        int num = Projectile.NewProjectile(player.GetProjectileSource_Accessory(Item), player.Center, vector.RotatedBy(MathHelper.ToRadians(Main.rand.NextFloat(-15, 15))), ProjectileID.VolatileGelatinBall, (int)(90 * CardPlayer.GetCardMultiplier(player)), 0f, player.whoAmI);
                        Main.projectile[num].friendly = true;
                        Main.projectile[num].hostile = false;
                        Main.projectile[num].DamageType = player.HeldItem.DamageType;
                        Main.projectile[num].Card().isCard = true;
                    }
                    player.Card()._volatileCD = 150;
                }
            }
        }
    }
}
