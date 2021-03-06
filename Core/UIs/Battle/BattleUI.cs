using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace CardMod.Core.UIs.Battle
{
    internal class BattleUI : UIState
    {
        public static bool visible = false;
        public UIPanel panel;
        public UIImage area;

        private static readonly Player player = Main.player[Main.myPlayer];
        private static readonly Player uiEnemy = player;

        public override void OnInitialize()
        {
            panel = new UIPanel();
            panel.Top.Set(300f, 0f);
            panel.Left.Set(Main.screenWidth / 2.5f, 0f);
            panel.Width.Set(388, 0f);
            panel.Height.Set(348, 0f);
            panel.SetPadding(0f);
            panel.BackgroundColor = new Color(255, 255, 255) * 0f;
            panel.BorderColor = new Color(255, 255, 255) * 0f;

            area = new UIImage(ModContent.Request<Texture2D>("CardMod/Assets/UIs/BattleUI_Area").Value);

            panel.Append(area);

            Append(panel);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            #region Drawing
            Rectangle hitbox = panel.GetInnerDimensions().ToRectangle();

            spriteBatch.Draw(ModContent.Request<Texture2D>("CardMod/Assets/UIs/BattleUI_Area").Value, hitbox, Color.White);

            GetTextures(player.UI().cards, out Asset<Texture2D>[] enemyText);
            DrawIcons(spriteBatch, enemyText, player.UI().cards);

            GetTextures(uiEnemy.UI().cards2, out Asset<Texture2D>[] playerText);
            DrawIcons(spriteBatch, playerText, uiEnemy.UI().cards2, true);

            DrawMiddle(spriteBatch, player.UI().cards, uiEnemy.UI().cards2);
            #endregion

            DoAction();
        }

        private static readonly int[] _actionTimer = new int[4] { 60, 60, 60, 60 };
        private static void DoAction()
        {
            if (Main.instance.IsActive && !Main.gamePaused)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (_actionTimer[i] <= 0)
                    {
                        if (player.UI().cards[i].health > 0 && !player.UI().cards2[i].dead)
                        {
                            player.UI().cards[i].health -= player.UI().cards2[i].damage;
                            if (player.UI().cards[i].health <= 0)
                                player.UI().cards[i].dead = true;
                        }

                        if (uiEnemy.UI().cards2[i].health > 0 && !player.UI().cards[i].dead)
                        {
                            uiEnemy.UI().cards2[i].health -= player.UI().cards[i].damage;
                            if (uiEnemy.UI().cards2[i].health <= 0)
                                uiEnemy.UI().cards2[i].dead = true;
                        }

                        _actionTimer[i] = 60;
                    }
                    else
                    {
                        _actionTimer[i] -= Main.rand.Next(10) + 1;
                    }
                }
            }
        }

        private void DrawIcons(SpriteBatch batch, Asset<Texture2D>[] values, CardStruct[] @struct, bool isPlayer = false)
        {
            Rectangle hitbox = panel.GetInnerDimensions().ToRectangle();

            int[] heights = new int[3] { 20, 92, 42 };
            if (isPlayer)
                heights = new int[3] { 236, 308, 258 };

            for (int i = 0; i < 4; i++)
            {
                Color cardColor = !@struct[i].dead ? Color.White : Color.DarkGray;
                Rectangle hits = new(hitbox.X + 20 + 90 * i, hitbox.Y + heights[0], 78, 92);
                Vector2[] vectors = new Vector2[2]
                {
                    new Vector2(hitbox.X + 26 + 90 * i, hitbox.Y + heights[1]),
                    new Vector2(hitbox.X + 54 + 90 * i, hitbox.Y + heights[1])
                };
                Vector2[] vectors2 = new Vector2[2]
                {
                    new Vector2(hitbox.X + 26 + 90 * i, hitbox.Y + heights[2]),
                    new Vector2(hitbox.X + 76 + 90 * i, hitbox.Y + heights[2])
                };

                batch.Draw(values[i].Value, hits, cardColor);
                for (int j = 0; j < 2; j++)
                    if (@struct[i].abilitiesOnCard.Length == 2 && @struct[i].abilitiesOnCard != null && @struct[i].abilitiesOnCard[j] >= 0)
                        batch.Draw(ModContent.Request<Texture2D>($"CardMod/Assets/UIs/Cards/Ability_{@struct[i].abilitiesOnCard[j]}").Value, vectors2[j], cardColor);

                if (@struct[i].dead)
                    batch.Draw(ModContent.Request<Texture2D>("CardMod/Assets/UIs/BattleUI_Cross").Value, hits, Color.White);

                if (@struct[i].damage > 0 && !@struct[i].dead)
                    Utils.DrawBorderString(batch, ToGoodInt(@struct[i].damage), vectors[0], Color.White, 0.8f);
                if (@struct[i].health > 0 && !@struct[i].dead)
                    Utils.DrawBorderString(batch, ToGoodInt(@struct[i].health), vectors[1], Color.White, 0.8f);
            }
        }

        private void DrawMiddle(SpriteBatch batch, CardStruct[] player, CardStruct[] enemy)
        {
            Rectangle hitbox = panel.GetInnerDimensions().ToRectangle();

            for (int i = 0; i < 4; i++)
            {
                Rectangle hits = new(hitbox.X + 20 + 90 * i, hitbox.Y + 128, 78, 92);

                if (player[i].card == enemy[i].card && player[i].card >= 1 && enemy[i].card >= 1 && !player[i].dead && !enemy[i].dead)
                    batch.Draw(ModContent.Request<Texture2D>("CardMod/Assets/UIs/BattleUI_Cross").Value, hits, Color.White);
            }
        }

        private static void GetTextures(CardStruct[] cards, out Asset<Texture2D>[] texture)
        {
            texture = new Asset<Texture2D>[4];
            for (int j = 0; j < 4; j++)
            {
                if (cards[j] != null && cards[j].card >= 1)
                {
                    texture[j] = ModContent.Request<Texture2D>($"CardMod/Assets/UIs/Cards/Card_{cards[j].card}");
                }
                else
                {
                    texture[j] = ModContent.Request<Texture2D>("CardMod/Assets/UIs/BattleUI_Card");
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        private static string ToGoodInt(int value)
        {
            int value2 = value;
            string size = "";

            if (value2 >= 1000)
            {
                value2 /= 1000;
                size = "k";
            }
            if (value2 >= 1000)
            {
                value2 /= 1000;
                size = "m";
            }
            if (value2 >= 1000)
            {
                value2 /= 1000;
                size = "b";
            }
            if (value2 >= 1000)
            {
                value2 /= 1000;
                size = "t";
            }

            string str = $"{value2}{size.ToUpper()}";
            if (value2 < 0)
                return "0";
            return str;
        }
    }
}
