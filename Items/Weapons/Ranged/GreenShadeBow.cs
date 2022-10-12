using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.DataStructures;
using HeroRegression.Projectiles.Friendly.Ranged;

namespace HeroRegression.Items.Weapons.Ranged
{
    class GreenShadeBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Green Shade Bow");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "绿荫弓");
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 68;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 15;
            Item.crit = 4;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.knockBack = 2;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item5;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.noMelee = true;
            Item.shootSpeed = 7;
            Item.rare = ItemRarityID.Green;
            Item.useAmmo = AmmoID.Arrow;
            Item.shoot = ProjectileID.WoodenArrowFriendly;

        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly) type = ModContent.ProjectileType<OriginNailFriend>();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.rand.NextBool(5))
            {
                Projectile.NewProjectile(source, position + Main.rand.NextVector2CircularEdge(20f, 20f), velocity, ModContent.ProjectileType<OriginNailFriend>(), damage, 5f, player.whoAmI);

            }
            return true;
        }
    }
}