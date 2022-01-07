using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CardMod.Content.Projectiles.Friendly
{
    public class ShadowOrbs : ModProjectile
	{
		public int timer;
		public float fadeOut = 0.75f;
		public Vector2 rotVec = new(0f, 65f);
		public float rot;

        public override string Texture => "CardMod/Assets/Projectiles/Friendly/ShadowOrbs";

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shadow Orb");
			Main.projFrames[Type] = 2;
		}

		public override void SetDefaults()
		{
			Projectile.width = 32;
			Projectile.height = 34;
			Projectile.aiStyle = -1;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 3600;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.player[Projectile.owner];
			hitDirection = (!(target.Center.X < player.Center.X)) ? 1 : (-1);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			for (int i = 0; i < 15; i++)
			{
				Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width * 2, Projectile.height * 2, DustID.Demonite, 0f, 0f, 100, default, 1.2f);
				int num2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width * 2, Projectile.height * 2, DustID.Demonite, 0f, 0f, 100, default, 1.2f);
				Main.dust[num2].noGravity = true;
			}
			Projectile.ai[1] = 1f;
		}

		public static Vector2 RotateVector(Vector2 origin, Vector2 vecToRot, float rot)
		{
			float num = (float)(Math.Cos(rot) * (vecToRot.X - origin.X) - Math.Sin(rot) * (vecToRot.Y - origin.Y) + origin.X);
			float num2 = (float)(Math.Sin(rot) * (vecToRot.X - origin.X) + Math.Cos(rot) * (vecToRot.Y - origin.Y) + origin.Y);
			return new Vector2(num, num2);
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			Lighting.AddLight(Projectile.Center, Color.MediumPurple.ToVector3());

			float num = 1f;
			bool flag = false;
			if (!flag)
            {
				num += 0.0275f;
				if (num >= 2.75f)
					flag = true;
            }
			else
			{
				num -= 0.0275f;
				if (num < 0.75f)
					flag = false;
			}

			rot += 0.05f;
			Projectile.Center = player.Center + RotateVector(default, new Vector2(0f, 175 * MathF.Sqrt(num)), rot + Projectile.ai[0] * 1.54666674f);
			Projectile.velocity.X = (Projectile.position.X > player.position.X) ? 1f : (-1f);
			if (Projectile.ai[1] > 0f)
			{
				fadeOut = 0.15f;
				Projectile.friendly = false;
				timer++;
				if (timer > 60)
				{
					for (int i = 0; i < 15; i++)
					{
						Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height * 2, DustID.Demonite, 0f, 0f, 100, default, 1.2f);
						int num2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height * 2, DustID.Demonite, 0f, 0f, 100, default, 1.2f);
						Main.dust[num2].noGravity = true;
						Dust obj = Main.dust[num2];
						obj.velocity *= 0.75f;
						int num3 = Main.rand.Next(-50, 51);
						int num4 = Main.rand.Next(-50, 51);
						Dust dust = Main.dust[num2];
						dust.position.X += num3;
						Dust dust2 = Main.dust[num2];
						dust2.position.Y += num4;
						Main.dust[num2].velocity.X = (0f - num3) * 0.075f;
						Main.dust[num2].velocity.Y = (0f - num4) * 0.075f;
					}
					Projectile.ai[1] = 0f;
					timer = 0;
				}
			}
			else
			{
				fadeOut = 0.75f;
				Projectile.friendly = true;
			}

			if (++Projectile.frameCounter > Main.tileFrameCounter[TileID.ShadowOrbs])
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 2)
					Projectile.frame = 0;
			}
		}
	}
}
