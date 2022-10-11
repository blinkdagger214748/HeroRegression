using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HeroRegression.Projectiles.Boss.SeedsOfOrigin
{
    class OriginLeaf : ModProjectile
    {
        public Texture2D GetTex(string path)
        {
            return ModContent.Request<Texture2D>(path).Value;
        }
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.light = .5f;
            Projectile.hostile = true;
            Projectile.damage = 10;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.Pi;
            Player player = Main.LocalPlayer;
            if(Projectile.timeLeft == 550)
            {
                Projectile.velocity = Vector2.Normalize(player.Center - Projectile.Center) * 10;
                
               
            }

        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D SO =GetTex("HeroRegression/Projectiles/Boss/SeedsOfOrigin/OriginLeaf");
    
            for (int i = 0; i < Projectile.oldPos.Length; i += 1)//trail
            {
                Vector2 drawPos = Projectile.oldPos[i] - Main.screenPosition + new Vector2(4) + new Vector2(0, Projectile.gfxOffY);
                Color color = Color.White * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
                float Sc = 1f;
                Main.EntitySpriteDraw(SO, drawPos, null, color, Projectile.rotation, new Vector2(11, 11), Sc, SpriteEffects.None, 0);
            }
            return false;
        }
    }
}
