using Terraria;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using HeroRegression.Projectiles;
using System;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using HeroRegression.Projectiles.Boss.FlameReaction;

namespace HeroRegression.NPCs.Boss.FlameReaction
{
    [AutoloadBossHead]
    public class FlameReactionBoss : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flame Reaction");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "焰色反应");
            NPCID.Sets.TrailCacheLength[NPC.type] = 24;
            NPCID.Sets.TrailingMode[NPC.type] = 3;
        
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot) { 
            return Vector2.Distance(target.Center, NPC.Center) <= 25f;
        }
        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 100;
            NPC.friendly = false;
            NPC.damage = Main.expertMode ?16 : 20;
            NPC.defense = 10;
            NPC.lifeMax = Main.expertMode ? 1500 : 2000;
            NPC.boss = true;
            NPC.knockBackResist = 0;
            NPC.noGravity = true;
            NPC.netAlways = true;
            NPC.noTileCollide = true;
            NPC.netUpdate = true;
            NPC.aiStyle = -1;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * .66666666666666667f * bossLifeScale);
        }
   

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D tex = HeroRegression.GetTex("HeroRegression/Textures/FlameReactionTex");
            Vector2 ori = new Vector2(32, 32);
            Vector2 pos1 = NPC.Center - Main.screenPosition;
            if (HasTrail)
            {
                for (int i = 0; i <= 23; i += 1)
                {
                    Vector2 pos2 = NPC.oldPos[i] + new Vector2(50, 50) - Main.screenPosition + new Vector2(0, NPC.gfxOffY);
                    if (!P2)
                    {
                        drawColor = Color.Gray * ((float)(NPC.oldPos.Length - i) / (NPC.oldPos.Length));
                    }
                    if (P2)
                    {
                        drawColor = (Dash ? Color.White : Main.DiscoColor) * ((float)(NPC.oldPos.Length - i) / (NPC.oldPos.Length));
                    }
                    spriteBatch.End();
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                    Main.EntitySpriteDraw(tex, pos2, null, drawColor, NPC.oldRot[i], ori, 1f, SpriteEffects.None, 0);
                    spriteBatch.End();
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
                }
            }
            Main.EntitySpriteDraw(tex, pos1, null, Color.White, NPC.rotation, ori, 1f, SpriteEffects.None, 0);
            return true;
        }
        public static NPC FR = null;
        public bool P2;
        public bool Dash;
        public bool HasTrail;
        public float SpellTimer;
        public float GlobalTimer;
        public Vector2 DashPos;
        private enum NState
        {
            P1Dash1,
            P1Dash2,
            P1RainbowStar,
            P2Switch,
            P2ArcMotion,
            P2Dash,
            P2Teleport,
            P2Shoot
        }
        private NState State
        {
            get { return (NState)(int)NPC.ai[1]; }
            set { NPC.ai[1] = (int)value; }
        }
        private void SwitchTo(NState state)
        {
            State = state;
        }
        public override void AI()
        {
            if (NPC.localAI[1] == 0)
            {
                if (FR == null || !FR.active)
                {
                    FR = NPC;
                }
                NPC.localAI[1] = 1;
            }
            Music = P2 ? MusicLoader.GetMusicSlot("HeroRegression/Sounds/Music/FlameReaction") : MusicID.Boss2;
            Lighting.AddLight(NPC.Center, new Vector3(1, 1, 1));
            NPC.direction = Utils.ToDirectionInt(false);
            NPC.spriteDirection = NPC.direction;
            NPC.TargetClosest(true);
            if (NPC.timeLeft < 1800 && !Main.LocalPlayer.dead)
            {
                NPC.timeLeft = 1800;
            }
            if (Main.LocalPlayer.dead)
            {
                NPC.active = false;
            }
            SpellTimer++;
            GlobalTimer++;
            if (GlobalTimer % 1 == 0)
            {
                float dustD = Math.Min(GlobalTimer / 2f, 60);
                float dustR1 = MathHelper.ToRadians(GlobalTimer);
                for (int i = 0; i <= 300; i += 60)
                {
                    float dustR2 = dustR1 + MathHelper.ToRadians(i);
                    for (int j = -60; j <= 60; j += 4)
                    {
                        float dustR3 = dustR2 + MathHelper.ToRadians(j);
                        Vector2 dustPos = dustR3.ToRotationVector2() * dustD / (float)Math.Cos(MathHelper.ToRadians(j));
                        Dust dust = Dust.NewDustDirect(dustPos + NPC.Center, 1, 1, DustID.WhiteTorch, 0, 0, 0);
                        dust.color = P2 ? Main.DiscoColor : Color.White;
                        dust.velocity = NPC.velocity;
                        dust.noGravity = true;
                        dust.scale = .9f;
                        dust.fadeIn = .6f;
                    }
                }
            }
            switch (State)
            {
                case NState.P1Dash1:
                    {
                        HasTrail = true;
                        if (Vector2.Distance((Main.LocalPlayer.Center + new Vector2(NPC.Center.X >= Main.LocalPlayer.Center.X ? 280 : -280, 280)), NPC.Center) <= 50 || SpellTimer >= 120)
                        {
                            SpellTimer = 0;
                            SwitchTo(NState.P1Dash2);
                        }
                        else
                        {
                            NPC.velocity = ((Main.LocalPlayer.Center + new Vector2(NPC.Center.X >= Main.LocalPlayer.Center.X ? 280 : -280, 280)) - NPC.Center) / 33f;
                        }
                        if (NPC.life <= NPC.lifeMax * .7f && !P2)
                        {
                            SpellTimer = 0;
                            SwitchTo(NState.P2Switch);
                        }
                        break;
                    }
                case NState.P1Dash2:
                    {
                        if (SpellTimer == 1)
                        {
                            
                             SoundEngine.PlaySound(new("HeroRegression/Sounds/Custom/roar"), NPC.Center);
                            DashPos = Main.LocalPlayer.Center + Vector2.Normalize(-NPC.Center + Main.LocalPlayer.Center) * (Vector2.Distance(NPC.Center, Main.LocalPlayer.Center) + 500);
                        }
                        if (SpellTimer > 1)
                        {
                            NPC.velocity = (DashPos - NPC.Center) / 30;
                            if (SpellTimer == 60)
                            {
                                SpellTimer = 0;
                                HasTrail = false;
                                SwitchTo(NState.P1RainbowStar);
                            }
                        }
                        if (NPC.life <= NPC.lifeMax * .7f && !P2)
                        {
                            SpellTimer = 0;
                            SwitchTo(NState.P2Switch);
                        }
                        break;
                    }
                case NState.P1RainbowStar:
                    {
                        NPC.velocity = Vector2.Lerp(NPC.velocity, (Main.LocalPlayer.Center + new Vector2(0, -400) - NPC.Center) / 20, .1f);
                        if (SpellTimer % 15 == 0)
                        {
                            SoundEngine.PlaySound(SoundID.Item9, NPC.Center);
                            Projectile.NewProjectile(null,Main.LocalPlayer.Center + new Vector2(Main.rand.NextFloat(-480, 480), -800), new Vector2(0, 3), ModContent.ProjectileType<FlameReactionStar>(), 5, .1f, Main.myPlayer);
                        }
                        if (SpellTimer > 300)
                        {
                            SpellTimer = 0;
                            HasTrail = true;
                            SwitchTo(NState.P1Dash1);
                        }
                        if (NPC.life <= NPC.lifeMax * .7f && !P2)
                        {
                            SpellTimer = 0;
                            SwitchTo(NState.P2Switch);
                        }
                        break;
                    }
                case NState.P2Switch:
                    {
                        P2 = true;
                        NPC.dontTakeDamage = true;
                        if (SpellTimer == 1)
                        {
                            NPC.Center = Main.LocalPlayer.Center + new Vector2(0, -400);
                            NPC.velocity *= 0;
                            SoundEngine.PlaySound(SoundID.Item4, NPC.Center);
                        }
                        if (Main.dayTime)
                        {
                            Main.time += 120;
                        }
                        HasTrail = true;
                        if (NPC.life < NPC.lifeMax)
                        {
                            NPC.life += (int)(NPC.lifeMax * .002f);
                        }
                        if (NPC.life >= NPC.lifeMax)
                        {
                            if (!Main.dayTime)
                            {
                                NPC.life = NPC.lifeMax;
                                SpellTimer = 0;
                                NPC.dontTakeDamage = false;
                                SwitchTo(NState.P2ArcMotion);
                            }
                        }
                        break;
                    }
                case NState.P2ArcMotion:
                    {
                        if (SpellTimer == 1)
                        {
                            SoundEngine.PlaySound(SoundID.Item9, NPC.Center);
                            NPC.Center = Main.LocalPlayer.Center + new Vector2(Main.LocalPlayer.Center.X <= NPC.Center.X ? -500 : 500, 0);
                            if (NPC.life >= NPC.lifeMax * .5f)
                            {
                                for (int i = 1; i <= 3; i++)
                                {
                                    Projectile.NewProjectile(null,NPC.Center, Vector2.Zero, ModContent.ProjectileType<FlameReactionBulletP2>(), 5, .1f, Main.myPlayer, i);
                                }
                            }
                            if (NPC.life < NPC.lifeMax * .5f)
                            {
                                for (int i = 1; i <= 4; i++)
                                {
                                    Projectile.NewProjectile(null,NPC.Center, Vector2.Zero, ModContent.ProjectileType<FlameReactionBulletP2>(), 5, .1f, Main.myPlayer, i);
                                }
                            }
                        }
                        if (SpellTimer >1)
                        {
                            float Rot = (NPC.Center - Main.LocalPlayer.Center).ToRotation();
                            if (Math.Abs(Main.LocalPlayer.Center.X - NPC.Center.X)>= 32f)
                            {
                                Rot += Main.LocalPlayer.Center.X <= NPC.Center.X ? -MathHelper.ToRadians(.2f * (float)Math.Log(SpellTimer / 3f, Math.E)) : MathHelper.ToRadians(.2f * (float)Math.Log(SpellTimer / 3f, Math.E));
                                Vector2 LerpPos = Main.LocalPlayer.Center + Rot.ToRotationVector2() * 500;
                                NPC.velocity = (LerpPos - NPC.Center);
                            }
                            else
                            {
                                SpellTimer = 0;
                                Dash = true;
                                NPC.velocity *= 0;
                                SwitchTo(NState.P2Dash);
                            }
                        }
                        break;
                    }
                case NState.P2Dash:
                    {
                        if (SpellTimer == 1)
                        {
                            DashPos = Main.LocalPlayer.Center + Vector2.Normalize(-NPC.Center + Main.LocalPlayer.Center) * (Vector2.Distance(NPC.Center, Main.LocalPlayer.Center) + 500);
                        }
                        if (SpellTimer < 30)
                        {
                            NPC.velocity = (Main.LocalPlayer.Center + new Vector2(0, -500) - NPC.Center) / 10;
                        }
                        if (SpellTimer == 30)
                        {
                            SoundEngine.PlaySound(new("HeroRegression/Sounds/Custom/roar"), NPC.Center);
                        }
                        if (SpellTimer > 30)
                        {
                            NPC.velocity = (DashPos - NPC.Center) / 30;
                            if (SpellTimer == 90)
                            {
                                SoundEngine.PlaySound(new("HeroRegression/Sounds/Custom/roar"), NPC.Center);
                                Projectile.NewProjectile(null,Main.LocalPlayer.Center + new Vector2(0, -150), Vector2.Zero, ModContent.ProjectileType<FallingSlash>(), 11, .1f);
                                SpellTimer = 0;
                                Dash = false;
                                SwitchTo(NState.P2Teleport);
                            }
                        }
                        break;
                    }
                case NState.P2Teleport:
                    {
                        if (SpellTimer < 300)
                        {
                            NPC.velocity = Vector2.Normalize(Main.LocalPlayer.Center - NPC.Center) * (Vector2.Distance(NPC.Center, Main.LocalPlayer.Center) > 320 ? 8f : Math.Max(Vector2.Distance(NPC.Center, Main.LocalPlayer.Center) / 40, 3f));
                        }
                        if (SpellTimer == 300)
                        {
                            SoundEngine.PlaySound(SoundID.Item4, NPC.Center);
                            NPC.velocity *= 0;
                            NPC.Center = Main.LocalPlayer.Center + new Vector2(-500, 0);
                        }
                        if (NPC.life >= NPC.lifeMax * .5f)
                        {
                            if (SpellTimer == 330 || SpellTimer == 340 || SpellTimer == 350)
                            {
                                SoundEngine.PlaySound(SoundID.Item9, NPC.Center);
                                Vector2 shoot1 = Vector2.Normalize(Main.LocalPlayer.Center - NPC.Center) * 15f;
                                Projectile.NewProjectile(null,NPC.Center, shoot1, ModContent.ProjectileType<FlameReactionBulletP2>(), 5, .1f, Main.myPlayer, 5);
                            }
                        }
                        if (NPC.life < NPC.lifeMax * .5f)
                        {
                            if (SpellTimer == 330 || SpellTimer == 337 || SpellTimer == 344 || SpellTimer == 351)
                            {
                                SoundEngine.PlaySound(SoundID.Item9, NPC.Center);
                                Vector2 shoot1 = Vector2.Normalize(Main.LocalPlayer.Center - NPC.Center) * 15f;
                                Projectile.NewProjectile(null,NPC.Center, shoot1, ModContent.ProjectileType<FlameReactionBulletP2>(), 5, .1f, Main.myPlayer, 5);
                            }
                        }
                        if (SpellTimer == 360)
                        {
                            SoundEngine.PlaySound(SoundID.Item4, NPC.Center);
                            NPC.velocity *= 0;
                            NPC.Center = Main.LocalPlayer.Center + new Vector2(500, 0);
                        }
                        if (SpellTimer == 390)
                        {
                            Dash = true;
                            SoundEngine.PlaySound(new("HeroRegression/Sounds/Custom/roar"), NPC.Center);
                            NPC.velocity = new Vector2(-15, 0);
                        }
                        if (SpellTimer > 390)
                        {
                            if (NPC.Center.X <= Main.LocalPlayer.Center.X - 500 || SpellTimer >= 500)
                            {
                                Dash = false;
                                SpellTimer = 0;
                                NPC.velocity *= 0;
                                SwitchTo(NState.P2ArcMotion);
                            }
                        }
                        break;
                    }
            }
        }

    }
}
