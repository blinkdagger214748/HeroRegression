using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using HeroRegression.Projectiles;

namespace HeroRegression.Items
{
    //文件名
    public class 共工 : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("共工");

        }

        public override void SetDefaults()
        {
            
            Item.damage = 25;
            
            Item.knockBack = 4f;
            
            Item.crit = 9;

            
            Item.rare = 4;
            
            Item.useTime = 16;
            Item.useAnimation = 16;
            
            
           
           
            
            
            Item.useStyle = 5;
            
            Item.autoReuse = true;
           
            
           
           
            
            
            Item.DamageType = DamageClass.Ranged;
          //  Item.noUseGraphic = true;
            Item.useAmmo = AmmoID.Arrow;
            Item.value = Item.sellPrice(0, 0, 80, 0);
            
            
            Item.UseSound = SoundID.Item1;
           
            Item.scale = 0.9f;
           
          
            Item.width =45;
            Item.height = 80;
         
            Item.maxStack = 1;
          
            Item.shoot = ModContent.ProjectileType<激流箭>();
            Item.shootSpeed = 5;
           
       
           
        }
       
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 vel = velocity;
            Projectile.NewProjectile(source,position, vel, ModContent.ProjectileType<激流箭>(), damage, 5f, player.whoAmI);
            return false;
        }
    }
}

