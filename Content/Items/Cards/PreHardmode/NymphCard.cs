using CardMod.Core;
using Terraria;

namespace CardMod.Content.Items.Cards.PreHardmode
{
    public class NymphCard : BaseCard
    {
        public NymphCard() : base(CardRarity.Rare, "Nymph Card", "Sight", "The closer you are to your enemy," +
            "\nthe higher damage you inflict upon them.")
        {
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals) => player.Card()._cardNymph = true;
    }
}
