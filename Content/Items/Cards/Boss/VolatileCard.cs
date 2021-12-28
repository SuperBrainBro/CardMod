﻿using CardMod.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace CardMod.Content.Items.Cards.Boss
{
    public class VolatileCard : BaseCard
    {
        public VolatileCard() : base(CardRarity.Epic)
        {
        }

        public override void SetStaticDefaults2() => Tooltip.SetDefault(@"Ability: Volatile
When moving, every 2.5 seconds will
shoot 8 volatile balloons");

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals)
        {
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
                        int num = Projectile.NewProjectile(player.GetProjectileSource_Accessory(Item), player.Center, vector.RotatedBy(MathHelper.ToRadians(Main.rand.NextFloat(-15, 15))), ProjectileID.VolatileGelatinBall, 90, 0f, player.whoAmI);
                        Main.projectile[num].friendly = true;
                        Main.projectile[num].hostile = false;
                    }
                    player.Card()._volatileCD = 150;
                }
            }
        }
    }
}
