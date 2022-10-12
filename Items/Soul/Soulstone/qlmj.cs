using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;


namespace HeroRegression.Items.Soul.Soulstone
{
    //文件名
    public class qlmj : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Wooden soul sword");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "嵌灵木剑");
        }

        public override void SetDefaults()
        {
            
            Item.damage = 19;
            
            Item.knockBack = 4f;
            
            Item.crit = 2;

            
            Item.rare = 5;
            
            Item.useTime = 20;
            Item.useAnimation = 20;
            
            
           
           
            
            
            Item.useStyle = 1;
            
            Item.autoReuse = true;
           
            
           
           
            
            
            Item.DamageType = DamageClass.Melee;

            Item.shoot = ModContent.ProjectileType<Projectiles.Friendly.Melee.Qljq>();
            Item.value = Item.sellPrice(0, 0, 80, 0);
            
            
            Item.UseSound = SoundID.Item1;
           
            Item.scale = 1.2f;
           
          
            Item.width = 44;
            Item.height = 46;
         
            Item.maxStack = 1;
          
           
            
            Item.shootSpeed = 12f;
       
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
		public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, DustID.Phantasmal, 0, 0, 150, Color.White, 0.8f);
        }
    }
}

