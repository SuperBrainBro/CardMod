using CardMod.Core;
using Terraria;

namespace CardMod.Content.Items.Cards.PreHardmode
{
    public class AnomuraFungusCard : BaseCard
    {
        public AnomuraFungusCard() : base(CardRarity.Uncommon,
            "Anomura Fungus Card",
            "Ability",
            "Description",
            "Weakness")
        {
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals)
        {
        }
    }
}
