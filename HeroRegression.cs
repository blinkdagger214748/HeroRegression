global using Terraria.Audio;
using HeroRegression.Skies;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace HeroRegression
{
    public class HeroRegression : Mod
    {
        public static HeroRegression Instance;

        public override void Load()
        {
            // 加载
            SkyManager.Instance["PupilBoss"] = new PupilSky();
        }
        public static Texture2D GetTex(string path)
        {
            return ModContent.Request<Texture2D>(path).Value;
        }



        private bool IsNull(object obj)
        {
            if (obj == null)
            {
                return true;
            }
            return false;
        }
    }

}