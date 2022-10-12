using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;

namespace HeroRegression.Projectiles.Friendly.Melee
{
    class AbsoluteZeroProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Absolute Zero");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "绝对零度");
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.aiStyle = -1;
            Projectile.light = 0.4f;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 540;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Projectile.owner == Main.myPlayer)
            {
                if (damage > 0)
                {
                    if (Projectile.ai[0] == 0f)
                    {
                        Projectile.velocity.X = 0f - Projectile.velocity.X;
                        Projectile.velocity.Y = 0f - Projectile.velocity.Y;
                        Projectile.netUpdate = true;
                    }
                    Projectile.ai[0] = 1f;
                }
            }
            target.AddBuff(BuffID.Frostburn, 5 * 60);
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (Projectile.owner == Main.myPlayer)
            {
                if (damage > 0)
                {
                    if (Projectile.ai[0] == 0f)
                    {
                        Projectile.velocity.X = 0f - Projectile.velocity.X;
                        Projectile.velocity.Y = 0f - Projectile.velocity.Y;
                        Projectile.netUpdate = true;
                    }
                    Projectile.ai[0] = 1f;
                }
            }
            target.AddBuff(BuffID.Frostburn, 5 * 60);
        }

        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            if (Projectile.owner == Main.myPlayer)
            {
                if (damage > 0)
                {
                    if (Projectile.ai[0] == 0f)
                    {
                        Projectile.velocity.X = 0f - Projectile.velocity.X;
                        Projectile.velocity.Y = 0f - Projectile.velocity.Y;
                        Projectile.netUpdate = true;
                    }
                    Projectile.ai[0] = 1f;
                }
            }
            target.AddBuff(BuffID.Frostburn, 5 * 60);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.owner == Main.myPlayer)
            {
                if (Projectile.damage > 0)
                {
                    if (Projectile.ai[0] == 0f)
                    {
                        Projectile.velocity.X = 0f - Projectile.velocity.X;
                        Projectile.velocity.Y = 0f - Projectile.velocity.Y;
                        Projectile.netUpdate = true;
                    }
                    Projectile.ai[0] = 1f;
                }
            }
            return false;
        }

        public override void AI()
        {
            for (int i = 0; i < 5; i++)
            {
                int dust = Dust.NewDust(new Vector2(Projectile.Center.X + Main.rand.Next(-10, 10), Projectile.Center.Y + Main.rand.Next(-10, 10)), 16, 16, 185);
                Main.dust[dust].noGravity = true;
            }
            if (Projectile.soundDelay == 0)
            {
                Projectile.soundDelay = 3;
                SoundEngine.PlaySound(SoundID.Item7, Main.LocalPlayer.position);
            }
            Player owner = Main.player[Projectile.owner];
            Projectile.rotation += 0.3f * Projectile.direction;
            if (Projectile.ai[0] == 0f)
            {
                Projectile.ai[1] += 1f;
                if (Projectile.ai[1] >= 45f)
                {
                    Projectile.ai[0] = 1f;
                    Projectile.ai[1] = 0f;
                    Projectile.netUpdate = true;
                }
            }
            else
            {
                Projectile.tileCollide = false;
                float BackSpeedX = 12f;
                float BackSpeedY = 0.4f;
                Vector2 projPosition = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
                float ToOwnerX = owner.position.X + owner.width / 2 - projPosition.X;
                float ToOwnerY = owner.position.Y + owner.height / 2 - projPosition.Y;
                float ToOwner = (float)Math.Sqrt(ToOwnerX * ToOwnerX + ToOwnerY * ToOwnerY);
                if (ToOwner > 3000f)
                {
                    Projectile.Kill();
                }
                ToOwner = BackSpeedX / ToOwner;
                ToOwnerX *= ToOwner;
                ToOwnerY *= ToOwner;
                if (Projectile.velocity.X < ToOwnerX)
                {
                    Projectile.velocity.X = Projectile.velocity.X + BackSpeedY;
                    if (Projectile.velocity.X < 0f && ToOwnerX > 0f)
                    {
                        Projectile.velocity.X = Projectile.velocity.X + BackSpeedY;
                    }
                }
                else if (Projectile.velocity.X > ToOwnerX)
                {
                    Projectile.velocity.X = Projectile.velocity.X - BackSpeedY;
                    if (Projectile.velocity.X > 0f && ToOwnerX < 0f)
                    {
                        Projectile.velocity.X = Projectile.velocity.X - BackSpeedY;
                    }
                }
                if (Projectile.velocity.Y < ToOwnerY)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y + BackSpeedY;
                    if (Projectile.velocity.Y < 0f && ToOwnerY > 0f)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y + BackSpeedY;
                    }
                }
                else if (Projectile.velocity.Y > ToOwnerY)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y - BackSpeedY;
                    if (Projectile.velocity.Y > 0f && ToOwnerY < 0f)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y - BackSpeedY;
                    }
                }
                if (Main.myPlayer == Projectile.owner)
                {
                    Rectangle myHitBox = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
                    Rectangle ownerHitBox = new Rectangle((int)owner.position.X, (int)owner.position.Y, owner.width, owner.height);
                    if (myHitBox.Intersects(ownerHitBox))
                    {
                        Projectile.Kill();
                    }
                }
            }
        }

    }
}