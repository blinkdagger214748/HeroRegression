using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using HeroRegression.Projectiles;

namespace HeroRegression.Items.Weapons.Magic
{
    public class Brilliant : BaseStaff
    {
        public override void SetStaticDefaults()
        {
            ItemName("Emerald Glow", "碧辉");
            ItemTooltip("Shoots emeralds that explode on impact.", "发射接触时爆炸的绿宝石。");
        }
        public override void SetDefaults()
        {
            Defaults(40, 68, 18, 15, Item.buyPrice(0, 2, 0, 0), 4f, ItemRarityID.Green, 8, true, true, 8, 5);
            Item.UseSound = SoundID.Item43;
            Item.shoot = ModContent.ProjectileType<NaturalCrystallization>();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(20, 0);
        }
    }
}
