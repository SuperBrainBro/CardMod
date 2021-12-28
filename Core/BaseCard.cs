using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace CardMod.Core
{
    public abstract class BaseCard : CardItem
    {
        protected BaseCard(int cardRarity = CardRarity.Common) : base(cardRarity)
        {
        }

        public sealed override void SetStaticDefaults()
        {
            SetStaticDefaults2();
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public virtual void SetStaticDefaults2()
        {
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
