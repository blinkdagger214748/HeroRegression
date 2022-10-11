using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace HeroRegression.Items.Boss.HeroOfBrewing
{
    class Sparklingbouquet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sparklingbouquet");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "闪耀花束");
			Tooltip.SetDefault("From hero of brewing.");
			Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "浮嘉·弥斯的魔法精华");
			// ticksperframe, frameCount

		}

		// TODO -- Velocity Y smaller, post NewItem?
		public override void SetDefaults()
		{

			Item.width = 32;
			Item.height = 32;
			Item.maxStack = 99;
			Item.value = 1000;
			Item.rare = 5;
		}
	}
    
}

