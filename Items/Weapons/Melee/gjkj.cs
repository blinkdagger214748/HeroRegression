using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;


namespace HeroRegression.Items.Weapons.Melee
{
    //文件名
    public class gjkj : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("古晶阔剑");

        }

        public override void SetDefaults()
        {
            
            Item.damage = 30;
            
            Item.knockBack = 5f;
            
            Item.crit = 3;

            
            Item.rare = 4;
            
            Item.useTime = 20;
            Item.useAnimation = 20;
            
            
           
           
            
            
            Item.useStyle = 1;
            
            Item.autoReuse = true;
           
            
           
           
            
            
            Item.DamageType = DamageClass.Melee;


            Item.value = Item.sellPrice(0, 0, 80, 0);
            
            
            Item.UseSound = SoundID.Item1;
           
            Item.scale = 1.2f;
           
          
            Item.width = 64;
            Item.height = 64;
         
            Item.maxStack = 1;
          


            Item.shootSpeed = 25f;
       
            Item.useTurn = false;
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

