using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CardMod.Core
{
    public class CardPlayer : ModPlayer
    {
        public bool _cardNymph;
        public int _goldenSlimeCD;
        public int _volatileCD;
        public bool _cardDiscount;
        public bool _cardWof;
        public bool _cardImp;
        public bool _cardSlime;
        public bool _cardTim;

        public override void ResetEffects()
        {
            _cardNymph = false;
            _cardDiscount = false;
            _cardWof = false;
            _cardImp = false;
            _cardSlime = false;

            if (_cardTim)
            {
                Player.statManaMax2 += 40;
            }
        }

        public override void ModifyWeaponCrit(Item item, ref int crit)
        {
            if (_cardTim)
            {
                if (item.mana >= 1)
                {
                    crit += 8;
                }
            }
        }

        public override void ModifyWeaponDamage(Item item, ref StatModifier damage, ref float flat)
        {
            if (_cardTim)
            {
                if (item.mana >= 1)
                {
                    damage += 4;
                }
            }
        }

        public override void UpdateDead()
        {
            _goldenSlimeCD = 0;
            _volatileCD = 0;
            _cardDiscount = false;
            _cardNymph = false;
            _cardWof = false;
            _cardImp = false;
            _cardSlime = false;
        }

        public override void PreUpdate()
        {
            if (_goldenSlimeCD > 0)
                _goldenSlimeCD--;
            if (_volatileCD > 0)
                _volatileCD--;

            if ((_cardWof || _cardImp) && !Player.inferno)
            {
                Lighting.AddLight((int)(Player.Center.X / 16.0), (int)(Player.Center.Y / 16.0), 0.65f, 0.4f, 0.1f);
                int type = 24;
                float num1 = _cardImp ? 175f : 225f;
                bool flag = Player.infernoCounter % (_cardImp ? 90 : 30) == 0;
                int num2 = _cardImp ? 5 : 20;
                if (Player.whoAmI == Main.myPlayer)
                {
                    for (int index2 = 0; index2 < 200; ++index2)
                    {
                        NPC npc = Main.npc[index2];
                        if (npc.active && !npc.friendly && npc.damage > 0 && !npc.dontTakeDamage && !npc.buffImmune[type] && Player.CanNPCBeHitByPlayerOrPlayerProjectile(npc) && Vector2.Distance(Player.Center, npc.Center) <= num1)
                        {
                            if (npc.FindBuffIndex(type) == -1)
                                npc.AddBuff(type, 120);
                            if (flag)
                                Player.ApplyDamageToNPC(npc, num2, 0.0f, 0, false);
                        }
                    }
                    if (Player.hostile)
                    {
                        for (int playerTargetIndex = 0; playerTargetIndex < byte.MaxValue; ++playerTargetIndex)
                        {
                            Player player2 = Main.player[playerTargetIndex];
                            if (player2 != Player && Player.active && !Player.dead && Player.hostile && !Player.buffImmune[type] && (player2.team != Player.team || Player.team == 0) && (double)Vector2.Distance(Player.Center, Player.Center) <= num1)
                            {
                                if (Player.FindBuffIndex(type) == -1)
                                    Player.AddBuff(type, 120);
                                if (flag)
                                {
                                    Player.Hurt(PlayerDeathReason.LegacyEmpty(), num2, 0, true);
                                    if (Main.netMode != NetmodeID.SinglePlayer)
                                    {
                                        PlayerDeathReason reason = PlayerDeathReason.ByOther(16);
                                        NetMessage.SendPlayerHurt(playerTargetIndex, reason, num2, 0, false, true, -1);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


    }
}
