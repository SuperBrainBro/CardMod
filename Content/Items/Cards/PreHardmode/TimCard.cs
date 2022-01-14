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
              "\n4% increased magic damage",
            "Due to chaotic powers, your health is decreased")
        {
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals)
        {
            player.statManaMax2 += (int)(40 * CardPlayer.GetCardMultiplier(player));
            player.GetDamage(DamageClass.Magic) += (int)(0.04f * CardPlayer.GetCardMultiplier(player));
            player.GetCritChance(DamageClass.Magic) += (int)(8 * CardPlayer.GetCardMultiplier(player));

            player.statLifeMax2 -= 10;
        }
    }
}
