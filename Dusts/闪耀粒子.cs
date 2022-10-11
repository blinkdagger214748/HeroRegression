using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace HeroRegression.Dusts
{
	public class 闪耀粒子 : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;
			dust.frame = new Rectangle(0, 0, 10, 10);
		}

		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.rotation += dust.velocity.Y - 20;
			dust.scale -= 0.01f;
			Lighting.AddLight(dust.position, 2.55f * 0.3f, 2.55f * 0.3f, 1 * 0.3f);
			if (dust.scale < 0.5f)
			{
				dust.active = false;
			}
			return false;
		}
	}
}