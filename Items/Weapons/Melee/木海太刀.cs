using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;


namespace HeroRegression.Items.Weapons.Melee
{
    //文件名
    public class 木海太刀 : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("木海太刀");

        }

        public override void SetDefaults()
        {
            
            Item.damage = 23;
            
            Item.knockBack = 4f;
            
            Item.crit = 4;

            
            Item.rare = 4;
            
            Item.useTime = 14;
            Item.useAnimation = 14;
            
            
           
           
            
            
            Item.useStyle = 1;
            
            Item.autoReuse = true;
           
            
           
           
            
            
            Item.DamageType = DamageClass.Melee;


            Item.value = Item.sellPrice(0, 0, 80, 0);
            
            
            Item.UseSound = SoundID.Item1;
           
            Item.scale = 1;
           
          
            Item.width = 35;
            Item.height = 40;
         
            Item.maxStack = 1;
          



       
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

