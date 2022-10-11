using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using HeroRegression.Projectiles;
using Terraria.DataStructures;
using HeroRegression.Items.Material;

namespace HeroRegression.Items.Weapons.Ranged
{
    class 浓眼瘤弓 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("浓眼瘤弓");
   
            Tooltip.SetDefault("真恶心！" +
                "\n将发出的箭替换为血幕"+ "\n有百分之20的几率吸血，吸血效果为一滴血");
          
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 50;
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 12;
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
            Item.shoot = ModContent.ProjectileType<眼球弹幕>();
           
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return true;
        }
        public override void AddRecipes()
		{
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<恶性眼瘤>(), 16)
            .AddTile(TileID.Anvils)
            .AddIngredient(ItemID.Lens, 5)

             
            .Register();
        }
    }
}