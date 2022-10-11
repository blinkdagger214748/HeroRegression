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

namespace HeroRegression.Projectiles
{

    class OriginNailFriend2 : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.FrostArrow;
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.light = .5f;
            Projectile.hostile = false;
            Projectile.timeLeft = 600;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Dust D = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.GemEmerald);
           
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D SO = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;


            Main.spriteBatch.Draw(SO, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, new Vector2(10, 5), 1, SpriteEffects.None, 0f);


            return false;
        }
    }
}
