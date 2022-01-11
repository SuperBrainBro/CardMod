using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CardMod.Content.Buffs.Debuffs
{
    public class GraniteSlow : ModBuff
    {
        public override string Texture => "Terraria/Images/Buff_32";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Lang.GetBuffName(BuffID.Slow));
            Description.SetDefault(Lang.GetBuffDescription(BuffID.Slow));
            Main.pvpBuff[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex) => player.slow = true;
    }
}
