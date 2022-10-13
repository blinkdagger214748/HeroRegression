namespace HeroRegression.Projectiles.Friendly.Magic
{
    public class EmeraldGlowStaff : BaseHeldProj
    {
        public override string Texture => BlankTexture;
        public override void SetStaticDefaults()
        {
            StaticDefaults(ChnTrans("Emerabld Glow Staff", "碧辉法杖"));
        }
        public override void SetDefaults()
        {
            HeldProjDefaults(180, 0);
            Projectile.DamageType = DamageClass.Magic;
        }
        public override bool? CanDamage()
        {
            return false;
        }
        public override void OnSpawn(IEntitySource source)
        {
            Projectile.frameCounter = 0;
        }
        public Vector2 GemCenter;
        public float ParticleCD;
        public override void AI()
        {
            Projectile.rotation = 0;
            if (CheckOwner(out Player owner, false))
            {
                Projectile.frameCounter++;
                Projectile.direction = Projectile.velocity.X > 0 ? 1 : -1;
                owner.direction = Projectile.direction;
                Projectile.Center = owner.Center + new Vector2(-19 + Projectile.direction * 17, 8) + new Vector2(0, owner.gfxOffY);
                GemCenter = Projectile.Center + new Vector2(19 + 36 * Projectile.direction, -50);
                ParticleCD++;
                if (ParticleCD >= 1 + (180f - Projectile.frameCounter) / 36f)
                {
                    ParticleCD = 0;
                    if (Main.netMode != NetmodeID.Server)
                    {
                        Vector2 randVel = Main.rand.NextVector2CircularEdge(20f, 20f);
                        Particle.Spawn<Sparkle>(GemCenter + randVel, -Vector2.Normalize(randVel) / 5f, Color.Green, new Vector2(Main.rand.NextFloat(.33f, 1)));
                    }
                }
                Lighting.AddLight(Projectile.Center + new Vector2(34 * Projectile.direction, -50), 0, Projectile.frameCounter / 360f, 0);
            }
            else
            {
                Projectile.Kill();
            }
        }
        public override bool PreKill(int timeLeft)
        {
            float Power = (Projectile.frameCounter / 150f + .2f) * 5f;
            SoundEngine.PlaySound(SoundID.Item28, GemCenter);
            if (Main.myPlayer == Projectile.owner)
            {
                Vector2 vel = ExtensionVec2.SNormalize(Main.MouseWorld - GemCenter) * Projectile.velocity.Length();
                Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), GemCenter, vel, ModContent.ProjectileType<EmeraldGlowProj>(), (int)(Projectile.damage * Power), Projectile.knockBack * Power, Projectile.owner);
                proj.scale = Power;
            }
            return true;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteBatch spriteBatch = Main.spriteBatch;
            Texture2D staff = GetTex("HeroRegression/Items/Weapons/Magic/Brilliant");
            Texture2D glow = GetTex("HeroRegression/Items/Weapons/Magic/Brilliant_Glow");
            Vector2 position = Projectile.Center - Main.screenPosition;
            Vector2 origin = new Vector2(0, 68);
            spriteBatch.Draw(staff, position, null, lightColor, 0f, origin, 1, Projectile.direction >= 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            spriteBatch.Draw(glow, position, null, Color.Lerp(lightColor, Color.White, Projectile.frameCounter / 180f), 0f, origin, 1, Projectile.direction >= 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }
    }
}
