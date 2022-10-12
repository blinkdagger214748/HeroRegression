using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace HeroRegression.Common.BaseClasses.BaseWeapon
{
    public abstract class BaseYoyoItem:ModItem
	{
        public void YoyoItemStatics(string name, string tooltip)
		{
			DisplayName.SetDefault(name);
			Tooltip.SetDefault(tooltip);
			ItemID.Sets.Yoyo[Item.type] = true;
			ItemID.Sets.GamepadExtraRange[Item.type] = 15;
			ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;
			SacrificeTotal = 1;
		}
		public void YoyoItemDefaults(int width, int height, int damage, float knockBack, int useTime, int projType, int rare, int value, float shootSpeed=10f)
		{
			Item.width = width;
			Item.height = height;
			Item.DamageType = DamageClass.Melee;
			Item.damage = damage;
			Item.knockBack = knockBack;
			Item.useTime = useTime;
			Item.useAnimation = useTime;
			Item.autoReuse = true;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.UseSound = new SoundStyle?(SoundID.Item1);
			Item.channel = true;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.shoot = projType;
			Item.shootSpeed = shootSpeed;
			Item.rare = rare;
			Item.value = value;
		}
    }
}
