using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroRegression.Items.Placeable.Trophy
{
    public class SeedBossTrophy:BaseTrophyItem
    {
        public override void SetStaticDefaults()
        {
            TrophyStatics(ChnTrans("Seed of Origin Trophy", "起源之种纪念章"));
        }
        public override void SetDefaults()
        {
            TrophyDefaults(0);
        }
    }
}
