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

namespace HeroRegression.Projectiles.Boss.SeedsOfOrigin
{
    class OriginNail : ModProjectile
    {
        public Texture2D GetTex(string path)
        {
            return ModContent.Request<Texture2D>(path).Value;
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
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Dust D = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.RuneWizard);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D SO = GetTex("HeroRegression/Projectiles/Boss/SeedsOfOrigin/OriginNail");


            Main.EntitySpriteDraw(SO, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, new Vector2(10, 5), 1, SpriteEffects.None, 0);


            return false;
        }
    }
}
