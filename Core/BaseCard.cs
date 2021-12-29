using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CardMod.Core
{
    public abstract class BaseCard : CardItem
    {
        public BaseCard(int cardRarity = CardRarity.Common, string name = "Base Card", string ability = "Ability", string abilityDescription = "Description") : base(cardRarity)
        {
            cardName = name;
            cardAbility = ability + "!";
            cardAbilityDescription = abilityDescription;
        }

        public string cardName;
        public string cardAbility;
        public string cardAbilityDescription;


        public sealed override void SetStaticDefaults()
        {
            DisplayName.SetDefault(cardName);
            Tooltip.SetDefault("Ability: " + cardAbility + Environment.NewLine + cardAbilityDescription);
        }

        public sealed override string Texture
        {
            get
            {
                if (ModContent.RequestIfExists<Texture2D>((GetType().Namespace + "." + Name).Replace('.', '/').Replace("Content", "Assets"), out _))
                    return (GetType().Namespace + "." + Name).Replace('.', '/').Replace("Content", "Assets");
                return $"CardMod/Assets/Items/Cards/Card{cardRarity}";
            }
        }

        public sealed override void UpdateAccessory(Player player, bool hideVisual) => CardEffects(player, hideVisual);

        public virtual void CardEffects(Player player, bool hideVisuals)
        {
        }
    }
}
