
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
using HeroRegression.HeroPlayers;
using HeroRegression.Items.TreasureBag;
using HeroRegression.Items.Placeable.Trophy;
using HeroRegression.Items.Weapons.Ranged;
using HeroRegression.Items.Weapons.Magic;
using HeroRegression.Items.Weapons.Yoyo;
using HeroRegression.Items.Weapons.Minion;
using HeroRegression.Items.Accessories;

namespace HeroRegression.NPCs.Boss.SeedsOfOrigin
{
    [AutoloadBossHead]
    public class SeedsOfOrigin : ModNPC
    {
        public Texture2D GetTex(string path)
        {
            return ModContent.Request<Texture2D>(path).Value;
        }

        private int Timer
        {
            get => (int)NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        bool[] flag = new bool[10];




        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Seeds Of Origin");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "起源之种");
            NPCID.Sets.TrailCacheLength[NPC.type] = 10;
            NPCID.Sets.TrailingMode[NPC.type] = 3;
        }

        public override void SetDefaults()
        {
            NPC.width = 62;
            NPC.height = 86;
            NPC.friendly = false;
            NPC.damage = 20;
            NPC.defense = 10;
            NPC.lifeMax = 1800;
            NPC.boss = true;
            NPC.knockBackResist = 0;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.noTileCollide = true;

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
        public void ShootProj(Vector2 pos, float rot, float dis, float vel, int amount, int ID, int damage, float knockback, float randomdis)//简单散射
        {
            if (amount % 2 == 0)
            {
                for (int k = amount / 2; k > -amount / 2; k--)
                {
                    if (k != 0)
                    {
                        float rdis = MathHelper.ToRadians(dis);
                        float tr = rot + (k - 0.5f) * rdis;
                        Projectile p = Projectile.NewProjectileDirect(null,pos, tr.ToRotationVector2() * vel, ID, damage, knockback, 0);
                        p.hostile = true; p.friendly = false;
                    }
                }
            }
            if (amount % 2 != 0)
            {
                for (int k = amount / 2; k >= -amount / 2; k--)
                {
                    float rdis = MathHelper.ToRadians(dis);
                    float tr = rot + k * rdis;
                    Projectile p = Projectile.NewProjectileDirect(null,pos, tr.ToRotationVector2() * vel, ID, damage, knockback, 0);
                    p.hostile = true; p.friendly = false;
                }
            }

        }
        #endregion
        public override void AI()
        {
            Lighting.AddLight(NPC.Center, 0.9f, 0.9f, 0.96f);
            #region 寻敌
            if (NPC.target < 0 || NPC.target >= 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
            Player player = Main.player[NPC.target];
            #endregion
            Timer++;
            Music = MusicLoader.GetMusicSlot("HeroRegression/Sounds/Music/SeedsOfOrigin");
            if (NPC.life >= NPC.lifeMax * 0.6f)
            {
                if (NPC.ai[3] == 0)//非1
                {
                    if (!flag[0] )
                    {

                        CombatText.NewText(new Rectangle((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height)
                       , Color.GreenYellow, "又见面了", true, false);
                        flag[0] = true;
                    }

                    LerpChase(player.Center + new Vector2(0, -440), 8, 0.05f);
                    if (Timer == 70 | Timer == 180)
                    {
                        if (!flag[1])
                        {

                            CombatText.NewText(new Rectangle((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height)
                        , Color.GreenYellow, "接着", true, false);
                            flag[1] = true;
                        }
                        ShootProj(NPC.Center, (player.Center - NPC.Center).ToRotation(), 30, 10, 3, ProjectileID.RuneBlast, 10, 1, 0);
                    }
                    if (Timer == 125)
                    {
                        if (!flag[2])
                        {
                            CombatText.NewText(new Rectangle((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height)
                         , Color.GreenYellow, "来", true, false);
                            flag[2] = true;
                        }
                        ShootProj(NPC.Center, (player.Center - NPC.Center).ToRotation(), 20, 10, 5, ProjectileID.RuneBlast, 10, 1, 0);
                    }
                    if (Timer > 210)
                    {
                        NPC.ai[3]++; Timer = 0;
                    }
                }


                if (NPC.ai[3] == 1)//非2
                {

                    LerpChase(player.Center + new Vector2(0, -400), 8f, 0.1f);
                    if (Timer == 30)
                    {
                        if (!flag[3])
                        {
                            CombatText.NewText(new Rectangle((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height)
                         , Color.GreenYellow, "看看这招！", true, false);
                            flag[3] = true;
                        }
                        NPC.NewNPC(null,(int)player.Center.X, (int)player.Center.Y - 200, ModContent.NPCType<OriginGuard>(), 0, -1);
                        NPC.NewNPC(null,(int)player.Center.X, (int)player.Center.Y - 200, ModContent.NPCType<OriginGuard>(), 0, 0);
                        NPC.NewNPC(null,(int)player.Center.X, (int)player.Center.Y - 200, ModContent.NPCType<OriginGuard>(), 0, 1);
                    }
                    if (Timer > 200)
                    {
                        NPC.ai[3]++; Timer = 0;
                    }
                }
                if (NPC.ai[3] == 2)//非3
                {

                    if (Timer == 5 | Timer == 80)
                    {
                        NPC.velocity = Vector2.Normalize(player.Center - NPC.Center) * 20;

                    }
                    NPC.rotation = NPC.velocity.ToRotation() + MathHelper.PiOver2;
                    for (int k = 0; k < 13; k++)
                    {
                        if (Timer == 10 + 5 * k | Timer == 85 + 5 * k)
                        {
                            if (!flag[4] && NPC.life <= 1000)
                            {
                                CombatText.NewText(new Rectangle((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height)
                     , Color.GreenYellow, "哈哈，你真是把我逼急了呀！", true, false);
                                flag[4] = true;
                            }

                            Projectile.NewProjectile(null,NPC.Center, Vector2.Zero, ModContent.ProjectileType<OriginLeaf>(), 10, 1, 0);
                        }
                    }


                    if (Timer > 50 && Timer < 80)
                    {
                        NPC.velocity *= 0.91f;
                    }
                    if (Timer > 110 && Timer < 150)
                    {
                        NPC.velocity *= 0.91f;
                    }
                    if (Timer == 150)
                    {
                        Timer = 0; NPC.ai[3] = 0; NPC.rotation = 0;
                    }
                }
            }
            if (NPC.ai[3] == 4 && Timer > 75)
            {
                if (!flag[5])
                {
                    CombatText.NewText(new Rectangle((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height)
                       , Color.GreenYellow, "哈哈", true, false);
                    flag[5] = true;
                }

                if (NPC.localAI[2] < 50)
                    NPC.localAI[2]++;
                player.GetModPlayer<ScreenPositionPlayer>().ScreenLock = true;
                player.GetModPlayer<ScreenPositionPlayer>().screengo = NPC.localAI[2];
                if (player.Center.X - NPC.Center.X < -Main.screenWidth / 2)
                {
                    player.Center = NPC.Center + new Vector2(-Main.screenWidth / 2 + 10, player.Center.Y - NPC.Center.Y); player.velocity *= 0;
                }
                if (player.Center.X - NPC.Center.X > Main.screenWidth / 2)
                {
                    player.Center = NPC.Center + new Vector2(Main.screenWidth / 2 - 10, player.Center.Y - NPC.Center.Y); player.velocity *= 0;
                }
                if (player.Center.Y - NPC.Center.Y < -Main.screenHeight / 2)
                {
                    player.Center = NPC.Center + new Vector2(player.Center.X - NPC.Center.X, -Main.screenHeight / 2 + 10); player.velocity *= 0;
                }
                if (player.Center.Y - NPC.Center.Y > Main.screenHeight / 2)
                {
                    player.Center = NPC.Center + new Vector2(player.Center.X - NPC.Center.X, Main.screenHeight / 2 - 10); player.velocity *= 0;
                }
            }
            if (NPC.ai[3] != 4)
            {

                player.GetModPlayer<ScreenPositionPlayer>().ScreenLock = false;
            }
            if (NPC.life < NPC.lifeMax * 0.6f)
            {
                if (NPC.localAI[1] == 0 && NPC.ai[3] != 4)
                {
                    NPC.ai[3] = 4; NPC.localAI[1]++; Timer = 0; NPC.rotation = 0;
                }
                if (NPC.ai[3] == 4)
                {
                    NPC.rotation = 0;
                    if (Timer < 60)
                        LerpChase(player.Center + new Vector2(0, -250), 20, 0.1f);
                    if (Timer == 60)
                    {
                        NPC.velocity *= 0;

                    }
                    for (int k = 0; k < 999; k++)
                    {
                        if (Timer == 60 + 3 * k && Timer < 240)
                        {
                            Projectile.NewProjectile(null,NPC.Center + new Vector2(Main.rand.Next(-1000, 1000), -1000), new Vector2(0, 12), ModContent.ProjectileType<OriginNail>(), 10, 1, 0);

                        }
                    }
                    if (Timer > 320)
                    {
                        Timer = 0; NPC.ai[3] = 5;
                    }
                }
                if (NPC.ai[3] == 5)
                {

                    if (Timer == 30)
                    {
                        for (int k = -3; k <= 3; k++)
                        { NPC.NewNPC(null,(int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<OriginGuard>(), 0, k, 1); }
                    }
                    if (Timer > 100)
                    {
                        Timer = 0; NPC.ai[3] = 6;
                    }
                }
                if (NPC.ai[3] == 6)
                {

                    LerpChase(player.Center + new Vector2(0, -220), 9f, 0.1f);
                    if (Timer == 40)
                    {
                        Projectile.NewProjectile(null,player.Center + new Vector2(-400, 0), Vector2.Zero, ModContent.ProjectileType<SeedWall>(), 10, 1, 0, 0);
                        Projectile.NewProjectile(null,player.Center + new Vector2(400, 0), Vector2.Zero, ModContent.ProjectileType<SeedWall>(), 10, 1, 0, 1);
                    }
                    if (Timer == 350)
                    {
                        Timer = 0; NPC.ai[3] = 7;
                    }
                }
                if (NPC.ai[3] == 7)
                {
                    NPC.velocity *= 0;
                    for (int k = 0; k < 10; k++)
                    {
                        if (Timer == 30 + 6 * k | Timer == 100 + 6 * k)
                        {
                            Projectile.NewProjectile(null,NPC.Center, Vector2.Normalize(player.Center - NPC.Center) * 10, ModContent.ProjectileType<DashingEMD>(), 10, 2, 0);
                        }
                    }
                    if (Timer == 190)
                    {
                        Timer = 0; NPC.ai[3] = 8;
                    }
                }
                if (NPC.ai[3] == 8)
                {
                    NPC.rotation = NPC.velocity.ToRotation() + MathHelper.PiOver2;
                    if (Timer == 15 | Timer == 65 | Timer == 115)
                    {
                        NPC.velocity = Vector2.Normalize(player.Center - NPC.Center) * 25;

                    }
                    for (int k = 0; k < 3; k++)
                    {
                        if (Timer > 45 + 50 * k && Timer < 65 + 50 * k)
                        {
                            NPC.velocity *= 0.92f;
                        }
                    }
                    for (int j = 0; j < 25; j++)
                    {
                        if (Timer == 15 + 7 * j)
                        {
                            Projectile.NewProjectile(null,NPC.Center, Vector2.Zero, ModContent.ProjectileType<DashingEMD>(), 10, 1, 0);
                        }
                    }
                    if (Timer > 165)
                    {
                        Timer = 0; NPC.ai[3] = 4;
                    }
                }
                if (NPC.ai[3] == 10)//尾杀
                {
                    if (!flag[6])
                    {
                        CombatText.NewText(new Rectangle((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height)
                            , Color.GreenYellow, "你！！！不可饶恕！", true, false);
                        flag[6] = true;
                    }
                    NPC.rotation = NPC.velocity.ToRotation() + MathHelper.PiOver2;
                    for (int k = 0; k < 10; k++)
                    {
                        if (Timer == 50 * k)
                        {
                            NPC.velocity = Vector2.Normalize(player.Center - NPC.Center) * 22f;
                        }
                    }
                    if (Timer > 430)
                    {
                        NPC.velocity *= 0.92f;
                    }
                    if (Timer >= 480)
                    {
                        NPC.velocity *= 0.9f;
                    }
                    if (Timer > 500)
                    {
                        NPC.life = 0;
                        NPC.checkDead();
                        for (int o = 0; o < 20; o++)
                        {
                            Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.RuneWizard); d.velocity = Main.rand.NextVector2Circular(12, 12);
                        }

                        //这里添加掉落物
                    }
                }
            }
            base.AI();

        }

        public override bool CheckDead()
        {
            if (NPC.ai[3] != 10)
            {

                NPC.dontTakeDamage = true;
                NPC.life = 1;
                NPC.ai[3] = 10;
                Timer = 0;
                return false;

            }
            else { return true; }
        }
        public override bool CheckActive()
        {
            Player player = Main.player[NPC.target];
            if (player.dead)
            {
                NPC.active = false;
            }
            if (!player.active)
            {
                NPC.active = false;
            }
            if (Vector2.Distance(player.Center, NPC.Center) > 4500)
            {
                NPC.active = false;
            }
            return false;
        }
        
   
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.ai[3] == 2 | NPC.ai[3] == 8 | NPC.ai[3] == 10)
            {
                Texture2D SO = GetTex("HeroRegression/NPCs/Boss/SeedsOfOrigin/SeedsOfOrigin");
                for (int i = 1; i < NPC.oldPos.Length; i += 1)//trail
                {
                    Vector2 drawPos = NPC.oldPos[i] - Main.screenPosition + new Vector2(34, 41) + new Vector2(0, NPC.gfxOffY);
                    Color color = Color.White * ((float)(NPC.oldPos.Length - i) / (float)NPC.oldPos.Length);
                    float Sc = 1f;
                    Main.EntitySpriteDraw(SO, drawPos, null, color, NPC.rotation, new Vector2(34, 41), Sc, SpriteEffects.None, 0);
                }

            }
            return true;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            BaseBossLoot npc = GetLootNPC(NPC);
            npc.NormalLoots.Add(new Vector4(ModContent.ItemType<Calcificationofcrystallization>(), 1, 10, 20));
            npc.OptionsLoots = new int[5]
            {
                ModContent.ItemType<Overloaded_Energy>(),
                ModContent.ItemType<Brilliant>(),
                ModContent.ItemType<GreenCrystalYoYo>(),
                ModContent.ItemType<OriginalInterestItem>(),
                ModContent.ItemType<GreenShadeBow>()
            };
            npc.ModifyLoots(npcLoot,ModContent.ItemType<起源之种财宝袋>(),ModContent.ItemType<SeedBossTrophy>(),ModContent.ItemType<SeedBossRelicItem>(),-1,-1);
        }

        #region AI

        /*非①
三发翡翠法杖弹幕>五发翡翠法杖弹幕>三发翡翠法杖弹幕
非②
在玩家头上召唤三个守卫   轮流发射弹幕
非③
向玩家冲刺   路径上留下10发自机狙弹幕

移速参考克眼一阶段

一阶段①>①>①>③>②循环   失去一定血量进入二阶段   使用符卡①

符卡①
叶绿「丛生的茂盛之力」
给予玩家buff「过剩精神力」
屏幕固定
玩家移速变的很快
种子飘到玩家头上发射覆盖全屏绿宝石钉雨（永恒史王）
宝石钉可以微移躲过     但是如果移速超快的会怎么样呢?（笑）

符卡①在血量消耗一定后停止
正式进入二阶段ai
非①
起源种召唤五只守卫在身边   守卫发出绿宝石苦无自机狙弹幕
非②
起源种召唤绿宝石柱子漂浮两边   飞到玩家头顶     柱子发出奇数狙
非③
发射间断的绿宝石弹链
非④
向玩家冲锋  路径留下绿宝石块  造成接触伤害   10喵消失
非5
短冲锋   

①③>③>③>②③>5>④>①>5
循环   到达血量强制切换阶段

最后200血    使用   生机「生长的漩涡」
屏幕锁定
围绕玩家高速转圈    并且出现两个幻影   幻影与本体以屏幕中心为中心发射苦无并且组成旋涡状    同时八个方向会袭来绿宝石玉柱   正四个一组    斜四个一组     两组循环发射
直到死掉为止
  */
        #endregion
    }
}