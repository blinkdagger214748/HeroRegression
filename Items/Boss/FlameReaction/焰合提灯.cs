
	using System;
	using System.IO;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using Terraria;
	using Terraria.ID;
	using Terraria.ModLoader;
	using static Terraria.ModLoader.ModContent;
	using Terraria.Localization;
using HeroRegression.NPCs.Boss.FlameReaction;

namespace HeroRegression.Items.Boss.FlameReaction
{
	public class 焰合提灯 : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flame lantern");
			DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "焰合提灯");
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.RecallPotion);
			Item.maxStack = 99;
			Item.consumable = true;
			Item.scale = 0.5f;
			Item.UseSound = SoundID.Item37;
			return;
		}
		public override bool CanUseItem(Player player)
		{
			return !NPC.AnyNPCs(NPCType<FlameReactionBoss>());
		}
		public override bool? UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI,NPCType<FlameReactionBoss>());
			return true;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
			.AddIngredient(ItemID.GoldBar, 4)
			.AddIngredient(ItemID.MeteoriteBar, 4)
			.AddIngredient(ItemID.FallenStar, 3)
			.AddTile(TileID.Anvils)
			.AddIngredient(ItemID.LavaBucket, 1)
			.Register();

			CreateRecipe().AddIngredient(ItemID.PlatinumBar, 4).AddIngredient(ItemID.MeteoriteBar, 4).AddIngredient(ItemID.FallenStar, 3).AddTile(TileID.Anvils).AddIngredient(ItemID.LavaBucket, 1).Register();


		}
	}

}
