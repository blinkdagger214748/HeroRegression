using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using HeroRegression.Items.Beginner;
using HeroRegression.Items;

namespace HeroRegression.Items.Soul.Icesoul
{
    [AutoloadEquip(EquipType.Legs)]
    public class FrozenPanty : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frozen Panty");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "寒锋裤");
            Tooltip.SetDefault("[c/3366FF:From the polar regions.]" +
                "Increases movement speed by 10%.");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "[c/3366FF:来自极地]" +
                "\n增加10%移速,5%的近战伤害与5%攻速");
        }

        public override void SetDefaults()
        {
            Item.width = 18;    //宽
            Item.height = 18;   //高
            Item.value = Item.sellPrice(0, 1, 17, 0);   //价格
            Item.rare = 4;  //稀有度

            // 防御+5
            Item.defense = 6;

        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.05f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.10f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<BeginnerPanty>(), 1)
            .AddIngredient(ItemID.EskimoPants, 1)
            .AddIngredient(ModContent.ItemType<Lingcuistone>(), 3)
            .AddIngredient(ItemID.Bone, 12)
            .AddIngredient(ItemID.IceTorch, 12)
            .AddTile(TileID.Anvils)
             
            .Register();
        }
    }
}