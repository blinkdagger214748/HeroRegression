using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace HeroRegression.Projectiles.Boss.SeedsOfOrigin
{
    class OriginKnife : ModProjectile
    {
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
            Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.PiOver2;
            if(Projectile.timeLeft < 570)
            {
                Projectile.velocity *= 1.08f;
            }
            if(Projectile.timeLeft % 3 == 0)
            {
                Dust D = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.RuneWizard);
                D.fadeIn = 0.6f;D.noGravity = true;D.scale = 0.8f;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
         
            Texture2D SO = ModContent.Request<Texture2D>("HeroRegression/Projectiles/Boss/SeedsOfOrigin/OriginKnife").Value;
            Texture2D line = ModContent.Request<Texture2D>("HeroRegression/Textures/LaserTex").Value;
            Vector2 drawPos = Projectile.Center - Main.screenPosition  + new Vector2(0, Projectile.gfxOffY);
                Color color = Color.White;
                float Sc = 1f;
                Main.EntitySpriteDraw(SO, drawPos, null, color, Projectile.rotation, new Vector2(11, 7), Sc, SpriteEffects.None, 0);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            if (Projectile.timeLeft > 560)
            Main.EntitySpriteDraw(line, drawPos, null, color, Projectile.velocity.ToRotation(), new Vector2(0, 4),new Vector2(9.6f,0.3f), SpriteEffects.None, 0);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }
    }
}
