using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System;
using HeroRegression.Items;

namespace HeroRegression.Projectiles
{
	public class BoomSun : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("ÏòÈÕ¿û±¬Õ¨");
			Projectile.timeLeft = 150;
			Projectile.light = 0.8f;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
			Main.projFrames[Projectile.type] = 1;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.LaserMachinegunLaser);
			Projectile.DamageType = DamageClass.Magic;
			Projectile.width = 98;
			Projectile.height = 98;
			Projectile.penetrate = 40;
			Projectile.timeLeft = 30;
			Projectile.tileCollide = false;
			Projectile.aiStyle = ProjectileID.LaserMachinegunLaser;
		}

		public override void AI()
		{
			for (int i = 0; i < 3; i++)
			{
				Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.SpectreStaff, 0, 0, 100, Color.GreenYellow, 1.2f);
				d.position = Projectile.Center - Projectile.velocity * i / 3f;
				d.velocity *= 0.2f;
				d.noGravity = true;
			}
	}
	}
}