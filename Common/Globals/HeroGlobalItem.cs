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
using HeroRegression.Items.Material;

namespace HeroRegression.NPCs
{

    class HeroGlobalItem : GlobalItem
    {
        public override void OpenVanillaBag(string context, Player player, int arg)
        {
            if (context == "bossBag")
            {
                switch(arg)
                {
                    case (3319):
                        {
                            player.QuickSpawnItem(player.GetSource_OpenItem(arg), ModContent.ItemType<恶性眼瘤>(), 1);
                            return;
                        }
                }
             
            }

        }
    }
}

          
           
       
