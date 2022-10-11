using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Terraria.DataStructures;
using HeroRegression.HeroPlayers;

namespace HeroRegression.Items.Accessories
{
    class Overloaded_Energy : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Overloaded Energy");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "过载原能");
            Tooltip.SetDefault("Critical strike inflicts poisioned for 4 sec.\n" +
                "Critical strike has 50% chance to heal player for 10 health.\n" +
                "When heal activated,a homing bullet will shoot at the mouse's direction,deals 9 damage.");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese,"暴击造成4秒中毒，且有50%几率回复玩家10生命值。\n" +
                "当生命回复触发时，玩家会向鼠标方向发射一枚追踪子弹，造成9点伤害。");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 6));
        }
        
        public override void SetDefaults()
        {
            Item.width = 300;
            Item.height = 300;
            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.expert = true;
            Item.value = Item.sellPrice(0, 0, 35, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            Main.LocalPlayer.GetModPlayer<HRPlr>().OverloadedEnergy = true;
        }
    }
}
