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
        public Func<bool> condition;
        public int[] abilitiesOnCard;

        public static CardStruct Null => new(-1, 0, 0, null, () => true, null);

        public CardStruct(int card, int damage = 0, int health = 1, Action<CardStruct> ability = null, Func<bool> condition = null, int[] abilitiesOnCard = null)
        {
            this.card = card;
            this.damage = damage;
            this.health = health;
            this.ability = ability;
            this.condition = condition;
            this.abilitiesOnCard = abilitiesOnCard;
        }
    }
}
