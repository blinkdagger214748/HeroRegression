using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace HeroRegression.Projectiles.YoYo
{
	public class OSyoyo : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("绿晶悠悠球");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 1;

		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(544);
			Projectile.damage = 16;
			Projectile.extraUpdates = 5;
			Projectile.aiStyle = 555;

		}
		public override void PostAI()
		{
			Projectile.rotation -= 6f;
		}


		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{

			if (Main.rand.NextBool(5))
			{

				Vector2 vel1 = Vector2.Normalize(target.Hitbox.Center() - Projectile.Center).RotatedBy(MathHelper.ToRadians(Main.rand.NextFloat(-30, 30))) * 2f;
				vel1 *= 4f;
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.position.X + 150, target.position.Y + 150, vel1.X, vel1.Y, ModContent.ProjectileType<OriginNailFriend>(), Projectile.damage / 2, 0, Main.myPlayer);
				Vector2 vel2 = Vector2.Normalize(target.Hitbox.Center() - Projectile.Center).RotatedBy(MathHelper.ToRadians(Main.rand.NextFloat(-30, 30))) * 2f;
				vel2 *= 4f;
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.position.X - 150, target.position.Y - 150, vel2.X, vel2.Y, ModContent.ProjectileType<OriginNailFriend>(), Projectile.damage / 2, 0, Main.myPlayer);
			}


		}

	}
}
