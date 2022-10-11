using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria.DataStructures;
using HeroRegression.HeroPlayers;
using HeroRegression.Items.Boss.SeedsOfOrigin;

namespace HeroRegression.Items.Accessories.Crystal
{
    class Chlorite : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chlorite");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "叶绿石");
            Tooltip.SetDefault("Increases armor piercing by 7\n");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "增加7点穿甲");
        }
        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 70;
            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 30, 0);
            Item.defense = 2;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetArmorPenetration(DamageClass.Generic) += 7;
        }
        public override void AddRecipes()
        {
            CreateRecipe()

            .AddIngredient(ItemID.JungleSpores, 15)
            .AddIngredient(ModContent.ItemType<Calcificationofcrystallization>(), 5)
            .AddIngredient(ItemID.Emerald, 5)
            .AddIngredient(ItemID.Stinger, 5)
            .AddIngredient(ItemID.Vine, 2)
            .AddIngredient(ItemID.JungleGrassSeeds, 1)
            .AddIngredient(ItemID.Bone, 20)
            // 在铁砧旁边才能合成
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}