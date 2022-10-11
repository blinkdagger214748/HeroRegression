using Terraria;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using HeroRegression.Projectiles;
using System;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using HeroRegression.NPCs.Boss.FlameReaction;
using HeroRegression;

namespace HeroRegression.Projectiles.Boss.FlameReaction
{
    public class FallingSlash : ModProjectile
    {
        NPC fr => FlameReactionBoss.FR; 
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flame Slash");
        }
        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 300;
            Projectile.hostile = true;
            Projectile.damage = 25;
            Projectile.timeLeft = 75;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
        }
        public override bool PreDraw(ref Color drawColor)
        {
            Texture2D tex = HeroRegression.GetTex("HeroRegression/Textures/FallingSlashTex");
            Vector2 ori = new Vector2(30, 150);
            Rectangle rec = new Rectangle(60 * Projectile.frameCounter, 0, 60, 300);
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, rec, Color.White, 0, ori, 1f, SpriteEffects.None, 0);
            return true;
        }
        public override void AI()
        {
            if (Projectile.timeLeft % 3 == 0)
            {
                if (Projectile.frameCounter >= 25)
                {
                    Projectile.frameCounter = 0;
                }
                else
                {
                    Projectile.frameCounter += 1;
                }
            }
        }
        public static Color[,] GetColors(Texture2D tex)
        {
            Color[] colors = new Color[tex.Width * tex.Height];
            tex.GetData(colors, 0, tex.Width * tex.Height);
            Color[,] colors2 = new Color[tex.Width, tex.Height];
            for (int i = 0; i < colors.Length; i += 10)
            {
                int a = i % tex.Width;
                int b = (int)Math.Floor((double)i / tex.Width);
                colors2[a, b] = colors[i];
            }
            return colors2;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return Vector2.Distance(Projectile.position + new Vector2(30,30 + 9.6f * Projectile.frameCounter), targetHitbox.Center()) <= 52;
        }
    }
}
