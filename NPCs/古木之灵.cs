using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HeroRegression.NPCs
{
    class 古木之灵 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("古木之灵");
        }

        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 40;
            NPC.damage = 50;
            NPC.defense = 5;
            NPC.lifeMax = 120;
            NPC.HitSound = SoundID.NPCHit23;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.friendly = false;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.dayTime && NPC.downedBoss2)
                return 0.02f;
            return 0.0f;
        }
        public override void AI()
        {
            if (NPC.target < 0 || NPC.target >= 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active && Vector2.Distance(Main.player[NPC.target].Center, NPC.Center) > 1000f)
            {
                NPC.TargetClosest(true);
            }
            Player player = Main.player[NPC.target];
            NPC.rotation = NPC.velocity.ToRotation() + 1.57f;
            if (Vector2.Distance(Main.player[NPC.target].Center, NPC.Center) > 1000f)
            {
                NPC.target = 255;
            }
            if (player.active && Vector2.Distance(Main.player[NPC.target].Center, NPC.Center) <= 1000f)
            {
                NPC.ai[0]++;
                if (NPC.ai[0] == 60)
                {
                    NPC.velocity = NPC.DirectionTo(player.Center) * 10f;
                }
                if (NPC.ai[0] > 75)
                {
                    NPC.velocity *= 0.95f;
                }
                if (NPC.ai[0] >= 90)
                {
                    NPC.ai[0] = 0;
                }
            }
            else
            {
                NPC.ai[0] = 0;
                // 左右游荡
                NPC.ai[1]++;
                if (NPC.ai[1] % 120 == 0)
                {
                    NPC.direction *= -1;
                }
                NPC.velocity.X = NPC.direction * 3f;
                NPC.velocity.Y = 0;
            }
        }
        public override void OnKill()  //NPC掉落
        {
            //方法：Item.NewItem(null,NPC.Center, ItemID, Main.rand.Next(1, 5));
            Item.NewItem(null, (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height,
                ModContent.ItemType<Items.Placeable.Block.古木>(), Main.rand.Next(1, 5));
            Item.NewItem(null, (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height,
               ModContent.ItemType<Items.Placeable.Block.Lingcuistone>(), Main.rand.Next(1, 5));
        }
    }
}