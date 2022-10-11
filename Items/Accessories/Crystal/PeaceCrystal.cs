using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria.DataStructures;
using HeroRegression.HeroPlayers;
using HeroRegression.Items.Beginner;

namespace HeroRegression.Items.Accessories.Crystal
{
    class PeaceCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("PeaceCrystal");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "和平石");
            Tooltip.SetDefault("Increase the summoning limit by 1.\n" +
            "Increases the full blast rate by 6%. \n");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "增加1召唤上限，增加6%的所有暴击率。\n");
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
            player.GetCritChance(DamageClass.Magic) += 6;
            player.GetCritChance(DamageClass.Melee) += 6;
            player.GetCritChance(DamageClass.Ranged) += 6;
            player.GetCritChance(DamageClass.Throwing) += 6;
            player.maxMinions += 1;
        }
        public override void AddRecipes()
        {
            CreateRecipe()

            .AddIngredient(ItemID.Sunflower, 5)
            .AddIngredient(ItemID.Topaz, 6)

            // 在铁砧旁边才能合成
            .AddTile(TileID.Anvils)
             
            .Register();
        }
    }
}
