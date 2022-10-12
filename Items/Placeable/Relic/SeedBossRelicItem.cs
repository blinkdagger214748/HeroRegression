namespace HeroRegression.Items.Placeable.Relic
{
    public class SeedBossRelicItem:BaseRelicItem
    {
        public override void SetStaticDefaults()
        {
            RelicStatics(ChnTrans("Seed of Origin", "起源之种"));
        }
        public override void SetDefaults()
        {
            RelicDefaults(ModContent.TileType<SeedBossRelic>());
        }
    }
}
