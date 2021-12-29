using CardMod.Core;
using Terraria;

namespace CardMod.Content.Items.Cards.PreHardmode
{
    public class BlueSlimeCard : BaseCard
    {
        public BlueSlimeCard() : base(CardRarity.Common, "Blue Slime Card", "Contact Slowness", "Inflicts slowness to enemies upon contact")
        {
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals) => player.Card()._cardSlime = true;
    }
}
