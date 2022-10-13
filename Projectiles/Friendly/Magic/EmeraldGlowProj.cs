namespace HeroRegression.Projectiles.Friendly.Magic
{
    public class EmeraldGlowProj : FriendlyProj
    {
        public override void SetStaticDefaults()
        {
            StaticDefaults(ChnTrans("Emerald Gem", "碧辉晶石"));
        }
        public override void SetDefaults()
        {
            Defaults(12, 12, 300, 1);
            RotOffset = 90;
            Projectile.DamageType = DamageClass.Magic;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, Color.Green.ToVector3() / 255f * Projectile.scale / 5.5f);
            if (Main.rand.NextFloat() <= .66f)
            {
                Dust d = Dust.NewDustDirect(Projectile.Center - new Vector2((int)(6 * Projectile.scale)), (int)(12 * Projectile.scale), (int)(12 * Projectile.scale), DustID.GemEmerald);
                d.noGravity = true;
                d.scale *= Main.rand.NextFloat(.9f, (float)Math.Sqrt(Projectile.scale));
            }
            else
            {
                Dust d = Dust.NewDustDirect(Projectile.Center - new Vector2((int)(6 * Projectile.scale)), (int)(12 * Projectile.scale), (int)(12 * Projectile.scale), DustID.GemEmerald);
                d.scale = .33f;
                d.scale *= Main.rand.NextFloat(.9f, (float)Math.Sqrt(Projectile.scale));
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Draw(Color.Lerp(lightColor, Color.Green, .33f));
            return false;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile explode = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<EmeraldGlowExplosion>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            explode.scale = Projectile.scale;
            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile explode = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<EmeraldGlowExplosion>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            explode.scale = Projectile.scale;
        }
    }
}
