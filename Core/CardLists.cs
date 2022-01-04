using System.Collections.Generic;
using Terraria.ID;

namespace CardMod.Core
{
    public class CardLists
    {
        private static List<int> slimes;

        public static List<int> Slimes { get => slimes; set => slimes = value; }

        public static void Load()
        {
            Slimes = new List<int>
            {
                NPCID.GreenSlime,
                NPCID.BlueSlime,
                NPCID.RedSlime,
                NPCID.PurpleSlime,
                NPCID.YellowSlime,
                NPCID.BlackSlime,
                NPCID.Pinky,
                NPCID.GoldenSlime,
                NPCID.MotherSlime,
                NPCID.BabySlime,
                NPCID.UmbrellaSlime,
                NPCID.IceSlime,
                NPCID.SpikedIceSlime,
                NPCID.JungleSlime,
                NPCID.SpikedJungleSlime,
                NPCID.SandSlime,
                NPCID.DungeonSlime,
                NPCID.LavaSlime,
                NPCID.ToxicSludge,
                NPCID.CorruptSlime,
                NPCID.Slimeling,
                NPCID.Slimer,
                NPCID.Slimer2,
                NPCID.Crimslime,
                NPCID.RainbowSlime,
                NPCID.IlluminantSlime,
                NPCID.Gastropod,
            };
        }

        public static void Unload()
        {
            Slimes = null;
        }
    }
}
