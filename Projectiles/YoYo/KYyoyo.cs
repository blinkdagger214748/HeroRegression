using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace HeroRegression.Projectiles.YoYo
{
	public class KYyoyo : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("浓眼悠悠球");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.CorruptYoyo);
			Projectile.damage = 15;
			Projectile.extraUpdates = 1;
			Projectile.aiStyle = 555;
		}
	
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
		
			Player player = Main.player[Projectile.owner];
			if (Main.rand.NextBool(5) )
			{

				player.statLife += 1;
				player.HealEffect(1);
			

			}
		}
	}
}