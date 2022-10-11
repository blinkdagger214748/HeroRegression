using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HeroRegression.Items
{
	public class 恶性眼瘤 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("恶性眼瘤");
			
			Tooltip.SetDefault("真恶心！");
			
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 32;
			Item.maxStack = 999;
			Item.value = 100;
			Item.rare = ItemRarityID.Green;
		}
	}
}