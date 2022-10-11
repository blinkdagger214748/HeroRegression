using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Localization;


namespace HeroRegression.Items.Soul
{
    //文件名
    public class Dawnanddusk : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Dawn and dusk");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "晨昏");

        }

        public override void SetDefaults()
        {
            
            Item.damage = 114;
            
            Item.knockBack = 1.5f;
            
            Item.crit = 32;

            
            Item.rare = 9;
            
            Item.useTime = 12;
            Item.useAnimation = 12;
            
            
           
           
            
            
            Item.useStyle = 1;
            
            Item.autoReuse = true;
           
            
           
           
            
            
            Item.DamageType = DamageClass.Melee;

          
            Item.value = Item.sellPrice(0, 11, 54, 14);
            
            
            Item.UseSound = SoundID.Item1;
           
            Item.scale = 1.8f;
           
          
            Item.width = 44;
            Item.height = 56;
         
            Item.maxStack = 1;
          


            Item.shootSpeed = 12f;
       
            Item.useTurn = true;
        }
       

  
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, DustID.Blood, 0, 0, 150, Color.White, 0.8f);
        }
    }
}
