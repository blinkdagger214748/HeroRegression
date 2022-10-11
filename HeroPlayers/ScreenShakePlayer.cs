using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace HeroRegression.HeroPlayers
{
    public class ScreenShakePlayer : ModPlayer
    {
        public float ScreenShakeTimer;
        public float ShakeTime;
        public float ShakeAmplitude;
        public bool SShake;
        public override void UpdateLifeRegen()
        {
            if (SShake) ScreenShakeTimer++;
            else ScreenShakeTimer = 0;
        }
        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            ScreenShakeTimer = 0;
        }
        public override void OnRespawn(Player player)
        {
            ScreenShakeTimer = 0;
        }
        public void ScreenShake(float totalTime, float maxAmplitude)
        {
            ShakeTime = totalTime;
            ShakeAmplitude = maxAmplitude;
            SShake = true;
        }
        public void Shake(float totalTime, float maxAmplitude)
        {
            if (SShake)
            {
                float progress = ScreenShakeTimer / totalTime;
                float radius = maxAmplitude * (float)Math.Sin(progress * MathHelper.Pi);
                Main.screenPosition += Main.rand.NextVector2Circular(radius, radius);
            }
            if (ScreenShakeTimer >= totalTime)
            {
                ShakeTime = 0;
                ShakeAmplitude = 0;
                SShake = false;
            }
        }
        public override void ModifyScreenPosition()
        {
            if (SShake) Shake(ShakeTime, ShakeAmplitude);
        }
    }
}
