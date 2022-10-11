using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using HeroRegression.Items;
using HeroRegression.Items.Armor;

namespace HeroRegression.Items.Soul.Icesoul
{
    [AutoloadEquip(EquipType.Body)]
    public class FrozenArmor : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frozen Armor");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "寒锋护甲");
            Tooltip.SetDefault("[c/3366FF:From the polar regions.]" +
                "\nIncreases melee damage by 6% and attack speed by 8%");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "[c/3366FF:来自极地]" +
                "\n增加6%的近战伤害与8%攻速");
        }

        public override void SetDefaults()
        {
            Item.width = 18;    //宽
            Item.height = 18;   //高
            Item.value = Item.sellPrice(0, 2, 22, 80);   //价格
            Item.rare = 4;  //稀有度
            Item.defense = 7;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetAttackSpeed(DamageClass.Melee) += 0.08f;
            player.GetDamage(DamageClass.Melee) += 0.06f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<BeginnerArmor>(), 1)
            .AddIngredient(ItemID.EskimoCoat, 1)
            .AddIngredient(ModContent.ItemType<Placeable.Block.Lingcuistone>(), 3)
            .AddIngredient(ItemID.Bone, 10)
            .AddIngredient(ItemID.IceTorch, 10)


            // 在铁砧旁边才能合成
            .AddTile(TileID.Anvils)
             
            .Register();
        }
    }
}