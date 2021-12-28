using System;
using System.Collections.Generic;
using System.IO;
using CardMod.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CardMod.Content.Items.Cards.Hardmode
{
    public class GoldenSlimeCard : BaseCard
    {
        public GoldenSlimeCard() : base(CardRarity.Legendary)
        {
        }

        public override void SetStaticDefaults2()
        {
            Tooltip.SetDefault(@"Ability: Golden Pants
Generates coin every minute
Money Generated:");
        }

        private int _generated = 0;

        public override void SafeSetDefaults() => isCard = true;

        public override void SafeModifyTooltips(ref List<TooltipLine> tooltips)
        {
            TooltipLine index = tooltips.Find(x => x.mod == Mod.Name && x.Name == "Tooltip2");

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
            if (player.Card()._goldenSlimeCD == 0)
            {
                _generated++;
                player.QuickSpawnItem(ItemID.CopperCoin);
                player.Card()._goldenSlimeCD = 3600;
            }
        }

        public override void SaveData(TagCompound tag) => tag["generatedCoins"] = _generated;

        public override void LoadData(TagCompound tag) => _generated = tag.GetInt("generatedCoins");

        public override void NetSend(BinaryWriter writer) => writer.Write(_generated);

        public override void NetReceive(BinaryReader reader) => _generated = reader.ReadInt32();
    }
}
