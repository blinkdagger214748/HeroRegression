
using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Localization;
using HeroRegression.Items.Accessories;
using HeroRegression.Items.Weapons.Magic;
using HeroRegression.Items.Weapons.Yoyo;
using HeroRegression.Items.Weapons.Ranged;
using HeroRegression.Items.Weapons.Melee;
using HeroRegression.Items.Material;
using HeroRegression.Items.Weapons.Minion;
using HeroRegression.NPCs.Boss.SeedsOfOrigin;
using HeroRegression.Items.Placeable.Trophy;

namespace HeroRegression.Items.TreasureBag
{
    public class 起源之种财宝袋 : BaseBossBag
    {
        public override void SetStaticDefaults()
        {
            BagStatics(ChnTrans("Seed of Origin", "起源之种"));
        }
        public override void SetDefaults()
        {
            BagDefaults(24, 24);
        }
        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            CommonLoots.Add(new Vector4(ItemType<Calcificationofcrystallization>(), 1, 10, 20));
            OneFromOptionsLoots = new int[5]
            {
                ItemType<Overloaded_Energy>(),
                ItemType<Brilliant>(),
                ItemType<GreenCrystalYoYo>(),
                ItemType<OriginalInterestItem>(),
                ItemType<GreenShadeBow>()
            };
            BagLoot(itemLoot, NPCType<SeedsOfOrigin>(), ItemType<Thebadgeofpurity>());
        }
    }
}