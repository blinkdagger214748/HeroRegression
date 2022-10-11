using HeroRegression.Items.Boss.SeedsOfOrigin;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HeroRegression.Items.Soul.Firesoul
{
    [AutoloadEquip(EquipType.Body)]
    public class ArbiterArmor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arbiter Armor");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "裁决护甲");
            Tooltip.SetDefault("[c/FF3333:From hell.]" +
                "\nnIncreases ranged damage by 5%.");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "[c/FF3333:来自地狱]" +
                "\n增加5%的召唤伤害");
        }

        public override void SetDefaults()
        {
            Item.width = 18;    //宽
            Item.height = 18;   //高
            Item.value = Item.sellPrice(0, 0, 75, 0);   //价格
            Item.rare = 3;  //稀有度
            Item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.05f;
        }


        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Calcificationofcrystallization>(), 5)
            .AddIngredient(ItemID.Hellstone, 40)
            .AddIngredient(ItemID.AshBlock, 20)
            .AddIngredient(ItemID.ClayBlock, 20)
            // 在铁砧旁边才能合成
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}