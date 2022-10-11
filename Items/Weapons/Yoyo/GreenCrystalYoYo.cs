using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using HeroRegression.Projectiles;
using HeroRegression.Projectiles.YoYo;

namespace HeroRegression.Items.Weapons.Yoyo
{
    public class GreenCrystalYoYo : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Green Crystal YoYo");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "绿晶悠悠球");
            Tooltip.SetDefault("[c/FF0000:BOSS Relics]");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "[c/FF0000:BOSS遗物]");
        }


        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.CorruptYoyo);
            Item.damage = 16;
            Item.value = 14000;
            Item.knockBack = 1;
            Item.channel = true;
            Item.useStyle = 5;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.expert = true;
            Item.shoot = ModContent.ProjectileType<OSyoyo>();
        }

    }
}