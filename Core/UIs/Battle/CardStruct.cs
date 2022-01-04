using System;

namespace CardMod.Core.UIs.Battle
{
    public class CardStruct
    {
        public int card;
        public int damage;
        public int health;
        public bool dead;
        public Action<CardStruct> ability;
        public bool condition;

        public static CardStruct Null => new(-1, 0, 0, null, true);

        public CardStruct(int card, int damage = 0, int health = 1, Action<CardStruct> ability = null, bool condition = true)
        {
            this.card = card;
            this.damage = damage;
            this.health = health;
            this.ability = ability;
            this.condition = condition;
        }
    }
}
