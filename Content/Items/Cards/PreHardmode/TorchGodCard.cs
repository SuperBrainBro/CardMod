﻿using System.Collections.Generic;
using CardMod.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CardMod.Content.Items.Cards.PreHardmode
{
    public class TorchGodCard : BaseCard
    {
        public TorchGodCard() : base(CardRarity.Uncommon)
        {
        }

        public override void SetStaticDefaults2() => Tooltip.SetDefault(@"'God himself held this card'
Ability: The Torch
You able to emit torch - like light!
Light will change depending in
what biome you are.");

        public override void SafeSetDefaults() => isCard = true;

        public override void SafeModifyTooltips(ref List<TooltipLine> tooltips)
        {
            TooltipLine line = tooltips.Find(x => x.mod == Mod.Name && x.Name == "Tooltip0");
            if (line != null)
                line.overrideColor = Color.DarkGray * alpha;
        }

        public override void CardEffects(Player player, bool hideVisuals)
        {
            int style = 0;
            if (player.ZoneDungeon)
                style = 13;
            else if (player.position.Y > (Main.UnderworldLayer * 16))
                style = 7;
            else if (player.ZoneHallow)
                style = 20;
            else if (player.ZoneCorrupt)
                style = 18;
            else if (player.ZoneCrimson)
                style = 19;
            else if (player.ZoneSnow)
                style = 9;
            else if (player.ZoneJungle)
                style = 21;
            else if (player.ZoneDesert && player.position.Y < Main.worldSurface * 16.0 || player.ZoneUndergroundDesert)
                style = 16;
            TorchID.TorchColor(style, out float R, out float G, out float B);

            Lighting.AddLight(player.Center, new Vector3(R, G, B));
        }
    }
}
