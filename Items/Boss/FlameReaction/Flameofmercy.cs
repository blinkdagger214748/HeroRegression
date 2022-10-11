using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using HeroRegression.Projectiles;

namespace HeroRegression.Items.Boss.FlameReaction
{
    class Flameofmercy : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flameofmercy");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "仁慈火炬");
            Tooltip.SetDefault("[c/FFFF99:Shooting colorful fire]");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "\n[c/FFFF99:焰色会保佑你]" +
                "\n[c/CCFFFF:射出【彩色幻焰聚合物】]");

            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.height = 60;
            Item.DamageType = DamageClass.Magic ;
            Item.damage = 17;
            Item.crit = 4;
            Item.value = Item.buyPrice(0, 3, 0, 0);
            Item.knockBack = 4;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item8;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType < Projectiles.Flameofmercydm2>();
            Item.shootSpeed = 7;
            Item.rare = 3;
            Item.mana = 5;

        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(20, 0);
        }

    }
}