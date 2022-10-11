
using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Localization;

namespace HeroRegression.Items.SummonItems
{
    public class 重生之晶 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("重生之晶");
            Tooltip.SetDefault("起源之种将会降临");
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.RecallPotion);
            Item.maxStack = 99;
            Item.consumable = true;
            Item.scale = 0.5f;
            Item.useStyle = 4;
            Item.scale = 1.2f;
            Item.UseSound = SoundID.Item37;
            return;
        }
        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(NPCType<NPCs.Boss.SeedsOfOrigin.SeedsOfOrigin>());
        }


        public override bool? UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, NPCType<NPCs.Boss.SeedsOfOrigin.SeedsOfOrigin>());
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.RichMahogany, 20)
            .AddTile(TileID.Anvils)
            .AddIngredient(ItemID.Emerald, 1)
            .Register();
        }
    }
}