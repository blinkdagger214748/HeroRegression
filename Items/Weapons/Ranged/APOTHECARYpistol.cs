using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using System;
using HeroRegression.Items.Boss.SeedsOfOrigin;
using Terraria.DataStructures;

namespace HeroRegression.Items.Weapons.Ranged
{
    class APOTHECARYpistol : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("APOTHECARY pistol");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "药水手枪");
            Tooltip.SetDefault("Would have irreversible consequences\n" +
                "[c/32CD99:Developer weapon]");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "会造成不可逆的后果,武器本身可以伤害敌怪\n" +
                "[c/32CD99:开发者.的武器]");
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 48;
            Item.damage = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.noMelee = false;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 4f;
            Item.rare = 8;
            Item.maxStack = 1;
            Item.crit = 4;
            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.scale = 0.7f;
            Item.UseSound = SoundID.Item41;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Bullet;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int proj1 = Projectile.NewProjectile(source, position, new Vector2(velocity.X, velocity.Y), type, damage, knockback, player.whoAmI);
            Main.projectile[proj1].GetGlobalProjectile<APOTHECARYpistolProj>().APOTHECARYpistol = true;
            return false;
        }

        class APOTHECARYpistolProj : GlobalProjectile
        {
            public bool APOTHECARYpistol;


            public override bool InstancePerEntity => true;

            public override void OnHitNPC(Projectile Projectile, NPC target, int damage, float knockback, bool crit)
            {
                if (Projectile.GetGlobalProjectile<APOTHECARYpistolProj>().APOTHECARYpistol)
                {
                    target.AddBuff(204, 6 * 60);//涂油
                    target.AddBuff(BuffID.CursedInferno, 6 * 60);
                    target.AddBuff(BuffID.Ichor, 8 * 60);
                    target.AddBuff(BuffID.Venom, 6 * 60);
                    target.AddBuff(153, 6 * 60);//暗影炎

                }
            }

            public override void OnHitPlayer(Projectile Projectile, Player target, int damage, bool crit)
            {
                if (Projectile.GetGlobalProjectile<APOTHECARYpistolProj>().APOTHECARYpistol)
                {
                    target.AddBuff(72, 5 * 60);//死亡掉钱
                    target.AddBuff(BuffID.OnFire, 3 * 60);
                    target.AddBuff(44, 3 * 60);//霜冻
                    target.AddBuff(BuffID.Confused, 2 * 60);
                    target.AddBuff(BuffID.CursedInferno, 2 * 60);
                    target.AddBuff(BuffID.Ichor, 2 * 60);
                    target.AddBuff(BuffID.Venom, 2 * 60);
                    target.AddBuff(153, 2 * 60);//暗影炎

                }
            }

            public override void OnHitPvp(Projectile Projectile, Player target, int damage, bool crit)
            {
                if (Projectile.GetGlobalProjectile<APOTHECARYpistolProj>().APOTHECARYpistol)
                {
                    target.AddBuff(72, 5 * 60);//死亡掉钱
                    target.AddBuff(BuffID.OnFire, 3 * 60);
                    target.AddBuff(44, 3 * 60);//霜冻
                    target.AddBuff(BuffID.Confused, 2 * 60);
                    target.AddBuff(BuffID.CursedInferno, 2 * 60);
                    target.AddBuff(BuffID.Ichor, 2 * 60);
                    target.AddBuff(BuffID.Venom, 2 * 60);
                    target.AddBuff(153, 2 * 60);//暗影炎
                }
            }
        }


        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Rapidfirepistol>(), 1)
            .AddIngredient(1006, 15)
            .AddIngredient(ItemID.FlaskofCursedFlames, 10)
            .AddIngredient(ItemID.FlaskofFire, 10)
            .AddIngredient(ItemID.FlaskofGold, 10)
            .AddIngredient(ItemID.FlaskofPoison, 10)
            .AddIngredient(ItemID.BottledHoney, 10)
            // 在铁砧旁边才能合成
            .AddTile(TileID.MythrilAnvil)
            .Register();
            CreateRecipe()
.AddIngredient(ModContent.ItemType<Rapidfirepistol>(), 1)
.AddIngredient(1006, 15)
.AddIngredient(ItemID.FlaskofIchor, 10)
.AddIngredient(ItemID.FlaskofFire, 10)
.AddIngredient(ItemID.FlaskofGold, 10)
.AddIngredient(ItemID.FlaskofPoison, 10)
.AddIngredient(ItemID.BottledHoney, 10)
// 在铁砧旁边才能合成
.AddTile(TileID.MythrilAnvil)
.Register();
        }
    }

}
