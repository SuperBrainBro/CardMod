using CardMod.Core;
using Terraria;

namespace CardMod.Content.Items.Cards.PreHardmode
{
    public class GraniteGolemCard : BaseCard
    {
        public GraniteGolemCard() : base(CardRarity.Common,
            "Granite Golem Card",
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
