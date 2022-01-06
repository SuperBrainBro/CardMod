using CardMod.Core;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CardMod.Content.Projectiles.Pets
{
    internal class FoxPet : ModProjectile
    {
        public override string Texture => base.Texture.Replace("Content", "Assets");

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 12;
            Main.projPet[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Puppy);
            Projectile.width = 70;
            Projectile.height = 40;
            Projectile.aiStyle = -1;
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            player.puppy = false;
            return true;
        }

        public override void AI()
        {
            {
                Player player = Main.player[Projectile.owner];
                CardPlayer modPlayer = player.Card();
                if (player.dead)
                {
                    modPlayer.foxPet = false;
                }
                if (modPlayer.foxPet)
                {
                    Projectile.timeLeft = 2;
                }
            }
            {
                if (!Main.player[Projectile.owner].active)
                {
                    Projectile.active = false;
                    return;
                }
                bool flag = false;
                bool flag2 = false;
                bool flag4 = false;
                bool flag5 = false;
                bool flag6 = false;
                int num96 = 85;
                int num274 = Projectile.type;
                if (num274 <= 854)
                {
                    switch (num274)
                    {
                        case 816:
                        case 821:
                        case 825:
                        case 854:
                            break;
                        default:
                            goto IL_013a;
                    }
                }
                else if (num274 <= 891)
                {
                    if ((uint)(num274 - 858) > 2u)
                    {
                        switch (num274)
                        {
                            case 881:
                            case 885:
                            case 889:
                            case 891:
                                break;
                            case 884:
                            case 890:
                                num96 = 80;
                                goto IL_013a;
                            default:
                                goto IL_013a;
                        }
                    }
                }
                else if (num274 != 897 && (uint)(num274 - 899) > 1u && num274 != 934)
                {
                    goto IL_013a;
                }
                num96 = 95;
                goto IL_013a;
            IL_4162:
                bool flag8;
                if (Projectile.ai[0] != 0f && !flag8)
                {
                    float num220 = 0.2f;
                    int num219 = 200;
                    Projectile.tileCollide = false;
                    Vector2 vector12 = new(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
                    float num218 = Main.player[Projectile.owner].position.X + (Main.player[Projectile.owner].width / 2) - vector12.X;
                    float num212 = Main.player[Projectile.owner].position.Y + (Main.player[Projectile.owner].height / 2) - vector12.Y;
                    if (Projectile.type == 127)
                    {
                        num212 = Main.player[Projectile.owner].position.Y - vector12.Y;
                    }
                    float num211 = (float)Math.Sqrt(num218 * num218 + num212 * num212);
                    float num209 = 10f;
                    if (num211 < num219 && Main.player[Projectile.owner].velocity.Y == 0f && Projectile.position.Y + Projectile.height <= Main.player[Projectile.owner].position.Y + (float)Main.player[Projectile.owner].height && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                    {
                        Projectile.ai[0] = 0f;
                        if (Projectile.velocity.Y < -6f)
                        {
                            Projectile.velocity.Y = -6f;
                        }
                    }
                    if (num211 < 60f)
                    {
                        num218 = Projectile.velocity.X;
                        num212 = Projectile.velocity.Y;
                    }
                    else
                    {
                        num211 = num209 / num211;
                        num218 *= num211;
                        num212 *= num211;
                    }
                    {
                        if (Projectile.velocity.X < num218)
                        {
                            Projectile.velocity.X += num220;
                            if (Projectile.velocity.X < 0f)
                            {
                                Projectile.velocity.X += num220 * 1.5f;
                            }
                        }
                        if (Projectile.velocity.X > num218)
                        {
                            Projectile.velocity.X -= num220;
                            if (Projectile.velocity.X > 0f)
                            {
                                Projectile.velocity.X -= num220 * 1.5f;
                            }
                        }
                        if (Projectile.velocity.Y < num212)
                        {
                            Projectile.velocity.Y += num220;
                            if (Projectile.velocity.Y < 0f)
                            {
                                Projectile.velocity.Y += num220 * 1.5f;
                            }
                        }
                        if (Projectile.velocity.Y > num212)
                        {
                            Projectile.velocity.Y -= num220;
                            if (Projectile.velocity.Y > 0f)
                            {
                                Projectile.velocity.Y -= num220 * 1.5f;
                            }
                        }
                    }
                    if (Projectile.type == Type)
                    {
                        Projectile.frameCounter++;
                        if (Projectile.frameCounter > 1)
                        {
                            Projectile.frame++;
                            Projectile.frameCounter = 0;
                        }
                        if (Projectile.frame < 7 || Projectile.frame > 10)
                        {
                            Projectile.frame = 7;
                        }
                        Projectile.rotation = Projectile.velocity.X * 0.1f;
                    }
                    else if (Projectile.spriteDirection == -1)
                    {
                        Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
                    }
                    else
                    {
                        Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 3.14f;
                    }
                }
                else
                {
                    bool flag12 = false;
                    if (Projectile.ai[1] != 0f)
                    {
                        flag2 = false;
                        flag4 = false;
                    }
                    else if (Projectile.type != 313 && !flag12)
                    {
                        Projectile.rotation = 0f;
                    }
                    float num134 = 0.08f;
                    float num133 = 6.5f;
                    if (Projectile.type == Type)
                    {
                        num133 = 8f;
                        num134 = 0.08f;
                    }
                    if (flag8)
                    {
                        num133 = 6f;
                    }
                    if (flag2)
                    {
                        if (Projectile.velocity.X > -3.5)
                        {
                            Projectile.velocity.X -= num134;
                        }
                        else
                        {
                            Projectile.velocity.X -= num134 * 0.25f;
                        }
                    }
                    else if (flag4)
                    {
                        if (Projectile.velocity.X < 3.5)
                        {
                            Projectile.velocity.X += num134;
                        }
                        else
                        {
                            Projectile.velocity.X += num134 * 0.25f;
                        }
                    }
                    else
                    {
                        Projectile.velocity.X *= 0.9f;
                        if (Projectile.velocity.X >= 0f - num134 && Projectile.velocity.X <= num134)
                        {
                            Projectile.velocity.X = 0f;
                        }
                    }
                    if (Projectile.type == 208)
                    {
                        Projectile.velocity.X *= 0.95f;
                        if (Projectile.velocity.X > -0.1 && Projectile.velocity.X < 0.1)
                        {
                            Projectile.velocity.X = 0f;
                        }
                        flag2 = false;
                        flag4 = false;
                    }
                    if (flag2 | flag4)
                    {
                        int num132 = (int)(Projectile.position.X + (Projectile.width / 2)) / 16;
                        int j3 = (int)(Projectile.position.Y + (Projectile.height / 2)) / 16;
                        if (Projectile.type == 236)
                        {
                            num132 += Projectile.direction;
                        }
                        if (flag2)
                        {
                            num132--;
                        }
                        if (flag4)
                        {
                            num132++;
                        }
                        num132 += (int)Projectile.velocity.X;
                        if (WorldGen.SolidTile(num132, j3, false))
                        {
                            flag6 = true;
                        }
                    }
                    if (Main.player[Projectile.owner].position.Y + Main.player[Projectile.owner].height - 8f > Projectile.position.Y + Projectile.height)
                    {
                        flag5 = true;
                    }
                    Collision.StepUp(ref Projectile.position, ref Projectile.velocity, Projectile.width, Projectile.height, ref Projectile.stepSpeed, ref Projectile.gfxOffY, 1, false, 0);
                    if (Projectile.velocity.Y == 0f || Projectile.type == 200)
                    {
                        if (!flag5 && (Projectile.velocity.X < 0f || Projectile.velocity.X > 0f))
                        {
                            int num130 = (int)(Projectile.position.X + (Projectile.width / 2)) / 16;
                            int j2 = (int)(Projectile.position.Y + (Projectile.height / 2)) / 16 + 1;
                            if (flag2)
                            {
                                num130--;
                            }
                            if (flag4)
                            {
                                num130++;
                            }
                            WorldGen.SolidTile(num130, j2, false);
                        }
                        if (flag6)
                        {
                            int num129 = (int)(Projectile.position.X + (Projectile.width / 2)) / 16;
                            int num128 = (int)(Projectile.position.Y + Projectile.height) / 16;
                            if (WorldGen.SolidTileAllowBottomSlope(num129, num128) || Main.tile[num129, num128].IsHalfBlock || Main.tile[num129, num128].Slope > 0 || Projectile.type == 200)
                            {
                                if (Projectile.type == 200)
                                {
                                    Projectile.velocity.Y = -3.1f;
                                }
                                else
                                {
                                    try
                                    {
                                        num129 = (int)(Projectile.position.X + (Projectile.width / 2)) / 16;
                                        num128 = (int)(Projectile.position.Y + (Projectile.height / 2)) / 16;
                                        if (flag2)
                                        {
                                            num129--;
                                        }
                                        if (flag4)
                                        {
                                            num129++;
                                        }
                                        num129 += (int)Projectile.velocity.X;
                                        if (!WorldGen.SolidTile(num129, num128 - 1, false) && !WorldGen.SolidTile(num129, num128 - 2, false))
                                        {
                                            Projectile.velocity.Y = -5.1f;
                                        }
                                        else if (!WorldGen.SolidTile(num129, num128 - 2, false))
                                        {
                                            Projectile.velocity.Y = -7.1f;
                                        }
                                        else if (WorldGen.SolidTile(num129, num128 - 5, false))
                                        {
                                            Projectile.velocity.Y = -11.1f;
                                        }
                                        else if (WorldGen.SolidTile(num129, num128 - 4, false))
                                        {
                                            Projectile.velocity.Y = -10.1f;
                                        }
                                        else
                                        {
                                            Projectile.velocity.Y = -9.1f;
                                        }
                                    }
                                    catch
                                    {
                                        Projectile.velocity.Y = -9.1f;
                                    }
                                }
                            }
                        }
                        else if (Projectile.type == 266 && (flag2 | flag4))
                        {
                            Projectile.velocity.Y -= 6f;
                        }
                    }
                    if (Projectile.velocity.X > num133)
                    {
                        Projectile.velocity.X = num133;
                    }
                    if (Projectile.velocity.X < 0f - num133)
                    {
                        Projectile.velocity.X = 0f - num133;
                    }
                    if (Projectile.velocity.X < 0f)
                    {
                        Projectile.direction = 1;
                    }
                    if (Projectile.velocity.X > 0f)
                    {
                        Projectile.direction = -1;
                    }
                    if (Projectile.velocity.X > num134 & flag4)
                    {
                        Projectile.direction = -1;
                    }
                    if (Projectile.velocity.X < 0f - num134 & flag2)
                    {
                        Projectile.direction = 1;
                    }
                    if (Projectile.type != 313)
                    {
                        if (Projectile.direction == -1)
                        {
                            Projectile.spriteDirection = 1;
                        }
                        if (Projectile.direction == 1)
                        {
                            Projectile.spriteDirection = -1;
                        }
                    }
                    bool flag10 = Projectile.position.X - Projectile.oldPosition.X == 0f;
                    if (Projectile.type == Type)
                    {
                        if (Projectile.velocity.Y == 0f)
                        {
                            if (flag10)
                            {
                                if (Projectile.frame > 0)
                                {
                                    Projectile.frameCounter += 2;
                                    if (Projectile.frameCounter > 6)
                                    {
                                        Projectile.frame++;
                                        Projectile.frameCounter = 0;
                                    }
                                    if (Projectile.frame >= 10)
                                    {
                                        Projectile.frame = 0;
                                    }
                                }
                                else
                                {
                                    Projectile.frame = 0;
                                    Projectile.frameCounter = 0;
                                }
                            }
                            else if (Projectile.velocity.X < -0.8 || Projectile.velocity.X > 0.8)
                            {
                                Projectile.frameCounter += (int)Math.Abs(Projectile.velocity.X * 0.75);
                                Projectile.frameCounter++;
                                if (Projectile.frameCounter > 6)
                                {
                                    Projectile.frame++;
                                    Projectile.frameCounter = 0;
                                }
                                if (Projectile.frame >= 10 || Projectile.frame < 1)
                                {
                                    Projectile.frame = 1;
                                }
                            }
                            else if (Projectile.frame > 0)
                            {
                                Projectile.frameCounter += 2;
                                if (Projectile.frameCounter > 6)
                                {
                                    Projectile.frame++;
                                    Projectile.frameCounter = 0;
                                }
                                if (Projectile.frame >= 10)
                                {
                                    Projectile.frame = 0;
                                }
                            }
                            else
                            {
                                Projectile.frame = 0;
                                Projectile.frameCounter = 0;
                            }
                        }
                        else if (Projectile.velocity.Y < 0f)
                        {
                            Projectile.frameCounter = 0;
                            Projectile.frame = 2;
                        }
                        else if (Projectile.velocity.Y > 0f)
                        {
                            Projectile.frameCounter = 0;
                            Projectile.frame = 4;
                        }
                        Projectile.velocity.Y += 0.4f;
                        if (Projectile.velocity.Y > 10f)
                        {
                            Projectile.velocity.Y = 10f;
                        }
                    }
                }
                return;
            IL_3e8a:
                if (Main.player[Projectile.owner].rocketDelay2 > 0)
                {
                    Projectile.ai[0] = 1f;
                }
                goto IL_4162;
            IL_013a:
                flag8 = Projectile.ai[0] is (-1f) or (-2f);
                bool num277 = Projectile.ai[0] == -1f;
                bool flag9 = Projectile.ai[0] == -2f;
                if (Projectile.type == Type)
                {
                    if (Main.player[Projectile.owner].dead)
                    {
                        Main.player[Projectile.owner].Card().foxPet = false;
                    }
                    if (Main.player[Projectile.owner].Card().foxPet)
                    {
                        Projectile.timeLeft = 2;
                    }
                }
                if (flag8)
                {
                    Projectile.timeLeft = 2;
                }
                if (Main.player[Projectile.owner].position.X + (Main.player[Projectile.owner].width / 2) < Projectile.position.X + (Projectile.width / 2) - num96)
                {
                    flag2 = true;
                }
                else if (Main.player[Projectile.owner].position.X + (Main.player[Projectile.owner].width / 2) > Projectile.position.X + (Projectile.width / 2) + num96)
                {
                    flag4 = true;
                }
                if (num277)
                {
                    flag2 = false;
                    flag4 = true;
                }
                if (flag9)
                {
                    flag2 = false;
                    flag4 = false;
                }
                {
                    if (Projectile.type != 885 && Projectile.type != 889)
                    {
                        bool flag15 = Projectile.ai[1] == 0f;
                        if (flag)
                        {
                            flag15 = true;
                        }
                        if (flag15)
                        {
                            num274 = Projectile.type;
                            if (num274 <= 860)
                            {
                                if (num274 <= 821)
                                {
                                    switch (num274)
                                    {
                                        case 816:
                                            break;
                                        default:
                                            goto IL_3e8a;
                                    }
                                }
                                else if (num274 != 825 && num274 != 854 && (uint)(num274 - 858) > 2u)
                                {
                                    goto IL_3e8a;
                                }
                            }
                            else if (num274 <= 884)
                            {
                                if (num274 != 881 && num274 != 884)
                                {
                                    goto IL_3e8a;
                                }
                            }
                            else if ((uint)(num274 - 890) > 1u)
                            {
                                switch (num274)
                                {
                                    case 897:
                                    case 900:
                                    case 934:
                                        break;
                                    default:
                                        goto IL_3e8a;
                                }
                            }
                            goto IL_3e8a;
                        }
                        goto IL_4162;
                    }
                    Player player3 = Main.player[Projectile.owner];
                    float num82 = 0.15f;
                    Projectile.tileCollide = false;
                    int num81 = 150;
                    Vector2 center = Projectile.Center;
                    float num80 = player3.Center.X - center.X;
                    float num79 = player3.Center.Y - center.Y;
                    num79 -= 65f;
                    num80 -= 30 * player3.direction;
                    float num76 = (float)Math.Sqrt(num80 * num80 + num79 * num79);
                    float num75 = 8f;
                    float num74 = num76;
                    float num73 = 2000f;
                    bool num281 = num76 > num73;
                    if (num76 < num81 && player3.velocity.Y == 0f && Projectile.position.Y + Projectile.height <= player3.position.Y + player3.height && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height) && Projectile.velocity.Y < -6f)
                    {
                        Projectile.velocity.Y = -6f;
                    }
                    if (num76 < 10f)
                    {
                        Projectile.velocity *= 0.9f;
                        if (Projectile.velocity.Length() < 0.5f)
                        {
                            Projectile.velocity = Vector2.Zero;
                        }
                        num82 = 0f;
                    }
                    else
                    {
                        if (num76 > num81)
                        {
                            num82 = 0.2f;
                            num75 = 12f;
                        }
                        num76 = num75 / num76;
                        num80 *= num76;
                        num79 *= num76;
                    }
                    if (num281)
                    {
                        int num71 = 234;
                        for (int j = 0; j < 12; j++)
                        {
                            float speedX4 = 1f - Main.rand.NextFloat() * 2f;
                            float speedY4 = 1f - Main.rand.NextFloat() * 2f;
                            int num70 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, num71, speedX4, speedY4, 0, default(Color), 1f);
                            Main.dust[num70].noLightEmittence = true;
                            Main.dust[num70].noGravity = true;
                        }
                        Projectile.Center = player3.Center;
                        Projectile.velocity = Vector2.Zero;
                        if (Main.myPlayer == Projectile.owner)
                        {
                            Projectile.netUpdate = true;
                        }
                    }
                    if (Projectile.velocity.X < num80)
                    {
                        Projectile.velocity.X += num82;
                        if (Projectile.velocity.X < 0f)
                        {
                            Projectile.velocity.X += num82;
                        }
                    }
                    if (Projectile.velocity.X > num80)
                    {
                        Projectile.velocity.X -= num82;
                        if (Projectile.velocity.X > 0f)
                        {
                            Projectile.velocity.X -= num82;
                        }
                    }
                    if (Projectile.velocity.Y < num79)
                    {
                        Projectile.velocity.Y += num82;
                        if (Projectile.velocity.Y < 0f)
                        {
                            Projectile.velocity.Y += num82;
                        }
                    }
                    if (Projectile.velocity.Y > num79)
                    {
                        Projectile.velocity.Y -= num82;
                        if (Projectile.velocity.Y > 0f)
                        {
                            Projectile.velocity.Y -= num82;
                        }
                    }
                    Projectile.direction = -player3.direction;
                    Projectile.spriteDirection = -Projectile.direction;
                    if (num74 >= num81)
                    {
                        Projectile.rotation += 0.5f;
                        if (Projectile.rotation > 6.28318548f)
                        {
                            Projectile.rotation -= 6.28318548f;
                        }
                        Projectile.frame = 6;
                        Projectile.frameCounter = 0;
                    }
                    else
                    {
                        Projectile.rotation *= 0.95f;
                        if (Projectile.rotation < 0.05f)
                        {
                            Projectile.rotation = 0f;
                        }
                        Projectile.frameCounter++;
                        if (Projectile.frameCounter % 5 == 0)
                        {
                            Projectile.frame++;
                            if (Projectile.frame > 5)
                            {
                                Projectile.frame = 0;
                            }
                        }
                        if (Projectile.frameCounter >= 40)
                        {
                            Projectile.frameCounter = 0;
                        }
                    }
                }
                return;
            }
        }
    }
}
