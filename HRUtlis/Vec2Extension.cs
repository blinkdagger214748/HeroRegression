using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;

namespace HeroRegression.HRUtils
{
    public static class ExtensionVec2
    {
        public static bool IsOnScreen(this Vector2 position, int extraWidth = 0, int extraHeight = 0)
        {
            return position.X > -extraWidth && position.X < Main.screenWidth + extraWidth && position.Y > -extraHeight && position.Y < Main.screenHeight + extraHeight;
        }
        public static bool IsWorldOnScreen(this Vector2 position, int extraWidth = 0, int extraHeight = 0)
        {
            return position.X - extraWidth > Main.screenPosition.X &&
                position.X + extraWidth < Main.screenPosition.X + Main.screenWidth &&
                position.Y - extraWidth > Main.screenPosition.Y &&
                position.Y + extraHeight < Main.screenPosition.Y + Main.screenHeight;
        }
        public static Vector2 SNormalize(Vector2 vec2)
        {
            return Utils.SafeNormalize(vec2, Vector2.Zero);
        }
        public static Vector2 RestrictedVec2(Vector2 origVec2, float maxLength, float minLength = 0)
        {
            float length = origVec2.Length();
            Vector2 normalized = SNormalize(origVec2);
            if (length > maxLength) length = maxLength;
            if (length < minLength) length = minLength;
            return normalized * length;
        }
    }
}
