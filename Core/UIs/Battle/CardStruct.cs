using System;

namespace CardMod.Core.UIs.Battle
{
    public class CardStruct
    {
        public int card;
        public int damage;
        public int health;
        public bool dead;
        public Action<CardStruct, CardStruct> ability;
        public Func<bool> condition;
        public int[] abilitiesOnCard;
        public string name;

        public static CardStruct Null => new(-1, 0, 0, null, () => true, null);

        public CardStruct(int card,
            int damage = 0,
            int health = 1,
            Action<CardStruct, CardStruct> ability = null,
            Func<bool> condition = null,
            int[] abilitiesOnCard = null,
            string name = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            }

            this.card = card;
            this.damage = damage;
            this.health = health;
            this.ability = ability ?? throw new ArgumentNullException(nameof(ability));
            this.condition = condition ?? throw new ArgumentNullException(nameof(condition));
            this.abilitiesOnCard = abilitiesOnCard ?? throw new ArgumentNullException(nameof(abilitiesOnCard));
            this.name = name;
        }

        public CardStruct(CardStruct copyFrom, string name = "")
        {
            if (copyFrom is null)
            {
                throw new ArgumentNullException(nameof(copyFrom));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            }

            card = copyFrom.card;
            ability = copyFrom.ability;
            damage = copyFrom.damage;
            dead = copyFrom.dead;
            health = copyFrom.health;
            abilitiesOnCard = copyFrom.abilitiesOnCard;
            condition = copyFrom.condition;
            if (name == "")
                this.name = copyFrom.card.ToString();
            else
                this.name = name;
        }
    }
}
