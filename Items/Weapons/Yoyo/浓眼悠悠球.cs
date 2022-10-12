using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using HeroRegression.Projectiles;
using HeroRegression.Projectiles.Friendly.Melee;

namespace HeroRegression.Items.Weapons.Yoyo
{
    public class 浓眼悠悠球 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eyes YoYo");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "浓眼悠悠球");
            Tooltip.SetDefault("[c/FF0000:BOSS Relics]");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "[c/FF0000:BOSS遗物]"+ "\n有百分之20的几率吸血，吸血效果为一滴血");
        }
       

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.RedsYoyo);
            Item.damage = 13;
            Item.value = 140;
            Item.knockBack = 1;
            Item.channel = true;
            Item.useStyle = 5;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.expert = true;
            Item.shoot = ModContent.ProjectileType<KYyoyo>();
        }
       
    }
}