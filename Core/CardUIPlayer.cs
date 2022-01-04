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
                InBattleWith = null;
                cards = new CardStruct[5] { CardStruct.Null, CardStruct.Null, CardStruct.Null, CardStruct.Null, CardStruct.Null };
                cards2 = new CardStruct[5] { CardStruct.Null, CardStruct.Null, CardStruct.Null, CardStruct.Null, CardStruct.Null };
            }
            else
            {
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
            if (CardMod.prepareCards.JustPressed && CardMod.prepareCards != null)
            {
                cards = Cards.GetRandCard(5).ToArray();
                cards2 = Cards.GetRandCard(5).ToArray();
            }

            if (CardMod.Experimental && CardMod.showUI.JustPressed && CardMod.prepareCards != null)
                BattleUI.visible = !BattleUI.visible;
            if (!CardMod.Experimental && BattleUI.visible)
                BattleUI.visible = false;
        }
    }
}
