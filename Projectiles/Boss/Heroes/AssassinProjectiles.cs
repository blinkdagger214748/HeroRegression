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

namespace HeroRegression.Projectiles.Boss.Heros
{
    class AssassinBolt : ModProjectile//潜伏者之矢
    {
        public Texture2D GetTex(string path)
        {
            return ModContent.Request<Texture2D>(path).Value;
        }
        public override string Texture => "Terraria/Images/Projectile_12"; public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.light = .5f;
            Projectile.hostile = true;
     
            Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D SO = GetTex("HeroRegression/Textures/WhiteBeam");



            for (int i = 1; i < Projectile.oldPos.Length; i += 2)//trail
            {
                Vector2 drawPos1 = Projectile.oldPos[i] - Main.screenPosition + new Vector2(Projectile.width / 2, Projectile.height / 2) + new Vector2(0, Projectile.gfxOffY);
                Color color = Color.White * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
                float Sc = 1f - 0.03f * i;
                Main.EntitySpriteDraw(SO, drawPos1, null, color, Projectile.rotation + MathHelper.Pi/2, SO.Size() / 2, Sc, SpriteEffects.None, 0);
            }
            Main.EntitySpriteDraw(SO, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation + MathHelper.Pi / 2, SO.Size() / 2, 1, SpriteEffects.None, 0);
            return false;
        }
    }
    class AssassinKnife : ModProjectile//潜伏者匕首
    {
        public Texture2D GetTex(string path)
        {
            return ModContent.Request<Texture2D>(path).Value;
        }
        public override string Texture => "Terraria/Images/Projectile_12";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] =2;
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.light = .5f;
            Projectile.hostile = true;
     
            Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            Projectile.rotation += 0.17f;
            Projectile.velocity *= 1.02f;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D SO = GetTex("HeroRegression/Projectiles/Boss/Heroes/ExampleProj");
            for (int i = 1; i < Projectile.oldPos.Length; i += 1)//trail
            {
                Vector2 drawPos1 = Projectile.oldPos[i] - Main.screenPosition + new Vector2(Projectile.width / 2, Projectile.height / 2) + new Vector2(0, Projectile.gfxOffY);
                Color color = Color.White * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
                float Sc = 1f - 0.1f * i;
                Main.EntitySpriteDraw(SO, drawPos1,null, color, 0, SO.Size()/2, Sc, SpriteEffects.None, 0);
            }

            Main.EntitySpriteDraw(SO, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, SO.Size() / 2, 1, SpriteEffects.None, 0);


            return false;
        }
    }
    class AssassinSword : ModProjectile//潜伏者之刃
    {
        public Texture2D GetTex(string path)
        {
            return ModContent.Request<Texture2D>(path).Value;
        }
        public override string Texture => "Terraria/Images/Projectile_12"; public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.light = .5f;
            Projectile.hostile = true;
      
            Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            if(Projectile.ai[0] == 0)
            {
                Projectile.ai[1]++;
                if(Projectile.ai[1] < 70)
                {
                    Projectile.velocity *= 0.9f;
                }
                if(Projectile.ai[1] > 70)
                {
                    Projectile.velocity = Vector2.Normalize(Projectile.velocity) * 15f;
                }
                Projectile.rotation += 0.4f;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D SO = GetTex("HeroRegression/Projectiles/Boss/Heroes/ExampleProj");
            for (int i = 1; i < Projectile.oldPos.Length; i += 1)//trail
            {
                Vector2 drawPos1 = Projectile.oldPos[i] - Main.screenPosition + new Vector2(Projectile.width / 2, Projectile.height / 2) + new Vector2(0, Projectile.gfxOffY);
                Color color = Color.White * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
                float Sc = 1f - 0.1f * i;
                Main.EntitySpriteDraw(SO, drawPos1, null, color, 0, SO.Size() / 2, Sc, SpriteEffects.None, 0);
            }

            Main.EntitySpriteDraw(SO, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, SO.Size() / 2, 1, SpriteEffects.None, 0);


            return false;
        }
    }
    class AssassinSickle : ModProjectile//潜伏者之刃
    {
        public Texture2D GetTex(string path)
        {
            return ModContent.Request<Texture2D>(path).Value;
        }
        public override string Texture => "Terraria/Images/Projectile_274";
        public override void SetDefaults()
        {
            Projectile.width = 128;
            Projectile.height =128;
            Projectile.light = .5f;
            Projectile.hostile = true;

            Projectile.timeLeft = 6000;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 chase = Vector2.Normalize(player.Center - Projectile.Center) ;
            float speed = Vector2.Distance(player.Center, Projectile.Center);
            if(speed < 180)
            Projectile.velocity = Vector2.Lerp(Projectile.velocity, chase * 4, 0.4f);
            if (speed >= 180 && speed <= 1000)
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, chase * 4 * (1+ 0.004f * speed), 0.5f);
            if(speed > 1000)
            {
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, chase * 4 * (1 + 0.004f * 1000), 0.5f);
            }
            Projectile.rotation += 0.4f;
            NPC npc = Main.npc[(int)Projectile.ai[0]];
            if(!npc.active || npc.ai[1] != 5)
            {
                Projectile.Kill();
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D SO = GetTex("HeroRegression/Projectiles/Boss/Heroes/ExampleProj");
            
            Texture2D tex = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;

            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, tex.Size() / 2,5, SpriteEffects.None, 0);


            return false;
        }
    }
    class AssassinSlash : ModProjectile//潜伏者斩击
    {
        public Texture2D GetTex(string path)
        {
            return ModContent.Request<Texture2D>(path).Value;
        }
        public override string Texture => "Terraria/Images/Projectile_12";
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.light = .5f;
            Projectile.hostile = true;
            
            Projectile.timeLeft = 25;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;

        }
        public override void AI()
        {
            
            
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D S = GetTex("HeroRegression/Images/Slash1");


            Main.EntitySpriteDraw(S, Projectile.Center - Main.screenPosition, null, Color.White , Projectile.rotation, S.Size() / 2, new Vector2(0.12f * Projectile.timeLeft,0.04f * Projectile.timeLeft), SpriteEffects.None, 0);


            return false;
        }
    }
    class AssassinSlash2 : ModProjectile//潜伏者刺
    {
        public Texture2D GetTex(string path)
        {
            return ModContent.Request<Texture2D>(path).Value;
        }
        public override string Texture => "Terraria/Images/Projectile_12";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.light = .5f;
            Projectile.hostile = true;

            Projectile.timeLeft = 150;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
         
        }
        public override void AI()
        {
            if(Projectile.timeLeft == 100)
            {
                Projectile.velocity = Vector2.Normalize(Projectile.velocity) * 39f;
            }
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
        public override bool PreDraw(ref Color lightColor)
        {
            float time = 150 - Projectile.timeLeft;
            Texture2D S = GetTex("HeroRegression/Textures/LaserTex2");
            for (int i = 0; i < 50; i++)
            {
                if (time == i && i < 33)
                    Main.EntitySpriteDraw(S, Projectile.Center - Main.screenPosition, null, Color.White , Projectile.velocity.ToRotation(), new Vector2(0, 80), new Vector2(400, 0.025f), SpriteEffects.None, 0);
                if (time == i && i >= 33)
                    Main.EntitySpriteDraw(S, Projectile.Center - Main.screenPosition, null, Color.White * 0.06f * (50 - i), Projectile.velocity.ToRotation(), new Vector2(0, 80), new Vector2(400, 0.025f), SpriteEffects.None, 0);
            }
            if (Projectile.timeLeft < 100)
            {
                Texture2D S1 = GetTex("HeroRegression/Images/Slash1");
                for (int i = 1; i < Projectile.oldPos.Length; i += 1)//trail
                {
                    Vector2 drawPos1 = Projectile.oldPos[i] - Main.screenPosition + new Vector2(Projectile.width / 2, Projectile.height / 2) + new Vector2(0, Projectile.gfxOffY);
                    Color color = Color.White * ((float)(Projectile.oldPos.Length - i) / (float)Projectile.oldPos.Length);
                    float Sc = 2 - 0.15f * i;
                    Main.EntitySpriteDraw(S1, drawPos1, null, color,Projectile.oldRot[i], new Vector2(32, 4), Sc, SpriteEffects.None, 0);
                }
            }
            return false;
        }
    }
    class AssassinSickle2 : ModProjectile//潜伏
    {
        public Texture2D GetTex(string path)
        {
            return ModContent.Request<Texture2D>(path).Value;
        }
        public override string Texture => "Terraria/Images/Projectile_97";
        public override void SetDefaults()
        {
            Projectile.width =64;
            Projectile.height =64;

            Projectile.hostile = true;

            Projectile.timeLeft = 6000;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            NPC npc = Main.npc[(int)Projectile.ai[0]];
            Projectile.Center = npc.Center;
        
            if(npc.ai[0] < 114)
            {
                Projectile.rotation += 0.2f;
            }
            if(npc.ai[0] >= 114 && npc.ai[2] != 1)
            {
                Projectile.rotation = npc.velocity.ToRotation() + 3 * MathHelper.Pi / 4;
            }
            if (!npc.active || npc.ai[1] != 6)
            {
                Projectile.Kill();
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            NPC npc = Main.npc[(int)Projectile.ai[0]];
            Texture2D SO = GetTex("HeroRegression/Projectiles/Boss/Heroes/ExampleProj");
            Texture2D tex = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            if(npc.ai[2] != 1)
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, tex.Size() / 2, 2.5f, SpriteEffects.None, 0);


            return false;
        }
    }
    class AssassinSickle3 : ModProjectile//潜伏者
    {
        public Texture2D GetTex(string path)
        {
            return ModContent.Request<Texture2D>(path).Value;
        }
        public override string Texture => "Terraria/Images/Projectile_274";
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.light = .5f;
            Projectile.hostile = true;

            Projectile.timeLeft = 300;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
        }
        public override void AI()
        {
            Dust.NewDust(Projectile.position, 32, 32, DustID.WhiteTorch);
            Projectile.rotation += 0.4f;
            Projectile.velocity *= 1.031f;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D SO = GetTex("HeroRegression/Textures/WhiteSickle");


            Main.EntitySpriteDraw(SO, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, SO.Size() / 2, 1.2f, SpriteEffects.None, 0);


            return false;
        }
    }
}
