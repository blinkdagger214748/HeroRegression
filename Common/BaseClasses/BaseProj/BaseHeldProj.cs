namespace HeroRegression.Common.BaseClasses.BaseProj
{
    public abstract class BaseHeldProj : FriendlyProj
    {
        public void HeldProjDefaults(int timeLeft, int extraUpdate)
        {
            Defaults(1, 1, timeLeft, -1, false, extraUpdate, 0, 1, 1, false, false, true);
        }
        public bool CheckOwner(out Player owner, bool followOwner = true)
        {
            Player player = Main.player[Projectile.owner];
            if (player.active && !player.dead && player.channel)
            {
                if (followOwner) Projectile.Center = player.Center;
                player.heldProj = Projectile.whoAmI;
                player.controlUseItem = true;
                player.itemTime = 2;
                player.itemAnimation = 2;
                Projectile.ownerHitCheck = true;
                owner = player;
                return true;
            }
            else
            {
                owner = null;
                return false;
            }
        }
    }
}
