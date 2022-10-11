using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System;

namespace HeroRegression.Items.Material
{
    class WorldCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("WorldCrystal");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "泰拉水晶");
        }
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 48;
            Item.rare = 9;
            Item.value = Item.buyPrice(0, 8, 90, 0);
            Item.maxStack = 999;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.ChlorophyteOre, 24)
            .AddIngredient(ItemID.FragmentSolar, 2)
            .AddIngredient(ItemID.FragmentVortex, 2)
            .AddIngredient(ItemID.FragmentNebula, 2)
            .AddIngredient(ItemID.FragmentStardust, 2)
            .AddIngredient(ItemID.SoulofLight, 2)
            .AddIngredient(ItemID.SoulofNight, 2)
            .AddIngredient(ItemID.CrystalShard, 2)
            .AddIngredient(ItemID.FallenStar, 2)
            .AddIngredient(ModContent.ItemType<BrokenInitialHeart>(), 4)
            // 在铁砧旁边才能合成
            .AddTile(TileID.MythrilAnvil)

            .Register();

        }
    }
}

