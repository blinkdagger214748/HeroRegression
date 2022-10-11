using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HeroRegression.Items.Boss.SeedsOfOrigin
{
    [AutoloadEquip(EquipType.Head)]
    public class SlimeHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slime Helmet");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "史莱姆头盔");
            Tooltip.SetDefault("[c/238E23:This is my last poem.]" +
                "\nIncreases magic damage and crit by 4%.ncreases movement speed by 5%.");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "[c/238E23:这是我的终末之诗]" +
                "\n增加4%的魔法伤害和暴击，5%移速。");
        }

        public override void SetDefaults()
        {
            Item.width = 18;    //宽
            Item.height = 18;   //高
            Item.value = Item.sellPrice(0, 0, 60, 0);   //价格
            Item.rare = ItemRarityID.Green; //稀有度
            Item.defense = 4;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) += 0.04f;
            player.GetCritChance(DamageClass.Magic) += 4;
            player.moveSpeed += 0.05f;

        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<SlimeArmor>() && legs.type == ModContent.ItemType<SlimePanty>();
        }

        public override void UpdateArmorSet(Player player)
        {
            // 套装描述
            string bonus = "增加5%的摸法暴击率。取消摔落伤害";
            player.setBonus = bonus;
            player.GetCritChance(DamageClass.Magic) += 5;
            player.noFallDmg = true;
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
