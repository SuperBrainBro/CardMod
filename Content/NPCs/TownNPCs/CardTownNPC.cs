using CardMod.Core;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CardMod.Content.NPCs.TownNPCs
{
    [AutoloadHead]
    public class CardTownNPC : ModNPC
    {
        private static int shopNum;
        private static readonly bool showCycleShop;
        public enum ShopGroups
        {
            Common,
            Uncommon,
            Rare,
            Epic,
            Legendary,
            Mythical,
            Other
        }

        public override bool IsLoadingEnabled(Mod mod) => false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Example Person");
            Main.npcFrameCount[Type] = 25;

            NPCID.Sets.ExtraFramesCount[Type] = 9;
            NPCID.Sets.AttackFrameCount[Type] = 4;
            NPCID.Sets.DangerDetectRange[Type] = 700;
            NPCID.Sets.AttackType[Type] = 0;
            NPCID.Sets.AttackTime[Type] = 90;
            NPCID.Sets.AttackAverageChance[Type] = 30;
            NPCID.Sets.HatOffsetY[Type] = 4;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new(0)
            {
                Direction = 1,
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 10;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.Guide;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("help"),
            });
        }

        public override string GetChat() => "My coder shucks!";

        public override string TownNPCName() => "HELPLS";

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
            if (showCycleShop)
            {
                button += $" {shopNum + 1}";
                button2 = "Cycle Shop";
            }
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                shop = true;
            }
            else
            {
                shopNum++;
            }

            if (shopNum > SellableItems.Count / 40)
                shopNum = 0;
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            List<int> sellableItems = SellableItems;
            int i = 0;
            foreach (int type in sellableItems)
            {
                if (++i < shopNum * 40)
                    continue;

                if (nextSlot >= 40)
                    break;

                shop.item[nextSlot].SetDefaults(type);
                shop.item[nextSlot].shopCustomPrice = shop.item[nextSlot].value * 5;
                nextSlot++;
            }
        }

        private static void TryAddItem(Item item, List<int>[] collections)
        {
            void AddToCollection(int type, ShopGroups group)
            {
                int group2 = (int)group;
                if (!collections[group2].Contains(type))
                    collections[group2].Add(type);
            };

            if (item.ModItem == null)
                return;

            if (item.ModItem.Name.EndsWith("Card") && item.ModItem.Mod.Name.Equals(CardMod.Mod.Name))
            {
                if (item.ModItem is BaseCard)
                {
                    BaseCard card = item.ModItem as BaseCard;
                    ShopGroups rare = card.cardRarity switch
                    {
                        0 => ShopGroups.Common,
                        1 => ShopGroups.Uncommon,
                        2 => ShopGroups.Rare,
                        3 => ShopGroups.Epic,
                        4 => ShopGroups.Legendary,
                        5 => ShopGroups.Mythical,
                        _ => ShopGroups.Other,
                    };

                    if (card.CanBeBoughtFromCardNPC())
                        AddToCollection(item.type, rare);
                }
            }
        }

        private static List<int> SellableItems
        {
            get
            {
                List<int>[] itemCollections = new List<int>[6];
                for (int i = 0; i < itemCollections.Length; i++)
                    itemCollections[i] = new List<int>();

                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player player = Main.player[i];
                    if (!player.active)
                        continue;

                    foreach (Item item in player.inventory)
                        TryAddItem(item, itemCollections);

                    foreach (Item item in player.armor)
                        TryAddItem(item, itemCollections);
                }

                List<int> sellableItems = new();
                for (int i = 0; i < itemCollections.Length; i++)
                {
                    sellableItems.AddRange(itemCollections[i]);
                }
                return sellableItems;
            }
        }
    }
}
