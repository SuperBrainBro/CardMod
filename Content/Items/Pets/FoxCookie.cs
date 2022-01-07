using CardMod.Content.Buffs.Pets;
using CardMod.Content.Projectiles.Pets;
using CardMod.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CardMod.Content.Items.Pets
{
    internal class FoxCookie : ModItem
	{
        public override string Texture => base.Texture.Replace("Content", "Assets");

        public override bool IsLoadingEnabled(Mod mod) => CardMod.Experimental;

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fox Cookie");
			Tooltip.SetDefault("Summons an adorable fox!" +
				"\n'Yummy yum!'");
		}

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.DogWhistle);
			Item.shoot = ModContent.ProjectileType<FoxPet>();
			Item.buffType = ModContent.BuffType<FoxPetBuff>();
		}

        public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 3600, true);
			}
		}
	}
}
