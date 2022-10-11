using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System;
using HeroRegression.Items.Material;

namespace HeroRegression.Items.Weapons.Ranged
{
    class Rapidfirepistol : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rapid-fire pistol");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "速射手枪");
            Tooltip.SetDefault("[c/42426F:The only thing faster than me is the light]");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "[c/42426F:比我快的只有光]");
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 48;
            Item.damage = 15;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 4f;
            Item.rare = 3;
            Item.maxStack = 1;
            Item.crit = 4;
            Item.useTime = 13;
            Item.useAnimation = 13;
            Item.scale = 0.7f;
            Item.UseSound = SoundID.Item41;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.FlintlockPistol, 1)
            .AddIngredient(ItemID.Harpoon, 1)
            .AddIngredient(ModContent.ItemType<Calcificationofcrystallization>(), 8)
            .AddIngredient(ItemID.SilverBar, 12)
            // 在铁砧旁边才能合成
            .AddTile(TileID.Anvils)
            .Register();
            CreateRecipe()
.AddIngredient(ItemID.FlintlockPistol, 1)
.AddIngredient(ItemID.Harpoon, 1)
.AddIngredient(ModContent.ItemType<Calcificationofcrystallization>(), 8)
.AddIngredient(ItemID.TungstenBar, 12)
// 在铁砧旁边才能合成
.AddTile(TileID.Anvils)
.Register();
        }
    }

}
