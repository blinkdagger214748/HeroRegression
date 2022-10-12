using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.Enums;
using HeroRegression.Dusts;
using HeroRegression.HRUtils;

namespace HeroRegression.Projectiles.Friendly.Melee
{
    class WorldSwordproj3 : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.light = .5f;
            Projectile.hostile = false;
            Projectile.timeLeft = 30;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
        }



        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;


            for (int i = 0; i < 2; i++)
            {
                float A4 = Projectile.velocity.X / 2f * i;
                float A5 = Projectile.velocity.Y / 2f * i;
                int A6 = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Phantasmal, Projectile.oldVelocity.X * 0.2f, Projectile.oldVelocity.Y * 0.2f);
                Main.dust[A6].position.X = Projectile.Center.X - A4;
                Main.dust[A6].position.Y = Projectile.Center.Y - A5;
                Main.dust[A6].noGravity = true;
                Main.dust[A6].velocity *= 0f;
                Main.dust[A6].scale *= 1f;
            }

        }
    }
}
