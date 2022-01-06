using CardMod.Content.Projectiles.Pets;
using CardMod.Core;
using Terraria;
using Terraria.ModLoader;

namespace CardMod.Content.Buffs.Pets
{
    internal class FoxPetBuff : ModBuff
    {
        public override string Texture => base.Texture.Replace("Content", "Assets");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Attractive Scent");
            Description.SetDefault("\"A fox is following you!\"");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            player.Card().foxPet = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<FoxPet>()] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.GetProjectileSource_Buff(buffIndex), player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, ModContent.ProjectileType<FoxPet>(), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}
