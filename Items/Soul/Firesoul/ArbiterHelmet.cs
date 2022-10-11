using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using HeroRegression.Items.Material;

namespace HeroRegression.Items.Soul.Firesoul
{
    [AutoloadEquip(EquipType.Head)]
    public class ArbiterHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arbiter Helmet");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "裁决头盔");
            Tooltip.SetDefault("[c/FF3333:From hell.]" +
                "\nIncreases ranged damage by 5%.Increase the summoning limit by 1.");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "[c/FF3333:来自地狱]" +
                "\n增加5%的召唤伤害，1召唤栏。");
        }

        public override void SetDefaults()
        {
            Item.width = 18;    //宽
            Item.height = 18;   //高
            Item.value = Item.sellPrice(0, 0, 60, 0);   //价格
            Item.rare = 3; //稀有度
            Item.defense = 4;
        }

        public override void UpdateEquip(Player player)
        {
            player.maxMinions += 1;
            player.GetDamage(DamageClass.Summon) += 0.05f;

        }


        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ArbiterArmor>() && legs.type == ModContent.ItemType<ArbiterPanty>();
        }

        public override void UpdateArmorSet(Player player)
        {
            // 套装描述
            string bonus = "增加1召唤栏.";
            player.setBonus = bonus;
            player.maxMinions += 1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Calcificationofcrystallization>(), 4)
            .AddIngredient(ItemID.Hellstone, 26)
            .AddIngredient(ItemID.AshBlock, 15)
            .AddIngredient(ItemID.ClayBlock, 15)
            // 在铁砧旁边才能合成
            .AddTile(TileID.Anvils)
             
            .Register();
        }

    }
}
