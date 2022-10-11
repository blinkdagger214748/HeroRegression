using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using HeroRegression.Items;
using HeroRegression.Items.Armor;

namespace HeroRegression.Items.Soul.Icesoul
{
    [AutoloadEquip(EquipType.Head)]
    public class FrozenHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frozen Helmet");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "寒锋面具");
            Tooltip.SetDefault("[c/3366FF:From the polar regions.]" +
                "\nIncreases melee crit by 6% and attack speed by 8%");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "[c/3366FF:来自极地]" +
                "\n增加6%的近战暴击与8%攻速");
        }

        public override void SetDefaults()
        {
            Item.width = 18;    //宽
            Item.height = 18;   //高
            Item.value = Item.sellPrice(0, 1, 60, 0);   //价格
            Item.rare = 4; //稀有度
            Item.defense = 8;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<FrozenArmor>() && legs.type == ModContent.ItemType<FrozenPanty>();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Melee) += 6;
            player.GetAttackSpeed(DamageClass.Melee) += 0.08f;

        }
        public override void UpdateArmorSet(Player player)
        {
            // 套装描述
            string bonus = "中幅增加下落，跳跃与移动机动性";
            player.setBonus = bonus;
            player.jumpSpeedBoost += 0.11f;
            player.maxFallSpeed += 0.2f;
            player.moveSpeed += 0.06f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<BeginnerHelmet>(), 1)
            .AddIngredient(ItemID.EskimoHood, 1)
            .AddIngredient(ModContent.ItemType<Placeable.Block.Lingcuistone>(), 4)
            .AddIngredient(ItemID.Bone, 9)
            .AddIngredient(ItemID.IceTorch, 9)
            .AddTile(TileID.Anvils)
             
            .Register();
        }

    }
}
