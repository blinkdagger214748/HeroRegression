using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace HeroRegression.Items
{
	public class Lingcuistone : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("灵翠石");
			Tooltip.SetDefault("蕴含生灵之力的结晶体");
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
			//Item.createTile = Mod.TileType("灵翠石物块");
		}


	}

}