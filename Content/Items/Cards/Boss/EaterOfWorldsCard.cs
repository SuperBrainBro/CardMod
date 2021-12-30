using CardMod.Core;
using Terraria;
using Terraria.ModLoader;

namespace CardMod.Content.Items.Cards.Boss
{
    public class EaterOfWorldsCard : BaseCard
    {
        public EaterOfWorldsCard() : base(CardRarity.Rare,
            "Eater of Worlds Card",
            "Symmetrical Shadow",
                "Around player are flying shadow orbs" +
              "\nthose deal lots of damage on collision with enemies.",
            "You're weak on sun light due to how empty you are.")
        {
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals)
        {
            if (player.ExposedToSunlight())
                player.GetDamage(DamageClass.Generic) -= 0.1f;
        }
    }
}
