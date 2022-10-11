using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;


namespace HeroRegression.Items.Tools.Axe
{
    //文件名
    public class 霜木斧 : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("霜木斧");

        }

        public override void SetDefaults()
        {

            Item.damage = 5;

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
          

             Item.axe = 9;

       
            Item.useTurn = false;
        }
       
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.SnowBlock, 15)
            .AddTile(TileID.WorkBenches)
            .AddIngredient(ItemID.BorealWood, 20)

             
            .Register();
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, DustID.SnowBlock, 0, 0, 150, Color.White, 0.8f);
        }
    }
}