using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ID;
using HeroRegression.Projectiles.Summon.FearOfColdCrystalWorms;
using HeroRegression.Buffs.Summon;
using Terraria.DataStructures;

namespace HeroRegression.Items.Weapons.Sentry
{
    public class FrostWhistle : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost Whistle");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "永冻霜哨");
        }

        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.knockBack = 1f;
            Item.noUseGraphic = true;
            Item.DD2Summon = true;
            Item.UseSound = SoundID.DD2_DarkMageHealImpact;
            Item.useTime = Item.useAnimation = 15;
            Item.useStyle = 3;
            Item.value = 100000;
            Item.useTurn = false;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<IceMan>();
            Item.buffType = ModContent.BuffType<IceManBuff>();
            Item.buffTime = 15;
            Item.mana = 5;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < player.maxMinions;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            position = Main.MouseWorld;
            int proj = Projectile.NewProjectile(source, position, Vector2.Zero, type, damage, 5f, player.whoAmI);
            (Main.projectile[proj].ModProjectile as IceMan).Counts = player.ownedProjectileCounts[Item.shoot];
            return false;
        }
    }
}
