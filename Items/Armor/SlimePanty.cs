using HeroRegression.Items.Material;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HeroRegression.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class SlimePanty : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slime Panty");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "史莱姆裤");
            Tooltip.SetDefault("[c/238E23:This is my last poem.]" +
                "Increases movement speed by 12%.Increases magic damage by 5%.");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "[c/238E23:这是我的终末之诗]" +
                "\n增加2%的魔法伤害和暴击，12%移速");
        }

        public override void SetDefaults()
        {
            Item.width = 18;    //宽
            Item.height = 18;   //高
            Item.value = Item.sellPrice(0, 0, 55, 0);   //价格
            Item.rare = ItemRarityID.Green;  //稀有度

            // 防御+5
            Item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            // 增加玩家百分之12的移动速度
            player.moveSpeed += 0.12f;

            player.GetDamage(DamageClass.Magic) += 0.02f;
            player.GetCritChance(DamageClass.Magic) += 2;
        }

        public override void AddRecipes()
        {
            CreateRecipe()

            .AddIngredient(ItemID.Gel, 25)
            .AddIngredient(ModContent.ItemType<Calcificationofcrystallization>(), 5)
            // 在铁砧旁边才能合成
            .AddTile(TileID.Anvils)

            .Register();
        }
    }
}