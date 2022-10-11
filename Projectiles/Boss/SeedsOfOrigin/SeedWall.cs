using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HeroRegression.Projectiles.Boss.SeedsOfOrigin
{
    class SeedWall : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 24;
            Projectile.light = .5f;
            Projectile.hostile = true;
            Projectile.damage = 10;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
        }
        public void ShootProj(Vector2 pos, float rot, float dis, float vel, int amount, int ID, int damage, float knockback, float randomdis)//简单散射
        {
            if (amount % 2 == 0)
            {
                for (int k = amount / 2; k > -amount / 2; k--)
                {
                    if (k != 0)
                    {
                        float rdis = MathHelper.ToRadians(dis);
                        float tr = rot + (k - 0.5f) * rdis;
                        Projectile p = Projectile.NewProjectileDirect(null,pos, tr.ToRotationVector2() * vel, ID, damage, knockback, 0);
                        p.hostile = true; p.friendly = false; 
                    }
                }
            }
            if (amount % 2 != 0)
            {
                for (int k = amount / 2; k >= -amount / 2; k--)
                {
                    float rdis = MathHelper.ToRadians(dis);
                    float tr = rot + k * rdis;
                    Projectile p = Projectile.NewProjectileDirect(null,pos, tr.ToRotationVector2() * vel, ID, damage, knockback, 0);
                    p.hostile = true; p.friendly = false; 
                }
            }

        }
        public override void AI()
        {
            Projectile.rotation = 0;

            Player player = Main.LocalPlayer;
            if(Projectile.ai[0] == 0)
            {
                Vector2 topos = Vector2.Normalize(player.Center + new Vector2(-640, 0) - Projectile.Center);
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, topos * 18, 0.1f);
            }
            if (Projectile.ai[0] == 1)
            {
                Vector2 topos = Vector2.Normalize(player.Center + new Vector2(640, 0) - Projectile.Center);
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, topos * 18, 0.1f);
            }
            Projectile.localAI[0]++;
            float Timer = Projectile.localAI[0];
            if (Timer == 70 | Timer == 180)
            {
                ShootProj(Projectile.Center, (player.Center - Projectile.Center).ToRotation(), 30, 10, 3, ProjectileID.RuneBlast, 10, 1, 0);
            }
            if (Timer == 125)
            {
                ShootProj(Projectile.Center, (player.Center - Projectile.Center).ToRotation(), 20, 10, 3, ProjectileID.RuneBlast, 10, 1, 0);
            }
            if(Timer == 220)
            {
                Projectile.Kill();
            }
        }
       
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D SO = ModContent.Request<Texture2D>("HeroRegression/Projectiles/Boss/SeedsOfOrigin/SeedWall").Value;


            Main.EntitySpriteDraw(SO, Projectile.Center - Main.screenPosition, null, Color.White, 0, new Vector2(20, 12), 1, SpriteEffects.None, 0);


            return false;
        }
    }
}
