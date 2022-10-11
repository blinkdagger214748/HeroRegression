using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HeroRegression.Projectiles
{
    class YinLeiSlash1 : ModProjectile
    {
        public bool Chase = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("YinLeislash");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "隐雷斩");

        }
        public override void SetDefaults()
        {
            Projectile.width = 128;
            Projectile.height = 128;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;
            Projectile.timeLeft = 1200;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
           
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            Player player = Main.player[Projectile.owner];
            SpriteEffects effects = player.direction > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, new Rectangle(0,Projectile.frame * 160,160,160),
                Color.White, Projectile.rotation, new Vector2(160/2,160/2),new Vector2(2f,1.1f), effects, 0f);
               
            
            return false;
        }
        public override void AI()
        {
            Projectile.Center = Vector2.Lerp(Projectile.Center, Main.MouseWorld,0.03f);
            Projectile.velocity = Vector2.Normalize(Main.MouseWorld - Projectile.Center) * 1f;
            Player player = Main.player[Projectile.owner];Projectile.Center = player.Center;
            if (player.dead) { Projectile.Kill(); }
            if (player.channel) { Projectile.timeLeft = 2; }
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            Projectile.frameCounter++;
            if (Projectile.frameCounter == 4)
            {
                Projectile.frame += 1; Projectile.frameCounter = 0;
            }
            if (Projectile.frame == 13) { Projectile.frame = 0; }
        }
    }
}
