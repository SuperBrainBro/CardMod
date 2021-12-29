using System;
using CardMod.Content.Items.Cards.PreHardmode;
using CardMod.Core;
using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CardMod
{
    public class CardMod : Mod
    {
        public static Mod mod;

        public override void Load()
        {
            mod = this;

            IL.Terraria.Player.TorchAttack += Player_TorchAttack;
            //IL.Terraria.Player.UpdateBuffs += Player_UpdateBuffs;
        }

        public override void Unload()
        {
            mod = null;

            IL.Terraria.Player.TorchAttack -= Player_TorchAttack;
            //IL.Terraria.Player.UpdateBuffs -= Player_UpdateBuffs;
        }

        private void Player_TorchAttack(ILContext il)
        {
            var c = new ILCursor(il);
            if (!c.TryGotoNext(i => i.MatchLdcI4(5043)))
                return;
            if (!c.TryGotoNext(i => i.MatchStloc(out _)))
                return;
            c.Index++;
            c.Emit(OpCodes.Ldarg_0);
            c.EmitDelegate<Action<Player>>((player) =>
            {
                int number = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, ModContent.ItemType<TorchGodCard>(), 1, false, 0, false, false);
                if (Main.netMode == NetmodeID.MultiplayerClient)
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, number, 1f, 0f, 0f, 0, 0, 0);
            });
        }

        /*private void Player_UpdateBuffs(ILContext il)
        {
            var c = new ILCursor(il);

            if (!c.TryGotoNext(i => i.MatchStfld<Player>(nameof(Player.inferno))))
                return;
            c.Index++;
            c.Emit(OpCodes.Ldarg_0);
            c.EmitDelegate<Action<Player>>(player =>
            {
                player.Card().infernoLevel += !player.Card().InfernoStrong ? 2f : 0.67f;
            });
            if (!c.TryGotoNext(i => i.MatchLdcR4(200f)))
                return;
            c.Index++;
            c.Emit(OpCodes.Ldarg_0);
            c.EmitDelegate<Func<Player, float>>(player =>
            {
                return player.Card().infernoLevel * 100;
            });
            if (!c.TryGotoNext(i => i.MatchLdcI4(60)))
                return;
            c.Index++;
            c.Emit(OpCodes.Ldarg_0);
            c.EmitDelegate<Func<Player, int>>(player =>
            {
                return (int)(90 / player.Card().infernoLevel);
            });
            if (!c.TryGotoNext(i => i.MatchLdcI4(10)))
                return;
            c.Index++;
            c.Emit(OpCodes.Ldarg_0);
            c.EmitDelegate<Func<Player, int>>(player =>
            {
                return (int)(player.Card().infernoLevel * MathHelper.SmoothStep(1f, 6.66f, CardUtils.InverseLerp(200f, 50f, player.Card().infernoLevel, true)));
            });
            if (!c.TryGotoNext(i => i.MatchLdcI4(120)))
                return;
            c.Index++;
            c.Emit(OpCodes.Ldarg_0);
            c.EmitDelegate<Func<Player, int>>(player =>
            {
                return (int)(40 * player.Card().infernoLevel);
            });
            if (!c.TryGotoNext(i => i.MatchLdcI4(120)))
                return;
            c.Index++;
            c.Emit(OpCodes.Ldarg_0);
            c.EmitDelegate<Func<Player, int>>(player =>
            {
                return (int)(40 * player.Card().infernoLevel);
            });
        }*/

        public override object Call(params object[] args)
        {
            if (args.Length <= 0)
            {
                mod.Logger.Error("Used zero arguments!");
                return null;
            }
            else
            {
                if (args[0] is string)
                {
                    string callType = (args[0] as string).ToLower();
                    switch (callType)
                    {
                        case "bluejellyimmune":
                            {
                                NPC npc = args[1] is NPC ? args[1] as NPC : null;
                                if (npc is null)
                                {
                                    mod.Logger.Error("Second argument should be NPC!");
                                    return null;
                                }
                                if (args[2] is bool boolean)
                                {
                                    npc.Card().jellyBlueImmune = boolean;
                                    return true;
                                }
                                else
                                {
                                    mod.Logger.Error("Third argument should be boolean!");
                                }
                                return false;
                            }
                        case "pinkjellyimmune":
                            {
                                NPC npc = args[1] is NPC ? args[1] as NPC : null;
                                if (npc is null)
                                {
                                    mod.Logger.Error("Second argument should be NPC!");
                                    return null;
                                }
                                if (args[2] is bool boolean)
                                {
                                    npc.Card().jellyPinkImmune = boolean;
                                    return true;
                                }
                                else
                                {
                                    mod.Logger.Error("Third argument should be boolean!");
                                }
                                return false;
                            }
                        case "greenjellyimmune":
                            {
                                NPC npc = args[1] is NPC ? args[1] as NPC : null;
                                if (npc is null)
                                {
                                    mod.Logger.Error("Second argument should be NPC!");
                                    return null;
                                }
                                if (args[2] is bool boolean)
                                {
                                    npc.Card().jellyGreenImmune = boolean;
                                    return true;
                                }
                                else
                                {
                                    mod.Logger.Error("Third argument should be boolean!");
                                }
                                return false;
                            }
                        default:
                            mod.Logger.Error("Unknown first argument!");
                            return false;
                    }
                }
                else
                {
                    mod.Logger.Error("First argument should be a string!");
                    return null;
                }
            }
        }

        static float time1;
        public static Color LerpColors(Color color1, Color color2)
        {
            time1 += 0.5f;
            if (time1 >= 200)
            {
                time1 = 0;
            }

            if (time1 < 100)
                return Color.Lerp(color1, color2, time1 / 100);
            else
                return Color.Lerp(color2, color1, (time1 - 100) / 100);
        }

        public static class Rarities
        {
            public class Common : ModRarity
            {
                public override Color RarityColor => Color.White;
            }
            public class Uncommon : ModRarity
            {
                public override Color RarityColor => new(140, 255, 226);
            }
            public class Rare : ModRarity
            {
                public override Color RarityColor => new(132, 175, 255);
            }
            public class Epic : ModRarity
            {
                public override Color RarityColor => new(144, 96, 255);
            }
            public class Legendary : ModRarity
            {
                public override Color RarityColor => new(255, 161, 99);
            }
            public class Mythic : ModRarity
            {
                public override Color RarityColor => new(255, 68, 171);
            }
        }
    }
}