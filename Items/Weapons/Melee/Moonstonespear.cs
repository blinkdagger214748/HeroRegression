using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using HeroRegression.Projectiles.Friendly.Melee;

namespace HeroRegression.Items.Weapons.Melee
{
    class Moonstonespear : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Moonstonespear");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "月长石投矛");
            Tooltip.SetDefault("[c/FFFF99:Shoot Moonstone spear");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "\n投出月长石投矛" +
                "\n传说明月飞升之时...");

            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.height = 60;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 11;
            Item.crit = 4;
            Item.value = Item.buyPrice(0, 3, 0, 0);
            Item.knockBack = 4;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item1;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Moonstonespeardm>();
            Item.shootSpeed = 8;
            Item.rare = 2;
            Item.noUseGraphic = true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(20, 0);
        }


    }
}