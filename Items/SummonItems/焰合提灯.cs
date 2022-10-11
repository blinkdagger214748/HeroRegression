
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

namespace HeroRegression.Items.SummonItems
{
    public class 焰合提灯 : SummonItem
    {
        public override void SetStaticDefaults()
        {
            ItemName("Flame lantern", "焰合提灯");
            ItemTooltip("Summons Flame Reaction when used at day", "白天使用时召唤焰色反应");
        }
        public override void SetDefaults()
        {
            Defaults(58, 58, 20, Item.sellPrice(0, 0, 50, 0), ItemRarityID.Green, true, 99, .5f);
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(NPCType<FlameReactionBoss>())&&Main.dayTime;
        }
        public override bool? UseItem(Player player)
        {
            SpawnBoss(player, SoundID.Roar, NPCType<FlameReactionBoss>(), 0, -200);
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddRecipeGroup(RecipeSystem.AnyGoldBar, 4)
            .AddRecipeGroup(RecipeSystem.AnyEvilBar, 4)
            .AddIngredient(ItemID.FallenStar, 3)
            .AddTile(TileID.Anvils)
            .AddIngredient(ItemID.LavaBucket, 1)
            .Register();
        }
    }

}
