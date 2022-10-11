

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HeroRegression.Projectiles{
    public class LC:ModProjectile
    {
        public int phase = 0;
        public int timer = 0;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("生灵飞廉");
        


        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 64;
            Projectile.height = 64;
            Projectile.timeLeft = 9999;//弹幕存在时间
            Projectile.tileCollide = true;//是否不穿墙
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.light = 1f;
            Projectile.penetrate = -1;

           // Projectile.aiStyle = ProjectileID.EnchantedBoomerang;



        }

        public override void AI()
        {
            
            Dust dust = Dust.NewDustDirect(Projectile.Center,1,1, DustID.Phantasmal, 0, 0, 0, Color.White,1f);
           
            dust.velocity = new Vector2(Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2));
            dust.noGravity = true;
            dust.fadeIn = 0.4f;
            base.AI();
            Projectile.rotation += 0.4f;
            if(phase == 0)
            { timer++;if (timer >= 14&& timer < 24) { Projectile.velocity *= 0.62f; }
                if (timer >= 24) { phase = 1; timer = 0; }
            }
            if(phase == 1) { Projectile.tileCollide = false; Vector2 top = Main.LocalPlayer.Center - Projectile.Center;top /= top.Length();top *= 32f;Projectile.velocity = top; if ((Projectile.Center - Main.LocalPlayer.Center).Length() < 20) { Projectile.active = false; } }
           
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            phase = 1; 
            base.OnHitNPC(target, damage, knockback, crit);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            phase = 1;
            return false;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 22;
            height = 22;
            return true;
        }
    }
}