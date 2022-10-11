using HeroRegression.Projectiles.Summon.FearOfColdCrystalWorms;
using Terraria;
using Terraria.ModLoader;

namespace HeroRegression.Buffs.Summon
{
    public class IceManBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
            Description.SetDefault("");
            Main.buffNoSave[Type] = true;
            // 不显示buff时间
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            MyPlYaer yaer = player.GetModPlayer<MyPlYaer>();
            if (player.ownedProjectileCounts[ModContent.ProjectileType<IceMan>()] > 0)
            {
                yaer.SummonIceMan = true;
            }
            if (!yaer.SummonIceMan)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
                return;
            }
            // 无限buff时间
            player.buffTime[buffIndex] = 9999;
        }
    }

    public class MyPlYaer : ModPlayer
    {
        public bool SummonIceMan;
        public override void ResetEffects()
        {
            SummonIceMan = false;
        }
    }
}