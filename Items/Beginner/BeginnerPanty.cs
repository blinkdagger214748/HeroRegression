using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HeroRegression.Items.Beginner
{
    [AutoloadEquip(EquipType.Legs)]
    public class BeginnerPanty : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Beginner's panty");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "初心者连裤");
            Tooltip.SetDefault("Never forget the original intention." +
                "Increases movement speed by 10%.");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "不忘初心，方得始终。" +
                "\n* 建议加到模组里。" +
                "\n增加10%的移动速度。");
        }

        public override void SetDefaults()
        {
            Item.width = 18;    //宽
            Item.height = 18;   //高
            Item.value = Item.sellPrice(0, 0, 45, 0);   //价格
            Item.rare = ItemRarityID.Green;  //稀有度

            // 防御+3
            Item.defense = 3;
        }

        public override void UpdateEquip(Player player)
        {
            // 增加玩家百分之10的移动速度
            player.maxRunSpeed += 0.1f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
          
            .AddIngredient(ItemID.Diamond, 2)  //钻石
            .AddIngredient(ModContent.ItemType<BrokenInitialHeart>(), 5)
            // 在铁砧旁边才能合成
            .AddTile(TileID.Anvils)
             
            .Register();
        }
    }
}