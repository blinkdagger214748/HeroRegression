using Terraria;
using Terraria.ModLoader;

namespace HeroRegression.NPCs
{
    class NPCOwner : GlobalNPC
    {
        public int ownerNPC;
        public bool noAI;

        public override bool InstancePerEntity => true;



        public override void SetDefaults(NPC NPC)
        {
            ownerNPC = 255;
            noAI = false;
        }
    }

    class ProjectileOwner : GlobalProjectile
    {
        public int ownerNPC;



        public override bool InstancePerEntity => true;

        public override void SetDefaults(Projectile Projectile)
        {
            ownerNPC = 255;
        }
    }
}