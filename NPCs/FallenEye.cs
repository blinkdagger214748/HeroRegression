using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Linq;
using Terraria.Localization;
using HeroRegression.Items.Beginner;
using Terraria.GameContent.ItemDropRules;

namespace HeroRegression.NPCs
{
	public class FallenEye : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Eye of the Fallen");
			DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "堕落之眼");
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)	//NPC生成，我这里写的是!Main.dayTime（！代表相反，大概吧）
		{
			if (!Main.dayTime)
				return 0.1f;
			return 0.0f;
		}

		public override void SetDefaults()
		{
			NPC.width = 32;
			NPC.height = 26;
			NPC.damage = 35;
			NPC.defense = 3;
			NPC.lifeMax = 90;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.aiStyle = 2;
			AIType = 2;
			AnimationType = 2;
			NPC.value = 100;
			Main.npcFrameCount[NPC.type] = 2;
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BrokenInitialHeart>(), 1, 1, 5));
		}
	}
}