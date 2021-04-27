using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace BetterFishing.Projectiles
{
    public class BobberPurple : Bobber
    {
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.BobberGolden);
        }

        public override Color GetLineColor(Vector2 position)
        {
            return Lighting.GetColor((int)position.X / 16, (int)(position.Y / 16), new Color(148, 0, 211, 100));
        }
    }
}