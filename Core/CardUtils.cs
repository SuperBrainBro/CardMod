using Microsoft.Xna.Framework;
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

        /// <summary>
        /// THIS HUGE FUCK IS BELONGS TO MrPlague,
        /// PERMISSION GRANTED BY MrPlague TO USE IT HERE
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static bool ExposedToSunlight(this Player player)
        {
            Tile[] wallTiles = new Tile[6];
            Point playerTilePoint = (Main.LocalPlayer.position / 16).ToPoint();
            wallTiles[0] = Framing.GetTileSafely(playerTilePoint.X, playerTilePoint.Y);
            wallTiles[1] = Framing.GetTileSafely(playerTilePoint.X, playerTilePoint.Y + 1);
            wallTiles[2] = Framing.GetTileSafely(playerTilePoint.X, playerTilePoint.Y + 2);
            wallTiles[3] = Framing.GetTileSafely(playerTilePoint.X + 1, playerTilePoint.Y);
            wallTiles[4] = Framing.GetTileSafely(playerTilePoint.X + 1, playerTilePoint.Y + 1);
            wallTiles[5] = Framing.GetTileSafely(playerTilePoint.X + 1, playerTilePoint.Y + 2);
            bool behindWall = false;
            foreach (var tile in wallTiles)
            {
                if (tile.wall > 0)
                {
                    behindWall = true;
                }
                else
                {
                    behindWall = false;
                    break;
                }
            }
            Tile[] largeWallTiles = new Tile[36];
            Point playerTilePointLarge = (Main.LocalPlayer.position / 16).ToPoint();
            largeWallTiles[0] = Framing.GetTileSafely(playerTilePointLarge.X, playerTilePointLarge.Y - 15);
            largeWallTiles[1] = Framing.GetTileSafely(playerTilePointLarge.X, playerTilePointLarge.Y - 14);
            largeWallTiles[2] = Framing.GetTileSafely(playerTilePointLarge.X, playerTilePointLarge.Y - 13);
            largeWallTiles[3] = Framing.GetTileSafely(playerTilePointLarge.X, playerTilePointLarge.Y - 12);
            largeWallTiles[4] = Framing.GetTileSafely(playerTilePointLarge.X, playerTilePointLarge.Y - 11);
            largeWallTiles[5] = Framing.GetTileSafely(playerTilePointLarge.X, playerTilePointLarge.Y - 10);
            largeWallTiles[6] = Framing.GetTileSafely(playerTilePointLarge.X, playerTilePointLarge.Y - 9);
            largeWallTiles[7] = Framing.GetTileSafely(playerTilePointLarge.X, playerTilePointLarge.Y - 8);
            largeWallTiles[8] = Framing.GetTileSafely(playerTilePointLarge.X, playerTilePointLarge.Y - 7);
            largeWallTiles[9] = Framing.GetTileSafely(playerTilePointLarge.X, playerTilePointLarge.Y - 6);
            largeWallTiles[10] = Framing.GetTileSafely(playerTilePointLarge.X, playerTilePointLarge.Y - 5);
            largeWallTiles[11] = Framing.GetTileSafely(playerTilePointLarge.X, playerTilePointLarge.Y - 4);
            largeWallTiles[12] = Framing.GetTileSafely(playerTilePointLarge.X, playerTilePointLarge.Y - 3);
            largeWallTiles[13] = Framing.GetTileSafely(playerTilePointLarge.X, playerTilePointLarge.Y - 2);
            largeWallTiles[14] = Framing.GetTileSafely(playerTilePointLarge.X, playerTilePointLarge.Y - 1);
            largeWallTiles[15] = Framing.GetTileSafely(playerTilePointLarge.X, playerTilePointLarge.Y);
            largeWallTiles[16] = Framing.GetTileSafely(playerTilePointLarge.X, playerTilePointLarge.Y + 1);
            largeWallTiles[17] = Framing.GetTileSafely(playerTilePointLarge.X, playerTilePointLarge.Y + 2);
            largeWallTiles[18] = Framing.GetTileSafely(playerTilePointLarge.X + 1, playerTilePointLarge.Y - 15);
            largeWallTiles[19] = Framing.GetTileSafely(playerTilePointLarge.X + 1, playerTilePointLarge.Y - 14);
            largeWallTiles[20] = Framing.GetTileSafely(playerTilePointLarge.X + 1, playerTilePointLarge.Y - 13);
            largeWallTiles[21] = Framing.GetTileSafely(playerTilePointLarge.X + 1, playerTilePointLarge.Y - 12);
            largeWallTiles[22] = Framing.GetTileSafely(playerTilePointLarge.X + 1, playerTilePointLarge.Y - 11);
            largeWallTiles[23] = Framing.GetTileSafely(playerTilePointLarge.X + 1, playerTilePointLarge.Y - 10);
            largeWallTiles[24] = Framing.GetTileSafely(playerTilePointLarge.X + 1, playerTilePointLarge.Y - 9);
            largeWallTiles[25] = Framing.GetTileSafely(playerTilePointLarge.X + 1, playerTilePointLarge.Y - 8);
            largeWallTiles[26] = Framing.GetTileSafely(playerTilePointLarge.X + 1, playerTilePointLarge.Y - 7);
            largeWallTiles[27] = Framing.GetTileSafely(playerTilePointLarge.X + 1, playerTilePointLarge.Y - 6);
            largeWallTiles[28] = Framing.GetTileSafely(playerTilePointLarge.X + 1, playerTilePointLarge.Y - 5);
            largeWallTiles[29] = Framing.GetTileSafely(playerTilePointLarge.X + 1, playerTilePointLarge.Y - 4);
            largeWallTiles[30] = Framing.GetTileSafely(playerTilePointLarge.X + 1, playerTilePointLarge.Y - 3);
            largeWallTiles[31] = Framing.GetTileSafely(playerTilePointLarge.X + 1, playerTilePointLarge.Y - 2);
            largeWallTiles[32] = Framing.GetTileSafely(playerTilePointLarge.X + 1, playerTilePointLarge.Y - 1);
            largeWallTiles[33] = Framing.GetTileSafely(playerTilePointLarge.X + 1, playerTilePointLarge.Y);
            largeWallTiles[34] = Framing.GetTileSafely(playerTilePointLarge.X + 1, playerTilePointLarge.Y + 1);
            largeWallTiles[35] = Framing.GetTileSafely(playerTilePointLarge.X + 1, playerTilePointLarge.Y + 2);
            bool behindLargeWall = false;
            foreach (var tile in largeWallTiles)
            {
                if (tile.wall > 0)
                {
                    behindLargeWall = true;
                }
                else
                {
                    behindLargeWall = false;
                    break;
                }
            }
            bool hasCeilingTile = false;
            Vector2 playerLocation = new(player.Center.X / 16, player.Center.Y / 16);
            for (int i = 0; i < 60; i++)
            {
                Tile ceilingTile = Main.tile[(int)playerLocation.X, (int)playerLocation.Y];
                if (ceilingTile != null && Main.tileSolid[ceilingTile.type] && ceilingTile.IsActiveUnactuated)
                {
                    hasCeilingTile = true;
                }
                if (playerLocation.Y > 0)
                {
                    playerLocation.Y -= 1;
                }
            }
            bool hasCeilingAbove;
            if (behindLargeWall || hasCeilingTile)
            {
                hasCeilingAbove = true;
            }
            else
            {
                hasCeilingAbove = false;
            }
            return (!hasCeilingAbove || !behindWall) && !(player.Center.Y > Main.worldSurface * 16.0) && Main.dayTime && !(Collision.DrownCollision(player.position, player.width, player.height, player.gravDir));
        }

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
