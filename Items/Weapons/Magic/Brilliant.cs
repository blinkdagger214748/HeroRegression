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
    public class Brilliant : BaseStaff
    {
        public override void SetStaticDefaults()
        {
            ItemName("Emerald Glow", "碧辉");
            ItemTooltip("Shoots emerald gems that explode on impact", "发射接触时爆炸的绿宝石");
        }
        public override void SetDefaults()
        {
            Defaults(40, 68, 12, 10, Item.buyPrice(0, 2, 0, 0), 6f, ItemRarityID.Green, 8, ModContent.ProjectileType<EmeraldGlowStaff>(), true, true, true, 16, 6, 10, true);
            Item.UseSound = SoundID.Item43;
        }
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<EmeraldGlowStaff>()] < 1;
        }
    }
}
