using HeroRegression.NPCs.Boss.SeedsOfOrigin;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;


namespace HeroRegression.HeroPlayers
{
    public class ScreenPositionPlayer : ModPlayer
    {
        public bool SeedsOfOrigin;
        public bool ScreenLock;
        public float screengo = 0;


        public override void Initialize()
        {
            SeedsOfOrigin = false;
        }

        public override void ResetEffects()
        {
            SeedsOfOrigin = false;
        }

        public override void UpdateDead()
        {
            SeedsOfOrigin = false;
            ScreenLock = false;
            screengo = 0;
        }

        public override void ModifyScreenPosition()
        {
            if (SeedsOfOrigin)
            {
                Main.screenPosition.X += Main.rand.Next(-10, 10);
                Main.screenPosition.Y += Main.rand.Next(-10, 10);
            }
            if (ScreenLock)
            {
                foreach (var npc in Main.npc)
                {
                    if (npc.type == ModContent.NPCType<SeedsOfOrigin>() && npc.active)
                    {
                        Main.screenPosition = Vector2.Lerp(Main.screenPosition, npc.Center - new Vector2(Main.screenWidth / 2, Main.screenHeight / 2), screengo / 50f);
                    }
                }
            }
        }
    }
}