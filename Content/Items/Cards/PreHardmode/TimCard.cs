using CardMod.Core;
using Terraria;
using Terraria.ModLoader;

namespace CardMod.Content.Items.Cards.PreHardmode
{
    public class TimCard : BaseCard
    {
        public TimCard() : base(CardRarity.Uncommon,
            "Tim Card",
            "Wizardly Chaos",
                "Increases maximum mana by 40" +
              "\n8% increased magic critical strike chance" +
              "\n4% increased magic damage")
        {
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals)
        {
            player.statManaMax2 += 40;
            player.GetDamage(DamageClass.Magic) += 0.04f;
            player.GetCritChance(DamageClass.Magic) += 8;
        }
    }
}
