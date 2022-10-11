using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;

namespace HeroRegression.NPCs.Soul
{
    class Icesoul : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Icesoul");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "冰幽灵");
            Main.npcFrameCount[NPC.type] = 5;
            NPCID.Sets.TrailCacheLength[NPC.type] = 5;//这个是拖尾的长度，也就是绘制a次拖尾
            NPCID.Sets.TrailingMode[NPC.type] = 2;
        }

        public override void SetDefaults()
        {
            NPC.width = 18;
            NPC.height = 40;
            NPC.damage = 50;
            NPC.defense = 5;
            NPC.lifeMax = 300;
            NPC.HitSound = SoundID.NPCHit23;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.friendly = false;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.frame.Y = 5;
        }
        public override void AI()
        {
            NPC.frameCounter++;
            if(NPC.frameCounter>=5)
            {
                NPC.frame.Y += 78; NPC.frameCounter = 0;
            }  
            if(NPC.frame.Y>=5*78)
            {
                NPC.frame.Y = 0;
            }

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
                Vector2 vel = Vector2.Normalize(player.Center - NPC.Center) * 5;
                NPC.velocity = vel;
            }

        }
     
       
    }
}
