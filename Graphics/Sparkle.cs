using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroRegression.Graphics
{
    public class Sparkle : Particle
    {
        public override string TexturePath => "HeroRegression/Extra/Particles/Sparkle";
        public float Radius = 10;
        public Vector2 MaxScale;
        public override void OnSpawn()
        {
            Frame = new Rectangle(0, 0, 33, 33);
            IsAdditive = true;
            Rotation = Main.rand.NextFloat(MathHelper.TwoPi);
            Alpha = 1f;
            MaxScale = Scale;
            Origin = Texture.Size() / 2f;
            Color = Color.LightBlue;
            Radius = 33f;
        }
        public override void Update()
        {
            Velocity *= .9f;
            Scale -= MaxScale / 60f;
            Alpha -= 0.0166f;
            Lighting.AddLight(Position, Color.ToVector3() / 255f * Alpha);
            if (Alpha <= 0f) Kill();
        }
        public override void Draw()
        {
            Main.EntitySpriteDraw(Texture, Position - Main.screenPosition, Frame, Color * Alpha, Rotation, new Vector2(Radius / 33f), Scale, Effects, 0);
        }
    }
}
