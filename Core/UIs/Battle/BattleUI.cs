using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public override void OnInitialize()
        {
            panel = new UIPanel();
            panel.Top.Set(300f, 0f);
            panel.Left.Set(Main.screenWidth / 2.5f, 0f);
            panel.Width.Set(452, 0f);
            panel.Height.Set(382, 0f);
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
            Player player = Main.LocalPlayer;
            Player uiEnemy = player;

            #region Drawing
            Rectangle hitbox = panel.GetInnerDimensions().ToRectangle();

            spriteBatch.Draw(ModContent.Request<Texture2D>("CardMod/Assets/UIs/BattleUI_Area").Value, hitbox, Color.White);

            GetTextures(player.UI().cards, out string[] enemyText);
            DrawIcons(spriteBatch, enemyText, player.UI().cards);

            GetTextures(uiEnemy.UI().cards2, out string[] playerText);
            DrawIcons(spriteBatch, playerText, uiEnemy.UI().cards2, true);

            DrawMiddle(spriteBatch, player.UI().cards, uiEnemy.UI().cards2);
            #endregion

            DoAction();
        }

        private static int _actionTimer = 60;
        private static void DoAction()
        {
            Player player = Main.LocalPlayer;
            Player uiEnemy = player;

            if (Main.instance.IsActive && !Main.gamePaused)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (_actionTimer <= 0)
                    {
                        if (player.UI().cards[i].health > 0 && !player.UI().cards2[i].dead)
                        {
                            player.UI().cards[i].health -= player.UI().cards2[i].damage;
                            if (player.UI().cards[i].health <= 0)
                                player.UI().cards[i].dead = true;
                        }

                        if (player.UI().cards2[i].health > 0 && !player.UI().cards[i].dead)
                        {
                            player.UI().cards2[i].health -= player.UI().cards[i].damage;
                            if (player.UI().cards2[i].health <= 0)
                                player.UI().cards2[i].dead = true;
                        }

                        _actionTimer = 60;
                    }
                    else
                    {
                        _actionTimer -= 10;
                    }
                }
            }
        }

        private void DrawIcons(SpriteBatch batch, string[] values, CardStruct[] @struct, bool isPlayer = false)
        {
            Rectangle hitbox = panel.GetInnerDimensions().ToRectangle();

            int[] heights = new int[2] { 64, 110, };
            if (isPlayer)
                heights = new int[2] { 236, 282 };

            for (int i = 0; i < 5; i++)
            {
                Rectangle hits = new(hitbox.X + 40 + 80 * i, hitbox.Y + heights[0], 52, 70);
                Vector2[] vectors = new Vector2[2]
                {
                    new Vector2(hitbox.X + 46 + 80 * i, hitbox.Y + heights[1]),
                    new Vector2(hitbox.X + 68 + 80 * i, hitbox.Y + heights[1])
                };

                batch.Draw(ModContent.Request<Texture2D>(values[i]).Value, hits, @struct[i].dead ? Color.DarkGray : Color.White);
                if (@struct[i].dead)
                    batch.Draw(ModContent.Request<Texture2D>("CardMod/Assets/UIs/BattleUI_Cross").Value, hits, Color.White);

                if (@struct[i].damage > 0 && !@struct[i].dead)
                    Utils.DrawBorderString(batch, ToGoodInt(@struct[i].damage), vectors[0], Color.White, 0.95f);
                if (@struct[i].health > 0 && !@struct[i].dead)
                    Utils.DrawBorderString(batch, ToGoodInt(@struct[i].health), vectors[1], Color.White, 0.95f);
            }
        }
        
        private void DrawMiddle(SpriteBatch batch, CardStruct[] player, CardStruct[] enemy)
        {
            Rectangle hitbox = panel.GetInnerDimensions().ToRectangle();

            for (int i = 0; i < 5; i++)
            {
                Rectangle hits = new(hitbox.X + 40 + 80 * i, hitbox.Y + 148, 52, 70);

                if (player[i].card == enemy[i].card && player[i].card >= 1 && enemy[i].card >= 1 && !player[i].dead && !enemy[i].dead)
                    batch.Draw(ModContent.Request<Texture2D>("CardMod/Assets/UIs/BattleUI_Cross").Value, hits, Color.White);
            }
        }

        private static void GetTextures(CardStruct[] cards, out string[] texture)
        {
            texture = new string[5];
            for (int j = 0; j < 5; j++)
            {
                if (cards[j] != null)
                {
                    if (cards[j].card == 1)
                    {
                        texture[j] = "CardMod/Assets/UIs/Cards/Card_KingSlime";
                    }
                    else if (cards[j].card >= 2)
                    {
                        texture[j] = "CardMod/Assets/UIs/Cards/Card" + (cards[j].card - 1);
                    }
                    else
                    {
                        texture[j] = "CardMod/Assets/UIs/BattleUI_Card";
                    }
                }
                else
                {
                    texture[j] = "CardMod/Assets/UIs/BattleUI_Card";
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

            bool thousands = false;

            if (value2 >= 1000)
            {
                value2 /= 1000;
                thousands = true;
            }

            if (thousands)
                return $"{value2}K";

            string str = value2.ToString();
            if (value2 <= 0)
                return "0";
            return str;
        }
    }
}
