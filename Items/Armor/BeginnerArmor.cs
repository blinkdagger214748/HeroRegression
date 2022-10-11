using HeroRegression.Items.Material;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HeroRegression.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class BeginnerArmor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Beginner's Light Armor");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "初心者轻甲");
            Tooltip.SetDefault("The first heart remains the same!" +
                "\nIncrease the summoning limit by 1.");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "初心依旧！" +
                "\n呜呜~群主别撤我管理员~" +
                "\n增加1召唤上限。");
        }

        public override void SetDefaults()
        {
            Item.width = 18;    //宽
            Item.height = 18;   //高
            Item.value = Item.sellPrice(0, 0, 45, 0);   //价格
            Item.rare = ItemRarityID.Green;  //稀有度
            Item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            // 增加1的最大召唤上限
            player.maxMinions += 1;
            // 增加20的生命上限
            player.statLifeMax2 += 20;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Diamond, 2) //钻石
            .AddIngredient(ModContent.ItemType<BrokenInitialHeart>(), 5)
            // 在铁砧旁边才能合成
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}
