using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using HeroRegression.Projectiles;
using Terraria.DataStructures;

namespace HeroRegression.Items.Boss.SeedsOfOrigin
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
            Item.shoot = 10;
           
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = ModContent.ProjectileType<OriginNailFriend2>();
            if (Main.rand.NextBool(5))
            {
                Projectile.NewProjectile(source, player.Center, velocity, type, damage, 5f, player.whoAmI);
               
            }
          
            return false;
        }
    }
}