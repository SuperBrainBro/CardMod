using CardMod.Content.Items.Cards.PreHardmode;
using CardMod.Core;
using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
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
            IL.Terraria.Player.UpdateBuffs += Player_UpdateBuffs;
        }

        public override void Unload()
        {
            mod = null;

            IL.Terraria.Player.TorchAttack -= Player_TorchAttack;
            IL.Terraria.Player.UpdateBuffs -= Player_UpdateBuffs;
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

        private void Player_UpdateBuffs(ILContext il)
        {
            var c = new ILCursor(il);
            if (!c.TryGotoNext(i => i.MatchStfld<Player>(nameof(Player.inferno))))
                return;
            c.Index++;
            c.Emit(OpCodes.Ldarg_0);
            c.EmitDelegate<Action<Player>>(player => player.Card().infernoLevel += 3f);
        }

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
                    if (args[1] is NPC npc)
                    {
                        if (npc is null)
                        {
                            mod.Logger.Error("Second argument should be NPC!");
                            return null;
                        }

                        switch (callType)
                        {
                            case "bluejellyimmune":
                                {
                                    switch (args[2])
                                    {
                                        case bool boolean:
                                            npc.Card().jellyBlueImmune = boolean;
                                            return true;
                                        default:
                                            mod.Logger.Error("Third argument should be boolean!");
                                            break;
                                    }
                                    return false;
                                }
                            case "pinkjellyimmune":
                                {
                                    switch (args[2])
                                    {
                                        case bool boolean:
                                            npc.Card().jellyPinkImmune = boolean;
                                            return true;
                                        default:
                                            mod.Logger.Error("Third argument should be boolean!");
                                            break;
                                    }
                                    return false;
                                }
                            case "greenjellyimmune":
                                {
                                    switch (args[2])
                                    {
                                        case bool boolean:
                                            npc.Card().jellyGreenImmune = boolean;
                                            return true;
                                        default:
                                            mod.Logger.Error("Third argument should be boolean!");
                                            break;
                                    }
                                    return false;
                                }
                            default:
                                mod.Logger.Error("Unknown first argument!");
                                return false;
                        }
                    }
                    else if (args[1] is Player || args[1] is int)
                    {
                        Player player = args[1] is int @int ? Main.player[@int] : args[1] as Player;
                        if (player is null)
                        {
                            mod.Logger.Error("Second argument should be Player!");
                            return null;
                        }
                        switch (callType)
                        {
                            case "bluejellyimmune":
                                {
                                    switch (args[2])
                                    {
                                        case bool boolean:
                                            player.Card().blueJellyCardImmune = boolean;
                                            return true;
                                        default:
                                            mod.Logger.Error("Third argument should be boolean!");
                                            break;
                                    }
                                    return false;
                                }
                            case "greenjellyimmune":
                                {
                                    switch (args[2])
                                    {
                                        case bool boolean:
                                            player.Card().greenJellyCardImmune = boolean;
                                            return true;
                                        default:
                                            mod.Logger.Error("Third argument should be boolean!");
                                            break;
                                    }
                                    return false;
                                }
                            case "pinkjellyimmune":
                                {
                                    switch (args[2])
                                    {
                                        case bool boolean:
                                            player.Card().pinkJellyCardImmune = boolean;
                                            return true;
                                        default:
                                            mod.Logger.Error("Third argument should be boolean!");
                                            break;
                                    }
                                    return false;
                                }
                            default:
                                mod.Logger.Error("Unknown first argument!");
                                return false;
                        }
                    }
                    return null;
                }
                else
                {
                    mod.Logger.Error("First argument should be a string!");
                    return null;
                }
            }
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