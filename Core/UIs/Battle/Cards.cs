using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ID;

namespace CardMod.Core.UIs.Battle
{
    /// <summary>
    /// Contains various pre-set Card Struct values
    /// </summary>
    public static class Cards
    {
        public static CardStruct KingSlime => new(1, 40, 400, condition: () => NPC.downedSlimeKing);
        public static CardStruct BlueSlime => new(2, 7, 25, abilitiesOnCard: new int[2] { 1, 2 });
        public static CardStruct GreenSlime => new(3, 6, 14, abilitiesOnCard: new int[2] { 1, 2 });
        public static CardStruct RedSlime => new(4, 12, 35, abilitiesOnCard: new int[2] { 1, 2 });
        public static CardStruct PurpleSlime => new(5, 12, 40, abilitiesOnCard: new int[2] { 1, 2 });
        public static CardStruct GoldenSlime => new(6, 5, 300, abilitiesOnCard: new int[2] { 1, 2 });
        public static CardStruct EyeOfCthulhu => new(7, 15, 560, condition: () => NPC.downedBoss1);
        public static CardStruct EaterOfWorlds => new(8, 22, 750, condition: () => NPC.downedBoss2 && !WorldGen.crimson);
        public static CardStruct BrainOfCthulhu => new(9, 30, 600, condition: () => NPC.downedBoss2 && WorldGen.crimson);
        public static CardStruct QueenBee => new(10, 30, 680, condition: () => NPC.downedQueenBee);
        public static CardStruct Skeletron => new(11, 32, 880, condition: () => NPC.downedBoss3);
        public static CardStruct Deerclops => new(12, 20, 1400, condition: () => NPC.downedDeerclops);
        public static CardStruct WallOfFlesh => new(13, 50, 1600, condition: () => Main.hardMode);
        /*public static readonly int Count = 14;

        public static class Sets
        {
            public static readonly SetFactory Factory = new(Count);

            public static bool[] BossCard => Factory.CreateBoolSet(1, 7, 8, 9, 10, 11, 12, 13);
        }*/

        public static Dictionary<CardStruct, string> GetCardDictionary()
        {
            Dictionary<CardStruct, string> cardStructs = new();

            List<FieldInfo> infos = typeof(Cards).GetFields(BindingFlags.Static | BindingFlags.Public).Where(f => f.FieldType == typeof(CardStruct)).ToList();
            foreach (FieldInfo info in infos)
            {
                Type type = info.FieldType;
                object value = info.GetValue(null);

                if (value.GetType() == typeof(CardStruct))
                {
                    CardStruct value2 = value as CardStruct;

                    if (value2.condition?.Invoke() ?? true)
                        cardStructs.Add(value2, type.Name);

                    CardMod.Mod.Logger.Debug($"Success! Field '{type.Name}' was added to an array.");
                }
                else
                {
                    CardMod.Mod.Logger.Warn($"Error! Field '{type.Name}' couldn't be added to an array.");
                }
            }

            return cardStructs;
        }

        public static List<CardStruct> GetRandCard(int tries)
        {
            HashSet<CardStruct> structs = new();
            List<CardStruct> _maxCards = GetCardDictionary().Keys.ToList();
            List<string> _cardNames = GetCardDictionary().Values.ToList();

            int num;
            int tries2 = tries;
            int totalTries = 0;
            do
            {
                num = Main.rand.Next(_maxCards.Count);

                if (structs.Add(new CardStruct(_maxCards[num], _cardNames[num])))
                    tries2--;

                if (++totalTries >= 10000)
                {
                    CardMod.Mod.Logger.Warn($"Out of possible tries amount.");
                    break;
                }
            }
            while (tries2 > 0);

            return structs.ToList();
        }
    }
}
