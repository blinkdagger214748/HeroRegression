

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
using HeroRegression.Slash;

namespace HeroRegression.NPCs.Boss.Heroes
{
	class SummonHero : ModItem
    {
		public override string Texture => "Terraria/Images/Item_" + ItemID.HellstoneBar;
        public override void SetDefaults()
        {
			Item.CloneDefaults(ItemID.SuspiciousLookingEye);
			Item.maxStack = 9999;
            base.SetDefaults();
        }
        public override bool? UseItem(Player player)
        {
			NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<HeroOfForge>());
            return true;
        }
    }
	public class HeroOfForge: ModNPC
	{
		public float exdmg = 1; Player player => Main.player[NPC.target];
		Vector2 toplr => (player.Center - NPC.Center);
		public override void SetStaticDefaults()
		{
			
			DisplayName.SetDefault("Hero of Forge"); DisplayName.AddTranslation(7, "莫尔忒,锻造英雄");
			NPCID.Sets.TrailCacheLength[NPC.type] = 10;//这个是拖尾的长度，也就是绘制a次拖尾
			NPCID.Sets.TrailingMode[NPC.type] = 3;
			Main.npcFrameCount[NPC.type] = 4;
		}
        public override bool CheckActive()
        {
			Player player = Main.player[NPC.target];
			if(player.active == false || Vector2.Distance(player.Center,NPC.Center)> 3200) { NPC.active = false; }
            return false;
        }
        public override void SetDefaults()
		{
			if (Main.expertMode && !Main.masterMode)
			{
				exdmg = 0.62f;
			}
			if (Main.masterMode)
			{
				exdmg = 0.55f;
			}
			NPC.aiStyle = -1;
			NPC.lifeMax = 48000;
			NPC.damage = Main.masterMode?40:Main.expertMode?60:80;
			NPC.defense = 0;
			NPC.knockBackResist = 0;
			NPC.width = 170;
			NPC.height = 170;
			NPC.value = Item.buyPrice(0, 999, 0, 0);
			NPC.npcSlots = 1f;
			NPC.boss = true;
			NPC.lavaImmune = true;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			Music = MusicID.OtherworldlyBoss2;
		

		}
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
			if(!Main.masterMode)
			NPC.lifeMax = (int)(NPC.lifeMax* 0.67114514f);
            if (Main.masterMode)
            {
				NPC.lifeMax = (int)(NPC.lifeMax * 0.5114514f);
			}
            base.ScaleExpertStats(numPlayers, bossLifeScale);
        }

        public void LerpChase(Vector2 pos, float vel, float v)
		{
			Vector2 topos = Vector2.Normalize(pos - NPC.Center);
			NPC.velocity = Vector2.Lerp(NPC.velocity, vel * topos, v);
		    if((pos - NPC.Center).Length()< 40)
            {
				NPC.velocity *= 0.8f;
            }
		}//简单渐进
		List<int> stages = new List<int>() { 1, 2, 3, 4, 5 };
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return (NPC.Center - player.Center).Length() < 60;
        }
        public override void AI()
		{/*
			if (NPC.ai[0] == 2)
			{
				HeroSlashMethod2.NPCslash(NPC.GetSource_FromAI(), 1, -0.6f, 300, 0.6f, 1, 1, 41, 0, 0, NPC.whoAmI, Color.White, ModContent.Request<Texture2D>("Terraria/Images/Extra_89").Value, 600, 0, 1);

			}*/
            if (NPC.ai[0] == 10)
            {
				HeroSlashMethod2.NPCslash(NPC.GetSource_FromAI(), (Main.MouseWorld - player.Center).ToRotation(), 1.9f, 243, 0.7f, 1,1, 1, 5, 0, NPC.whoAmI, Color.White, ModContent.Request<Texture2D>("Terraria/Images/Item_" + ItemID.CopperAxe).Value, 1500,0,6);
			}
			NPC.ai[3]+=0.1f;
			#region 寻敌
			if (NPC.target < 0 || NPC.target >= 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
			{
				NPC.TargetClosest(true);
			}
            switch (NPC.ai[1])
            {
				case 0:Start();break;
				case 1:CommonStage1(); break;
				case 2: CommonStage2(); break;
				case 3: SwordStage(); break;
				case 4: SpearStage(); break;
				case 5: Start(); break;
			}
			#endregion
			NPC.direction = NPC.spriteDirection = Math.Sign(NPC.velocity.X);
			ToPlayer();
			NPC.ai[0]++;

		}
		void Start()
		{
			LerpChase(player.Center + new Vector2(0, -200), 18, 0.02f);
			if (NPC.ai[0] == 1)
            {
				for(int i = 1; i <= 5; i++)
                {
					Projectile.NewProjectile(null, NPC.Center, Vector2.Zero, ModContent.ProjectileType<ForgeIngot>(), 0, 0, 0, NPC.whoAmI, i);
                }
            }
			if((player.Center - NPC.Center).Length() < 300)
            {
				ChangeToCommonAttack();NPC.velocity *= 0;
            }
        }
		void ChangeToSword()
        {NPC.velocity *= 0;
			NPC.ai[0] = 0;NPC.ai[1] = 3;
        }
		public void ChangeToCommonAttack()
        {
			NPC.ai[0] = 0;NPC.ai[1] = Main.rand.Next(2,3);
        }
		void CommonStage1()
        {
			for(int i = 0; i < 8; i++)
            {
				if(NPC.ai[0] == 60 + 45 * i)
                {
					NPC.velocity = toplr.SafeNormalize(Vector2.Zero) * 27;Projectile.NewProjectile(null, NPC.Center, Vector2.Zero, ModContent.ProjectileType<ForgeHammer>(), 16, 0, 0, NPC.whoAmI);
                }
				if (NPC.ai[0] > 68 + 45 * i && NPC.ai[0] < 95 + 45 * i)
				{
					NPC.velocity *= 0.92f;
				}
			}
			if (NPC.ai[0] > 340)
			{
				ChangeToSword();
			}
		}
		void CommonStage2()
		{
			for (int i = 0; i < 5; i++)
			{
				if (NPC.ai[0] == 60 + 55 * i)
				{
					NPC.velocity = toplr.SafeNormalize(Vector2.Zero) * 46; NPC.velocity = toplr.SafeNormalize(Vector2.Zero) * 20; Projectile.NewProjectile(null, NPC.Center, Vector2.Zero, ModContent.ProjectileType<ForgeHammer>(), 16, 0, 0, NPC.whoAmI);
				}
				if (NPC.ai[0] > 85 + 55 * i && NPC.ai[0] < 115 + 55 * i)
				{
					NPC.velocity *= 0.9f;
				}
			}
			if(NPC.ai[0] > 340)
            {
				ChangeToSword();
            }
		}
		void ChangeToSpearStage()
        {
			NPC.velocity *= 0;
			NPC.ai[0] = 0;NPC.ai[1] = 4;
        }
		void SpearStage()
		{
			
			if (NPC.ai[0] == 1)
			{
				int k = Main.rand.Next(stages);
				stages.Remove(k);
				foreach (var proj in Main.projectile)
				{
					if (proj.active && proj.type == ModContent.ProjectileType<ForgeIngot>())
					{
						if (proj.ai[1] == k)
						{
							proj.localAI[1] = 1;
							break;
						}
					}

				}
			}

			if (NPC.ai[0] > 90)
			{
				if (NPC.ai[0] % 30 == 10)
				{
					Projectile.NewProjectile(null, NPC.Center, 10 * (player.Center - NPC.Center).SafeNormalize(Vector2.Zero), ProjectileID.CultistBossFireBall, 20, 1, 0);
				}
				NPC.velocity = Vector2.Lerp(NPC.velocity, (player.Center + (0.04f * NPC.ai[0]).ToRotationVector2() * 270 - NPC.Center).SafeNormalize(Vector2.Zero) * 18f, 0.02f);
            }
		}
		void SwordStage()
		{
			if (NPC.ai[0] == 1)
			{

				int k = Main.rand.Next(stages);
				stages.Remove(k);
				foreach (var proj in Main.projectile) {
				if(proj.active && proj.type == ModContent.ProjectileType<ForgeIngot>())
                    {
						if(proj.ai[1] == k)
                        {
							proj.localAI[1] = 1;
							break;
                        }
                    }
				
				}
			}

			if(NPC.ai[0] > 905)
            {
			
				ChangeToSpearStage();
            }
		}
		public void ToPlayer(int flip = 1)//面朝玩家
        {
			NPC.direction = NPC.spriteDirection = -flip * Math.Sign(NPC.Center.X - player.Center.X);
        }
        public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter++;
			NPC.frame.Y = (int)NPC.frameCounter / 15 * frameHeight;

			if((int)NPC.frameCounter/15 >= 4)
            {
				NPC.frameCounter = 0;
            }
			base.FindFrame(frameHeight);
        }
		public Texture2D GetTex(string path)
		{
			return ModContent.Request<Texture2D>(path).Value;
		}
		void DrawShadow(Texture2D t,Rectangle r,Vector2 origin,SpriteEffects effects)
        {
			for (float i = 0; i < NPCID.Sets.TrailCacheLength[NPC.type]; i += 0.25f)
			{
				Color color27 = Color.White * 0.5f;
				color27.A = 100;
				color27 *= (float)(NPCID.Sets.TrailCacheLength[NPC.type] - i) / NPCID.Sets.TrailCacheLength[NPC.type];
				int max0 = (int)i - 1;//Math.Max((int)i - 1, 0);
				if (max0 < 0)
					max0 = 0;
				Vector2 center = Vector2.Lerp(NPC.oldPos[(int)i], NPC.oldPos[max0], 1 - i % 1);
				center += NPC.Size / 2;
				Main.EntitySpriteDraw(t, center - Main.screenPosition + new Vector2(0, NPC.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(r), color27,0, origin, NPC.scale, effects, 0);
			}
		}
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
			Texture2D hero = GetTex("HeroRegression/NPCs/Boss/Heroes/HeroOfForge"); Texture2D hero2 = GetTex("HeroRegression/NPCs/Boss/Heroes/HOFshadow");
			DrawShadow(hero2, NPC.frame, new Vector2(hero.Width / 2, hero.Height / 8), NPC.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
			Main.EntitySpriteDraw(hero, NPC.Center - Main.screenPosition, NPC.frame, Color.White, 0, new Vector2(hero.Width / 2, hero.Height / 8), 1, NPC.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
			return false;
		}
    }
}






