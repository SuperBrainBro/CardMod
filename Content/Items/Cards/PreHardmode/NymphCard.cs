using CardMod.Core;
using Terraria;
using Terraria.ID;

namespace CardMod.Content.Items.Cards.PreHardmode
{
    public class NymphCard : BaseCard
    {
        public NymphCard() : base(CardRarity.Rare,
            "Nymph Card",
            "Sight",
                "The closer you are to your enemy," +
              "\nthe higher damage you inflict upon them",
            "You blinded when there's no one to fool")
        {
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals)
        {
            player.Card()._cardNymph = true;

            bool flag;
            flag = false;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active && Main.npc[i].Distance(player.Center) <= 350f)
                    flag = true;
            }

            if (!flag)
                player.AddBuff(BuffID.Darkness, 2);
        }
    }
}
