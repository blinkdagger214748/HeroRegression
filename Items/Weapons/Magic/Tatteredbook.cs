using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using HeroRegression.Projectiles.Friendly.Magic;

namespace HeroRegression.Items.Weapons.Magic
{
    class Tatteredbook : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tattered book");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "破旧的书");
            Tooltip.SetDefault("Half Blood Prince＇s?");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "混血王子（？）");
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.height = 96;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 14;
            Item.crit = 4;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.knockBack = 4;
            Item.autoReuse = false;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.useTime = 17;
            Item.useAnimation = 17;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Tatteredbookdm>();
            Item.shootSpeed = 7;
            Item.mana = 6;
            Item.rare = ItemRarityID.Green;
        }

        public override void AddRecipes()
        {
        }
        public override Vector2? HoldoutOffset()
        {
            return base.HoldoutOffset();
        }
    }
}