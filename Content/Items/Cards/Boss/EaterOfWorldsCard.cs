using CardMod.Content.Projectiles.Friendly;
using CardMod.Core;
using System;
using Terraria;
using Terraria.ModLoader;

namespace CardMod.Content.Items.Cards.Boss
{
    public class EaterOfWorldsCard : BaseCard
    {
        public EaterOfWorldsCard() : base(CardRarity.Epic,
            "Eater of Worlds Card",
            "Symmetrical Shadow",
                "Around player are flying shadow orbs" +
              "\nthose deal lots of damage on collision with enemies.",
            "You're weak on sun light due to how empty you are.")
        {
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<ShadowOrbs>()] < 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    Projectile.NewProjectile(player.GetProjectileSource_Accessory(Item), player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<ShadowOrbs>(), 10, 8f, player.whoAmI, i);
                }
            }
            
            for (int j = 0; j < 1000; j++)
            {
                Projectile projectile = Main.projectile[j];
                if (projectile.active && projectile.owner == player.whoAmI && projectile.type == ModContent.ProjectileType<ShadowOrbs>())
                {
                    int damage = player.HeldItem.damage;
                    int cap = (int)(75
                        * MathF.Sqrt(player.HeldItem.damage * player.GetDamage(player.HeldItem.DamageType).Multiplicative)
                        / 10);
                    if (damage < 1)
                        damage = 1;
                    if (damage > cap)
                        damage = cap;

                    projectile.damage = damage;
                    projectile.DamageType = player.HeldItem.DamageType;
                    projectile.timeLeft = 2;
                }
            }

            if (player.ExposedToSunlight())
                player.GetDamage(DamageClass.Generic) -= 0.1f;
        }
    }
}
