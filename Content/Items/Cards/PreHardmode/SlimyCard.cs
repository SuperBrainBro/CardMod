using CardMod.Core;
using Terraria;

namespace CardMod.Content.Items.Cards.PreHardmode
{
    public class SlimyCard : BaseCard
    {
        public SlimyCard() : base(CardRarity.Common)
        {
        }

        public override void SetStaticDefaults2()
        {
            DisplayName.SetDefault("Slime Card");
            Tooltip.SetDefault(@"Ability: Blueslow
Inflicts on enemies slowness on contact");
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals) => player.Card()._cardSlime = true;
    }
}
