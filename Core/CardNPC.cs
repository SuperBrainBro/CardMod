using System;
using CardMod.Content.Items.Cards.Boss;
using CardMod.Content.Items.Cards.Hardmode;
using CardMod.Content.Items.Cards.PreHardmode;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CardMod.Core
{
    public class CardGlobalNPC : GlobalNPC
    {
        public bool blueJellyCard;
        public bool pinkJellyCard;
        public bool greenJellyCard;
        public bool jellyBlueImmune;
        public bool jellyPinkImmune;
        public bool jellyGreenImmune;

        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances => true;
        public override GlobalNPC Clone()
        {
            CardGlobalNPC clone = (CardGlobalNPC)base.Clone();
            clone.jellyBlueImmune = jellyBlueImmune;
            clone.jellyGreenImmune = jellyGreenImmune;
            clone.jellyPinkImmune = jellyPinkImmune;
            return clone;
        }

        public override void ResetEffects(NPC npc)
        {
            if (!npc.wet)
                blueJellyCard = false;
            if (!npc.wet)
                pinkJellyCard = false;
            if (!npc.wet)
                greenJellyCard = false;

            if (blueJellyCard && !jellyBlueImmune)
                npc.defense -= 10;
            if (greenJellyCard && !jellyGreenImmune)
            {
                npc.damage -= 20;
                if (npc.damage <= 0)
                    npc.damage = 1;
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (pinkJellyCard && !jellyPinkImmune)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 40;
                if (damage < 5)
                {
                    damage = 5;
                }
            }
        }

        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            if (!npc.buffImmune[BuffID.Slow] && target.Card()._cardSlime)
                target.AddBuff(BuffID.Slow, 160);
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            foreach (Item item in shop.item)
            {
                float mult = Main.LocalPlayer.Card()._cardDiscount ? 0.9f : 1f;
                item.value = (int)Math.Round(item.value * mult);
            }
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            const int normie = 200;
            const int boss = 100;

            switch (npc.type)
            {
                case NPCID.BlueSlime:
                    npcLoot.Add(ItemDropRule.Common(ItemType<BlueSlimeCard>(), normie));
                    break;
                case NPCID.UmbrellaSlime:
                    npcLoot.Add(ItemDropRule.Common(ItemType<UmbrellaSlimeCard>(), normie));
                    break;
                case NPCID.BlueJellyfish:
                    npcLoot.Add(ItemDropRule.Common(ItemType<BlueJellyfishCard>(), normie));
                    break;
                case NPCID.PinkJellyfish:
                    npcLoot.Add(ItemDropRule.Common(ItemType<PinkJellyfishCard>(), normie));
                    break;
                case NPCID.Nymph:
                    npcLoot.Add(ItemDropRule.Common(ItemType<NymphCard>(), normie));
                    break;
                case NPCID.GoldenSlime:
                    npcLoot.Add(ItemDropRule.Common(ItemType<GoldenSlimeCard>(), normie));
                    break;
                case NPCID.FireImp:
                    npcLoot.Add(ItemDropRule.Common(ItemType<ImpCard>(), normie));
                    break;

                case NPCID.GreenJellyfish:
                    npcLoot.Add(ItemDropRule.Common(ItemType<GreenJellyfishCard>(), normie));
                    break;
                case NPCID.PirateCorsair:
                    npcLoot.Add(ItemDropRule.Common(ItemType<DiscountCard>(), (int)(normie * 12.5)));
                    break;
                case NPCID.PirateCrossbower:
                    npcLoot.Add(ItemDropRule.Common(ItemType<DiscountCard>(), (int)(normie * 12.5)));
                    break;
                case NPCID.PirateDeadeye:
                    npcLoot.Add(ItemDropRule.Common(ItemType<DiscountCard>(), (int)(normie * 12.5)));
                    break;
                case NPCID.PirateDeckhand:
                    npcLoot.Add(ItemDropRule.Common(ItemType<DiscountCard>(), (int)(normie * 12.5)));
                    break;
                case NPCID.PirateCaptain:
                    npcLoot.Add(ItemDropRule.Common(ItemType<DiscountCard>(), normie * 5));
                    break;
                case NPCID.PirateShip:
                    npcLoot.Add(ItemDropRule.Common(ItemType<DiscountCard>(), normie));
                    break;

                case NPCID.WallofFlesh:
                    npcLoot.Add(ItemDropRule.Common(ItemType<WallOfFleshCard>(), boss));
                    break;
                case NPCID.QueenSlimeBoss:
                    npcLoot.Add(ItemDropRule.Common(ItemType<QueenSlimeCard>(), boss));
                    break;
            }
        }
    }
}
