using CardMod.Core;
using Microsoft.Xna.Framework;
using Terraria;

namespace CardMod.Content.Items.Cards.PreHardmode
{
    public class TimCard : BaseCard
    {
        public TimCard() : base(CardRarity.Common, "Tim Card", "Wizardly Chaos", @"Increases maximum mana by 40
8% increased magic critical strike chance
4% increased magic damage.")
        {
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals)
        {
            player.Card()._cardTim = true;
        }
    }
}
