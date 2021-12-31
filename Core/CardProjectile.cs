using CardMod.Content.Buffs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
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
            if (player.Card()._cardDemon || player.Card()._cardRedDevil)
            {
                if (target.active && !target.friendly && target.damage > 0 && !target.dontTakeDamage && player.CanNPCBeHitByPlayerOrPlayerProjectile(target) && !target.buffImmune[BuffID.OnFire])
                {
                    if (player.Card()._cardDemon)
                        target.AddBuff(ModContent.BuffType<OnFireDemon>(), 200);
                    else
                        target.AddBuff(ModContent.BuffType<OnFireDevil>(), 600);
                }
            }
        }

        public override void ModifyHitPvp(Projectile projectile, Player target, ref int damage, ref bool crit)
        {
            Player player = Main.player[projectile.owner];
            if (player.Card()._cardNymph)
            {
                float num = MathHelper.SmoothStep(1f, 2f, CardUtils.InverseLerp(400f, 175f, target.Distance(player.Center), true));
                damage = (int)Math.Ceiling(damage * num);
            }
            if (player.Card()._cardDemon || player.Card()._cardRedDevil)
            {
                if (target != player && target.active && !target.dead && target.hostile && (target.team != player.team || player.team == 0) && !target.buffImmune[BuffID.OnFire])
                {
                    if (player.Card()._cardDemon)
                        target.AddBuff(ModContent.BuffType<OnFireDemon>(), 200);
                    else
                        target.AddBuff(ModContent.BuffType<OnFireDevil>(), 600);
                }
            }
        }
    }
}
