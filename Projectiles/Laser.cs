using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HeroRegression.Projectiles
{
    class Laser : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 8;
            Projectile.light = .5f;
            Projectile.hostile = true;
            Projectile.damage = 10;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (Projectile.ai[0] == 1)
            {
                if (Projectile.timeLeft >= 540)
                {
                    Projectile.velocity *= 1.05f;
                }
            }
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Vector2 unit = Vector2.Normalize(Projectile.velocity);
            float point = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center - unit * 39, Projectile.Center + unit * 39, 7, ref point))
            {
                return true;
            }
            return false;
        }
    }
}
