using CardMod.Core;
using Microsoft.Xna.Framework;
using Terraria;

namespace CardMod.Content.Items.Cards.Hardmode
{
    public class GreenJellyfishCard : BaseCard
    {
        public GreenJellyfishCard() : base(CardRarity.Uncommon,
            "Green Jellyfish Card",
            "Jelly Weakness",
                "While in water, enemies nearby have less damage")
        {
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals)
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.friendly && npc.damage > 0 && !npc.dontTakeDamage && player.CanNPCBeHitByPlayerOrPlayerProjectile(npc) && player.wet && npc.Distance(player.Center) <= 200f)
                {
                    npc.Card().greenJellyCard = true;
                }
            }
            if (player.hostile)
            {
                for (int playerTargetIndex = 0; playerTargetIndex < byte.MaxValue; ++playerTargetIndex)
                {
                    Player player2 = Main.player[playerTargetIndex];
                    if (player2 != player && player.active && !player.dead && player.hostile && (player2.team != player.team || player.team == 0) && Vector2.Distance(player.Center, player.Center) <= 200f)
                    {
                        player2.Card().greenJellyCard = true;
                    }
                }
            }
        }
    }
}
