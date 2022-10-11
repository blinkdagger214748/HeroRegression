

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using System.Collections.Generic;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.Threading;
using Terraria.Localization;
using HeroRegression.NPCs;
using HeroRegression.Projectiles.Boss.Heros;
namespace HeroRegression.NPCs.Boss.Heroes
{
	public class HeroOfAssassin: ModNPC
	{
		public float exdmg = 1;
		public override void SetStaticDefaults()
		{
			
			DisplayName.SetDefault("Hero of assassin");
			DisplayName.AddTranslation(7,"潜行英雄");
			NPCID.Sets.TrailCacheLength[NPC.type] = 10;//这个是拖尾的长度，也就是绘制a次拖尾
			NPCID.Sets.TrailingMode[NPC.type] = 3;
		}
        public override bool CheckActive()
        {
			Player player = Main.player[NPC.target];
			if(player.active == false || Vector2.Distance(player.Center,NPC.Center)> 3200) { NPC.active = false; }
            return false;
        }
        public override void SetDefaults()
		{
			NPC.aiStyle = -1;
			NPC.lifeMax =38000;
			NPC.damage = 100;
			NPC.defense = 0;
			NPC.knockBackResist = 0;
			NPC.width = 70;
			NPC.height = 70;
			NPC.value = Item.buyPrice(0, 0, 0, 0);
			NPC.npcSlots = 1f;
			NPC.boss = true;
			NPC.lavaImmune = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			Music = MusicID.OtherworldlyWoF;
			NPC.frame.Y = 7;

		}
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
			NPC.lifeMax = (int)(NPC.lifeMax* bossLifeScale * 0.67114514f);
            base.ScaleExpertStats(numPlayers, bossLifeScale);
        }

        public void LerpChase(Vector2 pos, float vel, float v)
		{
			Vector2 topos = Vector2.Normalize(pos - NPC.Center);
			NPC.velocity = Vector2.Lerp(NPC.velocity, vel * topos, v);
		}//简单渐进
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
			if (Vector2.Distance(target.Center, NPC.Center) < 80) { return true; }
			else return false;
        }
        public override void AI()
		{
            if (Main.expertMode)
            {
				exdmg = 0.42f;
            }
            else { exdmg = 1; }
			#region 寻敌
			if (NPC.target < 0 || NPC.target >= 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
			{
				NPC.TargetClosest(true);
			}
			Player player = Main.player[NPC.target];
			#endregion
			NPC.direction = NPC.spriteDirection = Math.Sign(NPC.velocity.X);
			NPC.frameCounter++;
			if(NPC.frameCounter == 20)
            {
				NPC.frame.Y += 1;NPC.frameCounter = 0;
            }
			if(NPC.frame.Y >15) { NPC.frame.Y = 7; }
			NPC.ai[0]++;
			Vector2 pls = player.Center;
			if (NPC.ai[2] == 1)
			{
				NPC.dontTakeDamage = true;
			}
			else
			{
				NPC.dontTakeDamage = false;
			}
			if (NPC.life >= NPC.lifeMax * 0.5f)
			{
				switch (NPC.ai[1])
				{
					case 0:
						NPC.ai[2] = 1;
						Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.WhiteTorch, 0, 0, 90, default, 0.9f);
						if (NPC.ai[0] < 100)
							LerpChase(player.Center + Vector2.Normalize(NPC.Center - player.Center) * 420, 28, 0.03f);
						if (NPC.ai[0] == 100)
						{
							NPC.velocity = Vector2.Normalize(pls - NPC.Center) * 37f;
						}
						for (int i = 0; i < 15; i++)
							if (NPC.ai[0] == 100 + 3 * i)
							{
								Projectile p = Projectile.NewProjectileDirect(null,NPC.Center, new Vector2(0, 0), ModContent.ProjectileType<AssassinSlash>(), (int)(NPC.damage / 2 * exdmg), 1, 0);
								p.rotation = Main.rand.NextFloat(0, MathHelper.TwoPi);
							}
						if (NPC.ai[0] > 113)
						{
							NPC.velocity *= 0.941f;
						}
						if (NPC.ai[0] == 145)
						{
							NPC.ai[0] = 0; NPC.ai[1] += 1;
						}
						break;
					case 1:
						NPC.ai[2] = 0;
						if (NPC.ai[0] < 13)
							for (int i = 0; i < 5; i++)
							{ Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.WhiteTorch, 0, 0, 0, Color.Black, 2.8f); }
						if (NPC.ai[0] % 5 == 0) { Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.WhiteTorch, 0, 0, 0, Color.Black, 2.2f); }
						if (NPC.ai[0] < 50)
							LerpChase(player.Center + Vector2.Normalize(NPC.Center - player.Center) * 420, 28, 0.024f);
						if (NPC.ai[0] > 50)
							LerpChase(player.Center + Vector2.Normalize(NPC.Center - player.Center) * 420, 28, 0.01f);
						for (int k = 0; k < 10; k++)
						{
							if (NPC.ai[0] == 90 + 30 * k)
							{
								Projectile.NewProjectile(null,NPC.Center, Vector2.Normalize(pls - NPC.Center) * 10, ModContent.ProjectileType<AssassinBolt>(), (int)(NPC.damage / 2 * exdmg), 1, 0);
							}
						}
						if (NPC.ai[0] == 250) { NPC.ai[0] = 0; NPC.ai[1]++; }
						break;
					case 2:
						NPC.ai[2] = 1;
						if (NPC.ai[0] < 70)
						{
							LerpChase(pls + new Vector2(0, 450), 26, 0.024f);
						}
						if (NPC.ai[0] == 70)
						{
							NPC.velocity = new Vector2(0, -30);

						}
						for (int i = 0; i < 15; i++)
						{
							if (NPC.ai[0] == 70 + 5 * i)
							{
								Projectile.NewProjectile(null,NPC.Center, new Vector2(10, 0), ModContent.ProjectileType<AssassinSword>(), (int)(NPC.damage / 2 * exdmg), 1, 0);
								Projectile.NewProjectile(null,NPC.Center, new Vector2(-10, 0), ModContent.ProjectileType<AssassinSword>(), (int)(NPC.damage / 2 * exdmg), 1, 0);
							}
							if (NPC.ai[0] == 70 + 3 * i)
							{
								Projectile p = Projectile.NewProjectileDirect(null,NPC.Center, new Vector2(0, 0), ModContent.ProjectileType<AssassinSlash>(), (int)(NPC.damage / 2 * exdmg), 1, 0);
								p.rotation = Main.rand.NextFloat(0, MathHelper.TwoPi);
							}
						}
						if (NPC.ai[0] > 90)
						{
							NPC.ai[0] = 0; NPC.ai[1]++;
						}
						break;
					case 3:
						LerpChase(player.Center + Vector2.Normalize(NPC.Center - player.Center) * 320, 22, 0.05f); NPC.ai[2] = 0;
						if (NPC.ai[0] < 13)
							for (int i = 0; i < 5; i++)
							{ Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.WhiteTorch, 0, 0, 0, Color.Black, 2.8f); }
						if (NPC.ai[0] % 5 == 0) { Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.WhiteTorch, 0, 0, 0, Color.Black, 2.2f); }
						for (int s = 0; s < 15; s++)
						{
							if (NPC.ai[0] == 30 + 25 * s)
							{
								Projectile.NewProjectile(null,NPC.Center, Vector2.Normalize(pls - NPC.Center) * 2, ModContent.ProjectileType<AssassinKnife>(), (int)(NPC.damage / 2 * exdmg), 1, 0);
							}
						}
						if (NPC.ai[0] == 200)
						{
							NPC.ai[0] = 0; NPC.ai[1]++;
						}
						break;
					case 4:
						LerpChase(player.Center + Vector2.Normalize(NPC.Center - player.Center) * 320, 25, 0.02f); NPC.ai[2] = 0;
						for(int k = 0;k< 25; k++)
                        {
							if(NPC.ai[0] == 30 + 10 * k)
                            {
								float rot = -player.velocity.ToRotation()+ Main.rand.NextFloat(-1.3f, 1.3f);
								Projectile.NewProjectile(null,player.Center + rot.ToRotationVector2() * 1020, -rot.ToRotationVector2() * 0.001f, ModContent.ProjectileType<AssassinSlash2>(), (int)(NPC.damage / 2 * exdmg), 1, 0);
                            }
                        }
						
						if(NPC.ai[0] > 180)
                        {
							NPC.ai[0] = 0;NPC.ai[1] = 0;
                        }
						break;
				}
			}
			if(NPC.life < NPC.lifeMax * 0.5f && NPC.ai[3] !=1)
            {
				if(NPC.ai[1] < 5) { NPC.ai[1] = 5; NPC.ai[0] = 0; }
                switch (NPC.ai[1])
                {
					case 5:
						if(NPC.ai[0] == 60)
                        {
							Projectile.NewProjectile(null,NPC.Center, Vector2.Zero, ModContent.ProjectileType<AssassinSickle>(), (int)(NPC.damage / 2 * exdmg), 1, 0,NPC.whoAmI);
                        }
						for (int s = 0; s < 900; s++)
						{
							float rg = player.velocity.ToRotation() + MathHelper.Pi / 2;
							Vector2 pos1 = rg.ToRotationVector2() * 250 + player.Center;
							if (NPC.ai[0] > 30 + 140 * s && NPC.ai[0] < 140 + 140 * s)
							{ LerpChase(pos1, 25, 0.08f); }
							if (NPC.ai[0] == 140 + 140 * s)
                            {
								NPC.velocity = Vector2.Normalize(pls - NPC.Center) * 14f + player.velocity *2.2f ;
                            }
							for(int s2 = 0;s2 < 10; s2++)
                            {
								if(NPC.ai[0] == 140 + 140 * s + 3 * s2)
								{
									Projectile p = Projectile.NewProjectileDirect(null,NPC.Center, new Vector2(0, 0), ModContent.ProjectileType<AssassinSlash>(), (int)(NPC.damage / 2 * exdmg), 1, 0);
									p.rotation = Main.rand.NextFloat(0, MathHelper.TwoPi);
								}
                            }
						}
						if(NPC.ai[0] > 480)
                        {

							NPC.ai[0] = 0;
							NPC.ai[1]++;
                        }
						break;
					case 6:
						if(NPC.ai[0] < 114) { LerpChase(pls + new Vector2(0, -300),15,0.08f);
							for (int i = 0; i < 300; i++)
							{
								if (NPC.ai[0] ==  15 * i)
								{
									float r = 0.1f * NPC.ai[0];
									for (int a = 0; a < 5; a++)
									{
										float r1 =r+ a * MathHelper.Pi / 2.5f;
										Projectile.NewProjectile(null,NPC.Center, r1.ToRotationVector2() *12f, ModContent.ProjectileType<AssassinSickle3>(), 20, 1, 0);
									}
								}
							}
						}
						if(NPC.ai[0] == 10)
                        {
							Projectile.NewProjectile(null,NPC.Center, Vector2.Zero, ModContent.ProjectileType<AssassinSickle2>(), 20, 1, 0, NPC.whoAmI);
                        }
						for(int s = 0;s < 5; s++)
                        {
							if(NPC.ai[0] >= 114 +  100 * s && NPC.ai[0] < 180 + 100 * s)
							{
								 NPC.ai[2] = 1;
								LerpChase(pls + (NPC.Center - pls).ToRotation().ToRotationVector2() * 550, 24f, 0.1f);
                            }
							
							if (NPC.ai[0] == 180 + 100 * s)
							{
								
								NPC.velocity = Vector2.Normalize(player.Center - NPC.Center) * 35f;
							}
							if(NPC.ai[0] >= 180 + 100 * s && NPC.ai[0] < 214 + 100 * s)
                            {
								NPC.ai[2] = 0;
								for (int i = 0; i < 300; i++)
								{
									if (NPC.ai[0] == 180 + 100 * s+ 10 * i)
									{

										for (int a = 0; a < 2; a++)
										{
											float r1 =NPC.velocity.ToRotation() + 1.57f+ a * MathHelper.Pi ;
											Projectile.NewProjectile(null,NPC.Center, r1.ToRotationVector2() * 0.8f, ModContent.ProjectileType<AssassinSickle3>(), 20, 1, 0);
										}
									}
								}
							}
						}
						if(NPC.ai[0] > 630) { NPC.velocity *= 0.9f; }
						if(NPC.ai[0] > 700)
                        {
							NPC.ai[0] = 0;NPC.ai[1]++;
                        }
						break;
					case 7:
						NPC.ai[2] = 0;
						LerpChase(player.Center + new Vector2(0,-240), 25, 0.2f); NPC.ai[2] = 0;
						for (int k = 0; k < 30; k++)
						{
							if (NPC.ai[0] == 30 + 8 * k)
							{
								float rot = -player.velocity.ToRotation() + Main.rand.NextFloat(-1.3f, 1.3f);
								Projectile.NewProjectile(null,player.Center + rot.ToRotationVector2() * 1020, -rot.ToRotationVector2() * 0.001f, ModContent.ProjectileType<AssassinSlash2>(), (int)(NPC.damage / 2 * exdmg), 1, 0);
							}
						}
			
						if (NPC.ai[0] > 200)
						{
							NPC.ai[0] = 0; NPC.ai[1] = 0;
						}
						break;
				}
            }
			if(NPC.ai[3] == 1)
			{
				NPC.dontTakeDamage = true;
				if (NPC.ai[0] == 1)
                {
					NPC.velocity *= 0;
                }
				for(int k = 0;k< 1000; k++)
                {
					if(NPC.ai[0] == 100 + 55 * k)
                    {
						NPC.velocity = Vector2.Normalize(pls - NPC.Center) * 45f;
                    }
					if(NPC.ai[0] == 100 + 25 * k && NPC.ai[0] < 475)
                    {
						for (int a = 0; a < 2; a++)
						{
							float r1 = NPC.velocity.ToRotation() + 1.57f + a * MathHelper.Pi;
							Projectile.NewProjectile(null,NPC.Center, r1.ToRotationVector2() * 0.8f, ModContent.ProjectileType<AssassinSickle3>(), 20, 1, 0);
						}
					}
                }
				if(NPC.ai[0] > 355)
                {
					NPC.velocity *= 0.9f;
                }
				if(NPC.ai[0] ==514)
                {
					NPC.life = 0;
					NPC.checkDead();
					for (int i = 0; i < 15; i++)
					{ Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.WhiteTorch, 0, 0, 0, Color.Black, 2.8f); }
				}
            }
		}

		public override bool CheckDead()
		{
			if (NPC.ai[3] != 1)
			{
				NPC.dontTakeDamage = true;
				NPC.life = 1;
				NPC.ai[3] = 1;
				NPC.ai[0] = 0;
				NPC.ai[1] = 99;
				return false;
			}
			else { return true; }
		}
		public Texture2D GetTex(string path)
		{
			return ModContent.Request<Texture2D>(path).Value;
		}
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
			Texture2D hero = GetTex("HeroRegression/NPCs/Boss/Heroes/HeroOfAssassin");
			int cut = 16;
			Vector2 size = new Vector2(hero.Size().X, hero.Size().Y / cut);
			Vector2 drawPos = NPC.position - Main.screenPosition + new Vector2(NPC.width / 2, NPC.height / 2) + new Vector2(0, NPC.gfxOffY);
			if (NPC.ai[2] != 0) { Main.EntitySpriteDraw(hero, drawPos, new Rectangle(0, (int)NPC.frame.Y * (int)size.Y, (int)size.X, (int)size.Y), Color.White * 0.1f, 0, size / 2, 1, NPC.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0); }
			if (NPC.ai[2] == 0)
			{
				Texture2D hero2 = GetTex("HeroRegression/NPCs/Boss/Heroes/HOAshadow");
				for (int i = 1; i < NPC.oldPos.Length; i += 2)//trail
				{
					Vector2 drawPos1 = NPC.oldPos[i] - Main.screenPosition + new Vector2(NPC.width / 2, NPC.height / 2) + new Vector2(0, NPC.gfxOffY);
					Color color = Color.White * ((float)(NPC.oldPos.Length - i) / (float)NPC.oldPos.Length);
					float Sc = 1f;
					Main.EntitySpriteDraw(hero2, drawPos1, new Rectangle(0, (int)NPC.frame.Y * (int)size.Y, (int)size.X, (int)size.Y), color, 0, size / 2, Sc, NPC.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
				}
				Main.EntitySpriteDraw(hero, drawPos, new Rectangle(0, (int)NPC.frame.Y * (int)size.Y, (int)size.X, (int)size.Y), Color.White, 0, size / 2, 1, NPC.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
			}
			return false;
		}
    }
}






