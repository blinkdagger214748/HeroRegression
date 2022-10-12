using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;

namespace HeroRegression.Common.BaseClasses.BaseProj
{
    public abstract class BaseYoyo : FriendlyProj
    {
        public void YoyoStatics(string name, float yoyoTime = 10f, float yoyoRange = 250f, float yoyoSpeed = 10f, int trailLength = 0, int trailMode = 0)
        {
            StaticDefaults(name, trailLength, trailMode);
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Type] = yoyoTime;
            ProjectileID.Sets.YoyosMaximumRange[Type] = yoyoRange;
            ProjectileID.Sets.YoyosTopSpeed[Type] = yoyoSpeed;
        }
        public void YoyoDefaults(int width, int height, int maxUpdate, float scale = 1f, bool localImmune = false, bool IDImmune = false, int localHitCD = 20, int IDHitCD = 20)
        {
            Defaults(width, height, -1, -1, true, 0, 0, 1, 1, false, false, true);
            MaxTime = ProjectileID.Sets.TrailCacheLength[Type] + 10;
            Projectile.aiStyle = ProjAIStyleID.Yoyo;
            Projectile.MaxUpdates = maxUpdate;
            Projectile.scale = scale;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            if (localImmune)
            {
                Projectile.usesLocalNPCImmunity = true;
                Projectile.localNPCHitCooldown = localHitCD;
            }
            if (IDImmune)
            {
                Projectile.usesIDStaticNPCImmunity = true;
                Projectile.idStaticNPCHitCooldown = IDHitCD;
            }
        }
        public override bool PreAI()
        {
            if ((Projectile.position - Main.player[Projectile.owner].position).Length() > 3200f)
            {
                Projectile.Kill();
            }
            return true;
        }
    }
}
