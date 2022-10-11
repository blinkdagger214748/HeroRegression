using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HeroRegression.Items.Beginner
{
    [AutoloadEquip(EquipType.Head)] //将你的物品识别为头盔
    public class BeginnerHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Beginner's Helmet");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "初心者头盔");
            Tooltip.SetDefault("Lost the original intention?" +
                "\n* My first heart is as it was." +
                "\nIncreases all damage by 5%.");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "失初心乎？" +
                "\n* 我之初心如故。" +
                "\n增加5%的全部伤害。");
        }

        public override void SetDefaults()
        {
            Item.width = 18;    //宽
            Item.height = 18;   //高
            Item.value = Item.sellPrice(0, 0, 45, 0);   //价格
            Item.rare = ItemRarityID.Green; //稀有度
            Item.defense = 3;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.05f;
            //增加5％的全部伤害
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<BeginnerArmor>() && legs.type == ModContent.ItemType<BeginnerPanty>();
        }

        // 套装效果
        public override void UpdateArmorSet(Player player)
        {
            // 套装描述
            string bonus = "增加5%的所有暴击率";
            player.setBonus = bonus;
            player.GetCritChance(DamageClass.Generic) += 5;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
        
            .AddIngredient(ItemID.Diamond, 1)    //钻石
            .AddIngredient(ModContent.ItemType<BrokenInitialHeart>(), 5)
            // 在铁砧旁边才能合成
            .AddTile(TileID.Anvils)
             
            .Register();
        }
    }
}