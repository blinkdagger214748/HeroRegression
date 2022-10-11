using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HeroRegression.Items.Material
{
    public class BrokenInitialHeart : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Broken initial heart");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "破碎初心");
            Tooltip.SetDefault("The heart that has passed away.");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "已逝之心。");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 14;
            Item.maxStack = 999;
            Item.value = 100;
            Item.rare = ItemRarityID.Green;
        }
    }
}