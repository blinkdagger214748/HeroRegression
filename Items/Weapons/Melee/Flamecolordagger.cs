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
    class Flamecolordagger : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flamecolordagger");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "焰彩投刀");
            Tooltip.SetDefault("[c/FFFF99:The flame will protect you]");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "\n[c/FFFF99:焰色会保佑你]" +
                "\n[c/CCFFFF:投出充斥幻焰的【附魔投刀】]");

            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.height = 60;
            Item.DamageType = DamageClass.Melee;
            Item.damage = 14;
            Item.crit = 8;
            Item.value = Item.buyPrice(0, 3, 0, 0);
            Item.knockBack = 4;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.useStyle = 1;
            Item.UseSound = SoundID.Item1;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Flamecolordaggerdm>();
            Item.shootSpeed = 8;
            Item.rare = 3;
            Item.noUseGraphic = true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(20, 0);
        }


    }
}