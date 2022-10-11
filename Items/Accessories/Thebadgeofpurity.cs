using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria.DataStructures;
using HeroRegression.HeroPlayers;

namespace HeroRegression.Items.Accessories
{
    class Thebadgeofpurity : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The badge of purity");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "纯净徽章");
            Tooltip.SetDefault("Immune knockback, \n" +
                "slightly increased speed,magic regen and Max Health.\n" +
                "[c/238E68:(backstory)]");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "免疫击退，小幅提升速度，魔力回复和最大血量值\n" +
                "[c/238E68:（背景故事）]\n"+
                "[c/238E23:这是冒险家口中所说的树妖他老祖宗？妈了个逼的，怎么会有这]\n" +
                "[c/238E23:么个畸形东西，看上去像是已逝的灵魂被封印了一般。]\n" +
                "[c/238E23:总之，探险之旅还要继续]");
        }
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 35, 0);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 10;
            player.noKnockback = true;
            player.moveSpeed += 0.06f;
            player.manaRegen += 2;
        }
    }
}
