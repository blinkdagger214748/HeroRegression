using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using HeroRegression.Items.Boss.SeedsOfOrigin;
using Terraria.DataStructures;

namespace HeroRegression.Items.Boss.FearOfColdCrystalWorms
{
    class ColdSnake : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cold Snake");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "寒蛇");
            Tooltip.SetDefault("Scraping.");
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "刮刮乐");
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 48;
            Item.damage = 18;
            Item.DamageType = DamageClass.Ranged;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 4f;
            Item.rare = 3;
            Item.maxStack = 1;
            Item.crit = 4;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.scale = 0.85f;
            Item.UseSound = SoundID.Item41;
            Item.value = Item.buyPrice(0, 10, 0, 0);
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Bullet;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int proj1 = Projectile.NewProjectile(source,position, velocity, type, damage, 5f, player.whoAmI);
            Main.projectile[proj1].GetGlobalProjectile<ColdSnakeGlobalProj>().ColdSnake = true;
            int proj2 = Projectile.NewProjectile(source,position + new Vector2(20 * (Main.rand.NextBool(2)? -1 : 1), 0), velocity, type, damage, 5f, player.whoAmI);
            Main.projectile[proj2].GetGlobalProjectile<ColdSnakeGlobalProj>().ColdSnake = true;
            return false;
        }
    }
    class ColdSnakeGlobalProj : GlobalProjectile
    {
        public bool ColdSnake;
        public override bool InstancePerEntity => true;

        public override void OnHitNPC(Projectile Projectile, NPC target, int damage, float knockback, bool crit)
        {
            if (Projectile.GetGlobalProjectile<ColdSnakeGlobalProj>().ColdSnake)
            {
                target.AddBuff(BuffID.Frostburn, 5 * 60);
            }
        }

        public override void OnHitPlayer(Projectile Projectile, Player target, int damage, bool crit)
        {
            if (Projectile.GetGlobalProjectile<ColdSnakeGlobalProj>().ColdSnake)
            {
                target.AddBuff(BuffID.Frostburn, 5 * 60);
            }
        }

        public override void OnHitPvp(Projectile Projectile, Player target, int damage, bool crit)
        {
            if (Projectile.GetGlobalProjectile<ColdSnakeGlobalProj>().ColdSnake)
            {
                target.AddBuff(BuffID.Frostburn, 5 * 60);
            }
        }
    }
}
