using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroRegression.Tiles.Relics
{
    public class SeedBossRelic : BaseRelicTile
    {
        public override string RelicTextureName => "HeroRegression/Tiles/Relics/SeedBossRelic";

        public override int AssociatedItem => ModContent.ItemType<SeedBossRelicItem>();
    }
}
