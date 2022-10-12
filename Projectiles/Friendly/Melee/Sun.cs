using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HeroRegression.Projectiles.Friendly.Melee
{
    public class Sun : ModProjectile
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
            Projectile.penetrate = -1;




        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float point = 0f;
            // Vector2 endPoint = Main.NPC[(int)Projectile.ai[1]].Center;
            Vector2 endpoint = Projectile.Center + Projectile.rotation.ToRotationVector2() * 1000;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, endpoint, 4f, ref point);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = ModContent.Request<Texture2D>("HeroRegression/Projectiles/Sun").Value;
            Texture2D k = ModContent.Request<Texture2D>("HeroRegression/Textures/LargeLaserTex").Value;
            Player player = Main.player[Projectile.owner];
            SpriteEffects effects = player.direction > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Main.spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, null,
                 Color.Yellow, Projectile.rotation, new Vector2(0, 26), new Vector2(20, 1), SpriteEffects.None, 0f);

            return false;
        }
        int time = 0;
        public override void AI()
        {

            time++;
            Player player = Main.player[Projectile.owner]; Projectile.Center = player.Center;
            //  Vector2 pa = Projectile.rotation.ToRotationVector2();
            // Vector2 pma = Main.MouseWorld - player.Center;
            //pma.Normalize();
            //  pa = Vector2.Lerp(pa, pma, 0.06f);
            //  Projectile.rotation = pa.ToRotation();
            Projectile.rotation = (Main.MouseWorld - player.Center).ToRotation();
            if (player.dead) { Projectile.Kill(); }
            if (player.channel) { Projectile.timeLeft = 2; }
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            if (player.statMana > 0 && time % 20 == 0)
            {
                player.statMana -= 5;
            }
            if (player.statMana <= 0)
            {
                Projectile.active = false;
                player.statMana = 0;
            }
        }


    }




}


