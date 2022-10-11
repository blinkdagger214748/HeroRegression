using HeroRegression.Items.Material;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HeroRegression.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class SlimeArmor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slime Armor");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "史莱姆甲");
            Tooltip.SetDefault("[c/238E23:This is my last poem.]" +
                "\nIncreases magic damage and crit by 5%.ncreases movement speed by 5%.");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "[c/238E23:这是我的终末之诗]" +
                "\n增加5%的魔法伤害和暴击，5%移速。");
        }

        public override void SetDefaults()
        {
            Item.width = 18;    //宽
            Item.height = 18;   //高
            Item.value = Item.sellPrice(0, 0, 75, 0);   //价格
            Item.rare = ItemRarityID.Green;  //稀有度
            Item.defense = 6;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) += 0.05f;
            player.GetCritChance(DamageClass.Magic) += 5;
            player.moveSpeed += 0.05f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()

            .AddIngredient(ItemID.Gel, 30)
            .AddIngredient(ModContent.ItemType<Calcificationofcrystallization>(), 6)
            // 在铁砧旁边才能合成
            .AddTile(TileID.Anvils)

            .Register();
        }
    }
}