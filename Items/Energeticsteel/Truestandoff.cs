using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System;
using HeroRegression.Items.Beginner;
using HeroRegression.Items.Boss.FearOfColdCrystalWorms;
using Terraria.DataStructures;

namespace HeroRegression.Items.Energeticsteel
{
    class Truestandoff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Truestandoff");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "真·对峙");
            Tooltip.SetDefault("Fire and frostbite");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, " 泰   拉   枪 " +
                "\n会使敌怪受到灵液与咒火");
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 48;
            Item.damage = 68;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 4f;
            Item.rare = 8;
            Item.maxStack = 1;
            Item.crit = 21;
            Item.useTime = 13;
            Item.useAnimation = 13;
            Item.scale = 0.7f;
            Item.UseSound = SoundID.Item41;
            Item.value = Item.buyPrice(0, 40, 0, 0);
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int proj1 = Projectile.NewProjectile(source,position, velocity, type, damage, 5f, player.whoAmI);
            Main.projectile[proj1].GetGlobalProjectile<Truestandoffproj>().Truestandoff = true;
            int proj2 = Projectile.NewProjectile(source, position + new Vector2(20 * (Main.rand.NextBool(2)? -1 : 1), 0), velocity, type, damage, 5f, player.whoAmI);
            Main.projectile[proj2].GetGlobalProjectile<Truestandoffproj>().Truestandoff = true;
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()        
            .AddIngredient(ModContent.ItemType<Standoff>(), 1)
            .AddIngredient(ModContent.ItemType<WorldCrystal>(), 2)
            // 在铁砧旁边才能合成
            .AddTile(TileID.MythrilAnvil)
             
            .Register();

        }
    }
    class Truestandoffproj : GlobalProjectile
    {
        public bool Truestandoff;

        public override bool InstancePerEntity => true;

        public override void OnHitNPC(Projectile Projectile, NPC target, int damage, float knockback, bool crit)
        {
            if (Projectile.GetGlobalProjectile<Truestandoffproj>().Truestandoff)
            {
                target.AddBuff(BuffID.Ichor, 3 * 60);
                target.AddBuff(BuffID.CursedInferno, 3 * 60);
            }
        }

        public override void OnHitPlayer(Projectile Projectile, Player target, int damage, bool crit)
        {
            if (Projectile.GetGlobalProjectile<Truestandoffproj>().Truestandoff)
            {
                target.AddBuff(BuffID.Ichor, 3 * 60);
                target.AddBuff(BuffID.CursedInferno, 3 * 60);
            }
        }

        public override void OnHitPvp(Projectile Projectile, Player target, int damage, bool crit)
        {
            if (Projectile.GetGlobalProjectile<Truestandoffproj>().Truestandoff)
            {
                target.AddBuff(BuffID.Ichor, 3 * 60);
                target.AddBuff(BuffID.CursedInferno, 3 * 60);
            }
        }
    }

}
