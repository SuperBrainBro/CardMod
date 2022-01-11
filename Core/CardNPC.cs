using CardMod.Content.Items.Cards.Boss;
using CardMod.Content.Items.Cards.Hardmode;
using CardMod.Content.Items.Cards.PreHardmode;
using CardMod.Content.Items.Pets;
using System;
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
        public bool onFireDemon;
        public bool onFireDevil;

        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances => true;
        public override GlobalNPC Clone()
        {
            CardGlobalNPC clone = (CardGlobalNPC)base.Clone();
            clone.jellyBlueImmune = jellyBlueImmune;
            clone.jellyGreenImmune = jellyGreenImmune;
            clone.jellyPinkImmune = jellyPinkImmune;
            clone.onFireDemon = onFireDemon;
            clone.onFireDevil = onFireDevil;
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
            if (onFireDevil || onFireDemon)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen = 0;
                int value = 0;
                if (onFireDemon)
                    value += 50;
                if (onFireDevil)
                    value += 250;
                if (damage < value)
                {
                    damage = value;
                }
            }
        }

        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            if (!npc.buffImmune[BuffID.Slow] && target.Card()._cardSlime)
                npc.AddBuff(BuffID.Slow, target.Card()._cardSlimeGreen ? 100 : 160);
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            Player player = Main.LocalPlayer;
            foreach (Item item in shop.item)
            {
                float mult = 1f;
                if (player.Card()._cardDiscount)
                    mult -= 0.1f;
                if (player.Card()._cardBird)
                    mult += 0.1f;

                item.value = (int)Math.Round((decimal)(item.value * mult * 100)) / 100;
            }
        }

        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (player.Card()._cardSnowFlinx && player.ZoneSnow)
                spawnRate /= 2;
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            const int normie = 200;
            const int boss = 100;

            if (npc.SpawnedFromStatue)
                return;

            switch (npc.type)
            {
                case NPCID.GiantFlyingFox:
                    npcLoot.Add(ItemDropRule.ByCondition(new MCondition.IsExperimental(), ItemType<FoxCookie>(), 100));
                    break;

                case NPCID.EaterofWorldsHead:
                    npcLoot.Add(ItemDropRule.ByCondition(new MCondition.EOWPieceLast(0), ItemType<EaterOfWorldsCard>(), boss));
                    break;
                case NPCID.EaterofWorldsBody:
                    npcLoot.Add(ItemDropRule.ByCondition(new MCondition.EOWPieceLast(1), ItemType<EaterOfWorldsCard>(), boss));
                    break;
                case NPCID.EaterofWorldsTail:
                    npcLoot.Add(ItemDropRule.ByCondition(new MCondition.EOWPieceLast(2), ItemType<EaterOfWorldsCard>(), boss));
                    break;
                default:
                    break;
            }

            int card = npc.type switch
            {
                NPCID.Bunny or NPCID.BunnySlimed or NPCID.BunnyXmas or NPCID.PartyBunny => ItemType<BunnyCard>(),
                NPCID.Squirrel or NPCID.SquirrelRed => ItemType<SquirrelCard>(),
                NPCID.Bird or NPCID.BirdBlue or NPCID.BirdRed => ItemType<BirdCard>(),

                NPCID.GreenSlime => ItemType<GreenSlimeCard>(),
                NPCID.BlueSlime => ItemType<BlueSlimeCard>(),
                NPCID.UmbrellaSlime => ItemType<UmbrellaSlimeCard>(),
                NPCID.BlueJellyfish => ItemType<BlueJellyfishCard>(),
                NPCID.PinkJellyfish => ItemType<PinkJellyfishCard>(),
                NPCID.Nymph => ItemType<NymphCard>(),
                NPCID.GoldenSlime => ItemType<GoldenSlimeCard>(),
                NPCID.FireImp => ItemType<ImpCard>(),
                NPCID.Demon => ItemType<DemonCard>(),
                NPCID.SnowFlinx => ItemType<SnowFlinxCard>(),
                NPCID.GiantWalkingAntlion => ItemType<GiantAntlionChargerCard>(),
                NPCID.Harpy => ItemType<HarpyCard>(),
                NPCID.AnomuraFungus => ItemType<AnomuraFungusCard>(),
                NPCID.GraniteGolem => ItemType<GraniteGolemCard>(),

                NPCID.GreenJellyfish => ItemType<GreenJellyfishCard>(),
                NPCID.PirateShip => ItemType<ShopDiscountCard>(),
                NPCID.RedDevil => ItemType<RedDevilCard>(),
                NPCID.PirateCorsair or NPCID.PirateCrossbower or NPCID.PirateDeadeye or NPCID.PirateDeckhand => ItemType<ShopDiscountCard>(),
                NPCID.PirateCaptain => ItemType<ShopDiscountCard>(),

                NPCID.BloodNautilus => ItemType<DreadnautilusCard>(),

                NPCID.WallofFlesh => ItemType<WallOfFleshCard>(),
                NPCID.QueenSlimeBoss => ItemType<QueenSlimeCard>(),

                _ => 0,
            };

            int chance = !npc.boss ? npc.type switch
            {
                NPCID.PirateCorsair or NPCID.PirateCrossbower or NPCID.PirateDeadeye or NPCID.PirateDeckhand => (int)(normie * 12.5),
                NPCID.PirateCaptain => normie * 5,

                _ => normie,
            } : boss;

            if (card != 0)
                npcLoot.Add(ItemDropRule.Common(card, chance));
        }

        public static class MCondition
        {
            public class IsExperimental : IItemDropRuleCondition, IProvideItemConditionDescription
            {
                public bool CanDrop(DropAttemptInfo info)
                {
                    if (info.IsInSimulation)
                        return false;
                    return CardMod.Experimental;
                }

                public bool CanShowItemDropInUI() => CardMod.Experimental;

                public string GetConditionDescription() => null;
            }
            public class EOWPieceLast : IItemDropRuleCondition, IProvideItemConditionDescription
            {
                public int[] pieces = new int[3];

                public EOWPieceLast(int piece)
                {
                    if (piece <= 0)
                        pieces = new int[3] { 1, 0, 0 };
                    else if (piece == 1)
                        pieces = new int[3] { 0, 1, 0 };
                    else
                        pieces = new int[3] { 0, 0, 1 };
                }

                public EOWPieceLast(EOWPiece piece)
                {
                    if (piece <= EOWPiece.Head)
                        pieces = new int[3] { 1, 0, 0 };
                    else if (piece == EOWPiece.Body)
                        pieces = new int[3] { 0, 1, 0 };
                    else
                        pieces = new int[3] { 0, 0, 1 };
                }

                public bool CanDrop(DropAttemptInfo info)
                {
                    bool head = NPC.CountNPCS(NPCID.EaterofWorldsHead) == pieces[0];
                    bool body = NPC.CountNPCS(NPCID.EaterofWorldsBody) == pieces[1];
                    bool tail = NPC.CountNPCS(NPCID.EaterofWorldsTail) == pieces[2];
                    return head && body && tail;
                }

                public bool CanShowItemDropInUI() => true;

                public string GetConditionDescription() => null;
            }
        }
    }

    public enum EOWPiece
    {
        Head,
        Body,
        Tail
    }
}
