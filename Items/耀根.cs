using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using HeroRegression.Projectiles;

namespace HeroRegression.Items
{
    class 耀根 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("耀根");
            Tooltip.SetDefault("[c/F8D300FF:闪耀！汗水的光辉！]");
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.height = 96;
            Item.DamageType = DamageClass.Magic ;
            Item.damage = 8;
            Item.crit = 8;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.knockBack = 4;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.闪耀根>();
            Item.shootSpeed = 15f;
            Item.mana = 5;
            Item.rare = ItemRarityID.Green;
        }
        }
    }

