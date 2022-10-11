using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System;
using HeroRegression.Items;
using Terraria.Audio;

namespace HeroRegression.Projectiles
{
	public class Realflameofdaggerdm : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("真·焰彩投刀");
			Projectile.timeLeft = 200;
			Projectile.light = 1f;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 11;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
			Main.projFrames[Projectile.type] = 1;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.LaserMachinegunLaser);
			Projectile.DamageType = DamageClass.Melee;
			Projectile.width = 24;
			Projectile.height = 24;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 160;
			Projectile.tileCollide = true;
			Projectile.rotation = Projectile.velocity.ToRotation();
            const double V = 0.7;
            Projectile.scale = (float)V;
			Projectile.aiStyle = ProjectileID.LaserMachinegunLaser;

		}

		int time = 0;
		public override void AI()
		{

			if (Projectile.timeLeft < 45)
			{
				Projectile.velocity.X *= .200f;
				Projectile.velocity.Y += .9f;
			}
			for (int i = 0; i <= 20; i++)
			{
				Vector2 dustPos = Main.rand.NextFloat(0, 30f).ToRotationVector2() * Main.rand.NextFloat(.1f, 20f);
				Dust dust = Dust.NewDustDirect(Projectile.Center + dustPos, 1, 1, DustID.WhiteTorch);
				dust.noGravity = true;
				dust.fadeIn = .12f;
				dust.velocity = Vector2.Normalize(dustPos) * (float)Math.Log(4f - dustPos.Length(), 2);
				dust.color = Main.DiscoColor;
				dust.scale = 1f;
			}
			NPC target = null;
			float distanceMax = 190;
			foreach (NPC NPC in Main.npc)
			{
				if (NPC.active && !NPC.friendly && NPC.type != NPCID.TargetDummy)
				{
					Projectile.tileCollide = false;
					float currentDistance = Vector2.Distance(NPC.Center, Projectile.Center);
					if (currentDistance < distanceMax)
					{
						distanceMax = currentDistance;
						target = NPC;
					}
				}
			}
			if (target != null)
			{

				Vector2 targetVec = target.Center - Projectile.Center;
				targetVec.Normalize();
				targetVec *= ((int)Projectile.ai[0] == 1) ? 30f : 20f;
				Projectile.velocity = (Projectile.velocity * 30f + targetVec) / 31f;
			}
			else
			{


			}
		}
		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 3; k++)
			{
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.YellowTorch, Projectile.oldVelocity.X * 0.2f, Projectile.oldVelocity.Y * 0.2f);
			}
			SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);
		}
        public override bool PreDraw(ref Color lightColor)
        {
			Texture2D tex = ModContent.Request<Texture2D>("HeroRegression/Projectiles/Realflameofdaggerdm").Value;
			Vector2 ori = new Vector2(30, 22);
			Vector2 pos1 = Projectile.Center - Main.screenPosition;
			for (int i = 0; i <= 6; i += 1)
			{
				Vector2 pos2 = Projectile.oldPos[i] + new Vector2(30, 22) - Main.screenPosition;
				lightColor = Main.DiscoColor * ((float)(Projectile.oldPos.Length) / (Projectile.oldPos.Length));
				Main.spriteBatch.End();
				Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
				Main.spriteBatch.Draw(tex, pos2, null, lightColor, Projectile.oldRot[i], ori, 1f, SpriteEffects.None, 0f);
				Main.spriteBatch.End();
				Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
			}
			return true;
		}


	}
}