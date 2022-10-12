using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HeroRegression.Projectiles.Friendly.Ranged
{
    public class 激流箭 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("激流箭");



        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 11;
            Projectile.height = 11;
            Projectile.timeLeft = 500;//弹幕存在时间
            Projectile.tileCollide = true;//是否不穿墙
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.light = 0.8f;
            Projectile.penetrate = 1;
            Projectile.extraUpdates = 4;




        }

        public override void AI()
        {

            Dust dust = Dust.NewDustDirect(Projectile.position, 11, 11, DustID.IceTorch, 0, 0, 0, Color.White, 1f);

            dust.velocity = new Vector2(0, 0);
            dust.noGravity = true;
            dust.fadeIn = 0.4f;


            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.Pi / 2;
            Projectile.velocity.Y += 0.03f;

        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, new Vector2(13, 21), 1, SpriteEffects.None, 0);
            return false;
        }

    }







}


