using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using HeroRegression.Buffs.Summon;

namespace HeroRegression.Projectiles.Summon.FearOfColdCrystalWorms
{
    public class IceMan : ModProjectile
    {
        public int Counts;

        private NPC Target;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "");
            Main.projFrames[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 20;
            Projectile.noDropItem = true;
            Projectile.width = 22;
            Projectile.height = 75;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
        }

        public override bool MinionContactDamage()
        {
            return false;
        }

        public override void AI()
        {
            #region 帧图切换
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 10)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame >= 2)
                {
                    Projectile.frame = 0;
                }
            }
            #endregion
            #region 剩下的ai部分
            Projectile.spriteDirection = -Projectile.direction;
            Player player = Main.player[Projectile.owner];
            Projectile.active = player.active;
            Projectile.timeLeft = 2;
            if (!player.HasBuff(ModContent.BuffType<IceManBuff>()))
            {
                Projectile.Kill();
            }
            #region ai

            if (Target == null)
            {
                Projectile.ai[0] = 0;
            }

            if (Projectile.ai[0] == 1)
            {
                foreach (NPC NPC in Main.npc)
                {
                    if (NPC.active && !NPC.friendly && ((NPC.Center - player.Center).Length() < 200) && NPC != Target && NPC.type != NPCID.TargetDummy && ((NPC.Center - player.Center).Length() < (Target.Center - player.Center).Length()))
                    {
                        Target = NPC;
                    }
                }
                Vector2 MoveVel = Target.Center + new Vector2(0, -200) - Projectile.Center;
                MoveVel.Normalize();
                MoveVel *= 3f;
                Projectile.velocity = (Projectile.velocity * 30f + MoveVel * 5) / 31f;

                if (Projectile.ai[1] <= 0)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, ((Target.Center - Projectile.Center + Target.velocity).SafeNormalize(Vector2.Zero)) * 7, ModContent.ProjectileType<IceBullet>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
                    Projectile.ai[1] = 60;
                }
                else
                {
                    Projectile.ai[1]--;
                }
                if (!Target.active)
                {
                    Projectile.ai[0] = 0;
                    Target = null;
                }
            }
            else
            {
                if ((Projectile.Center - player.Center).Length() > 100)
                {
                    Vector2 MoveVel = player.Center + new Vector2(0, -70) - Projectile.Center;
                    MoveVel.Normalize();
                    MoveVel *= 4f;
                    Projectile.velocity = (Projectile.velocity * 30f + MoveVel * 5) / 31f;
                }
                if ((Projectile.Center - player.Center).Length() > 3000)
                {
                    Projectile.Center = player.position + (Main.rand.NextFloat() * MathHelper.TwoPi).ToRotationVector2() * 90;
                }
                Projectile.ai[1] = 0;
                foreach (NPC NPC in Main.npc)
                {
                    if (NPC.active && !NPC.friendly && ((NPC.Center - Projectile.Center).Length() < 1000) && NPC != Target && NPC.type != NPCID.TargetDummy)
                    {
                        Target = NPC;
                        Projectile.ai[0] = 1;
                    }
                }
            }
            #endregion
            #endregion
        }
    }

    public class IceBullet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 300;
        }

        public override void AI()
        {
            if (Main.rand.Next(10) < 5)
            {
                int dust = Dust.NewDust(Projectile.Center, 1, 1, DustID.Water);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}