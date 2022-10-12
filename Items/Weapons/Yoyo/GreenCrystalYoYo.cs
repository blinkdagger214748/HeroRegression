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
    public class GreenCrystalYoYo : BaseYoyoItem
    {
        public override void SetStaticDefaults()
        {
            YoyoItemStatics(ChnTrans("Green Crystal Yoyo", "绿晶悠悠球"), ChnTrans("Boss Relics", "BOSS遗物"));
        }
        public override void SetDefaults()
        {
            YoyoItemDefaults(40, 34, 16, 1, 25, ModContent.ProjectileType<Projectiles.Friendly.Melee.GreenCrystalYoyoProj>(), ItemRarityID.Green, 14000, 15);
            Item.expert = true;
        }
    }
}