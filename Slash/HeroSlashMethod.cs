using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace HeroRegression.Slash
{
    public class SlashTest : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.Meowmere;
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.TerraBlade);
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.channel = true;
            base.SetDefaults();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return false;
        }
    }
    public class HeroSlashMethod : ModProjectile
    {
        Effect SlashEff = ModContent.Request<Microsoft.Xna.Framework.Graphics.Effect>("HeroRegression/Effects/BigTentacle").Value;
        /// <summary>
        /// ·Startingrot决定这个东西挥刀的起始点，以水平向右为基准啊，别搞错了<br />
        /// ·rotation决定这玩意往哪个方向挥刀。<br />
        /// ·Length决定这个东西能在那个方向上挥出多远<br />
        /// ·thick决定这个挥刀的刀光宽度，纯纯的视觉参数罢了,0~1自己调<br />
        /// ·Yscale决定这个斩击有多扁，控制在0~1就行，别整多了<br />
        /// ·owner是isplayerproj真的时候存玩家whoami，ownernpc是为假时存的NPC.whoami<br />
        /// ·weapontex指的是这个刀光的挥舞武器是什么材质，记住要是剑柄在左下角奥<br />
        /// ·ShouldDoNextSlash指的是连击数，至少为1才会有连击<br />
        /// </summary>
        public static void Slash(bool IsPlayerProj, IEntitySource source, float rotation, float Startingrot, float Length, float Thick, float YScale, int ExtraSpeed = 0, int damage = 50, float Knockback = 0, int owner = 0, int ownerNPC = 0, Color color = default, Texture2D weaponTex = default, int ShouldDoNextSlash = 0, float KnockBackRotation = 0, float WeaponScale = 1)
        {
            if (IsPlayerProj)
            {
                var p = Projectile.NewProjectileDirect(source, Main.player[owner].Center, KnockBackRotation.ToRotationVector2() * Length, ModContent.ProjectileType<HeroSlashMethod>(), damage, Knockback, owner, 0, rotation);
                p.rotation = Startingrot;
                (p.ModProjectile as HeroSlashMethod).Reverse = Startingrot > 0;
                p.localAI[0] = Thick; p.localAI[1] = YScale;
                if (color != default) (p.ModProjectile as HeroSlashMethod).c = color;
                p.extraUpdates = ExtraSpeed; (p.ModProjectile as HeroSlashMethod).Scale1 = WeaponScale;
                (p.ModProjectile as HeroSlashMethod).ShouldDoNextSlash = ShouldDoNextSlash;
                if (weaponTex != default) (p.ModProjectile as HeroSlashMethod).t = weaponTex;
                var p1 = Projectile.NewProjectileDirect(source, Main.player[owner].Center, KnockBackRotation.ToRotationVector2() * Length, ModContent.ProjectileType<HeroSlash2>(), damage, Knockback, owner, 0, rotation);
                p1.rotation = Startingrot;
                (p1.ModProjectile as HeroSlash2).Reverse = Startingrot > 0;
                p1.localAI[0] = Thick; p1.localAI[1] = YScale;
                if (color != default) (p1.ModProjectile as HeroSlash2).c = color;
                p1.extraUpdates = ExtraSpeed; (p1.ModProjectile as HeroSlash2).T = weaponTex;
            }
            if (!IsPlayerProj)
            {
                var p = Projectile.NewProjectileDirect(source, Main.npc[ownerNPC].Center, Vector2.UnitX * Length, ModContent.ProjectileType<HeroSlashMethod>(), damage, Knockback, 0, ownerNPC, rotation);
                p.rotation = Startingrot;
                (p.ModProjectile as HeroSlashMethod).Reverse = Startingrot > 0; (p.ModProjectile as HeroSlashMethod).IsNPCproj = true;
                p.localAI[0] = Thick; p.localAI[1] = YScale; if (color != default) (p.ModProjectile as HeroSlashMethod).c = color;
                p.extraUpdates = ExtraSpeed;
                var p1 = Projectile.NewProjectileDirect(source, Main.npc[ownerNPC].Center, Vector2.UnitX * Length, ModContent.ProjectileType<HeroSlash2>(), damage, Knockback, 0, ownerNPC, rotation);
                p1.rotation = Startingrot;
                (p1.ModProjectile as HeroSlash2).Reverse = Startingrot > 0; (p1.ModProjectile as HeroSlash2).IsNPCproj = true;
                p1.localAI[0] = Thick; p1.localAI[1] = YScale; if (color != default) (p1.ModProjectile as HeroSlash2).c = color;
                p1.extraUpdates = ExtraSpeed; (p1.ModProjectile as HeroSlash2).T = weaponTex;
            }
        }
        //ai0决定主人Npc,ai1决定方向，速度长度决定斩击范围，localai0决定斩击厚度，localai1决定斩击扁度，自定义变量决定斩击正反位
        public bool Reverse = false; public bool IsNPCproj = false; public Color c = Color.White; public Texture2D t = null; public int ShouldDoNextSlash = 0; public float Scale1 = 1;
        public override string Texture => "Terraria/Images/Extra_193";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 40;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 24;
            Projectile.timeLeft = 100;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -11;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1000;
            Projectile.noEnchantmentVisuals = true;
            base.SetDefaults();
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Player player = Main.player[Projectile.owner];
            float point = 0f;
            Vector2 endPoint = player.Center + Projectile.velocity.Length() * Projectile.ai[1].ToRotationVector2();
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.Center, endPoint, Projectile.velocity.Length() * Projectile.localAI[1], ref point);
        }
        bool CanBlock(Projectile target)
        {
            Rectangle targetHitbox = target.Hitbox;
            Player player = Main.player[Projectile.owner];
            float point = 0f;
            Vector2 endPoint = player.Center + Projectile.velocity.Length() * Projectile.ai[1].ToRotationVector2();
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.Center, endPoint, Projectile.velocity.Length() * Projectile.localAI[1], ref point);
        }

        public override void AI()
        {
            Vector2 Directions = Projectile.rotation.ToRotationVector2(); Directions.Y *= Projectile.localAI[1]; Directions = Directions.RotatedBy(Projectile.ai[1]);
            float RealDirection = Directions.ToRotation();
            Main.player[Projectile.owner].itemRotation = RealDirection;
            if (Projectile.timeLeft > 40)
            {
                float r = MathHelper.Lerp(0.14f, 0, 1 - (Projectile.timeLeft - 40) / 60f);//角度增加渐变
                if (!Reverse)
                {
                    Projectile.rotation += r;
                }
                else
                {
                    Projectile.rotation -= r;
                }
            }
            if (!IsNPCproj)
            {
                Projectile.friendly = true;
                Projectile.DamageType = DamageClass.Melee;
            }
            if (Projectile.timeLeft == 60 && !IsNPCproj)
            {
                bool G = false;
                foreach (var proj in Main.projectile)
                {
                    if (proj.hostile && proj.damage > 0 && CanBlock(proj) && proj.active)
                    {
                        proj.Kill(); G = true;
                        Projectile.NewProjectile(null, proj.position, Vector2.Zero, ProjectileID.DaybreakExplosion, 0, 0, 0);
                    }
                }
                Player player = Main.player[Projectile.owner];
                if (ShouldDoNextSlash >= 1)
                {
                    float rd = Main.rand.NextFloat(-0.4f, 0.4f);
                    HeroSlashMethod.Slash(true, Projectile.GetSource_FromAI(), (Main.MouseWorld - player.Center).ToRotation(), Reverse ? -1.9f + rd : 1.9f + rd, 353, 0.45f, 0.35f, 5, Projectile.damage, 5, player.whoAmI, 0, c, t, ShouldDoNextSlash - 1);
                }

            }
            base.AI();
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 Directions = Projectile.rotation.ToRotationVector2(); Directions.Y *= Projectile.localAI[1]; Directions = Directions.RotatedBy(Projectile.ai[1]);
            float RealDirection = Directions.ToRotation();
            Vector2 ownerPos = Vector2.Zero;
            if (IsNPCproj)
            {
                ownerPos = Main.npc[(int)Projectile.ai[0]].Center - Main.screenPosition;
            }
            else
            {
                ownerPos = Main.player[Projectile.owner].Center - Main.screenPosition;
            }
            List<VertexInfo2> slash = new List<VertexInfo2>();
            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                if (Projectile.oldRot[i] != 0)
                {
                    Vector2 pos0 = Projectile.oldRot[i].ToRotationVector2() * Projectile.velocity.Length();//圆周半径
                    pos0.Y *= Projectile.localAI[1];
                    pos0 = pos0.RotatedBy(Projectile.ai[1]);
                    slash.Add(new VertexInfo2(ownerPos + pos0, new Vector3(1 - i / 40f, 0, 1), c * (1 - i / 40f)));
                    Vector2 pos1 = Projectile.oldRot[i].ToRotationVector2() * Projectile.velocity.Length() * (1 - Projectile.localAI[0] + Projectile.localAI[0] * i / 40f);//圆周半径
                    pos1.Y *= Projectile.localAI[1];
                    pos1 = pos1.RotatedBy(Projectile.ai[1]);
                    slash.Add(new VertexInfo2(ownerPos + pos1, new Vector3(1 - i / 40f, 1, 1), c * (1 - i / 40f)));
                }
            }
            if (t != null)
            {

                if (Reverse && Projectile.ai[1] > -MathHelper.Pi / 2f && Projectile.ai[1] < MathHelper.Pi / 2f)
                    Main.EntitySpriteDraw(t, ownerPos, null, Color.White, RealDirection + 0.785f + 1.5707f, new Vector2(t.Width, t.Height), Scale1 / (t.Size().Length()), SpriteEffects.FlipHorizontally, 0);
                if (!Reverse && Projectile.ai[1] > -MathHelper.Pi / 2f && Projectile.ai[1] < MathHelper.Pi / 2f)
                    Main.EntitySpriteDraw(t, ownerPos, null, Color.White, RealDirection + 0.785f, new Vector2(0, t.Height), Scale1 / t.Size().Length(), SpriteEffects.None, 0);
                else if (Reverse && Projectile.ai[1] <= -MathHelper.Pi / 2f || Projectile.ai[1] >= MathHelper.Pi / 2f)
                    Main.EntitySpriteDraw(t, ownerPos, null, Color.White, RealDirection + 0.785f + 1.5707f, new Vector2(t.Width, t.Height), Scale1 / t.Size().Length(), SpriteEffects.FlipHorizontally, 0);
                else if (!Reverse && Projectile.ai[1] <= -MathHelper.Pi / 2f || Projectile.ai[1] >= MathHelper.Pi / 2f)
                    Main.EntitySpriteDraw(t, ownerPos, null, Color.White, RealDirection + 0.785f, new Vector2(0, t.Height), Scale1 / t.Size().Length(), SpriteEffects.None, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            Main.graphics.GraphicsDevice.Textures[1] = t;
            SlashEff.CurrentTechnique.Passes[0].Apply();
            Main.graphics.GraphicsDevice.Textures[0] = TextureAssets.Projectile[Projectile.type].Value;

            if (slash.Count >= 3)
            {
                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, slash.ToArray(), 0, slash.Count - 2);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }
    }
    public class HeroSlash2 : ModProjectile
    {
        Effect SlashEff = ModContent.Request<Effect>("HeroRegression/Effects/BigTentacle").Value;
        public bool Reverse = false; public bool IsNPCproj = false; public Color c = Color.White;
        public Texture2D T = null;

        public override string Texture => "Terraria/Images/Extra_201";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 40;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 24;
            Projectile.timeLeft = 100;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -11;
            base.SetDefaults();
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        public override void AI()
        {
            if (Projectile.timeLeft > 40)
            {
                float r = MathHelper.Lerp(0.14f, 0, 1 - (Projectile.timeLeft - 40) / 60f);
                if (!Reverse)
                {
                    Projectile.rotation += r;
                }
                else
                {
                    Projectile.rotation -= r;
                }
            }

            base.AI();
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 ownerPos = Vector2.Zero;
            if (IsNPCproj)
            {
                ownerPos = Main.npc[(int)Projectile.ai[0]].Center - Main.screenPosition;
            }
            else
            {
                ownerPos = Main.player[Projectile.owner].Center - Main.screenPosition;
            }
            List<VertexInfo2> slash = new List<VertexInfo2>();
            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                if (Projectile.oldRot[i] != 0)
                {
                    Vector2 pos0 = Projectile.oldRot[i].ToRotationVector2() * Projectile.velocity.Length();//圆周半径
                    pos0.Y *= Projectile.localAI[1];
                    pos0 = pos0.RotatedBy(Projectile.ai[1]);
                    slash.Add(new VertexInfo2(ownerPos + pos0, new Vector3(1 - i / 40f, 0, 1), Color.White));
                    Vector2 pos1 = Projectile.oldRot[i].ToRotationVector2() * Projectile.velocity.Length() * (1 - Projectile.localAI[0] + Projectile.localAI[0] * i / 40f);//圆周半径
                    pos1.Y *= Projectile.localAI[1];
                    pos1 = pos1.RotatedBy(Projectile.ai[1]);
                    slash.Add(new VertexInfo2(ownerPos + pos1, new Vector3(1 - i / 40f, 1, 1), Color.White));
                }
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            Main.graphics.GraphicsDevice.Textures[1] = T;
            SlashEff.CurrentTechnique.Passes[0].Apply();
            Main.graphics.GraphicsDevice.Textures[0] = TextureAssets.Projectile[Projectile.type].Value;
            if (slash.Count >= 3)
            {
                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, slash.ToArray(), 0, slash.Count - 2);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }
    }
    public class HeroSlashMethod2 : ModProjectile
    {
        Effect SlashEff = ModContent.Request<Microsoft.Xna.Framework.Graphics.Effect>("HeroRegression/Effects/BigTentacle").Value;
        /// <summary>
        /// ·Startingrot决定这个东西挥刀的起始点，以水平向右为基准啊，别搞错了<br />
        /// ·rotation决定这玩意往哪个方向挥刀。<br />
        /// ·Length决定这个东西能在那个方向上挥出多远<br />
        /// ·thick决定这个挥刀的刀光宽度，纯纯的视觉参数罢了,0~1自己调<br />
        /// ·Yscale决定这个斩击有多扁，控制在0~1就行，别整多了<br />
        /// ·owner是isplayerproj真的时候存玩家whoami，ownernpc是为假时存的NPC.whoami<br />
        /// ·weapontex指的是这个刀光的挥舞武器是什么材质，记住要是剑柄在左下角奥<br />
        /// ·ShouldDoNextSlash指的是连击数，至少为1才会有连击<br />
        /// </summary>
        public static void NPCslash(IEntitySource source, float rotation, float Startingrot, float Length, float Thick, float YScale, int ExtraSpeed = 0, int damage = 50, float Knockback = 0, int owner = 0, int ownerNPC = 0, Color color = default, Texture2D weaponTex = default,int TimeLeft = 100, float KnockBackRotation = 0, float WeaponScale = 1)
        {
            
                var p = Projectile.NewProjectileDirect(source, Main.npc[ownerNPC].Center, Vector2.UnitX * Length, ModContent.ProjectileType<HeroSlashMethod2>(), damage, Knockback, 0, ownerNPC, rotation);
                p.rotation = Startingrot;
                (p.ModProjectile as HeroSlashMethod2).Reverse = Startingrot > 0; (p.ModProjectile as HeroSlashMethod2).IsNPCproj = true;
                p.localAI[0] = Thick; p.localAI[1] = YScale; if (color != default) (p.ModProjectile as HeroSlashMethod2).c = color;
                p.extraUpdates = ExtraSpeed;
                p.timeLeft = TimeLeft;
            (p.ModProjectile as HeroSlashMethod2).Scale1 = WeaponScale;
            (p.ModProjectile as HeroSlashMethod2).t = weaponTex;
        }
        //ai0决定主人Npc,ai1决定方向，速度长度决定斩击范围，localai0决定斩击厚度，localai1决定斩击扁度，自定义变量决定斩击正反位
        public bool Reverse = false; public bool IsNPCproj = false; public Color c = Color.White; public Texture2D t = null; public int ShouldDoNextSlash = 0; public float Scale1 = 1;
        public override string Texture => "Terraria/Images/Extra_193";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 40;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 24;
            Projectile.timeLeft = 100;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -11;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 1000;
            Projectile.noEnchantmentVisuals = true;
            base.SetDefaults();
        }
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float point = 0f;
            Vector2 endPoint = Projectile.Center + Projectile.velocity.Length() * Projectile.ai[1].ToRotationVector2();
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, endPoint, Projectile.velocity.Length() * Projectile.localAI[1], ref point);
        }

        public override void AI()
        {
            Projectile.Center = Main.npc[(int)Projectile.ai[0]].Center;
            if (Projectile.timeLeft > 40)
            {
                float r = 0.14f;//角度增加渐变
                if (!Reverse)
                {
                    Projectile.rotation += r;
                }
                else
                {
                    Projectile.rotation -= r;
                }
            }
            base.AI();
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 Directions = Projectile.rotation.ToRotationVector2(); Directions.Y *= Projectile.localAI[1]; Directions = Directions.RotatedBy(Projectile.ai[1]);
            float RealDirection = Directions.ToRotation();
            Vector2
          
                ownerPos = Main.npc[(int)Projectile.ai[0]].Center - Main.screenPosition;
      
            List<VertexInfo2> slash = new List<VertexInfo2>();
            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                if (Projectile.oldRot[i] != 0)
                {
                    Vector2 pos0 = Projectile.oldRot[i].ToRotationVector2() * Projectile.velocity.Length();//圆周半径
                    pos0.Y *= Projectile.localAI[1];
                    pos0 = pos0.RotatedBy(Projectile.ai[1]);
                    slash.Add(new VertexInfo2(ownerPos + pos0, new Vector3(1 - i / 40f, 0, 1), c * (1 - i / 40f)));
                    Vector2 pos1 = Projectile.oldRot[i].ToRotationVector2() * Projectile.velocity.Length() * (1 - Projectile.localAI[0] + Projectile.localAI[0] * i / 40f);//圆周半径
                    pos1.Y *= Projectile.localAI[1];
                    pos1 = pos1.RotatedBy(Projectile.ai[1]);
                    slash.Add(new VertexInfo2(ownerPos + pos1, new Vector3(1 - i / 40f, 1, 1), c * (1 - i / 40f)));
                }
            }

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            Main.graphics.GraphicsDevice.Textures[1] = t;
          //  SlashEff.CurrentTechnique.Passes[0].Apply();
            Main.graphics.GraphicsDevice.Textures[0] = TextureAssets.Projectile[Projectile.type].Value;

            if (slash.Count >= 3)
            {
                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, slash.ToArray(), 0, slash.Count - 2);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            if (t != null)
            {
                if (Reverse && Projectile.ai[1] > -MathHelper.Pi / 2f && Projectile.ai[1] < MathHelper.Pi / 2f)
                    Main.EntitySpriteDraw(t, ownerPos, null, Color.White, RealDirection + 0.785f + 1.5707f, new Vector2(t.Width, t.Height), Scale1, SpriteEffects.FlipHorizontally, 0);
                if (!Reverse && Projectile.ai[1] > -MathHelper.Pi / 2f && Projectile.ai[1] < MathHelper.Pi / 2f)
                    Main.EntitySpriteDraw(t, ownerPos, null, Color.White, RealDirection + 0.785f, new Vector2(0, t.Height), Scale1, SpriteEffects.None, 0);
                else if (Reverse && Projectile.ai[1] <= -MathHelper.Pi / 2f || Projectile.ai[1] >= MathHelper.Pi / 2f)
                    Main.EntitySpriteDraw(t, ownerPos, null, Color.White, RealDirection + 0.785f + 1.5707f, new Vector2(t.Width, t.Height), Scale1, SpriteEffects.FlipHorizontally, 0);
                else if (!Reverse && Projectile.ai[1] <= -MathHelper.Pi / 2f || Projectile.ai[1] >= MathHelper.Pi / 2f)
                    Main.EntitySpriteDraw(t, ownerPos, null, Color.White, RealDirection + 0.785f, new Vector2(0, t.Height), Scale1, SpriteEffects.None, 0);
            }
            return false;
        }
    }
}
