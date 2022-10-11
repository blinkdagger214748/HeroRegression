using Terraria;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using HeroRegression.Projectiles;
using System;
using System.IO;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace HeroRegression.Projectiles.Boss.FlameReaction
{
    public class FlameReactionStar : ModProjectile
    {
        float CollidingY;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fallen Star");
        }
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 24;
            Projectile.hostile = true;
            Projectile.damage = 10;
            Projectile.timeLeft = 330;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = GetTex("HeroRegression/Textures/StarDrawBack");
            Vector2 ori = new Vector2(17, 21);
            Vector2 pos = Projectile.Center - Main.screenPosition;
            Main.EntitySpriteDraw(tex, pos, null, Color.White * .85f, Projectile.velocity.ToRotation() + MathHelper.ToRadians(90), ori, 1f, SpriteEffects.None, 0);
            return true;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, new Vector3(.11f, .15f, .2f));
            Projectile.rotation += MathHelper.ToRadians(9);
            if (Projectile.velocity.Y <= 12f)
            {
                Projectile.velocity.Y += .2f;
            }
            if (Projectile.timeLeft == 329)
            {
                CollidingY = Main.LocalPlayer.Center.Y + 240;
            }
            if (Projectile.timeLeft < 329)
            {
                if (Projectile.Center.Y >= CollidingY)
                {
                    Projectile.Kill();
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(null,Projectile.Center, Vector2.Zero, ModContent.ProjectileType<StarExplode>(), Projectile.damage, .1f, Main.myPlayer);
            SoundEngine.PlaySound(SoundID.Item89, Projectile.Center);
        }
    }
}
