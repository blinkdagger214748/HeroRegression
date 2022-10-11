using HeroRegression.NPCs.Boss.SeedsOfOrigin;
using Terraria;
using Terraria.ModLoader;

namespace HeroRegression
{
    public class KingCrimson
    {
        internal static bool IsActive = false;

        // 碎块加载
        internal static void UpdateGore(On.Terraria.Gore.orig_Update orig, Gore self)
        {
            // 当跳过时间时 快速执行碎块的事件
            if (IsActive)
            {
                for (int t = 0; t < 120; t++)
                {
                    orig.Invoke(self);
                }
            }
            // 保持事件的正常运行
            orig.Invoke(self);
        }

        // 物品加载
        internal static void ItemUpdateHook(On.Terraria.Item.orig_UpdateItem orig, Item self, int i)
        {
            // 当跳过时间时 快速执行物品的事件
            if (IsActive)
            {
                for (int t = 0; t < 120; t++)
                {
                    orig.Invoke(self, i);
                }
            }
            // 保持事件的正常运行
            orig.Invoke(self, i);
        }

        // 弹幕加载
        internal static void ProjectileUpdateHook(On.Terraria.Projectile.orig_Update orig, Projectile self, int i)
        {
            // 当跳过时间时 快速执行弹幕的事件
            if (IsActive)
            {
                for (int t = 0; t < 120; t++)
                {
                    orig.Invoke(self, i);
                }
            }
            // 保持事件的正常运行
            orig.Invoke(self, i);
        }

        // NPC加载
        internal static void NPCUpdateHook(On.Terraria.NPC.orig_UpdateNPC orig, NPC self, int i)
        {
            // 当跳过时间时 快速执行NPC的事件
            if (IsActive)
            {
                for (int t = 0; t < 120; t++)
                {
                    orig.Invoke(self, i);
                }
            }
            // 保持事件的正常运行
            orig.Invoke(self, i);
        }

        // 粒子加载
        internal static void DustUpdateHook(On.Terraria.Dust.orig_UpdateDust orig)
        {
            // 当跳过时间时 快速执行粒子的事件
            if (IsActive)
            {
                for (int t = 0; t < 120; t++)
                {
                    orig.Invoke();
                }
            }
            // 保持事件的正常运行
            orig.Invoke();
        }

        // 玩家加载
        internal static void PlayerUpdateHook(On.Terraria.Player.orig_Update orig, Player self, int i)
        {
            // 当跳过时间时 快速执行玩家的事件
            if (IsActive)
            {
                for (int t = 0; t < 120; t++)
                {
                    orig.Invoke(self, i);
                }
            }
            // 保持事件的正常运行
            orig.Invoke(self, i);
        }
    }
}