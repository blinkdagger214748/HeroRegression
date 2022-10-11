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
using HeroRegression.Projectiles.Accessories;

namespace HeroRegression.HeroPlayers
{
    class HRPlr : ModPlayer
    {
        public bool OverloadedEnergy = false;

        public int ProjCoolingTime;

        public int SuckBloodCoolingTime;
     


        public override void ResetEffects()
        {
            OverloadedEnergy = false;
            if (ProjCoolingTime > 0)
            {
                ProjCoolingTime--;
            }
            if (SuckBloodCoolingTime > 0)
            {
                SuckBloodCoolingTime--;
            }
        }

        public override void Initialize()
        {
            ProjCoolingTime = 0;
            OverloadedEnergy = false;
            SuckBloodCoolingTime = 0;
        }

        public override void UpdateDead()
        {
            OverloadedEnergy = false;
            ProjCoolingTime = 0;
            SuckBloodCoolingTime = 0;
        }

        public override void ModifyHitNPC(Item Item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (OverloadedEnergy)
            {
                if (crit)
                {
                    target.AddBuff(BuffID.Poisoned, 240);
                    if (Main.rand.Next(1, 3) == 1)
                    {
                        if (SuckBloodCoolingTime <= 0)
                        {
                            SuckBloodCoolingTime = 6 * 60;
                            Player.statLife += 10;
                        }
                        if (ProjCoolingTime <= 0)
                        {
                            ProjCoolingTime = 3 * 60;
                            Projectile.NewProjectile(null, Player.Center, Vector2.Normalize(Main.MouseWorld - Main.LocalPlayer.Center) * 18f, ModContent.ProjectileType<PoisonProj>(), 9, 0.3f, Player.whoAmI);
                        }
                    }
                }
            }
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (OverloadedEnergy)
            {
                if (crit)
                {
                    target.AddBuff(BuffID.Poisoned, 240);
                    if (Main.rand.Next(1, 3) == 1)
                    {
                        if (SuckBloodCoolingTime <= 0)
                        {
                            SuckBloodCoolingTime = 6 * 60;
                            Player.statLife += 10;
                        }
                        if ((ProjCoolingTime <= 0) && (proj.type != ModContent.ProjectileType<PoisonProj>()))
                        {
                            ProjCoolingTime = 3 * 60;
                            Projectile.NewProjectile(null,Player.Center, Vector2.Normalize(Main.MouseWorld - Main.LocalPlayer.Center) * 18f, ModContent.ProjectileType<PoisonProj>(), 9, 0.3f, Player.whoAmI);
                        }
                    }
                }
            }
        }
        public override void OnEnterWorld(Player player)
        {
            Main.NewText("当前英殇版本v0.0.0.7，祝您游戏愉快！" , Color.Blue);
            if (HeroRegressionWorld.OriginF != true && !NPC.AnyNPCs(ModContent.NPCType<NPCs.TownNPC.Originaladventurer>()))
            {
               
                NPC.NewNPC(null,(int)player.Center.X, (int)player.Center.Y, ModContent.NPCType<NPCs.TownNPC.Originaladventurer>());
            }
            HeroRegressionWorld.OriginF = true;
        }
    }
}

