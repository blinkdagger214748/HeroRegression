using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using HeroRegression.Projectiles;

namespace HeroRegression.Items.Weapons.Melee
{
    class AbsoluteZero : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Absolute Zero");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "绝对零度");
            Tooltip.SetDefault("-273.15°C");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.damage = 26;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4f;
            Item.rare = ItemRarityID.Green;
            Item.maxStack = 6;
            Item.crit = 4;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<AbsoluteZeroProj>();
            Item.shootSpeed = 16f;
        }

        public override bool CanUseItem(Player player)
        {
            int MaxUse = Item.stack;
            return player.ownedProjectileCounts[ModContent.ProjectileType<AbsoluteZeroProj>()] < MaxUse;
        }

    }
}