using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria.DataStructures;
using HeroRegression.HeroPlayers;

namespace HeroRegression.Items.Accessories
{
    class Disorder : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Disorder");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "乱形");
            Tooltip.SetDefault("Completely defiled pure medals, sacrificing part of your life, \n" +
                " ancient necromancer powers and entangled ghosts will gladly begin the ritual\n" );
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "完全玷污的纯净勋章，献祭你的部分生命，\n" +
                "古老的死灵力量和纠缠怨魂会很乐意开始仪式\n" +
                "减少30HP上限16防御力,获得6％免伤与穿甲,\n" +
                "获得20％暴击率，召唤则是＋2召唤栏补偿");
        }
        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 70;
            Item.accessory = true;
            Item.rare = 6;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.defense -= 16;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 -= 30;
            player.GetCritChance(DamageClass.Generic) += 20;
            player.GetArmorPenetration(DamageClass.Generic) += 6;
            player.endurance += 0.06f;
            player.maxMinions += 2;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Thebadgeofpurity>(), 1)
            .AddIngredient(ItemID.SoulofNight, 15)
            // 在铁砧旁边才能合成
            .AddTile(TileID.MythrilAnvil)
             
            .Register();
        }
    }
}