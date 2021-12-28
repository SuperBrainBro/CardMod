using CardMod.Core;
using Terraria;

namespace CardMod.Content.Items.Cards.PreHardmode
{
    public class NymphCard : BaseCard
    {
        public NymphCard() : base(CardRarity.Rare)
        {
        }

        public override void SetStaticDefaults2()
        {
            Tooltip.SetDefault(@"Ability: Sight
The closer the enemy is to you, the higher your damage.");
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals) => player.Card()._cardNymph = true;
    }
}
