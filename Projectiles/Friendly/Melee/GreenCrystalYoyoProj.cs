using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace HeroRegression.Projectiles.Friendly.Melee
{
    public class GreenCrystalYoyoProj : BaseYoyo
    {
        public override void SetStaticDefaults()
        {
            YoyoStatics(ChnTrans("Green Crystal Yoyo", "绿晶悠悠球"), 15, 300, 15, 8, 1);
        }
        public override void SetDefaults()
        {
            YoyoDefaults(16, 16, 2, 1, true, false, 20);
        }
        public override void AI()
        {
            //Projectile.rotation += MathHelper.ToRadians(3);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteBatchTrail(Color.DarkGreen);
            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextFloat() <= .15f)
            {
                SoundEngine.PlaySound(SoundID.Item25, Projectile.Center);
                for (int i = 0; i < 3; i++)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Vector2 vel = Main.rand.NextVector2CircularEdge(8f, 8f);
                        Projectile proj =  Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center, vel, ModContent.ProjectileType<OriginNailFriend2>(), Projectile.damage / 2, Projectile.knockBack * .66f, Projectile.owner);
                        proj.DamageType = DamageClass.MeleeNoSpeed;
                    }
                }
            }
        }
    }
}
