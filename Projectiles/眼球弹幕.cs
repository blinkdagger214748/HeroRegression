using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HeroRegression.Dusts;
using System;
using Terraria.Audio;

namespace HeroRegression.Projectiles
{

    public class 眼球弹幕 : ModProjectile
    {
        private int Dusttime;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("眼球");

        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.light = .5f;
            Projectile.hostile = false;
            Projectile.timeLeft = 600;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
        }


        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

            Player player = Main.player[Projectile.owner];
            if (Main.rand.NextBool(5))
            {

                player.statLife += 1;
                player.HealEffect(1);


            }
        }
        public override void AI()
        {
       
            Player player = Main.player[Projectile.owner];
            if (Projectile.timeLeft < 598)
            {
                for (int i = 0; i < 3; i++)
                {
                    Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Blood, 0, 0, 100, Color.Red, 1.2f);
                    d.position = Projectile.Center - Projectile.velocity * i / 3f;
                    d.velocity *= 0.2f;
                    d.noGravity = true;
                }
            }
            Dust dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.BloodWater, 0, 0, 0, Color.White, 1f);

            dust.velocity = new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2));
            dust.noGravity = true;
            dust.fadeIn = 0.4f;
            base.AI();
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.Pi / 4;


        }

        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Blood, Projectile.oldVelocity.X * 0.2f, Projectile.oldVelocity.Y * 0.2f);
            }
            SoundEngine.PlaySound(SoundID.Dig, Main.LocalPlayer.position);
        }
    }
}







