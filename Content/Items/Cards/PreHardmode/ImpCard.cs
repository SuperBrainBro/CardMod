using CardMod.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CardMod.Content.Items.Cards.PreHardmode
{
    public class ImpCard : BaseCard
    {
        public ImpCard() : base(CardRarity.Uncommon, "Imp Card", "Weak Inferno", "Gives you weak inferno rings," +
            "\nwhich ignite enemies around you")
        {
        }

        public override void SafeSetDefaults() => isCard = true;

        public override void CardEffects(Player player, bool hideVisuals)
        {
            player.Card()._cardImp = true;
            player.Card().infernoLevel += 1f;
        }
    }

    public class ImpCardRings : PlayerDrawLayer
    {
        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) => drawInfo.drawPlayer.Card().InfernoWeak;

        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.IceBarrier);

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player player = drawInfo.drawPlayer;
            CardPlayer modPlayer = player.Card();
            if (drawInfo.shadow != 0.0)
                return;

            if (player.active && !player.outOfRange && modPlayer.InfernoWeak && !player.dead)
            {
                if (TextureAssets.FlameRing.State == 0)
                {
                    Main.Assets.Request<Texture2D>(TextureAssets.FlameRing.Name, AssetRequestMode.ImmediateLoad);
                }

                float num;
                float num2 = 0.1f;
                float num3 = 0.9f;
                if (!Main.gamePaused && Main.instance.IsActive)
                {
                    player.flameRingScale += 0.004f;
                }
                if (player.flameRingScale < 1f)
                {
                    num = player.flameRingScale;
                }
                else
                {
                    player.flameRingScale = 0.8f;
                    num = player.flameRingScale;
                }
                if (!Main.gamePaused && Main.instance.IsActive)
                {
                    player.flameRingRot += 0.05f;
                }
                if (player.flameRingRot > 6.28318548f)
                {
                    player.flameRingRot -= 6.28318548f;
                }
                if (player.flameRingRot < -6.28318548f)
                {
                    player.flameRingRot += 6.28318548f;
                }
                for (int j = 0; j < 3; j++)
                {
                    float num4 = num - num2 * j;
                    if (num4 > 1f)
                    {
                        num4 -= num2 * 2f;
                    }
                    float num5 = MathHelper.Lerp(0.8f, 0f, Math.Abs(num + num2 * j - num3) * 10f);
                    
                    Color color = Color.Lerp(Color.Yellow, Color.LightYellow, 0.86f);
                    Main.spriteBatch.Draw(TextureAssets.FlameRing.Value,
                           player.Center - Main.screenPosition,
                           new Rectangle(0, 400 * j, 400, 400),
                           //Color.Lerp(new Color(num5, num5, num5, num5 / 2f), Color.White, num5 / 2f),
                           Color.Lerp(new Color(num5, num5, num5, num5 / 2f),
                                j == 0 ? color : (j == 1 ? Color.Lerp(color, Color.White, num5 / 2f) : Color.White),
                                num5 / (j + 1)),
                           player.flameRingRot + 1.04719758f * j,
                           new Vector2(200f, 200f),
                           num4 * 0.75f,
                           SpriteEffects.None,
                           0f);
                }
            }
        }
    }
}
