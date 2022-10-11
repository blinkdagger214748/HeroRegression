using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using HeroRegression.Tiles.矿物;

namespace HeroRegression.Items.Placeable.Block
{
	public class 古木 : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Broken wood");
			DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "远古遗木");
			Tooltip.SetDefault("Made in heaven");
			Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "来自远古植物的遗产");
			// ticksperframe, frameCount

		}

		// TODO -- Velocity Y smaller, post NewItem?
		public override void SetDefaults() {
			Item.width = 32;
			Item.height = 32;
			Item.maxStack = 999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.consumable = true;
			Item.useAnimation = 5;
			Item.useTime = 5;
			Item.useStyle = 1;
			Item.value = 1000;
			Item.rare = ItemRarityID.Blue;
			Item.createTile = ModContent.TileType<古木物块>();
		}


	}

}