using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroRegression.Common.BaseClasses.BaseLoot
{
    public abstract class BaseRelicItem : ModItem
    {
		public void RelicStatics(string name)
		{
			DisplayName.SetDefault(name+ChnTrans(" Relic","圣物"));
			SacrificeTotal = 1;
		}
		public void RelicDefaults(int tileTpe)
		{
			Item.DefaultToPlaceableTile(tileTpe, 0);
			Item.width = 30;
			Item.height = 40;
			Item.maxStack = 99;
			Item.rare = -13;
			Item.master = true;
			Item.value = Item.buyPrice(0, 5, 0, 0);
		}
	}
}
