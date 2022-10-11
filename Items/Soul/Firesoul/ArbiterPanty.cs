using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using HeroRegression.Items.Boss.SeedsOfOrigin;

namespace HeroRegression.Items.Soul.Firesoul
{
    [AutoloadEquip(EquipType.Legs)]
    public class ArbiterPanty : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arbiter Panty");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "裁决裤");
            Tooltip.SetDefault("[c/FF3333:From hell.]" +
                "Increases movement speed by 10%.Increases ranged damage by 5%.");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "[c/FF3333:来自地狱]" +
                "\n增加5%的召唤伤害，10%移速");
        }

        public override void SetDefaults()
        {
            Item.width = 18;    //宽
            Item.height = 18;   //高
            Item.value = Item.sellPrice(0, 0, 55, 0);   //价格
            Item.rare = 3;  //稀有度

            // 防御+5
            Item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            // 增加玩家百分之12的移动速度
            player.maxRunSpeed += 0.12f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Calcificationofcrystallization>(), 4)
            .AddIngredient(ItemID.Hellstone, 35)
            .AddIngredient(ItemID.AshBlock, 17)
            .AddIngredient(ItemID.ClayBlock, 17)
            // 在铁砧旁边才能合成
            .AddTile(TileID.Anvils)
             
            .Register();
        }
    }
}