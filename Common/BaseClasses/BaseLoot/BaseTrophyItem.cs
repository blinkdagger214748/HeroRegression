using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ID;
using HeroRegression.Tiles;

namespace HeroRegression.Common.BaseClasses.BaseLoot
{
    public abstract class BaseTrophyItem:ModItem
    {
		public void TrophyStatics(string name)
		{
			SacrificeTotal = 1;
			DisplayName.SetDefault(name);
		}
		public void TrophyDefaults(int bossSort)
		{
			Item.width = 30;
			Item.height = 30;
			Item.maxStack = 99;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.value = 50000;
			Item.rare = ItemRarityID.Blue;
			Item.createTile = ModContent.TileType<BossTrophy>();
			Item.placeStyle = bossSort;
		}
	}
}
