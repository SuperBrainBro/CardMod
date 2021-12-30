using CardMod.Core;
using Terraria;

namespace CardMod.Content.Items.Cards.PreHardmode
{
    public class GreenSlimeCard : BaseCard
    {
        public GreenSlimeCard() : base(CardRarity.Common,
            "Green Slime Card",
            "Contact Slowness",
                "Inflicts slowness to enemies upon contact")
        {
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals) => player.Card()._cardSlime = true;
    }
}
