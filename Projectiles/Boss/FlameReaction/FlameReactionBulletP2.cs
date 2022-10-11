using Terraria;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using HeroRegression.Projectiles;
using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using HeroRegression.NPCs.Boss.FlameReaction;

namespace HeroRegression.Projectiles.Boss.FlameReaction
{
    public class FlameReactionBulletP2 : ModProjectile
    {
        NPC fr => FlameReactionBoss.FR;
        public bool Chased;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault( "Flame");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 12;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 64;
            Projectile.hostile = true;
            Projectile.damage = 12;
            Projectile.timeLeft = 330;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
        }
        public override bool PreDraw(ref Color drawColor)
        {
            Texture2D tex = GetTex("HeroRegression/Projectiles/Boss/FlameReaction/FlameReactionBulletP2");
            Vector2 ori = new Vector2(32, 32);
            Vector2 pos1 = Projectile.Center - Main.screenPosition;
            for (int i = 0; i <= 11; i += 1)
            {
                Vector2 pos2 = Projectile.oldPos[i] + new Vector2(32, 32) - Main.screenPosition;
                drawColor = Main.DiscoColor * ((float)(Projectile.oldPos.Length - i) / (Projectile.oldPos.Length));
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                Main.EntitySpriteDraw(tex, pos2, null, drawColor, Projectile.oldRot[i], ori, 1f, SpriteEffects.None, 0);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
            return true;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, Main.DiscoR * .2f, Main.DiscoG * .2f, Main.DiscoB * .2f);
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (fr != null && fr.active)
            {
                if (Projectile.ai[0] <= 4)
                {
                    if (Projectile.timeLeft > 330 - Projectile.ai[0] * 30)
                    {
                        if(fr.life >= fr.lifeMax * .5f)
                        {
                            Projectile.velocity = (fr.Center + MathHelper.ToRadians(Projectile.ai[0] * 120 + ((FlameReactionBoss)fr.ModNPC).GlobalTimer * -6f).ToRotationVector2() * 120 - Projectile.Center) / 2f;
                        }
                        if (fr.life < fr.lifeMax * .5f)
                        {
                            Projectile.velocity = (fr.Center + MathHelper.ToRadians(Projectile.ai[0] * 90 + ((FlameReactionBoss)fr.ModNPC).GlobalTimer * -6f).ToRotationVector2() * 120 - Projectile.Center) / 2f;
                        }
                    }
                    if (Projectile.timeLeft == 330 - Projectile.ai[0] * 30)
                    {
                        Projectile.velocity = 12f * Vector2.Normalize(Main.LocalPlayer.Center - Projectile.Center);
                    }
                }
                if (Projectile.ai[0] > 4)
                {
                    if (Vector2.Distance(Projectile.Center, Main.LocalPlayer.Center) <= 150 || Projectile.timeLeft <= 210)
                    {
                        Chased = true;
                    }
                    if (!Chased)
                    {
                        Projectile.velocity = Vector2.Normalize(Vector2.Normalize(Projectile.velocity) * 11f + Vector2.Normalize(Main.LocalPlayer.Center - Projectile.Center)) * 12f;
                    }
                    if (Chased)
                    {
                        Projectile.velocity *= 1.05f;
                    }
                }
            }
        }
    }
}
