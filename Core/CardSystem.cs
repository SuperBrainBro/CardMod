using CardMod.Core.UIs.Battle;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace CardMod.Core
{
    public class CardSystem : ModSystem
    {
        private GameTime _lastUpdateUiGameTime;

        internal CardMod mod = ModContent.GetInstance<CardMod>();

        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;
            if (mod.BattleInterface?.CurrentState != null)
            {
                mod.BattleUI.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "Card Mod: Battle UI",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && mod.BattleInterface?.CurrentState != null && BattleUI.visible && CardMod.Experimental)
                        {
                            mod.BattleInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));
            }
        }
    }
}
