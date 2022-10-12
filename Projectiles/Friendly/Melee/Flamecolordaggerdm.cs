using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System;
using HeroRegression.Items;
using Terraria.Audio;

namespace HeroRegression.Projectiles.Friendly.Melee
{
    public class Flamecolordaggerdm : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("焰彩投刀");
            Projectile.timeLeft = 150;
            Projectile.light = 0.8f;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 11;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            Main.projFrames[Projectile.type] = 1;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.LaserMachinegunLaser);
            Projectile.DamageType = DamageClass.Melee;
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 100;
            Projectile.tileCollide = true;
            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.aiStyle = ProjectileID.LaserMachinegunLaser;

        }
        public override void AI()
        {

            if (Projectile.timeLeft < 45)
            {
                Projectile.velocity.X *= .200f;
                Projectile.velocity.Y += .8f;
            }


        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.YellowTorch, Projectile.oldVelocity.X * 0.2f, Projectile.oldVelocity.Y * 0.2f);
            }
            SoundEngine.PlaySound(SoundID.Dig, Main.LocalPlayer.position);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Type].Value;
            Vector2 ori = new Vector2(30, 30);
            Vector2 pos1 = Projectile.Center - Main.screenPosition;
            for (int i = 0; i <= 6; i += 1)
            {
                Vector2 pos2 = Projectile.oldPos[i] + new Vector2(30, 30) - Main.screenPosition;
                lightColor = Main.DiscoColor * ((float)Projectile.oldPos.Length / Projectile.oldPos.Length);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                Main.spriteBatch.Draw(tex, pos2, null, lightColor, Projectile.oldRot[i], ori, 1f, SpriteEffects.None, 0f);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
            return true;
        }
    }
}