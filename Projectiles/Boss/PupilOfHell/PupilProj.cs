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
using Terraria.Audio;

namespace HeroRegression.Projectiles.Boss.PupilOfHell
{
    public class Chains : ModProjectile
    {
        public static Texture2D GetTex(string path)
        {
            return ModContent.Request<Texture2D>(path).Value;
        }
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("锁链");



        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 12;
            Projectile.height = 18;
            Projectile.timeLeft = 2900;//弹幕存在时间
            Projectile.tileCollide = false;//是否不穿墙

            Projectile.friendly = false;
            Projectile.ignoreWater = true;

            Projectile.penetrate = -1;


            Projectile.hostile = false;


        }

        public override void AI()
        {
            NPC NPC = Main.npc[(int)Projectile.ai[1]];
            Projectile.Center = NPC.Center;
            if (!NPC.active)
            {
                Projectile.Kill();
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            NPC boss = Main.npc[(int)Projectile.ai[0]]; Vector2 unit = boss.Center - Projectile.Center; float length = unit.Length();
            unit.Normalize();
            Texture2D line =  Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;

            for (float k = 0; k <= length; k += 18)
            {
                Vector2 drawPos = Projectile.Center + unit * k - Main.screenPosition;
                Main.EntitySpriteDraw(line, drawPos, null, Color.White * 1, unit.ToRotation() - MathHelper.Pi / 2, new Vector2(11, 11), 1f, SpriteEffects.None, 0);
                
            }



            return false;
        }



    }
    public class SpawnDoor : ModProjectile
    {
        public static Texture2D GetTex(string path)
        {
            return ModContent.Request<Texture2D>(path).Value;
        }
        Texture2D C1;
        Texture2D C2;
        Texture2D s;
        Texture2D s1;
        public float start = 0;
        public override string Texture => "Terraria/Images/Projectile_1";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("诞生之门");



        }
        public override void SetDefaults()
        {
            C1 = GetTex("HeroRegression/Textures/Pupil/Circle");
            C2 = GetTex("HeroRegression/Textures/Pupil/Circle2");
             s = GetTex("HeroRegression/Textures/Pupil/Storm");
             s1 = GetTex("HeroRegression/Textures/Pupil/Storm1");
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.timeLeft =200;//弹幕存在时间
            Projectile.tileCollide = false;//是否不穿墙

            Projectile.friendly = false;
            Projectile.ignoreWater = true;

            Projectile.penetrate = -1;


            Projectile.hostile = false;


        }

        public override void AI()
        {
            NPC boss = Main.npc[(int)Projectile.ai[0]];
            NPC NPC = Main.npc[(int)Projectile.ai[1]];
            Projectile.Center = NPC.Center;
            Projectile.rotation += 0.08f;
            if (start < 1f && Projectile.timeLeft > 50) { start+= 0.02f; }
            if(Projectile.timeLeft <= 50) { start -= 0.02f; }
        }
        public override bool PreDraw(ref Color lightColor)
        {

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            
            Main.EntitySpriteDraw(C1, Projectile.Center - Main.screenPosition, null, Color.Orange * 1,Projectile.rotation,C1.Size()/2,0.8f * start, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(C2, Projectile.Center - Main.screenPosition, null, Color.White * 1, -Projectile.rotation, C2.Size() / 2, 2.4f * start, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(s, Projectile.Center - Main.screenPosition, null, Color.White * 1, Projectile.rotation*0.8f, s.Size() / 2, 3.5f * start, SpriteEffects.None, 0);
            Main.EntitySpriteDraw(s1, Projectile.Center - Main.screenPosition, null, Color.White * 1, -Projectile.rotation * 0.9f, s1.Size() / 2,3.7f * start, SpriteEffects.None, 0);
         

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }



    }
    public class PupilProj : ModProjectile
    {
        public static Texture2D GetTex(string path)
        {
            return ModContent.Request<Texture2D>(path).Value;
        }
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("火焰弹");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;

        }
        Texture2D line;
        Texture2D l1 = GetTex("HeroRegression/Textures/LaserTex2");
        public override void SetDefaults()
        {
            line = GetTex("HeroRegression/Projectiles/Boss/PupilOfHell/PupilProj");
            
            base.SetDefaults();
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.timeLeft = 200;//弹幕存在时间
            Projectile.tileCollide = false;//是否不穿墙

            Projectile.friendly = false;
            Projectile.ignoreWater = true;

            Projectile.penetrate = -1;


            Projectile.hostile = true;


        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + 1.57f;
            if(Projectile.ai[0] == 1 && Projectile.timeLeft >140)
            {
                Projectile.velocity = Projectile.velocity.RotatedBy(0.019f);
            }
            if(Projectile.ai[0] == 2 && Projectile.timeLeft > 140)
            {
                Projectile.velocity = Projectile.velocity.RotatedBy(-0.019f);
            }
            if(Projectile.ai[0] == 3 && Projectile.velocity.Length() < 40)//有预瞄线的
            {
                Projectile.velocity *= 1.06f;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            for (int k = 0; k <= 7; k += 1)
            {
                Color color = Color.White * (float)(1-(k / 7f));
                
                Main.EntitySpriteDraw(line, Projectile.oldPos[k] - Main.screenPosition + new Vector2(15) , null, color,Projectile.oldRot[k],line.Size()/2, (float)(1 - (k / 7f)), SpriteEffects.None, 0);

            }
            Main.EntitySpriteDraw(line, Projectile.Center - Main.screenPosition, null, Color.White * 1, Projectile.rotation, line.Size() / 2, 1f, SpriteEffects.None, 0);

            if (Projectile.ai[0] == 3)
            {
                int timelef = Projectile.timeLeft - 150;
                Main.EntitySpriteDraw(l1, Projectile.Center - Main.screenPosition, null, Color.White * ((float)(timelef)/50), Projectile.velocity.ToRotation() + 1.57f, l1.Size() / 2, new Vector2(1,100), SpriteEffects.None, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
           

            return false;
        }



    }
    public class MinionProj : ModProjectile
    {
        public static Texture2D GetTex(string path)
        {
            return ModContent.Request<Texture2D>(path).Value;
        }
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("僚机火箭弹");



        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 32;
            Projectile.height = 40;
            Projectile.timeLeft = 340;//弹幕存在时间
            Projectile.tileCollide = false;//是否不穿墙

            Projectile.friendly = false;
            Projectile.ignoreWater = true;

            Projectile.penetrate = -1;


            Projectile.hostile = true;


        }

        public override void AI()
        {
            
            Projectile.rotation = Projectile.velocity.ToRotation() + 1.57f;
            Player player = Main.player[Projectile.owner];
          
            Dust.NewDust(Projectile.position - Vector2.Normalize(Projectile.velocity) * 25, Projectile.width, Projectile.height, DustID.Torch);
            if (Projectile.timeLeft > 250 && Projectile.timeLeft <= 300)
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, Vector2.Normalize(player.Center - Projectile.Center) * 16f, 0.05f);
        }

        public override bool PreDraw(ref Color lightColor)
        {
           ;
        Main.EntitySpriteDraw(Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation,  Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value.Size() / 2, 1, SpriteEffects.None, 0);
            //这个正常吗
            
            return false;
        }



    }
    public class PupLaser : ModProjectile
    {
        public static Texture2D GetTex(string path)
        {
            return ModContent.Request<Texture2D>(path).Value;
        }
        public override string Texture => "Terraria/Images/Item_1";
        Texture2D laser = GetTex("HeroRegression/Textures/Pupil/PupLaser");
        Texture2D laser2 = GetTex("HeroRegression/Textures/LaserTex2");
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("狱焰激光");



        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.timeLeft = 600;//弹幕存在时间
            Projectile.tileCollide = false;//是否不穿墙

            Projectile.friendly = false;
            Projectile.ignoreWater = true;

            Projectile.penetrate = -1;


            Projectile.hostile = true;


        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 endPoint = Projectile.Center + Projectile.rotation.ToRotationVector2() * 1920;
            if (Projectile.ai[1] == 0 && Projectile.timeLeft < 575)//正常激光
            {
                if (Projectile.ai[0] < 1)
                    Projectile.ai[0] += 1f / 25f;
            }
            if (Projectile.ai[1] == 1 )//水平发射的预判激光
            {
                if (Projectile.ai[0] < 1 && Projectile.timeLeft < 575)
                    Projectile.ai[0] += 1f / 25f;
                if(Projectile.timeLeft > 575)
                Projectile.Center = new Vector2(Projectile.Center.X, player.Center.Y + player.velocity.Y * 60);
                if(Projectile.timeLeft < 300)
                {
                    Projectile.Kill();
                }
            }
            if (Projectile.ai[1] == 2 )//正常激光
            {
                if (Projectile.ai[0] < 1)
                    Projectile.ai[0] += 1f / 25f;
                NPC NPC = Main.npc[Projectile.frameCounter];
                if (NPC != null && NPC.active) { 
                Projectile.rotation = NPC.rotation;Projectile.Center = NPC.Center;
                }
                Vector2 unit = Projectile.rotation.ToRotationVector2();
                if (Projectile.timeLeft % 20 == 0)
                {
                    for (int i = 2; i < 8; i+=2)
                    {
                        Vector2 gopos = unit * 150 * i + Projectile.Center;
                        Projectile.NewProjectile(null,gopos, unit.RotatedBy(1.57f) * 3f, ModContent.ProjectileType<ExpProj>(), 20, 1, NPC.target, 1);
                        Projectile.NewProjectile(null,gopos, unit.RotatedBy(-1.57f) * 3f, ModContent.ProjectileType<ExpProj>(), 20, 1, NPC.target,2);
                    }
                }
                if (!NPC.active)
                {
                    Projectile.Kill();
                }
            }
            if (Projectile.ai[1] == 3)//正常激光
            {
                if (Projectile.ai[0] < 1)
                    Projectile.ai[0] += 1f / 25f;
                NPC NPC = Main.npc[Projectile.frameCounter];
                if (NPC != null && NPC.active)
                {
                    Projectile.rotation = NPC.rotation; Projectile.Center = NPC.Center;
                }
                if (!NPC.active)
                {
                    Projectile.Kill();
                }
            }
            if (Projectile.ai[1] == 4)//正常激光
            {
                if (Projectile.ai[0] < 1)
                    Projectile.ai[0] += 1f / 25f;
                NPC NPC = Main.npc[Projectile.frameCounter];
                if (NPC != null && NPC.active)
                {
                    if(Projectile.timeLeft >= 450)
                    Projectile.rotation += MathHelper.Pi/430 * ((1.3f * (float)Math.Sin((600 - Projectile.timeLeft) * (3.1416f / 150))));
                }
                if (!NPC.active)
                {
                    Projectile.Kill();
                }
            }
            if (Projectile.ai[1] == 5)//正常激光
            {
                if (Projectile.ai[0] < 1)
                    Projectile.ai[0] += 1f / 25f;
                NPC NPC = Main.npc[Projectile.frameCounter];
                if (NPC != null && NPC.active)
                {
                    if (Projectile.timeLeft >= 450)
                        Projectile.rotation -= MathHelper.Pi / 430 * ((1.3f * (float)Math.Sin((600 - Projectile.timeLeft) * (3.1416f / 150))));
                }
                if (!NPC.active)
                {
                    Projectile.Kill();
                }
            }
        }
        public override bool PreKill(int timeLeft)//激光收束
        {
            Projectile.ai[0] -= 1f / 25f;
            if(Projectile.ai[0] < 0)
            {
                Projectile.active = false;
            }
            return false ;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float point = 0;
            Vector2 endPoint = Projectile.Center + Projectile.rotation.ToRotationVector2() * 1920;
            Vector2 StartPoint = Projectile.Center - Projectile.rotation.ToRotationVector2() * 1920;
            if(Projectile.timeLeft >= 550 && Projectile.ai[1] < 2)
            {
                return false;
            }
            if(Projectile.ai[1]==0)
                return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, endPoint, 32, ref point);
            if (Projectile.ai[1] == 1)
                return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(),StartPoint, endPoint, 32, ref point);
            if (Projectile.ai[1] == 2)
                return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, endPoint, 32, ref point);
            if (Projectile.ai[1] == 3)
                return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, endPoint, 32, ref point);
            if (Projectile.ai[1] == 4)
                return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, endPoint, 32, ref point);
            if (Projectile.ai[1] ==5)
                return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, endPoint, 32, ref point);
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            if (Projectile.timeLeft >=575 && Projectile.ai[1] < 2)
            {
                Main.EntitySpriteDraw(laser2, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation + 3.1416f / 2, new Vector2(laser2.Size().X / 2,laser2.Size().Y/2), new Vector2((float)(Projectile.timeLeft - 575) * 0.04f , 100), SpriteEffects.None, 0);
            }
            if (Projectile.timeLeft < 575)
            {
                if (Projectile.ai[1] == 0)
                    Main.EntitySpriteDraw(laser, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation + 3.1416f / 2, new Vector2(0, laser.Size().Y / 2), new Vector2(Projectile.ai[0], 100), SpriteEffects.None, 0);
                if (Projectile.ai[1] == 1)
                    Main.EntitySpriteDraw(laser, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation + 3.1416f / 2, laser.Size() / 2, new Vector2(Projectile.ai[0], 100), SpriteEffects.None, 0);
            }
            if (Projectile.ai[1] == 2)
            { 
                Main.EntitySpriteDraw(laser, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation - 3.1416f / 2, new Vector2( laser.Size().X / 2,0), new Vector2(Projectile.ai[0], 100), SpriteEffects.None, 0);
            }
            if (Projectile.ai[1] == 3)
            {
                Main.EntitySpriteDraw(laser, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation - 3.1416f / 2, new Vector2(laser.Size().X / 2, 0), new Vector2(Projectile.ai[0], 100), SpriteEffects.None, 0);
            }
            if (Projectile.ai[1] ==4)
            {
                Main.EntitySpriteDraw(laser, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation - 3.1416f / 2, new Vector2(laser.Size().X / 2, 0), new Vector2(Projectile.ai[0], 100), SpriteEffects.None, 0);
            }
            if (Projectile.ai[1] == 5)
            {
                Main.EntitySpriteDraw(laser, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation - 3.1416f / 2, new Vector2(laser.Size().X / 2, 0), new Vector2(Projectile.ai[0], 100), SpriteEffects.None, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }



    }
    public class PupExp : ModProjectile
    {
        public static Texture2D GetTex(string path)
        {
            return ModContent.Request<Texture2D>(path).Value;
        }
        public override string Texture => "Terraria/Images/Item_1";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("爆炸");



        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.timeLeft = 60;//弹幕存在时间
            Projectile.tileCollide = false;//是否不穿墙

            Projectile.friendly = false;
            Projectile.ignoreWater = true;

            Projectile.penetrate = -1;
            Projectile.extraUpdates = 1;

            Projectile.rotation = Main.rand.NextFloat(-3.1415f, 3.14f);
            Projectile.hostile = true;


        }

        public override void AI()
        {
            Projectile.ai[0]++;
            if (Projectile.ai[0] == 10)
            {
                Projectile.ai[1] += 1; Projectile.ai[0] = 0;
            }
            if (Projectile.ai[1] > 6)
            {
                Projectile.ai[1] = 6;
            }
            Projectile.scale = 2.2f;
            //Projectile.rotation += 0.2f;

        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath6);


            base.Kill(timeLeft);
        }
        public override bool PreDraw(ref Color lightColor)
        {

            Texture2D line = GetTex("HeroRegression/Textures/Pupil/PupExp");
            Vector2 drawpos = Projectile.Center - Main.screenPosition;
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            Main.EntitySpriteDraw(line, drawpos, new Rectangle(0, 98 * ((int)Projectile.ai[1]), 98, 98), Color.White, Projectile.rotation, new Vector2(49, 49), Projectile.scale, SpriteEffects.None, 0);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }



    }
    public class ExpProj : ModProjectile
    {
        public override string Texture => "Terraria/Images/Item_1";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("火焰弹");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;

        }
        public override void SetDefaults()
        {

            base.SetDefaults();
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.timeLeft =1000;//弹幕存在时间
            Projectile.tileCollide = false;//是否不穿墙

            Projectile.friendly = false;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 4;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.light = 1.1f;

        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if(Projectile.timeLeft % 15 == 0)
            {
                Projectile.NewProjectile(null,Projectile.Center, Vector2.Zero, ModContent.ProjectileType<PupExp>(), 10, 1, 0);
            }
            Vector2 topos = Vector2.Normalize(player.Center - Projectile.Center) * 2.7f;
            if(Projectile.timeLeft > 300 && Projectile.ai[0] ==0)
            Projectile.velocity = Vector2.Lerp(Projectile.velocity, topos, 0.003f);
            if(Projectile.ai[0] == 1)
            {
                Projectile.velocity = Projectile.velocity.RotatedBy(0.003f);
            }
            if (Projectile.ai[0] ==2)
            {
                Projectile.velocity = Projectile.velocity.RotatedBy(-0.003f);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {

            return false;
        }



    }
}
