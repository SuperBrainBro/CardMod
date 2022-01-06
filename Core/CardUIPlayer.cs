﻿using CardMod.Core.UIs.Battle;
using System;
using Terraria;
using Terraria.ModLoader;

namespace CardMod.Core
{
    public class CardUIPlayer : ModPlayer
    {
        public Tuple<bool, int> InBattleWith;
        public CardStruct[] cards = new CardStruct[4];
        public CardStruct[] cards2 = new CardStruct[4];

        public override void ResetEffects()
        {
            if (cards.Length <= 3 || cards.Length >= 5)
            {
                throw new IndexOutOfRangeException("Cards array have larger or smaller amount of structs than it supposed to have!");
            }

            if (!BattleUI.visible)
            {
                InBattleWith = null;
                cards = new CardStruct[4] { CardStruct.Null, CardStruct.Null, CardStruct.Null, CardStruct.Null };
                cards2 = new CardStruct[4] { CardStruct.Null, CardStruct.Null, CardStruct.Null, CardStruct.Null };
            }
            else
            {
            }
        }

        public override void UpdateDead()
        {
            InBattleWith = null;
            cards = new CardStruct[4] { CardStruct.Null, CardStruct.Null, CardStruct.Null, CardStruct.Null };
            cards2 = new CardStruct[4] { CardStruct.Null, CardStruct.Null, CardStruct.Null, CardStruct.Null };
        }

        public override void PostUpdateMiscEffects()
        {
            if (CardMod.prepareCards.JustPressed && CardMod.prepareCards != null)
            {
                for (int i = 0; i < cards.Length; i++)
                {
                    cards[i] = new CardStruct(
                        Main.rand.NextBool(2).ToInt() + 1,
                        Main.rand.Next(1, 100),
                        Main.rand.Next(100, 2500),
                        abilitiesOnCard: new int[2] { Main.rand.NextBool(2).ToInt() + 1, Main.rand.NextBool(2).ToInt() + 1 });
                    cards2[i] = new CardStruct(
                        Main.rand.NextBool(2).ToInt() + 1,
                        Main.rand.Next(1, 100),
                        Main.rand.Next(100, 2500),
                        abilitiesOnCard: new int[2] { Main.rand.NextBool(2).ToInt() + 1, Main.rand.NextBool(2).ToInt() + 1 });
                }
            }

            if (CardMod.Experimental && CardMod.showUI.JustPressed && CardMod.prepareCards != null)
                BattleUI.visible = !BattleUI.visible;
            if (!CardMod.Experimental && BattleUI.visible)
                BattleUI.visible = false;
        }
    }
}
