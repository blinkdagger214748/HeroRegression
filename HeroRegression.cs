global using Terraria.Audio;
global using HeroRegression.Common.BaseClasses.BaseSummon;
global using HeroRegression.Common.BaseClasses.BaseWeapon;
global using HeroRegression.Common.BaseClasses.BaseProj;
global using HeroRegression.Common.BaseClasses.BaseLoot;
global using HeroRegression.Common.Systems;
global using HeroRegression.Projectiles.Friendly;
global using HeroRegression.Items.Material;
global using HeroRegression.HeroPlayers;
global using HeroRegression.Tiles.Relics;
global using HeroRegression.Items.Placeable.Relic;
global using HeroRegression.HRUtils;
global using HeroRegression.Graphics;
global using Microsoft.Xna.Framework.Graphics;
global using Terraria.ID;
global using Terraria.ModLoader;
global using Terraria.DataStructures;
global using Terraria.ObjectData;
global using Microsoft.Xna.Framework;
global using Terraria;
global using static HeroRegression.HRHelper;
global using Terraria.Localization;
global using Terraria.GameContent;
global using System;
global using System.Collections.Generic;
using HeroRegression.Skies;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using System.IO;


namespace HeroRegression
{
    public class HeroRegression : Mod
    {
        public static HeroRegression Instance;
        public static float GameTimeNoPause;
        public override void Load()
        {
            // 加载
            SkyManager.Instance["PupilBoss"] = new PupilSky();
        }
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            MsgTypeSystem PacketType = (MsgTypeSystem)reader.ReadByte();
            switch (PacketType)
            {
                /*
                 * 这个数据包会在玩家使用任何一个继承了SummonItem的物品时，被服务器接收。
                 * 这里我们从数据包读取玩家的index、boss种类和生成位置的偏移。
                 * 然后用NPC.SpawnBoss来生成boss，记得在服务器端运行，且把对应玩家的位置加上偏移量。
                 */
                case MsgTypeSystem.SummonBoss:
                    {
                        int index = reader.ReadInt32();
                        int type = reader.ReadInt32();
                        Vector2 Offset = reader.ReadVector2();
                        Player plr = Main.player[index];
                        if (Main.netMode == NetmodeID.Server)
                        {
                            NPC.SpawnBoss((int)(plr.Center.X + Offset.X), (int)(plr.Center.Y + Offset.Y), type, index);
                        }
                        break;
                    }
            }
        }
    }
    public static class HRHelper
    {
        public static Texture2D GetTex(string path)
        {
            return ModContent.Request<Texture2D>(path).Value;
        }
        public static bool IsNull(object obj)
        {
            if (obj == null)
            {
                return true;
            }
            return false;
        }
        public static string ChnTrans(string Eng, string Chn)
        {
            return Language.ActiveCulture.Name == "zh-Hans" ? Chn : Eng;
        }
        public static BaseBossLoot GetLootNPC(NPC npc)
        {
            return npc.GetGlobalNPC<BaseBossLoot>();
        }
        public static string BlankTexture => "HeroRegression/Textures/blank";
    }
}