using CardMod.Core.UIs.Battle;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CardMod.Core
{
    public class CardUIPlayer : ModPlayer
    {
        public Tuple<bool, int> InBattleWith;
        public CardStruct[] cards = new CardStruct[5];
        public CardStruct[] cards2 = new CardStruct[5];

        public override void ResetEffects()
        {
            if (cards.Length <= 4 || cards.Length >= 6)
            {
                throw new IndexOutOfRangeException("Cards array have larger or smaller amount of structs than it supposed to have!");
            }

            if (!BattleUI.visible)
            {
                cards = new CardStruct[5] { CardStruct.Null, CardStruct.Null, CardStruct.Null, CardStruct.Null, CardStruct.Null };
                cards2 = new CardStruct[5] { CardStruct.Null, CardStruct.Null, CardStruct.Null, CardStruct.Null, CardStruct.Null };
            }
            else
            {
                /*for (int i = 0; i < 5; i++)
                {
                    if (cards[i].health <= 0 && !cards[i].dead)
                        cards[i].dead = true;
                    if (cards2[i].health <= 0 && !cards2[i].dead)
                        cards2[i].dead = true;
                }*/
            }
        }

        public override void UpdateDead()
        {
            InBattleWith = null;
            cards = new CardStruct[5] { CardStruct.Null, CardStruct.Null, CardStruct.Null, CardStruct.Null, CardStruct.Null };
            cards2 = new CardStruct[5] { CardStruct.Null, CardStruct.Null, CardStruct.Null, CardStruct.Null, CardStruct.Null };
        }

        public override void PostUpdateMiscEffects()
        {
            if (CardMod.prepareCards.JustPressed)
            {
                for (int i = 0; i < 5; i++)
                {
                    cards[i] = new CardStruct(Main.rand.Next(0, 5), Main.rand.Next(1, 100), Main.rand.Next(500, 2500));
                    cards2[i] = new CardStruct(Main.rand.Next(0, 5), Main.rand.Next(1, 100), Main.rand.Next(500, 2500));
                }
            }

            if (CardMod.Experimental && CardMod.showUI.JustPressed)
                BattleUI.visible = !BattleUI.visible;
            if (!CardMod.Experimental && BattleUI.visible)
                BattleUI.visible = false;
        }
    }
}
