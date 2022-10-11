using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace HeroRegression.Items.Weapons.Ranged
{
    class PolarBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Polar Bow");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "极地之弓");
            Tooltip.SetDefault("\"Winter has arrived\"" +
                "\nFires a barrage of ice and snow." +
                "\nWhen it hits an enemy, it has an 80% chance of giving them a frost effect.");
         
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, "“凛冬已至”" +
                "\n发射出冰雪弹幕。" +
                "\n命中敌人时，有80%的概率给予敌人霜冻效果。");
        }

        public override void SetDefaults()
        {
            Item.damage = 8;
            Item.DamageType = DamageClass.Ranged;
            Item.crit = 0;
            Item.width = 30;
            Item.height = 48;
            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 1;
            Item.value = 4500;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Arrow;
            Item.shootSpeed = 12f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = 118;
          
            int proj3 = Projectile.NewProjectile(source, position, velocity, type, damage, 5f, player.whoAmI);
          
            Main.projectile[proj3].GetGlobalProjectile<Weapons.Ranged.ColdSnakeGlobalProj>().ColdSnake = true;

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
    }
}