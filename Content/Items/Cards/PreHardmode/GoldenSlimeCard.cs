using CardMod.Core;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CardMod.Content.Items.Cards.PreHardmode
{
    public class GoldenSlimeCard : BaseCard
    {
        public GoldenSlimeCard() : base(CardRarity.Legendary,
            "Golden Slime Card",
            "Money Pants",
                "Periodically generate a passive income!",
            "The more money you have, the slower you move")
        {
        }


        private int _generated = 0;

        public override void SafeSetDefaults() => isCard = true;

        public override void SafeModifyTooltips(ref List<TooltipLine> tooltips)
        {
            TooltipLine index = tooltips.Find(x => x.mod == Mod.Name && x.text.StartsWith("Money Generated:"));

            if (index != null)
            {
                string displayValue = " ";

                if (_generated > 1000000)
                {
                    displayValue += (((Math.Abs(_generated) % 100000000) - (Math.Abs(_generated) % 1000000)) / 1000000) + $" platinum, ";
                }
                if (_generated > 10000)
                {
                    displayValue += (((Math.Abs(_generated) % 1000000) - (Math.Abs(_generated) % 10000)) / 10000) + $" gold, ";
                }
                if (_generated > 100)
                {
                    displayValue += (((Math.Abs(_generated) % 10000) - (Math.Abs(_generated) % 100)) / 100) + $" silver, and ";
                }
                if (_generated > 1)
                {
                    displayValue += Math.Abs(_generated) % 100 + $" copper coins";
                }
                else
                {
                    displayValue += $"0 coins";
                }
                string color = (Colors.CoinGold * alpha).Hex3();
                index.text = $"Money Generated:[c/{color}:{displayValue}]";
            }
        }

        public override void CardEffects(Player player, bool hideVisuals)
        {
            float value = 0;
            if (_generated > 0)
                value = MathF.Sqrt(1 / 8 * _generated) / 100;

            if (player.Card()._goldenSlimeCD == 0)
            {
                _generated++;
                player.QuickSpawnItem(ItemID.GoldCoin);
                player.Card()._goldenSlimeCD = 1;
                Main.NewText(value * 100);
                Mod.Logger.Debug(value * 100);
            }

            player.moveSpeed -= value;
        }

        public override void SaveData(TagCompound tag) => tag["generatedCoins"] = _generated;

        public override void LoadData(TagCompound tag) => _generated = tag.GetInt("generatedCoins");

        public override void NetSend(BinaryWriter writer) => writer.Write(_generated);

        public override void NetReceive(BinaryReader reader) => _generated = reader.ReadInt32();
    }
}
