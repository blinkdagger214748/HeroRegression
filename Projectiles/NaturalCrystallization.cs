using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HeroRegression.Dusts;
using Terraria.Localization;

namespace HeroRegression.Projectiles
{
    public class NaturalCrystallization : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("自然结晶");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 80;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 20;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.timeLeft = 1200;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 3;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Player player = Main.player[Projectile.owner];
            SpriteEffects effects = player.direction > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, new Rectangle(0, Projectile.frame * 10, 10, 10),
                Color.LightSeaGreen, Projectile.rotation, new Vector2(20, 20), new Vector2(2f, 1.1f), effects, 0f);
            Texture2D tex = ModContent.Request<Texture2D>("HeroRegression/Projectiles/NaturalCrystallization").Value;
            Texture2D k = ModContent.Request<Texture2D>("HeroRegression/Textures/K").Value;
            Vector2 drawOrigin = new Vector2(8, 8);//绘制中心是弹幕贴图中心

            for (int i = 0; i < Projectile.oldPos.Length; i += 4)
            {
                Vector2 drawPos = Projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Color.LightGreen * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
                float Sc = 0.3f - 0.02f * i;
                Main.spriteBatch.Draw(k, drawPos, null, color, Projectile.oldRot[i], new Vector2(36, 36), Sc, SpriteEffects.None, 0f);
            }
            // spriteBatch.Draw(tex,Projectile.Center - Main.screenPosition,new Rectangle(0,(int)Projectile.frame*42,52,42),Color.White*0.85f)
            return false;
        }
        bool flag = false;
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + 1.57f;
            bool proj = true;

            Projectile.velocity = Vector2.Normalize(Main.MouseWorld - Projectile.Center) * 4f;
            Player player = Main.player[Projectile.owner];
            if (proj && player.channel)
            {

                Projectile.velocity = Vector2.Normalize(Main.MouseWorld - Projectile.Center) * 4f;
            }
            if (!player.channel)
            {
                proj = false;
                Projectile.velocity = Vector2.Normalize(Projectile.velocity) * 4f;
            }
            if (!flag && player.ownedProjectileCounts[ModContent.ProjectileType<NaturalCrystallization>()] <= 10)
            {
                Main.NewText("<碧辉弹幕计数器(到10弹幕消失哦亲)>"+player.ownedProjectileCounts[ModContent.ProjectileType<NaturalCrystallization>()], Color.LightGreen);
                flag = true;
            }
            if (player.ownedProjectileCounts[ModContent.ProjectileType<NaturalCrystallization>()] > 10)
            {
                Projectile.Kill();
                flag = true;
                
            }
        }
    }
}

