

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace HeroRegression.Projectiles{
    public class Qljq : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("嵌灵剑气");



        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.timeLeft = 500;//弹幕存在时间
            Projectile.tileCollide = true;//是否不穿墙
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.light = 1.2f;
            Projectile.penetrate = 1;





        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.timeLeft < 598)
            {
                for (int i = 0; i < 3; i++)
                {
                    Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.SpectreStaff, 0, 0, 100, Color.GreenYellow, 1.2f);
                    d.position = Projectile.Center - Projectile.velocity * i / 3f;
                    d.velocity *= 0.2f;
                    d.noGravity = true;
                }
            }
                Dust dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Phantasmal, 0, 0, 0, Color.White, 1f);

                dust.velocity = new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2));
                dust.noGravity = true;
                dust.fadeIn = 0.4f;
                base.AI();
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.Pi / 4;


            }


        }






    }



