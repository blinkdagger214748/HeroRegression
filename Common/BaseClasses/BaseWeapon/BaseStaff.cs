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
    public abstract class BaseStaff:ModItem
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Eng">The English name of this item.</param>
        /// <param name="Chn">The Chinese name of this item.</param>
        public void ItemName(string Eng, string Chn)
        {
            DisplayName.SetDefault(Eng);
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, Chn);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Eng">The English name of this item.</param>
        /// <param name="Chn">The Chinese name of this item.</param>
        public void ItemTooltip(string Eng, string Chn)
        {
            Tooltip.SetDefault(Eng);
            Tooltip.AddTranslation((int)GameCulture.CultureName.Chinese, Chn);
            Item.staff[Item.type] = true;
        }
        public void Defaults(int width, int height, int damage, int useTime, int value, float knockback, int rare, int crit = 0, bool autoReuse = true, bool useTurn =true, int shootSpeed = 10, int mana = 6, int reuseDelay = 0)
        {
            Item.width = width;
            Item.height = height;
            Item.DamageType = DamageClass.Magic;
            Item.damage = damage;
            Item.useTime = useTime;
            Item.useAnimation = useTime;
            Item.value = value;
            Item.knockBack = knockback;
            Item.rare = rare;
            Item.crit = crit;
            Item.autoReuse = autoReuse;
            Item.useTurn = useTurn;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shootSpeed = shootSpeed;
            Item.mana = mana;
            Item.reuseDelay = reuseDelay;
            Item.noMelee = true;
        }
    }
}
