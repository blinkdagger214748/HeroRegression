using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using HeroRegression.NPCs.Boss;
using HeroRegression.Projectiles.Boss.SeedsOfOrigin;
using HeroRegression.Projectiles;
using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using HeroRegression.Projectiles.Boss.PupilOfHell;

using Terraria.Graphics.Effects;
using Terraria.Audio;

namespace HeroRegression.NPCs.Boss.PupilOfHell
{
    [AutoloadBossHead]
    public class PupilOfHell : ModNPC
    {
        public Texture2D GetTex(string path)
        {
            return ModContent.Request<Texture2D>(path).Value;
        }
        private Texture2D SO;
        private Texture2D bosstex;
        private Texture2D lasertex2;
        Texture2D tf;
        Texture2D warn;
        bool flash;
        Color flashcolor;
        bool dash;
        float rotp;
        Vector2 plrpos;
        public override void SetStaticDefaults()
        {
            
              DisplayName.SetDefault("Pupil Of Hell");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "狱门之瞳");
            NPCID.Sets.TrailCacheLength[NPC.type] = 10;
            NPCID.Sets.TrailingMode[NPC.type] = 3;
        }

   
        public override void SetDefaults()
        {
            SO = GetTex("HeroRegression/Textures/Pupil/Phase2");
            tf = GetTex("HeroRegression/Textures/Pupil/flashing");
            warn = GetTex("HeroRegression/Textures/Pupil/Warning");
            lasertex2 = GetTex("HeroRegression/Textures/LaserTex2");
            bosstex = GetTex("HeroRegression/NPCs/Boss/PupilOfHell/PupilOfHell");
            NPC.width = 108;
            NPC.height = 100;
            NPC.friendly = false;
            NPC.damage = 90;
            NPC.defense = 45;
            NPC.lifeMax = 112000;
            NPC.boss = true;
            NPC.knockBackResist = 0;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.noTileCollide = true;
            Music = MusicID.OtherworldlyTowers;

        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(bossLifeScale * NPC.lifeMax * 0.7f);
            base.ScaleExpertStats(numPlayers, bossLifeScale);
        }
        
        
        #region 自制简易方法
        public void LerpChase(Vector2 pos, float vel, float v)
        {
            Vector2 topos = Vector2.Normalize(pos - NPC.Center);
            NPC.velocity = Vector2.Lerp(NPC.velocity, vel * topos, v);
        }//简单渐进
        public float LerpRot(float rotation,float endrotation,float v)
        {
            Vector2 ro = rotation.ToRotationVector2();Vector2 end = endrotation.ToRotationVector2();    
            ro = Vector2.Lerp(ro, end, v);
            return ro.ToRotation();
        }
        public void ChangePhase(int phase)
        {
            NPC.ai[0] = 0;NPC.ai[1] = phase;
        }//切换阶段方法
        #endregion
        public override void AI()
        {
            if (!SkyManager.Instance["PupilBoss"].IsActive())
                SkyManager.Instance.Activate("PupilBoss");
            if (flash) { NPC.localAI[2] += 1; }
            if (NPC.localAI[2] == 50) { NPC.localAI[2] = 0; flash = false; }
            #region 寻敌
            if (NPC.target < 0 || NPC.target >= 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
            Player player = Main.player[NPC.target];
            #endregion
            //出场
            //以NPC.ai[1]为攻击模式切换
            NPC.ai[0]++;
            if(NPC.ai[1] == 0)
            {
                NPC.rotation = (player.Center - NPC.Center).ToRotation();
                NPC.velocity = new Vector2(0);
                if(NPC.ai[0] == 1)
                {
                    SoundEngine.PlaySound(SoundID.Roar);
                    Projectile.NewProjectile(null,NPC.Center, new Vector2(0), ModContent.ProjectileType<SpawnDoor>(), 0, 0, 0, NPC.whoAmI, 0);
                    for(int i = 0; i < 6; i++)
                    {
                        Vector2 pos = NPC.Center + new Vector2(0, -500) + (i * MathHelper.Pi / 3).ToRotationVector2() * 500;
                        NPC.NewNPC(null,(int)NPC.Center.X,(int)(NPC.Center.Y),ModContent.NPCType<PupilMinion>(),0,NPC.whoAmI,i);
                    }
                }
                if (NPC.ai[0] >= 360)
                {
                    ChangePhase(1);
                }

            }
            if(NPC.ai[1] == 1)
            {
                NPC.rotation = NPC.velocity.ToRotation();
                for (int i = 0;i < 6; i++)
                {
                    if(NPC.ai[0] == 60 + 70 * i)
                    {
                        SoundEngine.PlaySound(SoundID.Roar);
                        NPC.velocity = Vector2.Normalize(player.Center - NPC.Center) * 25;
                    }
                    if(NPC.ai[0] >= 110 + 70 * i && NPC.ai[0] <= 130 + 70 * i)
                    {
                        NPC.velocity *= 0.9f;
                    }
                }
                if(NPC.ai[0] > 150 + 70 * 5)
                {
                    ChangePhase(2);
                }
            }//草想不出来AI
            if(NPC.ai[1] == 2)
            {
                NPC.rotation = (player.Center - NPC.Center).ToRotation();
                LerpChase(player.Center + new Vector2(0, NPC.Center.Y - player.Center.Y > 0 ? 500 : -500), 21, 0.12f);
                for(int i = 0;i < 40; i++)
                {
                    if(NPC.ai[0] == 7 * i && NPC.ai[0] >80)
                        Projectile.NewProjectile(null,NPC.Center, (Vector2.Normalize(player.Center - NPC.Center) * 27).RotatedBy(-0.46f), ModContent.ProjectileType<PupilProj>(), 34, 0, 0, 1);
                }//顺时针
                for (int i = 0; i < 40; i++)
                {
                    if (NPC.ai[0] == 7 * i && NPC.ai[0] >80)
                        Projectile.NewProjectile(null,NPC.Center, (Vector2.Normalize(player.Center - NPC.Center) * 27).RotatedBy(0.46f), ModContent.ProjectileType<PupilProj>(), 34, 0, 0, 2);
                }//逆时针
                for(int i = 0; i < 8; i++)
                {
                    if(NPC.ai[0] == 40 * i)
                    {
                        float r = Main.rand.NextBool(2)==true? Main.rand.NextFloat(-0.1f, 0.1f): Main.rand.NextFloat(-0.1f + 3.1416f, 0.1f + 3.1416f);
                        float r1 = r + 1.5707f;
                        Projectile.NewProjectile(null,player.Center - r.ToRotationVector2() * 1200, r.ToRotationVector2() *1.9f, ModContent.ProjectileType<PupilProj>(), 34, 0, 0, 3);
                        Projectile.NewProjectile(null,player.Center - r1.ToRotationVector2() * 1200, r1.ToRotationVector2() * 1.9f, ModContent.ProjectileType<PupilProj>(), 34, 0, 0, 3);
                    }
                }
                if(NPC.ai[0] == 330)
                {
                    flash = true; flashcolor = Color.Orange;
                    ChangePhase(3);
                }
            }
            if(NPC.ai[1] == 3)
            {
                NPC.rotation = (player.Center - NPC.Center).ToRotation();
                LerpChase(player.Center ,6f, 0.1f);
                if(NPC.ai[0] %20 == 0 && NPC.ai[0] > 90)
                {
                    for (int i = -1; i <= 1; i += 2)
                        Projectile.NewProjectile(null,NPC.Center, Vector2.Normalize((player.Center - NPC.Center).RotatedBy(i*1.1707f)) * 16f, ModContent.ProjectileType<MinionProj>(), 25, 1, NPC.target);
                }
                if(NPC.ai[0] >= 450)
                {
                    ChangePhase(4);
                }
                if(NPC.ai[0] < 120)
                {
                    float r = 2000 - 1400 * NPC.ai[0] / 120 ;
                   
                    for (int i = 0;i< 25; i++)
                    {
                        float k = Main.rand.NextFloat(0, 3);
                        Vector2 pos = NPC.Center + (k + i * MathHelper.TwoPi / 25).ToRotationVector2() * r;
                        Dust d = Dust.NewDustDirect(pos, 0, 0, DustID.Torch);d.noGravity = true; d.fadeIn = 0.3f; d.scale = 1.5f;
                    }
                    if(Vector2.Distance(NPC.Center,player.Center) > r)
                    {
                        player.velocity = Vector2.Normalize(NPC.Center - player.Center) * 3f;
                    }
                }
                if (NPC.ai[0] >= 120)
                {
                    float r = 2000 - 1400;
                    for (int i = 0; i < 25; i++)
                    {
                        float k = Main.rand.NextFloat(0, 3);
                        Vector2 pos = NPC.Center + (k +i* MathHelper.TwoPi/25).ToRotationVector2() * r;
                        Dust d = Dust.NewDustDirect(pos, 0, 0, DustID.Torch); d.noGravity = true;d.fadeIn = 0.3f;d.scale = 1.5f;
                    }
                    if (Vector2.Distance(NPC.Center, player.Center) > r)
                    {
                        player.velocity = Vector2.Normalize(NPC.Center - player.Center) * 3f;
                    }
                }
                for(int i = 0; i < 5; i++)
                {
                    if(NPC.ai[0] == 100 + 60 * i)
                    {
                        Projectile proj = Projectile.NewProjectileDirect(null,player.Center, Vector2.Zero, ModContent.ProjectileType<PupLaser>(), 25, 0, NPC.target, 0, 1);proj.rotation = 0;
                        SoundEngine.PlaySound(SoundID.Zombie104);
                    }
                }
                
            }//我也不知道叫啥
            if(NPC.ai[1] == 4)
            {
                LerpChase(player.Center + Vector2.Normalize(NPC.Center - player.Center) * 300, 24f, 0.02f);NPC.rotation = (player.Center - NPC.Center).ToRotation();
                for (int i = 0; i < 4; i++)
                {
                    if (NPC.ai[0] == 70 + 58 * i)
                    {
                        for(int k = 0;k < 13; k++)
                        {
                            Vector2 upperpos = new Vector2(-1660 + 240 * k, -800);
                            Vector2 lowerpos = new Vector2(-1660 + 240 * k, 800);
                            Projectile.NewProjectile(null,upperpos + NPC.Center, new Vector2(1, 1), ModContent.ProjectileType<PupilProj>(), 24, 1, NPC.target, 3);
                            Projectile.NewProjectile(null,lowerpos + NPC.Center, new Vector2(1, -1), ModContent.ProjectileType<PupilProj>(), 24, 1, NPC.target, 3);
                        }

                    }
                }
                if(NPC.ai[0] == 250)
                {
                    ChangePhase(5);
                }
            }//十字线
            if(NPC.ai[1] == 5)
            {
                for (int i = 0; i < 20; i++)
                {
                    if (NPC.ai[0] == 40 * i)
                    {
                        float r = Main.rand.NextBool(2) == true ? Main.rand.NextFloat(-0.1f, 0.1f) : Main.rand.NextFloat(-0.1f + 3.1416f, 0.1f + 3.1416f);
                        float r1 = r + 1.5707f;
                        Projectile.NewProjectile(null,player.Center - r.ToRotationVector2() * 1200, r.ToRotationVector2() * 1.9f, ModContent.ProjectileType<PupilProj>(), 34, 0, 0, 3);
                        Projectile.NewProjectile(null,player.Center - r1.ToRotationVector2() * 1200, r1.ToRotationVector2() * 1.9f, ModContent.ProjectileType<PupilProj>(), 34, 0, 0, 3);
                    }
                }
                for (int i = 0; i < 5; i++)
                {
                    if(NPC.ai[0] ==100 + 43 + 150 * i)
                    {
                        dash = true;NPC.velocity = -Vector2.Normalize(player.Center - NPC.Center) * 7;
                    }
                    if (NPC.ai[0] == 100 + 60 + 150 * i)
                    {
                        NPC.velocity = Vector2.Normalize(player.Center - NPC.Center) * 35; SoundEngine.PlaySound(SoundID.Roar);
                    }
                    if (NPC.ai[0] >= 100 + 85 + 150 * i && NPC.ai[0] <94 + 100 + 150 * i)
                    {
                        NPC.velocity *= 0.9f;
                    }
                    if(NPC.ai[0] == 94+ 100 + 150 * i)
                    {
                        dash = false;
                    }
                }
                if (dash) { NPC.rotation = NPC.velocity.ToRotation(); }
                if (!dash)
                {
                    LerpChase(player.Center + (Vector2.Normalize(NPC.Center - player.Center) * 420).RotatedBy(0.003f * NPC.ai[0]), 24f, 0.02f); NPC.rotation = (player.Center - NPC.Center).ToRotation();
                }
                if(NPC.ai[0] > 690)
                {
                    ChangePhase(6);
                }
            }//旋转跳跃我闭着眼
            if(NPC.ai[1] == 6)
            {
                NPC.rotation = (player.Center - NPC.Center).ToRotation();
                LerpChase(player.Center + new Vector2( NPC.Center.X - player.Center.X > 0 ? 500 : -500,0), 17, 0.03f);
                for(int i = 0;i< 8; i++)
                {
                    if(NPC.ai[0] == 10 + 80 * i)
                    {
                        Vector2 gap = new Vector2(player.Center.X, player.Center.Y + Main.rand.Next(-2, 3) * 100);//设一个向量坐标，为玩家垂直方向上下200格
                        for(int j = 0; j < 14; j++)
                        {
                            Projectile.NewProjectile(null,NPC.Center.X - player.Center.X > 0 ? new Vector2(player.Center.X - 800, gap.Y - j * 50) : new Vector2(player.Center.X + 800, gap.Y - j * 50), new Vector2(NPC.Center.X - player.Center.X > 0 ? 8 : -8, 0), ModContent.ProjectileType<PupilProj>(), 26, 1, NPC.target, 0);
                            Projectile.NewProjectile(null,NPC.Center.X - player.Center.X > 0 ? new Vector2(player.Center.X - 800, gap.Y+200 + j * 50) : new Vector2(player.Center.X + 800, gap.Y + 200 + j * 50), new Vector2(NPC.Center.X - player.Center.X > 0 ? 8 :- 8, 0), ModContent.ProjectileType<PupilProj>(), 26, 1, NPC.target, 0);
                        }
                    }
                }
              
                    for (int i = 0; i < 90; i++)
                    {
                   
                        if (NPC.ai[0] == 7 * i && NPC.ai[0] > 50)
                            Projectile.NewProjectile(null,NPC.Center, (Vector2.Normalize(player.Center - NPC.Center) * 33).RotatedBy(-0.55f), ModContent.ProjectileType<PupilProj>(), 34, 0, 0, 1);
                    }//顺时针
                    for (int i = 0; i < 90; i++)
                    {
                        if (NPC.ai[0] == 7 * i && NPC.ai[0] > 50)
                            Projectile.NewProjectile(null,NPC.Center, (Vector2.Normalize(player.Center - NPC.Center) * 33).RotatedBy(0.55f), ModContent.ProjectileType<PupilProj>(), 34, 0, 0, 2);
                    }//逆时针
                if(NPC.ai[0] > 620)
                {
                    ChangePhase(1);
                }
            }//木恩要求的跨栏
            //二阶段
            if(NPC.ai[1] <= 6 && NPC.life < NPC.lifeMax * 0.55f)//二阶段开始
            {
                ChangePhase(7); SoundEngine.PlaySound(SoundID.Roar);
            }
            if(NPC.ai[1] == 7)
            {
                
                if (NPC.ai[0] <= 120)
                {
                    NPC.rotation += ( 0.00005f * NPC.ai[0]) * NPC.ai[0];//越来越快的转动
                }
                if (NPC.ai[0] > 120 && NPC.ai[0] <= 240)
                {
                    NPC.rotation += ( 0.00005f * 120 - 0.00005f * (NPC.ai[0] - 120)) * NPC.ai[0];//越来越快的转动
                }
                NPC.velocity *= 0;
               // NPC.position += Main.rand.NextVector2Circular(13,13);//随机震动
               
                Dust d = Dust.NewDustDirect(NPC.Center, 0, 0, DustID.Torch);d.velocity = Main.rand.NextVector2Circular(13, 13);
                if(NPC.ai[0] > 240)
                {
                    ChangePhase(8);
                }
            }
            if(NPC.ai[1] == 8)
            {
                NPC.rotation = NPC.velocity.ToRotation();
                for (int i = 0; i < 6; i++)
                {
                    if (NPC.ai[0] == 60 + 70 * i)
                    {
                        NPC.velocity = Vector2.Normalize(player.Center - NPC.Center) * 25; SoundEngine.PlaySound(SoundID.ForceRoar); //eoc roar
                        Projectile.NewProjectile(null,NPC.Center, Vector2.Normalize(player.Center - NPC.Center) * 3, ModContent.ProjectileType<ExpProj>(), 24, 0, NPC.target);
                    }
                    if (NPC.ai[0] >= 110 + 70 * i && NPC.ai[0] <= 130 + 70 * i)
                    {
                        NPC.velocity *= 0.9f;
                    }
                }
                if (NPC.ai[0] > 150 + 70 * 5)
                {
                    ChangePhase(9);NPC.velocity *= 0.3f;
                }
            }
            if(NPC.ai[1] == 9)
            {
                
                NPC.rotation = (player.Center - NPC.Center).ToRotation();
                if(NPC.ai[0] < 60)
                LerpChase(player.Center + new Vector2(0, NPC.Center.Y - player.Center.Y > 0 ? 300 : -300), 21, 0.21f);
                if(NPC.ai[0] == 60) { NPC.velocity *= 0;flash = true;flashcolor = Color.Yellow; }
                for (int i = 0;i< 100; i++)
                {
                    if(NPC.ai[0] == 5 * i && NPC.ai[0] > 65)
                    {
                        float rot = 0.08f * i;
                        for(int k = 0; k < 5; k++)
                        {
                            Vector2 speed = (rot + k * MathHelper.Pi / 2.5f).ToRotationVector2() * 14f;
                            Projectile.NewProjectile(null,NPC.Center,speed, ModContent.ProjectileType<PupilProj>(), 24, 0, NPC.target,0);
                        }
                    }
                }
                if (NPC.ai[0] < 120)
                {
                    float r = 2000 - 1400 * NPC.ai[0] / 120;

                    for (int i = 0; i < 25; i++)
                    {
                        float k = Main.rand.NextFloat(0, 3);
                        Vector2 pos = NPC.Center + (k + i * MathHelper.TwoPi / 25).ToRotationVector2() * r;
                        Dust d = Dust.NewDustDirect(pos, 0, 0, DustID.Torch); d.noGravity = true; d.fadeIn = 0.3f; d.scale = 1.5f;
                    }
                    if (Vector2.Distance(NPC.Center, player.Center) > r)
                    {
                        player.velocity = Vector2.Normalize(NPC.Center - player.Center) * 3f;
                    }
                }
                if (NPC.ai[0] >= 120)
                {
                    float r = 2000 - 1400;
                    for (int i = 0; i < 25; i++)
                    {
                        float k = Main.rand.NextFloat(0, 3);
                        Vector2 pos = NPC.Center + (k + i * MathHelper.TwoPi / 25).ToRotationVector2() * r;
                        Dust d = Dust.NewDustDirect(pos, 0, 0, DustID.Torch); d.noGravity = true; d.fadeIn = 0.3f; d.scale = 1.5f;
                    }
                    if (Vector2.Distance(NPC.Center, player.Center) > r)
                    {
                        player.velocity = Vector2.Normalize(NPC.Center - player.Center) * 3f;
                    }
                }
                if(NPC.ai[0] > 400)
                {
                    ChangePhase(10);
                }
            }//泰拉跟着转
            if(NPC.ai[1] == 10)
            {
                for(int i = 0;i <= 2; i++)
                {
                    if(NPC.ai[0] == 0) { rotp =(NPC.Center - player.Center).ToRotation(); }
                    if (NPC.ai[0] == 260 +260 * i) { rotp = (NPC.Center - player.Center).ToRotation() - MathHelper.Pi/6; }
                    if(NPC.ai[0] > 260 * i && NPC.ai[0] < 60 + 260 * i)
                    {
                        Vector2 pos = player.Center + rotp.ToRotationVector2() * 300;
                        LerpChase(pos, 24, 0.1f);
                        NPC.rotation = LerpRot(NPC.rotation, rotp + 1.57f,0.02f);
                    }
                    if(NPC.ai[0] == 60 + 260 * i) { SoundEngine.PlaySound(SoundID.Zombie104); Projectile p = Projectile.NewProjectileDirect(null,NPC.Center, Vector2.Zero, ModContent.ProjectileType<PupLaser>(), 40, 1, NPC.target, 0, 2);p.frameCounter = NPC.whoAmI;p.rotation = NPC.rotation; }
                    if(NPC.ai[0] >= 60 + 260 * i && NPC.ai[0] < 260 + 260 * i)
                    {
                        NPC.velocity *= 0.9f;
                        NPC.rotation += 3.1416f / 96 * ( ( 1.3f * (float)Math.Sin((NPC.ai[0] - 60 - 260 * i)*(3.1416f/200))));
                        Dust d = Dust.NewDustDirect(NPC.Center, 0, 0, DustID.Torch); d.velocity = Main.rand.NextVector2Circular(13, 13);
                    }
                    if(NPC.ai[0] == 259+ 260 * i)
                    {
                        foreach(var proj in Main.projectile)
                        {
                            if(proj.type == ModContent.ProjectileType<PupLaser>() && proj.active)
                            {
                                proj.Kill();break;
                            }
                        }

                    }
                    if(NPC.ai[0] == 260 + 521)
                    {
                        ChangePhase(11);
                    }
                }

            }//迫真致敬
            if(NPC.ai[1] == 11)
            {
                for(int i = 0; i < 10; i++)
                {
                    if(NPC.ai[0] == 150 * i + 50)
                    {
                        Vector2 gap = new Vector2(player.Center.X, player.Center.Y + Main.rand.Next(-4, 1) * 100);//设一个向量坐标，为玩家垂直方向上下200格
                        for (int j = 0; j < 14; j++)
                        {
                            Projectile.NewProjectile(null,new Vector2(player.Center.X - 800, gap.Y - j * 150), new Vector2( 8, 0), ModContent.ProjectileType<PupilProj>(), 26, 1, NPC.target, 0);
                            Projectile.NewProjectile(null, new Vector2(player.Center.X - 800, gap.Y + 350 + j * 150), new Vector2(8, 0), ModContent.ProjectileType<PupilProj>(), 26, 1, NPC.target, 0);
                            Projectile.NewProjectile(null, new Vector2(player.Center.X + 800, gap.Y - j * 150), new Vector2(-8, 0), ModContent.ProjectileType<PupilProj>(), 26, 1, NPC.target, 0);
                            Projectile.NewProjectile(null,new Vector2(player.Center.X + 800, gap.Y + 350 + j * 150), new Vector2(-8, 0), ModContent.ProjectileType<PupilProj>(), 26, 1, NPC.target, 0);
                        }
                    }
                }
                
                for (int i = 0; i < 5; i++)
                {
                    if (NPC.ai[0] == 80 + 43 + 150 * i)
                    {
                        dash = true; NPC.velocity = -Vector2.Normalize(player.Center - NPC.Center) * 5;
                    }
                    if(NPC.ai[0] >= 80 + 43 + 150 * i && NPC.ai[0] <= 100 + 60 + 150 * i && NPC.alpha > 0)
                    {
                        NPC.alpha -= 255 / 18; NPC.dontTakeDamage = false;
                    }
                    if(NPC.alpha < 0)
                    {
                        NPC.alpha = 0;
                    }
                    if (NPC.ai[0] == 100 + 60 + 150 * i)
                    {
                        NPC.velocity = Vector2.Normalize(player.Center - NPC.Center) * 35; SoundEngine.PlaySound(SoundID.Roar);
                    }
                    if (NPC.ai[0] >= 100 + 85 + 150 * i && NPC.ai[0] < 94 + 100 + 150 * i)
                    {
                        NPC.velocity *= 0.9f; 
                    }
                    if (NPC.ai[0] == 94 + 100 + 150 * i)
                    {
                        dash = false;
                    }
                    if(NPC.ai[0] > 194 + 150 * i && NPC.ai[0] < 250 + 43 + 150 * i)
                    {
                        NPC.dontTakeDamage = true;
                        if (NPC.alpha <= 255) { NPC.alpha += 10; }
                        if (NPC.alpha > 255) { NPC.alpha = 255; }
                    }
                }
                if (dash) { NPC.rotation = NPC.velocity.ToRotation(); }
                if (!dash)
                {
                    LerpChase(player.Center + (Vector2.Normalize(NPC.Center - player.Center) *360).RotatedBy(0.003f * NPC.ai[0]), 24f, 0.02f); NPC.rotation = (player.Center - NPC.Center).ToRotation();
                }

                if(NPC.ai[0] == 800)
                {
                    NPC.dontTakeDamage = false;
                    NPC.alpha = 0;ChangePhase(8);
                }
            }
            if(NPC.ai[1] == 12)
            {
                if(NPC.ai[0] == 10)
                {
                    plrpos = player.Center;
                    for(int i = 0; i < 3; i++)
                    {
                        Vector2 pos = player.Center + (i * MathHelper.Pi / 1.5f + 1.5707f).ToRotationVector2() * 700;
                        NPC.NewNPC(null,(int)pos.X, (int)pos.Y, ModContent.NPCType<PupilMinion>(),0,NPC.whoAmI,114514);
                        
                    }
                    rotp = 0;
                }
                if (NPC.ai[0] < 120)
                {
                    float r = 2000 - 1400 * NPC.ai[0] / 120;

                    for (int i = 0; i < 25; i++)
                    {
                        float k = Main.rand.NextFloat(0, 3);
                        Vector2 pos = plrpos + (k + i * MathHelper.TwoPi / 25).ToRotationVector2() * r;
                        Dust d = Dust.NewDustDirect(pos, 0, 0, DustID.Torch); d.noGravity = true; d.fadeIn = 0.3f; d.scale = 1.5f;
                    }
                    if (Vector2.Distance(plrpos, player.Center) > r)
                    {
                        player.velocity = Vector2.Normalize(plrpos - player.Center) * 3f;
                    }
                }
                if (NPC.ai[0] >= 120 && NPC.ai[0] < 260 + 60 + 460)
                {
                    float r = 2000 - 1400;
                    for (int i = 0; i < 25; i++)
                    {
                        float k = Main.rand.NextFloat(0, 3);
                        Vector2 pos = plrpos + (k + i * MathHelper.TwoPi / 25).ToRotationVector2() * r;
                        Dust d = Dust.NewDustDirect(pos, 0, 0, DustID.Torch); d.noGravity = true; d.fadeIn = 0.3f; d.scale = 1.5f;
                    }
                    if (Vector2.Distance(plrpos, player.Center) > r)
                    {
                        player.velocity = Vector2.Normalize(plrpos - player.Center) * 3f;
                    }
                }
                if(NPC.ai[0] > 30 && NPC.ai[0] <150)
                {
                    NPC.rotation = LerpRot(NPC.rotation,rotp,0.06f);
                    LerpChase(plrpos + new Vector2(-390, 700), 16, 0.03f);
                }
                if(NPC.ai[0] >= 90 + 60 && NPC.ai[0] < 180 + 60)
                {
                    if(NPC.ai[0] == 150)
                    {
                       Projectile p =  Projectile.NewProjectileDirect(null,NPC.Center, Vector2.Zero, ModContent.ProjectileType<PupLaser>(), 39, 1, NPC.target, 0, 3);p.frameCounter = NPC.whoAmI;p.rotation = NPC.rotation;
                    }
                    NPC.velocity *= 0;
                    NPC.position += new Vector2(0, -1400 / 90 * ((1.2f * (float)Math.Sin((NPC.ai[0] - 90 - 60) * (3.1416f / 90)))));//(float)Math.Sin( (NPC.ai[0] - 90))/90 * 3.1416f )
                    if(NPC.ai[0] == 180 + 59)
                    {
                        foreach (var proj in Main.projectile)
                        {
                            if (proj.type == ModContent.ProjectileType<PupLaser>() && proj.active)
                            {
                                proj.Kill(); break;
                            }
                        }
                    }
                }
                if(NPC.ai[0] == 260) { rotp = 1.5707f; }
                if (NPC.ai[0] > 30 + 230 && NPC.ai[0] < 150 + 230)
                {
                    NPC.rotation = LerpRot(NPC.rotation, rotp, 0.06f);
                    LerpChase(plrpos + new Vector2(800, -390), 16, 0.03f);
                }
                if (NPC.ai[0] >= 90 + 60 + 230 && NPC.ai[0] < 180 + 60 + 230)
                {
                    if (NPC.ai[0] == 150 + 230)
                    {
                        Projectile p = Projectile.NewProjectileDirect(null,NPC.Center, Vector2.Zero, ModContent.ProjectileType<PupLaser>(), 39, 1, NPC.target, 0, 3); p.frameCounter = NPC.whoAmI; p.rotation = NPC.rotation;
                    }
                    NPC.velocity *= 0;
                    NPC.position += new Vector2(-1722 / 90 * ((1.2f * (float)Math.Sin((NPC.ai[0] - 90 - 60 - 230) * (3.1416f / 90)))),0);//(float)Math.Sin( (NPC.ai[0] - 90))/90 * 3.1416f )
                    if (NPC.ai[0] == 180 + 59 + 230)
                    {
                        foreach (var proj in Main.projectile)
                        {
                            if (proj.type == ModContent.ProjectileType<PupLaser>() && proj.active)
                            {
                                proj.Kill(); break;
                            }
                        }
                    }
                }
                if (NPC.ai[0] > 30 + 460 && NPC.ai[0] < 150 + 460)
                {
                    NPC.rotation = LerpRot(NPC.rotation, 0, 0.06f);
                    LerpChase(plrpos + new Vector2(-800, 0), 16, 0.03f);
                }
                if (NPC.ai[0] >= 90 + 60 + 460 && NPC.ai[0] < 320 + 60 + 460)
                {
                    if (NPC.ai[0] == 150 + 460)
                    {
                        Projectile p = Projectile.NewProjectileDirect(null,NPC.Center, Vector2.Zero, ModContent.ProjectileType<PupLaser>(), 39, 1, NPC.target, 0, 4); p.frameCounter = NPC.whoAmI; p.rotation = NPC.rotation - MathHelper.Pi/2.8f;
                        Projectile p1 = Projectile.NewProjectileDirect(null,NPC.Center, Vector2.Zero, ModContent.ProjectileType<PupLaser>(), 39, 1, NPC.target, 0, 5); p1.frameCounter = NPC.whoAmI; p1.rotation = NPC.rotation + MathHelper.Pi / 2.8f;
                    }
                    NPC.velocity *= 0;

                    if (NPC.ai[0] == 320 + 59 + 460)
                    {
                        foreach (var proj in Main.projectile)
                        {
                            if (proj.type == ModContent.ProjectileType<PupLaser>() && proj.active)
                            {
                                proj.Kill(); 
                            }
                        }
                    }
                }
                if( NPC.ai[0] > 320 + 60 + 460)
                {
                    NPC.rotation += 0.5f;
                    Dust d = Dust.NewDustDirect(NPC.Center, 0, 0, DustID.Torch); d.velocity = Main.rand.NextVector2Circular(16, 16);d.scale = 1.3f;
                }

                if(NPC.ai[0] > 1060)
                {
                    NPC.life = 0;NPC.checkDead();
                    for(int i = 0; i < 5; i++)
                    {
                        SoundEngine.PlaySound(SoundID.NPCDeath6);

                    }
                }
            }
        }
        public override bool CheckDead()
        {
            if (NPC.ai[1] != 12)
            {
                foreach (var proj in Main.projectile)
                {
                    if (proj.type == ModContent.ProjectileType<PupLaser>() && proj.active)
                    {
                        proj.Kill(); break;
                    }
                }
                NPC.alpha = 0;
                NPC.life = 1;
                NPC.dontTakeDamage = true;
                NPC.velocity *= 0;
                ChangePhase(12);
                return false;
            }
            if(NPC.ai[1] == 12)
            {
                return true;
            }
            return false;
        }
        public override bool CheckActive()
        {
            Player player = Main.player[NPC.target];
            if (!player.active)
            {
                NPC.active = false;
            }
            if (Vector2.Distance(player.Center, NPC.Center) > 4500)
            {
                NPC.active = false;
            }
            if(Vector2.Distance(player.Center, NPC.Center) > 2250)
            {
                NPC.timeLeft -= 1;
            }
            return false;
        }
        public override void OnKill()
        {
            if (SkyManager.Instance["PupilBoss"].IsActive())
                SkyManager.Instance.Deactivate("PupilBoss");
            base.OnKill();
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {

            if(NPC.ai[1] == 0)
            {
                if(NPC.ai[0]>= 60 && NPC.ai[0] <= 160)
                {
                    Main.EntitySpriteDraw(bosstex, NPC.Center - Main.screenPosition, null, Color.White * ((NPC.ai[0] - 60)/100), NPC.rotation - 3.1416f/2, bosstex.Size() / 2, 1f, SpriteEffects.None, 0);
                }
                if (NPC.ai[0] > 160)
                {
                    Main.EntitySpriteDraw(bosstex, NPC.Center - Main.screenPosition, null, Color.White, NPC.rotation - 3.1416f / 2, bosstex.Size() / 2, 1f, SpriteEffects.None, 0);
                }
            }

            if (NPC.ai[1] !=0 && NPC.ai[1] < 7)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                for (int k = 1; k < 10; k += 1)
                {
                    Color color = NPC.GetAlpha(Color.White) * (float)(1 - (k / 10));

                    Main.EntitySpriteDraw(Terraria.GameContent.TextureAssets.Npc[NPC.type].Value, NPC.oldPos[k] - Main.screenPosition + new Vector2(NPC.width / 2, NPC.height / 2), null, color, NPC.oldRot[k] - 3.1416f / 2, Terraria.GameContent.TextureAssets.Npc[NPC.type].Value.Size() / 2, (float)(1 - (k / 20)), SpriteEffects.None, 0);

                }
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                Main.EntitySpriteDraw(bosstex, NPC.Center - Main.screenPosition, null, NPC.GetAlpha(Color.White), NPC.rotation - 3.1416f / 2, bosstex.Size() / 2, 1f, SpriteEffects.None, 0);

            }
            if ( NPC.ai[1] >=7)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                for (int k = 1; k < 10; k += 1)
                {
                    Color color = NPC.GetAlpha(Color.White) * (float)(1 - (k / 10));

                    Main.EntitySpriteDraw(SO, NPC.oldPos[k] - Main.screenPosition + new Vector2(NPC.width / 2, NPC.height / 2), null, color, NPC.oldRot[k] - 3.1416f / 2, Terraria.GameContent.TextureAssets.Npc[NPC.type].Value.Size() / 2, (float)(1 - (k / 20)), SpriteEffects.None, 0);

                }
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                Main.EntitySpriteDraw(SO, NPC.Center - Main.screenPosition, null, NPC.GetAlpha(Color.White), NPC.rotation - 3.1416f / 2, bosstex.Size() / 2, 1f, SpriteEffects.None, 0);

            }
            if (flash) { Main.EntitySpriteDraw(tf, NPC.position - Main.screenPosition + new Vector2(36, 36) + new Vector2(0, NPC.gfxOffY), null, flashcolor * 0.02f * (50 - NPC.localAI[2]), 0, new Vector2(36, 36), 10, SpriteEffects.None, 0); }
            if(NPC.ai[1] == 10)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                for (int i = 0; i <= 2; i++)
                {
                    if (NPC.ai[0] > 260 * i && NPC.ai[0] < 60 + 260 * i)
                    {

                        Main.EntitySpriteDraw(lasertex2, NPC.Center - Main.screenPosition, null, Color.White, NPC.rotation - 3.1416f / 2, new Vector2(lasertex2.Size().X / 2, 0), new Vector2(1.5f, 200), SpriteEffects.None, 0);
                    }
                }

                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
            if (NPC.ai[1] == 12)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                for (int i = 0; i <= 1; i++)
                {
                    if (NPC.ai[0] > 30 + 230 * i && NPC.ai[0] < 90 + 60 + 230 * i)
                    {

                        Main.EntitySpriteDraw(lasertex2, NPC.Center - Main.screenPosition, null, Color.White, NPC.rotation - 3.1416f / 2, new Vector2(lasertex2.Size().X / 2, 0), new Vector2(1.5f, 200), SpriteEffects.None, 0);
                    }
                

                }
                if (NPC.ai[0] > 30 + 460 && NPC.ai[0] < 90 + 60 + 460)
                {
                    Main.EntitySpriteDraw(lasertex2, NPC.Center - Main.screenPosition, null, Color.White, NPC.rotation - 3.1416f / 2 - MathHelper.Pi / 2.8f, new Vector2(lasertex2.Size().X / 2, 0), new Vector2(1.5f, 200), SpriteEffects.None, 0);
                    Main.EntitySpriteDraw(lasertex2, NPC.Center - Main.screenPosition, null, Color.White, NPC.rotation - 3.1416f / 2 + MathHelper.Pi /2.8f, new Vector2(lasertex2.Size().X / 2, 0), new Vector2(1.5f, 200), SpriteEffects.None, 0);
                }

                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            }
            return false;
        }
        
    }

}