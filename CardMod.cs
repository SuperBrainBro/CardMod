using CardMod.Content.Items.Cards.Misc;
using CardMod.Core;
using CardMod.Core.UIs.Battle;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace CardMod
{
    public class CardMod : Mod
    {
        public static bool Experimental => CardConfig.Instance.ExperimentalFuncs;
        private static Mod mod;
        public UserInterface BattleInterface;
        internal BattleUI BattleUI;
        internal static ModKeybind prepareCards;
        internal static ModKeybind showUI;

        public static Mod Mod { get => mod; private set => mod = value; }

        public override void Load()
        {
            Mod = this;

            CardLists.Load();

            if (Experimental)
            {
                prepareCards = KeybindLoader.RegisterKeybind(Mod, "Prepare to Battle", Keys.F5);
                showUI = KeybindLoader.RegisterKeybind(Mod, "Show Battle UI", Keys.F6);
            }
            else
            {
                prepareCards = KeybindLoader.RegisterKeybind(Mod, "Experimental Func #1", Keys.F5);
                showUI = KeybindLoader.RegisterKeybind(Mod, "Experimental Func #2", Keys.F6);
            }

            IL.Terraria.NPC.NPCLoot_DropMoney += NPC_NPCLoot_DropMoney;
            IL.Terraria.Player.TorchAttack += Player_TorchAttack;
            IL.Terraria.Player.UpdateBuffs += Player_UpdateBuffs;

            if (!Main.dedServ)
            {
                BattleUI = new BattleUI();
                BattleUI.Activate();
                BattleInterface = new UserInterface();
                BattleInterface.SetState(BattleUI);
            }
        }

        public override void Unload()
        {
            Mod = null;

            CardLists.Unload();

            IL.Terraria.NPC.NPCLoot_DropMoney -= NPC_NPCLoot_DropMoney;
            IL.Terraria.Player.TorchAttack -= Player_TorchAttack;
            IL.Terraria.Player.UpdateBuffs -= Player_UpdateBuffs;

            BattleUI.visible = false;
            BattleUI = null;
            BattleInterface = null;
        }

        public override void PostSetupContent()
        {
            BattleInterface?.SetState(BattleUI);
        }

        private void NPC_NPCLoot_DropMoney(ILContext il)
        {
            var c = new ILCursor(il);
            if (!c.TryGotoNext(i => i.MatchLdfld(typeof(NPC).GetField(nameof(NPC.midas)))))
                return;
            int num4 = 0;
            if (!c.TryGotoNext(i => i.MatchLdloc(out num4)))
                return;
            if (!c.TryGotoNext(i => i.MatchStloc(out _)))
                return;
            if (!c.TryGotoNext(i => i.MatchStloc(out _)))
                return;

            c.Index++;
            c.Emit(OpCodes.Ldarg_1);
            c.Emit(OpCodes.Ldloc, num4);
            c.EmitDelegate<Func<Player, float, float>>((player, num) =>
            {
                if (player.Card()._cardDiscount)
                    num *= 1f - Main.rand.Next(15, 55) * 0.01f;
                return num;
            });
            c.Emit(OpCodes.Stloc, num4);
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
                Mod.Logger.Error("Used zero arguments!");
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
                            Mod.Logger.Error("Second argument should be NPC!");
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
                                            Mod.Logger.Error("Third argument should be boolean!");
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
                                            Mod.Logger.Error("Third argument should be boolean!");
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
                                            Mod.Logger.Error("Third argument should be boolean!");
                                            break;
                                    }
                                    return false;
                                }
                            default:
                                Mod.Logger.Error("Unknown first argument!");
                                return false;
                        }
                    }
                    else if (args[1] is Player || args[1] is int)
                    {
                        Player player = args[1] is int @int ? Main.player[@int] : args[1] as Player;
                        if (player is null)
                        {
                            Mod.Logger.Error("Second argument should be Player!");
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
                                            Mod.Logger.Error("Third argument should be boolean!");
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
                                            Mod.Logger.Error("Third argument should be boolean!");
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
                                            Mod.Logger.Error("Third argument should be boolean!");
                                            break;
                                    }
                                    return false;
                                }
                            default:
                                Mod.Logger.Error("Unknown first argument!");
                                return false;
                        }
                    }
                    return null;
                }
                else
                {
                    Mod.Logger.Error("First argument should be a string!");
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

            public class Dedicated : ModRarity
            {
                public override Color RarityColor
                {
                    get
                    {
                        Color[] itemNameCycleColors = new Color[]
                        {
                            new Color(164, 119, 255),
                            new Color(232, 191, 255),
                            new Color(191, 247, 205),
                            new Color(112, 159, 255),
                        };

                        float counter = 90f / itemNameCycleColors.Length;
                        float fade = Main.GameUpdateCount % (int)counter / counter;
                        int index = (int)(Main.GameUpdateCount / (int)counter % itemNameCycleColors.Length);

                        return Color.Lerp(itemNameCycleColors[index], itemNameCycleColors[(index + 1) % itemNameCycleColors.Length], fade);
                    }
                }
            }
        }
    }
}