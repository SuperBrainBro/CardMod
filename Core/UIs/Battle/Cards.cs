using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;

namespace CardMod.Core.UIs.Battle
{
    /// <summary>
    /// Contains various pre-set Card Struct values
    /// </summary>
    public class Cards
    {
        public static CardStruct KingSlime => new(1, 40, 400, condition: () => NPC.downedSlimeKing);
        public static CardStruct BlueSlime => new(2, 7, 25, abilitiesOnCard: new int[2] { 1, 2 });
        public static CardStruct GreenSlime => new(3, 6, 14, abilitiesOnCard: new int[2] { 1, 2 });
        public static CardStruct RedSlime => new(4, 12, 35, abilitiesOnCard: new int[2] { 1, 2 });
        public static CardStruct PurpleSlime => new(5, 12, 40, abilitiesOnCard: new int[2] { 1, 2 });
        public static CardStruct GoldenSlime => new(6, 5, 300, abilitiesOnCard: new int[2] { 1, 2 });
        internal static int Count = 7;

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

            int num;
            int tries2 = tries;
            int totalTries = 0;
            do
            {
                num = Main.rand.Next(_maxCards.Count);

                if (structs.Add(_maxCards[num]))
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
