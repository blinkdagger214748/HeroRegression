
	using System;
	using System.IO;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using Terraria;
	using Terraria.ID;
	using Terraria.ModLoader;
	using static Terraria.ModLoader.ModContent;
	using Terraria.Localization;
    using HeroRegression.Items.Boss.SeedsOfOrigin;
    using HeroRegression.Items.Accessories;

 namespace HeroRegression.Items.Boss.SeedsOfOrigin
{
	public class 起源之种财宝袋 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("财宝袋");
			Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}
		public override void SetDefaults()
		{
			Item.maxStack = 999;
			Item.consumable = true;
			Item.width = 24;
			Item.height = 24;
			Item.rare = ItemRarityID.Expert;
			Item.expert = true;
		}
		public override bool CanRightClick()
		{
			return true;
		}
		public override void OpenBossBag(Player player)
		{
			CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height)
						  , Color.GreenYellow, "......", true, false);
			int k = Main.rand.Next(5);
			if (k == 0)
			{
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<GreenShadeBow>(), 1);
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<Thebadgeofpurity>(), 1);
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<GreenCrystalYoYo>(), 1);
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<Overloaded_Energy>(), 1);
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<Calcificationofcrystallization>(), 12);
			}
			if (k == 1)
			{
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<OriginalInterestItem>(), 1);
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<GreenCrystalYoYo>(), 1);
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<Overloaded_Energy>(), 1);
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<Calcificationofcrystallization>(), 16);
			}
			if (k == 2)
			{
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<Brilliant>(), 1);
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<GreenCrystalYoYo>(), 1);
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<Overloaded_Energy>(), 1);
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<Calcificationofcrystallization>(), 17);

			}
			if (k == 3)
			{
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<GreenCrystalYoYo>(), 1);
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<GreenShadeBow>(), 1);
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<GroupOfHeroes>(), 1);
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<Overloaded_Energy>(), 1);
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<Calcificationofcrystallization>(), 14);
			}
			if (k == 4)
			{
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<GreenCrystalYoYo>(), 1);
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<GroupOfHeroes>(), 1);
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<Brilliant>(), 1);
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<Overloaded_Energy>(), 1);
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<Calcificationofcrystallization>(), 13);
			}
			if (k == 5)
			{
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<OriginalInterestItem>(), 1);
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<GreenCrystalYoYo>(), 1);
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<GroupOfHeroes>(), 1);
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<Overloaded_Energy>(), 1);
				player.QuickSpawnItem(player.GetSource_OpenItem(Type), ItemType<Calcificationofcrystallization>(), 14);
			}
		}
		public override int BossBagNPC => NPCType<NPCs.Boss.SeedsOfOrigin.SeedsOfOrigin>();
	}
}