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
using HeroRegression.NPCs.Boss.Heroes;

namespace HeroRegression.Projectiles.Boss.Heros
{
    class ForgeHammer : ModProjectile//锻造之锤
    {
        public override string Texture => "HeroRegression/Textures/HOFweapons/锤子"; public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.width = 68;
            Projectile.height = 68;
            Projectile.light = .5f;
            Projectile.hostile = true;
     
            Projectile.timeLeft = 55;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            
            NPC npc = Main.npc[(int)Projectile.ai[0]];
            float k = (float)Math.Sign(npc.velocity.ToRotation());
            Projectile.Center = npc.Center;
            if (npc.ai[1] == 2)
            {
                if(Projectile.timeLeft == 54)
                {
                    Projectile.rotation = npc.velocity.ToRotation() + k;
                  
                }
                Projectile.rotation -= 2 * k / 55f;
            }
            if (npc.ai[1] == 1)
            {
                if (Projectile.timeLeft == 54)
                {
                    Projectile.rotation = npc.velocity.ToRotation() + k;
                    Projectile.timeLeft -= 10;
                   
                }
                Projectile.rotation -= 2 * k / 45f;
            }

        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D SO = GetTex("HeroRegression/Textures/HOFweapons/锤子");
            for (int i = 1; i < Projectile.oldPos.Length; i += 1)//trail
            {
                Vector2 drawPos1 = Projectile.oldPos[i] - Main.screenPosition + new Vector2(Projectile.width / 2, Projectile.height / 2) + new Vector2(0, Projectile.gfxOffY);
                Color color = Color.White * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);color.A = 50;
                float Sc = 1f - 0.03f * i;
                Main.EntitySpriteDraw(SO, drawPos1, null, color, Projectile.oldRot[i], SO.Size() / 2, Sc, SpriteEffects.None, 0);
            }
            Main.EntitySpriteDraw(SO, Projectile.Center - Main.screenPosition + Projectile.rotation.ToRotationVector2() * 22f, null, Color.White, Projectile.rotation, SO.Size() / 2, 1, SpriteEffects.None, 0);
            return false;
        }
    }
    class ForgeIngot : ModProjectile//铁锭
    {
        public override string Texture => "HeroRegression/Textures/HOFweapons/铁胚子"; public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.width = 5;
            Projectile.height = 5;
            Projectile.light = .5f;
            Projectile.hostile = true;

            Projectile.timeLeft = 100;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            Projectile.localAI[0] += 0.01f;
            Projectile.localAI[0] = Projectile.localAI[0] > 1 ? 1 : Projectile.localAI[0];
            
            NPC npc = Main.npc[(int)Projectile.ai[0]];
            if (Projectile.localAI[1] == 0)
            {
                float rot = npc.ai[3] + Projectile.ai[1] * 6.283F / (npc.life < 0.5f * npc.lifeMax ? 8 : 5);
                Projectile.Center = npc.Center + rot.ToRotationVector2() * 100 * Projectile.localAI[0];
            }
            if(Projectile.localAI[1] == 1)
            {
                Projectile.timeLeft = 100;
                Projectile.velocity = (npc.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 3.2f;
                if((npc.Center - Projectile.Center).Length() < 3)
                {
                    var p = Projectile.NewProjectileDirect(null, Projectile.Center, Vector2.Zero, 695, 0, 0, 0); p.scale = 3;
                    if (npc.ai[1] == 3)
                    {
                        Projectile.NewProjectileDirect(null, npc.Center, Vector2.Zero, ModContent.ProjectileType<ForgeSword>(), 15, 0, 0, npc.whoAmI, 0);
                    }
                    if (npc.ai[1] == 4)
                    {
                        var ps = Projectile.NewProjectileDirect(null, npc.Center, Vector2.Zero, ModContent.ProjectileType<ForgeSpear>(), 15, 0, 0, npc.whoAmI, 0); 
                    }
                    Projectile.Kill();
                }
            }
            if(npc.type == ModContent.NPCType<HeroOfForge>() && npc.active)
            {
                Projectile.timeLeft = 10;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D SO = GetTex("HeroRegression/Textures/HOFweapons/铁胚子");
            Texture2D SO2 = GetTex("HeroRegression/Textures/Glowing");
            for (int i = 0; i < Projectile.oldPos.Length; i += 1)//trail
            {
                Vector2 drawPos1 = Projectile.oldPos[i] - Main.screenPosition + new Vector2(Projectile.width / 2, Projectile.height / 2) + new Vector2(0, Projectile.gfxOffY);
                Color color = Color.White * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length); color.A = 0;
                float Sc = 1f - 0.03f * i;
                if (Projectile.localAI[1] == 0)
                    Main.EntitySpriteDraw(SO, drawPos1, null, color, Projectile.oldRot[i], SO.Size() / 2, Sc, SpriteEffects.None, 0);
                if (Projectile.localAI[1] == 1)
                    Main.EntitySpriteDraw(SO2, drawPos1, null, color,0, SO2.Size() / 2, Sc * 0.5f, SpriteEffects.None, 0);
            }
            if (Projectile.localAI[1] == 0)
                Main.EntitySpriteDraw(SO, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, SO.Size() / 2, 1, SpriteEffects.None, 0);
            return false;
        }
    }
    class ForgeSword : ModProjectile//锻造之剑
    {
        float t = 0;
        public override string Texture => "HeroRegression/Textures/HOFweapons/长剑"; public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.width = 68;
            Projectile.height = 68;
            Projectile.light = .5f;
            Projectile.hostile = true;

            Projectile.timeLeft = 505;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            Projectile.localAI[0]++;
            NPC npc = Main.npc[(int)Projectile.ai[0]];Player player = Main.player[npc.target];
            if (npc.active) Projectile.timeLeft = 2;
            if(Projectile.localAI[0] < 30)
            {
                Projectile.rotation = -3.14159f;
            }
            if(Projectile.localAI[0] >= 30 && Projectile.localAI[0] <= 100)
            {
                Projectile.rotation = LerpAngle(Projectile.rotation, (player.Center - npc.Center).ToRotation() - 1.57f, 0.1f);
                if(Projectile.localAI[0] > 50)
                {
                    Vector2 s = Main.rand.NextVector2CircularEdge(150, 70);
                    var d = Dust.NewDustDirect(s + npc.Center, 0, 0, DustID.ShadowbeamStaff, 0, 0, 0, Color.Gold, 2);d.velocity = -s / 6f;
                }
            }
            if(Projectile.localAI[0]<150)
            Projectile.Center = npc.Center;
            if (Projectile.localAI[0] == 150)
            {
                Main.NewText(Projectile.extraUpdates);
                npc.velocity *= 0;
                if (Projectile.extraUpdates < 2)
                {
                    var x = Projectile.NewProjectileDirect(null, npc.Center, Vector2.Zero, Projectile.type, 24, 1, 0, npc.whoAmI); x.extraUpdates = Projectile.extraUpdates + 1;
                }
                if(Projectile.extraUpdates == 2)
                {
                    npc.ai[1]++;npc.ai[0] = 0;
                }
                Projectile.Kill();

            }
            if (Projectile.localAI[0] == 101)
            {
                for(int i = 0; i < 30; i++)
                {
                    float r = i * 3.1416f / 15f;
                    Vector2 rd = r.ToRotationVector2() * 50;
                    rd.X *= 0.5f;
                    rd = rd.RotatedBy(Projectile.rotation + 1.57f);
                    Dust d = Dust.NewDustDirect(0.1f * rd + Projectile.Center, 0, 0, DustID.ShadowbeamStaff, 0, 0, 0, Main.OurFavoriteColor, 1.4f);d.velocity = rd;
                }
                npc.velocity = (Projectile.rotation + 1.57f).ToRotationVector2() * 75f * ( 1 + 0.5f * Projectile.extraUpdates);
            }
            if(Projectile.localAI[0] > 120 && Projectile.localAI[0] < 150)
            {
                
                npc.velocity *= 0.8f;
            }
            if (Projectile.localAI[0] >= 150)
            {
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, (player.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 13f, 0.03f);
            }
            if(Projectile.localAI[0] > 150)
            {
                for(int i = 1; i < 20; i++)
                {
                    if(Projectile.localAI[0] == 150 +45 * (2 * i - 1))
                    {
                        t = (player.Center - Projectile.Center).ToRotation() - 1.57f - 3f;
                        Projectile.rotation = t ;
                    }
                    if (Projectile.localAI[0] == 150 + 45 * (2 * i))
                    {
                        t = (player.Center - Projectile.Center).ToRotation() - 1.57f + 3f;
                        Projectile.rotation = t;
                    }
                    if (Projectile.localAI[0] > 150 + 45 * (2*i -1) && Projectile.localAI[0] < 150 + 45 * 2 * i)
                    {
                        Projectile.rotation  += 6f/45f;
                    }
                    if (Projectile.localAI[0] > 150 + 45 * (2 * i) && Projectile.localAI[0] < 150 + 45 * (2 * i+1))
                    {
                        Projectile.rotation -=6f / 45f;
                    }
                }
                Vector2 pos = player.Center + new Vector2(Math.Sign(npc.Center.X - player.Center.X) * 300, 0);
                npc.velocity = Vector2.Lerp(npc.velocity, (pos - npc.Center).SafeNormalize(Vector2.Zero) * 18f, 0.04f);
                if(npc.ai[0] % 40 == 0)
                {
                    for(int i = -1;i<=1;i+=2)
                    Projectile.NewProjectile(null, npc.Center, (player.Center - npc.Center).SafeNormalize(Vector2.Zero).RotatedBy(i * 0.7f) * 15f, ProjectileID.CultistBossFireBall, 10, 0, 0);
                }
            }
            if(Projectile.localAI[0] > 540)
            {
                Projectile.Kill();
            }
            
        }
        float LerpAngle(float r,float r2,float factor)
        {
            Vector2 rv = r.ToRotationVector2();Vector2 rv2 = r2.ToRotationVector2();
            Vector2 rv3 = rv + (rv2 - rv) * factor;
            return rv3.ToRotation();
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D SO = GetTex("HeroRegression/Textures/HOFweapons/长剑");
            Texture2D S2 = GetTex("HeroRegression/Textures/Line");
            if (Projectile.localAI[0] > 30)
            for (int i = 1; i < Projectile.oldPos.Length; i += 1)//trail
            {
                Vector2 drawPos1 = Projectile.position - Main.screenPosition + new Vector2(Projectile.width / 2, Projectile.height / 2) + new Vector2(0, Projectile.gfxOffY);
                Color color = Color.White * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length); color.A = 50;
                    float Sc = 1f;
                Main.EntitySpriteDraw(SO, drawPos1 + (Projectile.oldRot[i] + 1.57f).ToRotationVector2() * 40f, null, color, Projectile.oldRot[i], SO.Size() / 2, Sc, SpriteEffects.None, 0);
            }
            if(Projectile.localAI[0] <= 30)
            {
                for (int i = 1; i >= -1; i -= 2)
                {
                    Main.EntitySpriteDraw(SO, Projectile.Center - Main.screenPosition + (Projectile.rotation + 1.57f).ToRotationVector2() * 40f + new Vector2(i* Projectile.localAI[0] * 5,0), null, Color.White * (1-0.033f * Projectile.localAI[0]), Projectile.rotation, SO.Size() / 2, 1, SpriteEffects.None, 0);
                }
            }
            if(Projectile.localAI[0] >50 && Projectile.localAI[0] <= 100)
            {
                Color r = Color.Red;r.A = 0;
                for(int i = 1; i >= -1; i -= 2)
                Main.EntitySpriteDraw(S2, Projectile.Center - Main.screenPosition , null,r * (0.02f * (Projectile.localAI[0] - 50)), Projectile.rotation + 1.57f + i*0.4f *((0.02f * (Projectile.localAI[0] - 100))), new Vector2(0,S2.Height/2), new Vector2(1000,0.5F), SpriteEffects.None, 0);
            }
            Main.EntitySpriteDraw(SO, Projectile.Center - Main.screenPosition + (Projectile.rotation + 1.57f).ToRotationVector2() * 40f , null, Color.White, Projectile.rotation, SO.Size() / 2, 1, SpriteEffects.None, 0);
            return false;
        }
    }
    class ForgeSpear : ModProjectile
    {
        public override string Texture => "HeroRegression/Textures/HOFweapons/长矛"; public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.width = 33;
            Projectile.height = 33;
            Projectile.light = .5f;
            Projectile.hostile = true;

            Projectile.timeLeft = 555;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            Projectile.localAI[0]++;Projectile.localAI[1]++;
            Player player = Main.player[Projectile.owner];
            if(Projectile.localAI[1] < 60)
            {
                Projectile.velocity *= 0.8f;
                Projectile.rotation += 0.35f;
            }
            if(Projectile.localAI[1] == 60)
            {
                Projectile.rotation = (player.Center - Projectile.Center).ToRotation() + 0.785f;
                Projectile.velocity = (player.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 26f;
            }
            if (Projectile.localAI[1] > 100) Projectile.localAI[1] = 0;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D SO = GetTex("HeroRegression/Textures/HOFweapons/长矛");
            for (int i = 1; i < Projectile.oldPos.Length; i += 1)//trail
            {
                Vector2 drawPos1 = Projectile.oldPos[i] - Main.screenPosition + new Vector2(Projectile.width / 2, Projectile.height / 2) + new Vector2(0, Projectile.gfxOffY);
                Color color = Color.White * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length); 
                float Sc = 1f - 0.03f * i;
                Main.EntitySpriteDraw(SO, drawPos1, null, color, Projectile.oldRot[i], SO.Size() / 2, Sc *0.07f * (Projectile.localAI[0] > 50?50:Projectile.localAI[0]), SpriteEffects.None, 0);
            }
            Main.EntitySpriteDraw(SO, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, SO.Size() / 2, 0.07f * (Projectile.localAI[0] > 50 ? 50 : Projectile.localAI[0]), SpriteEffects.None, 0);
            return false;
        }
    }

}
