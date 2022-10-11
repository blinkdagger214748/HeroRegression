using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System;
using Terraria.DataStructures;
using HeroRegression.Items.Material;

namespace HeroRegression.Items.Weapons.Ranged
{
    class Standoff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Standoff");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "对峙");
            Tooltip.SetDefault("Fire and frostbite");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "“生命可能会凋零，但真神从未陨落。”" +
                "\n会使敌怪冰火两重天");
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 48;
            Item.damage = 24;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 4f;
            Item.rare = 5;
            Item.maxStack = 1;
            Item.crit = 4;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.scale = 0.7f;
            Item.UseSound = SoundID.Item41;
            Item.value = Item.buyPrice(0, 27, 0, 0);
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int proj1 = Projectile.NewProjectile(source, position, velocity, type, damage, 5f, player.whoAmI);
            Main.projectile[proj1].GetGlobalProjectile<Standoffproj>().Standoff = true;
            int proj2 = Projectile.NewProjectile(source, position + new Vector2(20 * (Main.rand.Next(2) == 0 ? -1 : 1), 0), velocity, type, damage, 5f, player.whoAmI);
            Main.projectile[proj2].GetGlobalProjectile<Standoffproj>().Standoff = true;
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(219, 1)
            .AddIngredient(ModContent.ItemType<ColdSnake>(), 1)
            .AddIngredient(ModContent.ItemType<BrokenInitialHeart>(), 5)
            // 在铁砧旁边才能合成
            .AddTile(TileID.MythrilAnvil)

            .Register();

        }
    }
    class Standoffproj : GlobalProjectile
    {
        public bool Standoff;

        public override bool InstancePerEntity => true;

        public override void OnHitNPC(Projectile Projectile, NPC target, int damage, float knockback, bool crit)
        {
            if (Projectile.GetGlobalProjectile<Standoffproj>().Standoff)
            {
                target.AddBuff(BuffID.OnFire, 2 * 60);
                target.AddBuff(44, 2 * 60);
            }
        }

        public override void OnHitPlayer(Projectile Projectile, Player target, int damage, bool crit)
        {
            if (Projectile.GetGlobalProjectile<Standoffproj>().Standoff)
            {
                target.AddBuff(BuffID.OnFire, 2 * 60);
                target.AddBuff(44, 2 * 60);
            }
        }

        public override void OnHitPvp(Projectile Projectile, Player target, int damage, bool crit)
        {
            if (Projectile.GetGlobalProjectile<Standoffproj>().Standoff)
            {
                target.AddBuff(BuffID.OnFire, 2 * 60);
                target.AddBuff(44, 2 * 60);
            }
        }
    }

}