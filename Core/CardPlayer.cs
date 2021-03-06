using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

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
        public bool _cardSlimeGreen;
        public bool greenJellyCard;
        public bool greenJellyCardImmune;
        public bool pinkJellyCard;
        public bool pinkJellyCardImmune;
        public bool blueJellyCard;
        public bool blueJellyCardImmune;
        public bool _cardTorchGod;
        public bool _cardBird;
        public bool _cardDemon;
        public bool _cardRedDevil;
        public bool onFireDemon;
        public bool onFireDevil;
        public bool _cardQueenSlime;
        public bool _cardSnowFlinx;
        public bool _cardGiantAntlionCharger;
        public bool _cardHarpy;
        public bool _cardGraniteGolem;
        public bool _cardAnomuraFungus;

        public bool haveObtainedTorchGodCardCheck;
        public float infernoLevel;
        public bool foxPet;

        private float CardMultiplier = 1f;
        public static float GetCardMultiplier(Player player) => player.Card().CardMultiplier;

        public bool InfernoWeak => infernoLevel is < 3f and >= 1f;
        public bool InfernoMedium => infernoLevel is >= 3f and < 5f;
        public bool InfernoStrong => infernoLevel is >= 5f and < 9f;
        public bool InfernoSpecial => infernoLevel >= 9f;

        public override void ResetEffects()
        {
            _cardNymph = false;
            _cardDiscount = false;
            _cardWof = false;
            _cardImp = false;
            _cardSlime = false;
            _cardSlimeGreen = false;
            infernoLevel = 0;
            greenJellyCard = false;
            greenJellyCardImmune = false;
            pinkJellyCard = false;
            pinkJellyCardImmune = false;
            blueJellyCard = false;
            blueJellyCardImmune = false;
            _cardTorchGod = false;
            _cardBird = false;
            _cardDemon = false;
            _cardRedDevil = false;
            _cardQueenSlime = false;
            _cardSnowFlinx = false;
            _cardGiantAntlionCharger = false;
            _cardHarpy = false;
            _cardAnomuraFungus = false;
            _cardGraniteGolem = false;
            foxPet = false;

            float values = 1f;
            if (CardMod.cardMultipliers.Count > 0)
            {
                foreach (ValueTuple<Func<bool>, float> dict in CardMod.cardMultipliers)
                {
                    if (dict.Item1.Invoke())
                        values += dict.Item2;
                }
            }
            CardMultiplier = MathF.Sqrt(values);
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
            _cardSlimeGreen = false;
            infernoLevel = 0;
            greenJellyCard = false;
            greenJellyCardImmune = false;
            pinkJellyCard = false;
            pinkJellyCardImmune = false;
            blueJellyCard = false;
            blueJellyCardImmune = false;
            _cardTorchGod = false;
            _cardBird = false;
            _cardDemon = false;
            _cardRedDevil = false;
            onFireDemon = false;
            onFireDevil = false;
            _cardQueenSlime = false;
            _cardSnowFlinx = false;
            _cardGiantAntlionCharger = false;
            _cardHarpy = false;
            _cardAnomuraFungus = false;
            _cardGraniteGolem = false;
            foxPet = false;
            CardMultiplier = 1f;
        }

        public override void PreUpdate()
        {
            if (greenJellyCardImmune)
                greenJellyCard = false;
            if (pinkJellyCardImmune)
                pinkJellyCard = false;
            if (blueJellyCardImmune)
                blueJellyCard = false;

            if (blueJellyCard)
                Player.statDefense -= 10;
            if (greenJellyCard)
                Player.GetDamage(DamageClass.Generic) -= 0.1f;
            if (_goldenSlimeCD > 0)
                _goldenSlimeCD--;
            if (_volatileCD > 0)
                _volatileCD--;

            if (_cardWof || _cardImp)
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
                                npc.AddBuff(type, _cardImp ? 60 : 180);
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
                                    Player.AddBuff(type, _cardImp ? 60 : 180);
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

        public override void UpdateBadLifeRegen()
        {
            if (_cardHarpy && !Player.ExposedToSunlight())
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= (int)MathHelper.SmoothStep(150, 10, CardUtils.InverseLerp(Main.maxTilesY * 16, 0, Player.Center.Y, true));
            }
            if (pinkJellyCard)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 40;
            }
            if ((_cardDemon || _cardRedDevil || onFireDevil || onFireDemon) && Player.wet)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                int value = 0;
                if (_cardDemon)
                    value += 20;
                if (onFireDemon)
                    value += 20;
                if (_cardRedDevil)
                    value += 80;
                if (onFireDevil)
                    value += 80;
                Player.lifeRegen -= value;
            }
        }

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (_cardAnomuraFungus && crit)
            {
                int num = Main.rand.Next(1, 4);
                Player.statLife -= num;
                npc.life += num;
                npc.HealEffect(num);
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (_cardAnomuraFungus && crit)
            {
                int num = Main.rand.Next(1, 4);
                target.life -= num;
                Player.statLife += num;
                Player.HealEffect(num);
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (_cardAnomuraFungus && crit)
            {
                int num = Main.rand.Next(1, 4);
                target.life -= num;
                Player.statLife += num;
                Player.HealEffect(num);
            }
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (_cardGiantAntlionCharger)
            {
                if ((damageSource.SourceProjectileType == ProjectileID.RollingCactusSpike || damageSource.SourceProjectileType == ProjectileID.RollingCactus)
                        && (Player.ZoneDesert || Player.ZoneUndergroundDesert))
                {
                    damage /= 2;
                }
                else
                {
                    damage = (int)(damage * Main.rand.NextFloat(1f, 1.75f));
                }
            }
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            if (CardLists.Slimes.Contains(npc.type) && Player.Card()._cardQueenSlime && !Player.ZoneHallow)
                damage = (int)(damage * Main.rand.NextFloat(0.95f, 3f));
        }

        public override void SaveData(TagCompound tag)
        {
            tag["HaveObtainedTorchGodCardCheck"] = haveObtainedTorchGodCardCheck;
        }

        public override void LoadData(TagCompound tag)
        {
            haveObtainedTorchGodCardCheck = tag.GetBool("HaveObtainedTorchGodCardCheck");
        }
    }
}
