using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HeroRegression.Items.Placeable.Trophy;

namespace HeroRegression.Tiles
{
	public class BossTrophy : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.StyleWrapLimit = 36;
			TileObjectData.addTile(Type);
			DustType = 7;
			TileID.Sets.DisableSmartCursor[Type] = true;
			ModTranslation name = CreateMapEntryName(null);
			name.SetDefault(ChnTrans("Trophy","纪念章"));
			AddMapEntry(new Color(120, 85, 60), name);
			TileID.Sets.FramesOnKillWall[Type] = true;
		}
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			int item = 0;
			switch (frameX / 54)
			{
				case 0:
					item = ModContent.ItemType<SeedBossTrophy>();
					break;
			}
			if (item > 0)
			{
				Item.NewItem(new EntitySource_TileBreak(i, j, null), i * 16, j * 16, 48, 48, item, 1, false, 0, false, false);
			}
		}
	}
}
