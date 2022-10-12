using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HeroRegression.Projectiles.Friendly.Ranged
{
    public class 焰色 : ModProjectile
    {
        int Time = 0;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("焰色");



        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 20;
            Projectile.height = 42;
            Projectile.timeLeft = 500;//弹幕存在时间
            Projectile.tileCollide = true;//是否不穿墙
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.light = 1.2f;
            Projectile.penetrate = 1;






        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Time++;
            Vector2 offsets = new Vector2(0f, Projectile.gfxOffY) - Main.screenPosition;
            Color alpha = Projectile.GetAlpha(Color.White);
            Rectangle spriteRec = new Rectangle(0, 0, tex.Width, tex.Height);
            Vector2 spriteOrigin = spriteRec.Size() / 2f;
            SpriteEffects spriteEffects = (SpriteEffects)(Projectile.spriteDirection == -1 ? 1 : 0);
            Texture2D aura = ModContent.Request<Texture2D>("冲刺特效").Value;
            Vector2 drawStart = Projectile.Center + Projectile.velocity;
            Vector2 drawStart2 = Projectile.Center - Projectile.velocity * 0.5f;
            Vector2 spinPoint = new Vector2(0f, -2f);
            float time = Time % 216000f / 60f;
            Rectangle auraRec = aura.Frame(1, 1, 0, 0);
            Color yellow = Main.DiscoColor * 0.4f;
            Color white = Color.White * 0.5f;
            white.A = 0;
            yellow.A = 0;
            Vector2 auraOrigin = new Vector2(auraRec.Width / 2f, 10f);
            Main.spriteBatch.Draw(aura, drawStart + offsets + spinPoint.RotatedBy(6.2831855f * time, default), new Rectangle?(auraRec), yellow, Projectile.velocity.ToRotation() + 1.5707964f, auraOrigin, 1.3f, 0, 0f);
            Main.spriteBatch.Draw(aura, drawStart + offsets + spinPoint.RotatedBy(6.2831855f * time + 2.0943952f, default), new Rectangle?(auraRec), yellow, Projectile.velocity.ToRotation() + 1.5707964f, auraOrigin, 0.9f, 0, 0f);
            Main.spriteBatch.Draw(aura, drawStart + offsets + spinPoint.RotatedBy(6.2831855f * time + 4.1887903f, default), new Rectangle?(auraRec), yellow, Projectile.velocity.ToRotation() + 1.5707964f, auraOrigin, 1.1f, 0, 0f);
            for (float d = 0f; d < 1f; d += 0.5f)
            {
                float scaleMult = time % 0.5f / 0.5f;
                scaleMult = (scaleMult + d) % 1f;
                float colorMult = scaleMult * 2f;
                if (colorMult > 1f)
                {
                    colorMult = 2f - colorMult;
                }
                Main.spriteBatch.Draw(aura, drawStart2 + offsets, new Rectangle?(auraRec), white * colorMult, Projectile.velocity.ToRotation() + 1.5707964f, auraOrigin, 0.15f + scaleMult * 0.4f, 0, 0f);
            }
            Main.spriteBatch.Draw(tex, Projectile.Center + offsets, new Rectangle?(spriteRec), alpha, Projectile.rotation, spriteOrigin, Projectile.scale + 0.1f, spriteEffects, 0f);
            return false;
        }


        public override void AI()
        {
            Player player = Main.player[Projectile.owner];


            base.AI();
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.Pi / 2;
            if (Projectile.timeLeft <= 200)
            {
                NPC target = null;
                float distanceMax = 500;
                foreach (NPC npc in Main.npc)
                {
                    if (npc.active && !npc.friendly && npc.type != NPCID.TargetDummy)
                    {
                        Projectile.tileCollide = false;
                        float currentDistance = Vector2.Distance(npc.Center, Projectile.Center);
                        if (currentDistance < distanceMax)
                        {
                            distanceMax = currentDistance;
                            target = npc;
                        }
                    }
                }
                if (target != null)
                {

                    Vector2 targetVec = target.Center - Projectile.Center;
                    targetVec.Normalize();
                    targetVec *= (int)Projectile.ai[0] == 1 ? 30f : 20f;
                    Projectile.velocity = (Projectile.velocity * 30f + targetVec) / 31f;
                }
                else
                {


                }
            }

        }


    }
}














