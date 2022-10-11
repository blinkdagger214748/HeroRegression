using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace HeroRegression.Items.Weapons.Melee
{
    //文件名
    public class 生灵飞镰 : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("生灵飞镰");


        }

        public override void SetDefaults()
        {
            
            Item.damage = 37;
            
            Item.knockBack = 5f;
            
            Item.crit = 5;

            
            Item.rare = 5;
            
            Item.useTime = 20;
            Item.useAnimation = 20;
            
            
           
           
            
            
            Item.useStyle = 1;
            
            Item.autoReuse = false;
           
            
           
           
            
            
            Item.DamageType = DamageClass.Melee;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            
            Item.value = Item.sellPrice(0, 0, 80, 0);
            
            
            Item.UseSound = SoundID.Item1;
           
            Item.scale = 1.2f;
           
          
            Item.width = 64;
            Item.height = 64;
         
            Item.maxStack = 1;
            Item.shoot = ModContent.ProjectileType<Projectiles.LC>();

            Item.shootSpeed = 25f;
       
            Item.useTurn = false;
        }
        public override bool CanUseItem(Player player)
        { 
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {   Vector2 o1 = new Vector2(velocity.X, velocity.Y);
            Projectile.NewProjectileDirect(source, position, o1, ModContent.ProjectileType<Projectiles.LC>(), damage, knockback, player.whoAmI);
            return false;
        }
       
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Placeable.Block.古木>(), 20)
            .AddTile(TileID.Anvils)
            .AddIngredient(ModContent.ItemType<Placeable.Block.Lingcuistone>(), 5)

             
            .Register();
        }

    }
}

