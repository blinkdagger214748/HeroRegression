using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria.DataStructures;
using HeroRegression.HeroPlayers;
using HeroRegression.Items.Material;

namespace HeroRegression.Items.Accessories.Crystal
{
    class Lazurite : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lazurite");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "青金石");
            Tooltip.SetDefault("Increases the Mana limit by 40 and Mana regeneration by 1,\n" +
            "reducing spell damage by 5% \n");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "增加40点魔力上限和1点魔法回复，减少5%魔法伤害\n");
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
            player.GetDamage(DamageClass.Magic) += 0.05f;

            player.manaRegen += 2;
            player.statManaMax2 += 40;
        }
        public override void AddRecipes()
        {
            CreateRecipe()

            .AddIngredient(ItemID.Sapphire, 5)
            .AddIngredient(ItemID.ManaCrystal, 3)
            .AddIngredient(ModContent.ItemType<BrokenInitialHeart>(), 3)
            // 在铁砧旁边才能合成
            .AddTile(TileID.Anvils)
             
            .Register();
        }
    }
}