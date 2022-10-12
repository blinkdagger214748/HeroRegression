using HeroRegression.Projectiles.Friendly.Ranged;
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
    public class Flameofmercydm2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("仁慈红弹");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 11;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            Main.projFrames[Projectile.type] = 1;


        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 18;
            Projectile.height = 28;
            Projectile.timeLeft = 400;//弹幕存在时间
            Projectile.tileCollide = true;//是否不穿墙
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.light = 1.2f;
            Projectile.penetrate = 1;
        }

        public override void AI()
        {

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.Pi / 2;
            Projectile.velocity.Y += 0.04f;


        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = ModContent.Request<Texture2D>("HeroRegression/Projectiles/Flameofmercydm2").Value;
            Vector2 ori = new Vector2(9, 14);
            Vector2 pos1 = Projectile.Center - Main.screenPosition;
            for (int i = 0; i <= 4; i += 1)
            {
                Vector2 pos2 = Projectile.oldPos[i] + new Vector2(9, 14) - Main.screenPosition;
                lightColor = Main.DiscoColor * ((float)Projectile.oldPos.Length / Projectile.oldPos.Length);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                Main.spriteBatch.Draw(tex, pos2, null, lightColor, Projectile.oldRot[i], ori, 1f, SpriteEffects.None, 0f);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
            return true;

        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.penetrate--;
            if (Projectile.penetrate <= 0)
            {

            }
            if (Main.rand.NextBool(5))
            {
                Vector2 vel1 = new Vector2(-1, -1);
                vel1 *= 4f;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.position.X + 150, target.position.Y + 150, vel1.X, vel1.Y, ModContent.ProjectileType<焰色>(), Projectile.damage / 2, 0, Main.myPlayer);
                Vector2 vel2 = new Vector2(1, 1);
                vel2 *= 4f;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.position.X - 150, target.position.Y - 150, vel2.X, vel2.Y, ModContent.ProjectileType<焰色>(), Projectile.damage / 2, 0, Main.myPlayer);
                Vector2 vel3 = new Vector2(1, -1);
                vel3 *= 4f;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.position.X - 150, target.position.Y + 150, vel3.X, vel3.Y, ModContent.ProjectileType<焰色>(), Projectile.damage / 2, 0, Main.myPlayer);
                Vector2 vel4 = new Vector2(-1, 1);
                vel4 *= 4f;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.position.X + 150, target.position.Y - 150, vel4.X, vel4.Y, ModContent.ProjectileType<焰色>(), Projectile.damage / 2, 0, Main.myPlayer);
                Vector2 vel5 = new Vector2(0, -1);
            }
        }
    }



}







