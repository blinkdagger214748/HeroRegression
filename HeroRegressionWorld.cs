using System.Collections.Generic;
using Terraria;
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

namespace HeroRegression
{

    public class HeroRegressionWorld : ModSystem
    {
        public static bool downedSeedsOfOrigin;
        public static bool downedFlameReaction;
        public static bool downedBoss2 = NPC.downedBoss2;
        public static bool OriginF;
        public static bool LCSM;//灵翠石
        public override void PostUpdateEverything()
        {
            base.PostUpdateEverything();
        }/*
        public override void PostUpdate()
        {
            if (NPC.downedBoss2 && !LCSM)
            {
                Main.NewText("灵翠古物已然降临你的世界", 50, 184, 191, false);
                for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 0.001); k++)
                {
                 
                    int i = WorldGen.genRand.Next(0, Main.maxTilesX);
                 
                    int j = WorldGen.genRand.Next((int)(Main.maxTilesY * 0.4f), (int)(Main.maxTilesY * 0.7f));
                    WorldGen.OreRunner(i, j, 3, WorldGen.genRand.Next(8, 16), (ushort)mod.TileType("灵翠石"));
     int K = WorldGen.genRand.Next((int)(Main.maxTilesY * 0.4f), (int)(Main.maxTilesY * 0.7f));
                    WorldGen.OreRunner(i, K, 3, WorldGen.genRand.Next(8, 16), (ushort)mod.TileType("古木"));
                }
                LCSM = true;
            }
        }*/
        public override void SaveWorldData(TagCompound tag)
        {
            base.SaveWorldData(tag);
        }
     /*   public override TagCompound Save()
        {
         return new TagCompound{
        {"LCSM", LCSM},
            {"downedFlameReaction ", downedFlameReaction},
        {"downedSeedsOfOrigin", downedSeedsOfOrigin}
         };
            
         }*//*
        public override void Load(TagCompound tag)
        {


            LCSM = tag.GetBool("LCSM");
            downedFlameReaction = tag.GetBool("downedFlameReaction");
            downedSeedsOfOrigin = tag.GetBool("downedSeedsOfOrigin");
        }*/
        /*
        public override void Initialize()
        {
            LCSM = false;
        }*/
    }
}
