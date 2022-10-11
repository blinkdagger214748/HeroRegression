using Terraria;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using HeroRegression.Projectiles.Boss.SeedsOfOrigin;
using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
 
using HeroRegression.HeroPlayers;

namespace HeroRegression.NPCs.Boss.SeedsOfOrigin
{
    public class OriginGuard : ModNPC
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Origin Guard");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "起源守卫");
          
        }

        public override void SetDefaults()
        {
            NPC.width = 42;
            NPC.height = 36;
            NPC.damage = 15;
            NPC.defense = 2;
            NPC.lifeMax = 30;
            NPC.dontTakeDamage = true;

        }
        public void LerpChase(Vector2 pos, float vel, float v)
        {
            Vector2 topos = Vector2.Normalize(pos - NPC.Center);
            NPC.velocity = Vector2.Lerp(NPC.velocity, vel * topos, v);
        }//简单渐进
        public override void AI()
        {
            Lighting.AddLight(NPC.Center, 0.9f, 0.9f, 0.95f);
            #region 寻敌
            if (NPC.target < 0 || NPC.target >= 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
            Player player = Main.player[NPC.target];
            #endregion
            NPC.localAI[0]++;
            if (NPC.ai[1] == 0)
            {
                if (NPC.localAI[0] < 100)
                { LerpChase(player.Center + new Vector2(NPC.ai[0] * 450, -400), 28, 0.04f); }
                if (NPC.localAI[0] == 75 | NPC.localAI[0] == 80 | NPC.localAI[0] == 85)
                {
                    Projectile p = Projectile.NewProjectileDirect(null,NPC.Center, Vector2.Normalize(player.Center - NPC.Center) * 9f, ProjectileID.GreenLaser, 19, 1, 0);
                    p.friendly = false; p.hostile = true; 
                }
                if (NPC.localAI[0] >= 100)
                {
                    NPC.velocity = Vector2.Normalize(NPC.Center - player.Center) * 5f;
                }

            }
            if(NPC.ai[1] == 1)
            {
                foreach(var seed in Main.npc)
                {
                    if(seed.type == ModContent.NPCType<SeedsOfOrigin>() && seed.active)
                    {
                        float r = (player.Center - seed.Center).ToRotation();
                        { LerpChase(seed.Center + (r + NPC.ai[0] * 0.2f).ToRotationVector2() * 220, 28, 0.04f); }
                        if(NPC.localAI[0] == 60)
                        {   
                            Projectile.NewProjectile(null,NPC.Center, (r + NPC.ai[0] * 0.2f).ToRotationVector2() * 0.9f, ModContent.ProjectileType<OriginKnife>(), 15, 0, 0);
                        }
                        if(NPC.localAI[0] == 100)
                        {
                            Projectile.NewProjectile(null,NPC.Center, Vector2.Normalize(player.Center - NPC.Center) * 0.9f, ModContent.ProjectileType<OriginKnife>(), 15, 0, 0);
                        }

                    }
                }
            }
            if (NPC.localAI[0] == 135)
            {
                NPC.active = false;
            }
        }
    }
}
