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

namespace HeroRegression.Projectiles.Friendly.Ranged
{
    class OriginNailFriend : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Emerald Arrow");
            DisplayName.AddTranslation(7,"绿晶箭");
        }
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.arrow = true;
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.light = .25f;
            Projectile.timeLeft = 600;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = false;
            Projectile.penetrate = 1;
            Projectile.aiStyle = ProjAIStyleID.Arrow;

        }
        public override bool PreAI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (Main.rand.NextFloat() <= .66f)
            {
                Dust d = Dust.NewDustDirect(Projectile.position, 10, 10, DustID.GemEmerald);
                d.noGravity = true;
            }
            else
            {
                Dust d = Dust.NewDustDirect(Projectile.position, 10, 10, DustID.GemEmerald);
                d.scale = .33f;
            }
            return true;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D SO = TextureAssets.Projectile[Projectile.type].Value;
            Main.spriteBatch.Draw(SO, Projectile.Center - Main.screenPosition, null, Color.Lerp(lightColor, Color.White, .33f), Projectile.rotation, new Vector2(7, 8), 1, SpriteEffects.None, 0f);


            return false;
        }
    }
}
