using System;
using Terraria;

namespace CardMod.Core
{
    public static class CardUtils
    {
        public static CardGlobalNPC Card(this NPC npc) => npc.GetGlobalNPC<CardGlobalNPC>();
        public static CardProjectile Card(this Projectile projectile) => projectile.GetGlobalProjectile<CardProjectile>();
        public static CardGlobalItem Card(this Item item) => item.GetGlobalItem<CardGlobalItem>();
        public static CardPlayer Card(this Player player) => player.GetModPlayer<CardPlayer>();

        public static bool Moving(this Player player) => Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y) > 1.0f && !player.rocketFrame;

        public static float InverseLerp(float from, float to, float t, bool clamped = false)
        {
            if (clamped)
            {
                if (from < to)
                {
                    if (t < from)
                        return 0.0f;
                    if (t > to)
                        return 1f;
                }
                else
                {
                    if (t < to)
                        return 1f;
                    if (t > from)
                        return 0.0f;
                }
            }
            return (float)((t - from) / (to - from));
        }
    }
}
