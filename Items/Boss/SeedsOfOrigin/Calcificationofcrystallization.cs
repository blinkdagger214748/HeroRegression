using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using HeroRegression.Projectiles;

namespace HeroRegression.Items.Boss.SeedsOfOrigin
{
    class Calcificationofcrystallization : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Calcification of crystallization");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "钙化结晶");
            Tooltip.SetDefault("And stones are a variety");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "结石（bushi");
        }
        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.value = Item.sellPrice(0, 0, 10, 0);   //价格
            Item.rare = ItemRarityID.Green; //稀有度
        }
    }
    
}