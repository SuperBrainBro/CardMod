using CardMod.Core;
using Terraria;

namespace CardMod.Content.Items.Cards.PreHardmode
{
    public class GiantAntlionChargerCard : BaseCard
    {
        public GiantAntlionChargerCard() : base(CardRarity.Common,
            "Giant Antlion Charger Card",
            "Spiky Friends",
            "Damage dealt to you from Cacti" +
            "\nwhile in Desert is decreased",
            "Any damage sources outside Desert" +
            "\ndeal more damage to you")
        {
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals) => player.Card()._cardGiantAntlionCharger = true;
    }
}
