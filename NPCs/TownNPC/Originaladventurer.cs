using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria.Utilities;
using Microsoft.Xna.Framework.Graphics;
using HeroRegression.Items.Accessories;

namespace HeroRegression.NPCs.TownNPC
{
    [AutoloadHead]
    class Originaladventurer : ModNPC
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Original adventurer");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "起源冒险家");
            //该NPC的游戏内显示名
            Main.npcFrameCount[NPC.type] = 52;
            //NPC总共帧图数，一般为16+下面两种帧的帧数
            NPCID.Sets.ExtraFramesCount[NPC.type] = 5;
            //额外活动帧，一般为5
            //关于这个，一般来说是除了攻击帧以外的那几帧（包括坐下、谈话等动作），但实际上填写包含攻击帧在内的帧数也不影响（比如你写9），如果有知道的可以解释一下。
            NPCID.Sets.AttackFrameCount[NPC.type] = NPCID.Sets.AttackFrameCount[NPCID.Merchant];
            //攻击帧，这个帧数取决于你的NPC攻击类型，射手填5，战士和投掷填4，法师填2，当然，也可以多填，就是不知效果如何（这里直接引用商人的）
            NPCID.Sets.DangerDetectRange[NPC.type] = 1200;
            //巡敌范围，以像素为单位，这个似乎是半径
            NPCID.Sets.AttackType[NPC.type] = 0;
            //攻击类型，默认为0，想要模仿其他NPC就填他们的AttackType
            NPCID.Sets.AttackTime[NPC.type] = 240;
            //单次攻击持续时间，越短，则该NPC攻击越快（可以用来模拟长时间施法的NPC）
            NPCID.Sets.AttackAverageChance[NPC.type] = 5;
            //NPC遇敌的攻击优先度，该数值越大则NPC遇到敌怪时越会优先选择逃跑，反之则该NPC越好斗。
            //最小一般为1，你可以试试0或负数LOL~
            //NPCID.Sets.MagicAuraColor[base.NPC.type] = Color.Purple;
            //如果该NPC属于法师类，你可以加上这个来改变NPC的魔法光环颜色，这里用紫色
        }
        public override void SetDefaults()
        {
            NPC.townNPC = true;
            //必带项，没有为什么
            //加上这个后NPC就不会在退出地图后消失了，你可以灵活运用一下
            NPC.friendly = true;
            //如果你想写敌对NPC也行
            NPC.width = 22;
            //碰撞箱宽
            NPC.height = 32;
            //碰撞箱高            
            NPC.aiStyle = 7;
            //必带项，如果你能自己写出城镇NPC的AI可以不带
            //PS:这个决定了NPC是否能入住房屋
            NPC.damage = 10;
            //碰撞伤害，由于城镇NPC没有碰撞伤害所以可以忽略
            NPC.defense = 15;
            //防御力
            NPC.lifeMax = 450;
            //生命值
            NPC.HitSound = SoundID.NPCHit1;
            //受伤音效
            NPC.DeathSound = SoundID.NPCDeath1;
            //死亡音效
            NPC.knockBackResist = 0.5f;
            //抗击退性，数字越小抗性越高，0就是完全抗击退
            AnimationType = NPCID.Merchant;
            //如果你的NPC属于除投掷类NPC以外的其他攻击类型，请带上，值可以填对应NPC的ID
        }
        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
          
            if (numTownNPCs >= 1)
            {
                return true;
            }
            return false;
        }
        public override List<string> SetNPCNameList()
        {
            switch (WorldGen.genRand.Next(3))
            {
                case 0:
                    return new List<string>() { "拉格尔" };
                case 1:
                    return new List<string>() { "杜芬" };
                default:
                    return new List<string>() { "非墨" };
            }
        }
  
        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();
            {
                if (HeroRegressionWorld.downedSeedsOfOrigin)
                {
                    chat.Add("你真的破坏它了？");
                }
                chat.Add("那些堕落生物中有着强大的灵魂力量，你必须善用它们。");
                chat.Add("我活了几百年了？天杀的神佑可能知道吧!?");
                chat.Add("这泰拉大陆值得你去探索！");
               // chat.Add("木恩和点子？在我看来，木恩是点子的狗");木÷好似
                chat.Add("当矿物聚合在一起的时候，你会看到最绚丽的焰彩。");
                chat.Add("斯派莫特？这是我形象的作者！");
                chat.Add("木恩啊,他给予了我语言能力");
                chat.Add("树妖?她祖宗被扼杀在一块结晶里了，那着魔的东西似乎非常暴躁。");
                chat.Add("你可以在天空中找到一种亮紫色的物质，它拥有很强大的电能。");
                chat.Add("伞二啊,她创造了我！");
                chat.Add("几百年前那把苍青色的镰刀有着上百点的伤害，可惜现在变钝了(´;︵;`)。");
                chat.Add("沉睡着的漆黑之翼啊！苏醒吧（突发恶疾）！");
                chat.Add("有些木头似乎有生命。");
                chat.Add("欢迎来到英雄之殇。");
                chat.Add("欢迎来到英雄之殇。");

                return chat;
            }
        }
        public override void SetChatButtons(ref string button, ref string button2)
        {
          
            button = Language.GetTextValue("LegacyInterface.28");
        }
        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {

            if (firstButton)
            {
                shop = true;
            }
        }
        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            if (!Main.dayTime)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Weapons.Melee.Moonstonespear > ());
                shop.item[nextSlot].value = 55000;
                nextSlot++;
            }
            if (!Main.dayTime)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Weapons.Magic.Tatteredbook>());
                shop.item[nextSlot].value = 55000;
                nextSlot++;
            }
            if (!Main.dayTime)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.Accessories.ToughBlade>());
                shop.item[nextSlot].value = 25000;
                nextSlot++;
            }
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.PolarBow>());
            shop.item[nextSlot].value = 10000;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(3069);
            shop.item[nextSlot].value = 7500;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(109);
            shop.item[nextSlot].value = 5000;
            nextSlot++;
        }
        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 12;
            knockback = 3f;
        }
        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 1;
            randExtraCooldown = 1;
           
        }
        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<Projectiles.OriginNailFriend>();
            
            attackDelay = 120;
           
        }
     
        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 15f;
          
            gravityCorrection = 0f;
        
            randomOffset = 1.5f;
           
        }
    }
}

