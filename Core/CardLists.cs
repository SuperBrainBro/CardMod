using CardMod.Content.Items.Pets;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace CardMod.Core
{
    public class CardLists
    {
        private static List<int> slimes;
        private static Dictionary<int, string> dedicatedItems;

        public static List<int> Slimes { get => slimes; set => slimes = value; }
        public static Dictionary<int, string> DedicatedItems { get => dedicatedItems; set => dedicatedItems = value; }

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
            DedicatedItems = new Dictionary<int, string>()
            {
                { ModContent.ItemType<FoxCookie>(), "FoxXD_" },
            };
        }

        public static void Unload()
        {
            Slimes = null;
            DedicatedItems = null;
        }
    }
}
