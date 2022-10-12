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

namespace HeroRegression.Projectiles
{
    class OriginNailFriend : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.light = .5f;
            Projectile.hostile = false;
            Projectile.timeLeft = 600;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = 1;
            Item s = new Item();
         
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Dust D = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.GemEmerald);
            NPC target = null;
            float distanceMax = 500;
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly && npc.type != NPCID.TargetDummy)
                {
                    Projectile.tileCollide = false;
                    float currentDistance = Vector2.Distance(npc.Center, Projectile.Center);
                    if (currentDistance < distanceMax)
                    {
                        distanceMax = currentDistance;
                        target = npc;
                    }
                }
            }
            if (target != null)
            {

                Vector2 targetVec = target.Center - Projectile.Center;
                targetVec.Normalize();
                targetVec *= ((int)Projectile.ai[0] == 1) ? 30f : 20f;
                Projectile.velocity = (Projectile.velocity * 30f + targetVec) / 31f;
            }
            else
            {


            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D SO = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;


            Main.spriteBatch.Draw(SO, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation, new Vector2(10, 5), 1, SpriteEffects.None, 0f);


            return false;
        }
    }
}
