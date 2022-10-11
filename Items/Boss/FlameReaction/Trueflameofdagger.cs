using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using HeroRegression.Projectiles;
using HeroRegression.Items.Boss.HeroOfBrewing;

namespace HeroRegression.Items.Boss.FlameReaction
{
    class Trueflameofdagger : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Trueflameofdagger");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "真·焰彩投刀");
            Tooltip.SetDefault("[FFFF99:Contains the power of flame to reflect the creator]");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "[c/FFFF99:感受焰色反应造物主的力量]");

            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.height = 60;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 50;
            Item.crit = 15;
            Item.value = Item.buyPrice(0, 3, 20, 0);
            Item.knockBack = 4;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item1;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Realflameofdaggerdm>();
            Item.shootSpeed = 9;
            Item.rare =5;
            Item.noUseGraphic = true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(20, 0);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Flamecolordagger>(), 1)
            .AddTile(TileID.SkyMill)
            .AddIngredient(ItemID.SoulofSight, 20)
            .AddIngredient(ModContent.ItemType<Sparklingbouquet>(), 15)
             
            .Register();

        }
    }
}