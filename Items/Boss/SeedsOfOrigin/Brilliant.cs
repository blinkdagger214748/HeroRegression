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
    class Brilliant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brilliant");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "碧辉");
            Tooltip.SetDefault("Seems to be folded from the seed of HeroRegression." +
                "\nAnyway, the thing shot out is the origin of the seed of the curtain of fire." +
                "\nif proj> 10 proj Kill"+
                "\nHit will give the enemy additional poisoning Debuff.");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "似乎是从起源之种上折下来的。" +
                "\n总之射出去的玩意是起源之种的弹幕。" +
                "\n弹幕总数大于10自动清除（）"+
                "\n命中会给敌人附加中毒Debuff。");
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.height = 68;
            Item.DamageType = DamageClass.Magic ;
            Item.damage = 18;
            Item.crit = 8;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.knockBack = 4;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<NaturalCrystallization>();
            Item.shootSpeed = 7;
            Item.mana = 5;
            Item.rare = ItemRarityID.Green;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(20, 0);
        }
    }
}
