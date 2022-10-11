using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria.DataStructures;
using HeroRegression.HeroPlayers;

namespace HeroRegression.Items.Accessories
{
    class ToughBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tough blade");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "坚韧刀片");
            Tooltip.SetDefault("Increases melee crit by 5%. \n" +
                "Buy it from the adventurer at night\n");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "增加5%近战暴击率（夜晚起源冒险家）\n");
        }
        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 70;
            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 35, 0);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetCritChance(DamageClass.Melee) += 5;
        }
    }
}
