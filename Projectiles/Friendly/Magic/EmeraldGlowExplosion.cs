namespace HeroRegression.Projectiles.Friendly.Magic
{
    public class EmeraldGlowExplosion : FriendlyProj
    {
        public override string Texture => BlankTexture;
        public override void SetStaticDefaults()
        {
            StaticDefaults(ChnTrans("Emerald Blast", "碧辉冲击"));
        }
        public override void SetDefaults()
        {
            Defaults(1, 1, 3, -1);
            Projectile.DamageType = DamageClass.Magic;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, Color.Green.ToVector3() / 255f);
            float radius = Projectile.scale * 50;
            for (int i = 1; i <= 50*Projectile.scale; i++)
            {
                Vector2 vel = Main.rand.NextVector2Circular(radius, radius) / 10f;
                Dust dust = Dust.NewDustDirect(Projectile.position, 1, 1, DustID.GemEmerald);
                dust.scale = Main.rand.NextFloat(.9f, (float)Math.Sqrt(Projectile.scale));
                dust.noGravity = true;
                dust.velocity = vel;
            }
        }
        public override bool PreKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
            return true;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return Vector2.Distance(projHitbox.Center.ToVector2(), targetHitbox.Center.ToVector2()) <= Projectile.scale * 50;
        }
    }
}
