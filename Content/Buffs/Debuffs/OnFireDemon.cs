using CardMod.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CardMod.Content.Buffs.Debuffs
{
    public class OnFireDemon : ModBuff
    {
        public override string Texture => "Terraria/Images/Buff_24";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Lang.GetBuffName(BuffID.OnFire));
            Description.SetDefault(Lang.GetBuffDescription(BuffID.OnFire));
            Main.pvpBuff[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) => player.Card().onFireDemon = true;

        public override void Update(NPC npc, ref int buffIndex) => npc.Card().onFireDemon = true;
    }

    public class OnFireDevil : ModBuff
    {
        public override string Texture => "Terraria/Images/Buff_24";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Lang.GetBuffName(BuffID.OnFire));
            Description.SetDefault(Lang.GetBuffDescription(BuffID.OnFire));
            Main.pvpBuff[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) => player.Card().onFireDevil = true;

        public override void Update(NPC npc, ref int buffIndex) => npc.Card().onFireDevil = true;
    }
}
