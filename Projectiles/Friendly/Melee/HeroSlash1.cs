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

namespace HeroRegression.Projectiles.Friendly.Melee
{
    class HeroSlash1 : ModProjectile
    {
        public bool Chase = false;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heroslash");
            DisplayName.AddTranslation((int)GameCulture.CultureName.Chinese, "群英斩");

        }
        public override void SetDefaults()
        {
            Projectile.width = 158;
            Projectile.height = 138;
            Projectile.DamageType = DamageClass.Melee;

            Projectile.friendly = true;
            Projectile.timeLeft = 1200;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;

            Projectile.penetrate = -1;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D = TextureAssets.Projectile[Projectile.type].Value;
            Player player = Main.player[Projectile.owner];
            SpriteEffects effects = player.direction < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Main.spriteBatch.Draw(texture2D, Projectile.Center - Main.screenPosition, new Rectangle(0, Projectile.frame * 128, 128, 128), Color.White, Projectile.rotation, new Vector2(64, 64), new Vector2(2f, 1.3f), effects, 0f);


            return false;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner]; Projectile.Center = player.Center;
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
            if (Projectile.frame == 21) { Projectile.frame = 0; }
        }
    }
}
