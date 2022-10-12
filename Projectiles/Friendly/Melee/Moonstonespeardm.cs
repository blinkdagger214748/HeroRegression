using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System;
using HeroRegression.Items;
using Terraria.Audio;

namespace HeroRegression.Projectiles.Friendly.Melee
{
    public class Moonstonespeardm : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("月长石投矛");
            Projectile.timeLeft = 75;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 11;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            Main.projFrames[Projectile.type] = 1;
        }

        public override void SetDefaults()
        {

            Projectile.DamageType = DamageClass.Melee;
            Projectile.width = 24;
            Projectile.height = 40;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 150;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 4;
            Projectile.aiStyle = ProjectileID.LaserMachinegunLaser;
            const double V = 0.7;
            Projectile.scale = (float)V;
        }

        public override void AI()
        {

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.Pi * 5 / 6;
            Projectile.velocity.Y += 0.03f;

        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.YellowTorch, Projectile.oldVelocity.X * 0.2f, Projectile.oldVelocity.Y * 0.2f);
            }
            SoundEngine.PlaySound(SoundID.Dig, Main.LocalPlayer.position);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, new Vector2(13, 20), 1, SpriteEffects.None, 0);
            return false;
        }
    }
}
