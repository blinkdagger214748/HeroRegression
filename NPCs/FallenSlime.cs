using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using HeroRegression.Items.Beginner;
using Terraria.GameContent.ItemDropRules;

namespace HeroRegression.NPCs
{
	public class FallenSlime : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fallen Slime");
			DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "堕落史莱姆");
			Main.npcFrameCount[NPC.type] = 2;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (Main.dayTime)
				return 0.1f;
			return 0.0f;
		}

		public override void SetDefaults()
		{
			NPC.width = 32;
			NPC.height = 26;
			NPC.damage = 35;
			NPC.defense = 3;
			NPC.lifeMax = 80;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.aiStyle = 1;
			NPC.aiStyle = NPCID.BlueSlime;
			AnimationType = NPCID.BlueSlime;
			NPC.value = 100;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BrokenInitialHeart>(), 1,1,5));
		}
	}
}
