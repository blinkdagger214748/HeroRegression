using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using HeroRegression.Items.Material;

namespace HeroRegression.Items.Weapons.Melee
{
    //文件名
    public class 浓眼晶状刃 : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("浓眼晶状刃");
            Tooltip.SetDefault("好恶心的剑刃！" + "\n有百分之10的几率吸血，吸血效果为一滴血");

        }

        public override void SetDefaults()
        {
            
            Item.damage = 13;
            
            Item.knockBack = 1.5f;
            
            Item.crit = 2;

            
            Item.rare = ItemRarityID.Green;
            
            Item.useTime = 20;
            Item.useAnimation = 20;
            
            
           
           
            
            
            Item.useStyle = 1;
            
            Item.autoReuse = true;
           
            
           
           
            
            
            Item.DamageType = DamageClass.Melee;

            Item.shoot = ModContent.ProjectileType<Projectiles.眼球弹幕>();
            Item.value = Item.sellPrice(0, 0, 80, 0);
            
            
            Item.UseSound = SoundID.Item1;
           
            Item.scale = 1.2f;
           
          
            Item.width = 44;
            Item.height = 46;
         
            Item.maxStack = 1;
          


            Item.shootSpeed = 12f;
       
            Item.useTurn = false;
        }
       

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<恶性眼瘤>(), 14)
            .AddTile(TileID.Anvils)
            .AddIngredient(ItemID.Lens, 5)


            .Register();
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, DustID.Blood, 0, 0, 150, Color.Red, 0.8f);
        }
    }
}

