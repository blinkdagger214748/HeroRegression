

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using HeroRegression.Items;
using Terraria.Audio;

namespace HeroRegression.Projectiles
{
    public class WorldSwordproj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("WorldSwordproj");
            Projectile.timeLeft = 200;
            Projectile.light = 1f;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 11;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            Main.projFrames[Projectile.type] = 1;


        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.LaserMachinegunLaser);
            Projectile.DamageType = DamageClass.Melee;
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 160;
            Projectile.tileCollide = true;
            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.aiStyle = ProjectileID.LaserMachinegunLaser;

        }
        int time = 0;
        public override void AI()
        {
            for (int i = 0; i <= 20; i++)
            {
                Vector2 dustPos = Main.rand.NextFloat(0, 30f).ToRotationVector2() * Main.rand.NextFloat(.1f, 20f);
                Dust dust = Dust.NewDustDirect(Projectile.Center + dustPos, 1, 1, DustID.WhiteTorch);
                dust.noGravity = true;
                dust.fadeIn = .12f;
                dust.velocity = Vector2.Normalize(dustPos) * (float)Math.Log(4f - dustPos.Length(), 2);
                dust.color = Color.Yellow;
                dust.scale = 1f;
            }
            NPC target = null;
            float distanceMax = 250;
            foreach (NPC NPC in Main.npc)
            {
                if (NPC.active && !NPC.friendly &&!NPC.dontTakeDamage && NPC.type != NPCID.TargetDummy && NPC.type != 400)
                {
                    Projectile.tileCollide = false;
                    float currentDistance = Vector2.Distance(NPC.Center, Projectile.Center);
                    if (currentDistance < distanceMax)
                    {
                        distanceMax = currentDistance;
                        target = NPC;
                    }
                }
            }
            if (target != null)
            {

                Vector2 targetVec = target.Center - Projectile.Center;
                targetVec.Normalize();
                targetVec *= ((int)Projectile.ai[0] == 1) ? 30f : 20f;
                Projectile.velocity = (Projectile.velocity * 30f + targetVec) / 31f;
            }
            else
            {


            }
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.Phantasmal, Projectile.oldVelocity.X * 0.2f, Projectile.oldVelocity.Y * 0.2f);
            }
            SoundEngine.PlaySound(SoundID.Dig, Main.LocalPlayer.position);
            for (int i = 0; i < 20; i++)
            {
                Vector2 projDirection = Utils.RotatedBy(Projectile.velocity, (double)(0.314 * i), default(Vector2));
                int A7 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.oldVelocity.X, Projectile.oldVelocity.Y, 1f, 1f, ProjectileType<WorldSwordproj2>(), Projectile.damage , 0.1f, 0, 0, 0);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = ModContent.Request<Texture2D>("HeroRegression/Projectiles/WorldSwordproj").Value;
            Vector2 ori = new Vector2(20, 20);
            Vector2 pos1 = Projectile.Center - Main.screenPosition;
            for (int i = 0; i <= 5; i += 1)
            {
                Vector2 pos2 = Projectile.oldPos[i] + new Vector2(20, 20) - Main.screenPosition;
                lightColor = Color.Lerp(Color.Yellow,Color.YellowGreen,0.5f) * ((float)(Projectile.oldPos.Length/2) / (Projectile.oldPos.Length/2));
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