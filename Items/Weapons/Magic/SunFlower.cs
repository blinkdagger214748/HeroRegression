using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HeroRegression.Items.Weapons.Magic
{
    class SunFlower : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("“Sun”Flower");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "向“日”葵");
            Tooltip.SetDefault("SunFlower?No！");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "太阳花？不！");
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.height = 96;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 11;
            Item.crit = 8;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.knockBack = 4;
            Item.autoReuse = false;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Friendly.Magic.SunFlower>();
            Item.shootSpeed = 7;
            Item.mana = 5;
            Item.rare = ItemRarityID.Green;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(20, 0);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.WandofSparking, 1)
            .AddIngredient(ItemID.FallenStar, 5)
            .AddTile(TileID.Anvils)
            .AddIngredient(ItemID.Daybloom, 3)
            .AddIngredient(ItemID.DemoniteBar, 12)
            .Register();

            CreateRecipe()
            .AddIngredient(ItemID.WandofSparking, 1)
            .AddIngredient(ItemID.FallenStar, 5)
            .AddTile(TileID.Anvils)
            .AddIngredient(ItemID.Daybloom, 3)
            .AddIngredient(ItemID.CrimtaneBar, 12)
            .Register();
        }
    }
}

