using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria.DataStructures;
using HeroRegression.HeroPlayers;
using HeroRegression.Items.Beginner;

namespace HeroRegression.Items.Accessories.Crystal
{
    class BloodCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood Crystal");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "血结晶");
            Tooltip.SetDefault("Raise the 20 HealthMax. 0.5 Regen\n" );
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "提升20血量上限，0.5血量回复\n");
        }
        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 70;
            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 30, 0);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegen+= 1;
            player.statLifeMax2 += 20;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Ruby, 3)
            .AddIngredient(ItemID.Coral, 3)
            .AddIngredient(ItemID.LifeCrystal, 1)
            .AddIngredient(ModContent.ItemType<BrokenInitialHeart>(), 3)
            // 在铁砧旁边才能合成
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
