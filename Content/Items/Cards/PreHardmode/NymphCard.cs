using CardMod.Core;
using Terraria;

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

            player.blind = true;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active && Main.npc[i].Distance(player.Center) <= 350f && !Main.npc[i].friendly && !Main.npc[i].townNPC)
                    player.blind = false;
            }
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                if (Main.player[i].whoAmI != player.whoAmI && !Main.player[i].dead && !Main.player[i].ghost && Main.player[i].active && Main.player[i].Distance(player.Center) <= 350f && Main.player[i].hostile && Main.player[i].team != player.team)
                    player.blind = false;
            }
        }
    }
}
