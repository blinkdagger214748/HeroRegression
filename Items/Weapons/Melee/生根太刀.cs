using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;


namespace HeroRegression.Items
{
    //文件名
    public class 生根太刀 : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("生根太刀");

        }

        public override void SetDefaults()
        {
            
            Item.damage = 15;
            
            Item.knockBack = 5f;
            
            Item.crit = 3;

            
            Item.rare = 4;
            
            Item.useTime = 17;
            Item.useAnimation = 17;
            
            
           
           
            
            
            Item.useStyle = 1;
            
            Item.autoReuse = true;
           
            
           
           
            
            
            Item.DamageType = DamageClass.Melee;

            
            Item.value = Item.sellPrice(0, 0, 80, 0);
            
            
            Item.UseSound = SoundID.Item1;
           
            Item.scale = 1.2f;
           
          
            Item.width = 30;
            Item.height = 56;
         
            Item.maxStack = 1;
          
           

           
       
            Item.useTurn = false;
        }
		// 物品合成表的设置部分
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

