using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HeroRegression.Projectiles.Accessories
{
    class PoisonProj : ModProjectile
    {
        
        public bool Chase = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Poisonous Bullet");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "毒弹");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 10;
            Projectile.light = .25f;
            Projectile.damage = 9;
            Projectile.friendly = true;
            Projectile.timeLeft = 1200;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = false;
            Projectile.penetrate = 1;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (Chase)
            {
                Texture2D texture2D = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
                Vector2 drawOrigin = new Vector2(texture2D.Width * 0.5f, Projectile.height * 0.5f);
                for (int i = 0; i < Projectile.oldPos.Length; i++)
                {
                    Vector2 drawPos = Projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                    Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
                    float Sc = (float)Math.Sqrt((double)((double)(Projectile.oldPos.Length - i) / (double)Projectile.oldPos.Length));
                    Main.spriteBatch.Draw(texture2D, drawPos, null, color, Projectile.rotation, drawOrigin, Sc, SpriteEffects.None, 0f);
                }
            }
            return true;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            for (int k = 0; k < 200; k++)
            {
                if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5)
                {
                    Vector2 newMove = Main.npc[k].Center - Projectile.Center;
                    newMove = Vector2.Normalize(newMove) * 5f;
                    Chase = true;
                    Projectile.velocity = (Projectile.velocity * 10f + newMove) / 11f;
                }
            }
            if (Chase)
            {
                if (Projectile.timeLeft % 2 == 0)
                {
                    Vector2 dustPos = Projectile.Center - 11 * Vector2.Normalize(Projectile.velocity);
                    Dust dust = Dust.NewDustDirect(dustPos, 1, 1, DustID.GreenTorch);
                    dust.velocity *= 0;
                    dust.noGravity = true;
                    dust.fadeIn = .5f;
                }
            }
        }
    }
}
