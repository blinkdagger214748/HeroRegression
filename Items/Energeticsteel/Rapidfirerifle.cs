using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System;
using HeroRegression.Items.Beginner;
using Terraria.DataStructures;

namespace HeroRegression.Items.Energeticsteel
{
    class Rapidfirerifle : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rapid-fire rifle");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "速射步枪");
            Tooltip.SetDefault("[c/42426F:Four shots at a time]");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "[c/42426F:一次射出四发子弹]" +
                "\n[c/42426F:会使敌怪迷惑]");
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 48;
            Item.damage = 46;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 4f;
            Item.rare = 11;
            Item.maxStack = 1;
            Item.crit = 4;
            Item.useTime = 9;
            Item.useAnimation = 9;
            Item.scale = 0.7f;
            Item.UseSound = SoundID.Item41;
            Item.value = Item.buyPrice(1, 22, 0, 0);
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int proj1 = Projectile.NewProjectile(source,position, velocity, type, damage, 5f, player.whoAmI);
            Main.projectile[proj1].GetGlobalProjectile<RapidfirerifleProj>().Rapidfirerifle = true;
            int proj2 = Projectile.NewProjectile(source, position + new Vector2(10 * (Main.rand.NextBool(2)? -1 : 1), 0), velocity, type, damage, 5f, player.whoAmI);
            Main.projectile[proj2].GetGlobalProjectile<RapidfirerifleProj>().Rapidfirerifle = true;
            int proj3 = Projectile.NewProjectile(source, position + new Vector2(10 * (Main.rand.NextBool(2)? -1 : 1), 0), velocity, type, damage, 5f, player.whoAmI);
            Main.projectile[proj3].GetGlobalProjectile<RapidfirerifleProj>().Rapidfirerifle = true;
            int proj4 = Projectile.NewProjectile(source, position + new Vector2(10 * (Main.rand.NextBool(2)? -1 : 1), 0), velocity, type, damage, 5f, player.whoAmI);
            Main.projectile[proj4].GetGlobalProjectile<RapidfirerifleProj>().Rapidfirerifle = true;
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Rapidfiresubmachinegun>(), 1)
            .AddIngredient(ItemID.LunarBar, 5)
            // 在铁砧旁边才能合成
            .AddTile(TileID.MythrilAnvil)
             
            .Register();

        }
    }
    class RapidfirerifleProj : GlobalProjectile
    {
        public bool Rapidfirerifle;


        public override bool InstancePerEntity => true;

        public override void OnHitNPC(Projectile Projectile, NPC target, int damage, float knockback, bool crit)
        {
            if (Projectile.GetGlobalProjectile<RapidfirerifleProj>().Rapidfirerifle)
            {
                target.AddBuff(BuffID.Confused, 3 * 60);
            }
        }

        public override void OnHitPlayer(Projectile Projectile, Player target, int damage, bool crit)
        {
            if (Projectile.GetGlobalProjectile<RapidfirerifleProj>().Rapidfirerifle)
            {
                target.AddBuff(BuffID.Confused, 3 * 60);
            }
        }

        public override void OnHitPvp(Projectile Projectile, Player target, int damage, bool crit)
        {
            if (Projectile.GetGlobalProjectile<RapidfirerifleProj>().Rapidfirerifle)
            {
                target.AddBuff(BuffID.Confused, 3 * 60);
            }
        }
    }

}