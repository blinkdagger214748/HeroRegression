using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria.DataStructures;
using HeroRegression.HeroPlayers;
using HeroRegression.Items.Accessories.Crystal;

namespace HeroRegression.Items.Accessories
{
    class ColorfulCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("ColorfulCrystal");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "星环石");
            Tooltip.SetDefault("Raise the 20 HealthMax and ManaMax." +
                "Increases armor piercing by 4,Increases the full blast rate by 7%. \n");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "提升20血量与魔力上限，4点穿甲7%暴击\n");
        }
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.accessory = true;
            Item.rare = 5;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.defense = 4;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 20;
            player.statManaMax2 += 20;
            player.GetCritChance(DamageClass.Generic) += 7;
            player.GetArmorPenetration(DamageClass.Generic) += 4;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<BloodCrystal>(), 1)
            .AddIngredient(ModContent.ItemType<Lazurite>(), 1)
            .AddIngredient(ModContent.ItemType<Chlorite>(), 1)
            .AddIngredient(ModContent.ItemType<PeaceCrystal>(), 1)
            .AddIngredient(ItemID.Diamond, 4)
            .AddIngredient(ItemID.SoulofFlight, 5)
            // 在铁砧旁边才能合成
            .AddTile(TileID.MythrilAnvil)
             
            .Register();
        }
    }
}
