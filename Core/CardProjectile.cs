using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CardMod.Core
{
    public class CardProjectile : GlobalProjectile
    {
        public bool isCard = false;

        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances => true;
        public override GlobalProjectile Clone()
        {
            CardProjectile card = (CardProjectile)base.Clone();
            card.isCard = isCard;
            return card;
        }

        public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[projectile.owner];
            if (player.Card()._cardNymph)
            {
                float num = MathHelper.SmoothStep(1f, 2f, CardUtils.InverseLerp(400f, 175f, target.Distance(player.Center), true));
                damage = (int)Math.Ceiling(damage * num);
            }
        }
    }
}
