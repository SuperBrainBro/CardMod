using CardMod.Core;
using Terraria;

namespace CardMod.Content.Items.Cards.PreHardmode
{
    public class GraniteGolemCard : BaseCard
    {
        public GraniteGolemCard() : base(CardRarity.Common,
            "Granite Golem Card",
            "Overprotective",
            "You have chance to block some damage",
            "After block your movement speed decreases")
        {
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals) => player.Card()._cardGraniteGolem = true;
    }
}
