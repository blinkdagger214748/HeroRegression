using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;


namespace HeroRegression.Skies
{
    public class PupilSky : CustomSky
    {
        private bool isActive = false;
        private float intensity = 0f;
        private float lifeIntensity = 0f;
        private int delay = 0;
        private int[] xPos = new int[50];
        private int[] yPos = new int[50];
        
        private bool increase = true;

        public override void Update(GameTime gameTime)
        {
            if (increase)
            {
                float increment = 0.04f;

                intensity += increment;
                if (intensity > 1f)
                {
                    intensity = 1f;
                    increase = false;
                }
            }
            else
            {
                float increment = 0.01f;

                intensity -= increment;
                if (intensity < 0f)
                {
                    intensity = 0f;
                    increase = true;
                    Deactivate();
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {   
            if (maxDepth >= 0 && minDepth < 0)
            {

                        spriteBatch.Draw(HeroRegression.GetTex("HeroRegression/Skies/DemonSky"),
                            new Rectangle(0, 0, Main.screenWidth, Main.screenHeight),Color.OrangeRed * 0.4f);
            }
        }

        public override float GetCloudAlpha()
        {
            return 1f - intensity;
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            isActive = true;
        }

        public override void Deactivate(params object[] args)
        {
            isActive = false;
        }

        public override void Reset()
        {
            isActive = false;
        }

        public override bool IsActive()
        {
            return isActive;
        }


    }
}