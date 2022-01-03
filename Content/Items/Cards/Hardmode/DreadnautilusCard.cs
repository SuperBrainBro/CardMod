using CardMod.Core;
using Terraria;
using Terraria.ModLoader;

namespace CardMod.Content.Items.Cards.Hardmode
{
    public class DreadnautilusCard : BaseCard
    {
        public DreadnautilusCard() : base(CardRarity.Rare,
            "Dreadnautilus Card",
            "Blood Sacrifice",
            "When you have lower than 50% of max health," +
            "\nyour damage is increased by 10%",
            "If your hp is larger than 75% of max health," +
            "\nyou have decreased defense")
        {
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals)
        {
            if (player.statLife < player.statLifeMax2 * 0.5)
            {
                player.GetDamage(DamageClass.Generic) += 0.1f;
            }

            if (player.statLife > player.statLifeMax2 * 0.75)
            {
                player.statDefense -= 10;
            }
        }
    }
}
