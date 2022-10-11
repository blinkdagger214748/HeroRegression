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
using Microsoft.Xna.Framework.Graphics;

namespace HeroRegression.Projectiles.Boss.FlameReaction
{
    public class StarExplode : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Star Explode");
        }
        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 64;
            Projectile.hostile = true;
            Projectile.damage = 10;
            Projectile.timeLeft = 2;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }
        public override void AI()
        {
            for (int i = 0; i <= 80; i++)
            {
                Vector2 dustPos = Main.rand.NextFloat(0, 6.28f).ToRotationVector2() * Main.rand.NextFloat(.1f, 63.9f);
                Dust dust = Dust.NewDustDirect(Projectile.Center + dustPos, 1, 1, DustID.WhiteTorch);
                dust.noGravity = true;
                dust.fadeIn = .25f;
                dust.velocity = Vector2.Normalize(dustPos) * (float)Math.Log(65f - dustPos.Length(),2);
                dust.color = Main.DiscoColor;
                dust.scale = 2f;
            }
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return Vector2.Distance(Main.LocalPlayer.Center,Projectile.Center)<= 72f;
        }
    }
}
