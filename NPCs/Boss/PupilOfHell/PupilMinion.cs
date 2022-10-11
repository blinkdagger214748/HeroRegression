using Terraria;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.DataStructures;
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
using HeroRegression.HeroPlayers;


namespace HeroRegression.NPCs.Boss.PupilOfHell
{
   
    public class PupilMinion : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Minion");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "僚机");
        }

        public override void SetDefaults()
        {
            NPC.width = 52;
            NPC.height = 54;
            NPC.friendly = false;
            NPC.damage = 80;
            NPC.defense = 35;
            NPC.lifeMax = 460;
           
            NPC.knockBackResist = 0;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.noTileCollide = true;
            

        }
        #region 自制简易方法
        public void LerpChase(Vector2 pos,float v)
        {
            NPC.Center = Vector2.Lerp(NPC.Center,pos,v);
        }//简单渐进

        #endregion
        public override void AI()
        {
            NPC.localAI[0]++;
            NPC boss = Main.npc[(int)NPC.ai[0]];
            Player player = Main.player[boss.target];
            if (NPC.ai[1] != 114514)
            {
                Vector2 pos = boss.Center + (NPC.ai[1] * MathHelper.Pi / 3).ToRotationVector2() * 500;
                Vector2 pos1 = boss.Center + (NPC.ai[1] * MathHelper.Pi / 3).ToRotationVector2() * 300;
                if (NPC.localAI[0] < 200)
                    LerpChase(pos, 0.04f);
             
                if (NPC.localAI[0] == 1)
                {
                    Projectile.NewProjectile(null,NPC.Center, Vector2.Zero, ModContent.ProjectileType<Projectiles.Boss.PupilOfHell.Chains>(), 0, 0, 0, NPC.ai[0], NPC.whoAmI);
                }
                if (NPC.localAI[0] % 25 == 0)
                {
                    boss.life += 40; boss.lifeMax += 40;
                    boss.HealEffect(40, true);
                }
                if (NPC.localAI[0] > 200)
                { LerpChase(pos1, 0.06f); NPC.ai[1] += 0.05f; }
                for (int k = 0; k < 10; k++)
                {
                    if (NPC.localAI[0] == 240 + 30 * k)
                    {
                        Vector2 vel = Vector2.Normalize(player.Center - NPC.Center);
                        Projectile.NewProjectile(null,NPC.Center, 10 * vel, ModContent.ProjectileType<PupilProj>(), 34, 1, 0, 0);
                    }
                }
            }
            if(NPC.ai[1] == 114514)
            {
                for(int k = 0;k < 99; k++)
                {
                    if(NPC.localAI[0] == 190 + 25 * k)
                    {
                        for(int i = 0; i < 3;i++) 
                        {
                            float rot = 0.25f * k + i * MathHelper.TwoPi / 3;
                            Projectile.NewProjectile(null,NPC.Center, rot.ToRotationVector2() * 8.9f, ModContent.ProjectileType<PupilProj>(), 10, 0, 0);
                        }
                        break;
                    }
                }
                NPC.dontTakeDamage = true;
            }
            if (!boss.active)
            {
                NPC.active = false;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D bosstex = Terraria.GameContent.TextureAssets.Npc[NPC.type].Value;
            Main.EntitySpriteDraw(bosstex, NPC.Center - Main.screenPosition, null, Color.White * 1, 0, bosstex.Size() / 2, 1f, SpriteEffects.None, 0);
            return false;
        }

    }

}