namespace HeroRegression.Graphics
{
    [Autoload(Side = ModSide.Client)]
    public sealed class ParticleManager : ILoadable
    {
        public static int MaxParticles => 4000;
        public static List<Particle> Particles { get; private set; }
        void ILoadable.Load(Mod mod)
        {
            Particles = new List<Particle>(MaxParticles);
            On.Terraria.Dust.UpdateDust += UpdateParticles;
            On.Terraria.Main.DrawDust += DrawParticles;
        }
        void ILoadable.Unload()
        {
            On.Terraria.Dust.UpdateDust -= UpdateParticles;
            On.Terraria.Main.DrawDust -= DrawParticles;
            Particles?.Clear();
            Particles = null;
        }
        public static T Spawn<T>(Vector2 position, Vector2 velocity = default, Color? color = null, Vector2? scale = null, float rotation = 0f, float alpha = 1f) where T : Particle, new()
        {
            T particle = new()
            {
                Position = position,
                Velocity = velocity,
                Color = color ?? Color.White,
                Scale = scale ?? Vector2.One,
                Rotation = rotation,
                Alpha = alpha
            };
            particle.OnSpawn();
            if (Particles.Count >= MaxParticles) return particle;
            Particles.Add(particle);
            return particle;
        }

        public static bool Kill<T>(T particle) where T : Particle
        {
            bool success = Particles.Remove(particle);
            if (success) particle.OnKill();
            return success;
        }

        private static void UpdateParticles(On.Terraria.Dust.orig_UpdateDust orig)
        {
            orig();
            for (int i = 0; i < Particles.Count; i++) Particles[i]?.Update();
        }

        private static void DrawParticles(On.Terraria.Main.orig_DrawDust orig, Main self)
        {
            orig(self);
            Main.spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend, SamplerState.PointClamp, default, Main.Rasterizer, default, Main.GameViewMatrix.TransformationMatrix);
            for (int i = 0; i < Particles.Count; i++)
            {
                Particle particle = Particles[i];

                if (particle == null || particle.IsAdditive || !particle.Position.IsWorldOnScreen(particle.Width, particle.Height)) continue;
                particle.Draw();
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Texture, BlendState.Additive, SamplerState.PointClamp, default, Main.Rasterizer, default, Main.GameViewMatrix.TransformationMatrix);
            for (int i = 0; i < Particles.Count; i++)
            {
                Particle particle = Particles[i];
                if (particle == null || !particle.IsAdditive || !particle.Position.IsWorldOnScreen(particle.Width, particle.Height)) continue;
                particle.Draw();
            }
            Main.spriteBatch.End();
        }
    }
}
