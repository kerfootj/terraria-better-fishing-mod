using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BetterFishing.Projectiles
{
    public class Bobber : ModProjectile
    {

        /**
         * Return the color of the fishing line
         *  - defaults to silver
         */
        public virtual Color GetLineColor(Vector2 position)
        {
            return Lighting.GetColor((int)(position.X / 16), (int)(position.Y / 16), new Color(192, 192, 192));
        }

        /**
         * Attach line to rod based on the size of the sprite
         */
        public virtual void AttachLineToRod(float gravDir, ref float x, ref float y)
        {
            x += 43 * Main.player[base.projectile.owner].direction;

            if (Main.player[base.projectile.owner].direction < 0)
            {
                x -= 13f;
            }

            y -= 31f * gravDir;
        }

        /**
         * Draw the line attached to the tip of the fishing rod
         */
        public override bool PreDrawExtras(SpriteBatch spriteBatch)
        {
            Lighting.AddLight(projectile.Center, 0.9f, 0.4f, 0.4f);

            Player player = Main.player[projectile.owner];
            Item selectedItem = player.inventory[player.selectedItem];

            if (projectile.bobber && selectedItem.holdStyle > 0)
            {
                float posX = player.MountedCenter.X;
                float posY = player.MountedCenter.Y;
                posY += player.gfxOffY;

                float gravDir = player.gravDir;

                AttachLineToRod(gravDir, ref posX, ref posY);

                // Calculate the line in air 
                Vector2 position = new Vector2(posX, posY);
                position = player.RotatedRelativePoint(position + new Vector2(8f), true) - new Vector2(8f);

                float projPosX = projectile.position.X + (float)projectile.width * 0.5f - position.X;
                float projPosY = projectile.position.Y + (float)projectile.height * 0.5f - position.Y;

                // A bunch of decompiled code, don't look too close
                bool flag2 = true;
                if (projPosX == 0f && projPosY == 0f)
                {
                    flag2 = false;
                }
                else
                {
                    float projPosXY = (float)Math.Sqrt((double)(projPosX * projPosX + projPosY * projPosY));
                    projPosXY = 12f / projPosXY;
                    projPosX *= projPosXY;
                    projPosY *= projPosXY;
                    position.X -= projPosX;
                    position.Y -= projPosY;
                    projPosX = projectile.position.X + (float)projectile.width * 0.5f - position.X;
                    projPosY = projectile.position.Y + (float)projectile.height * 0.5f - position.Y;
                }
                while (flag2)
                {
                    float num = 12f;
                    float num2 = (float)Math.Sqrt((double)(projPosX * projPosX + projPosY * projPosY));
                    float num3 = num2;
                    if (float.IsNaN(num2) || float.IsNaN(num3))
                    {
                        break;
                    }
                    else
                    {
                        if (num2 < 20f)
                        {
                            num = num2 - 8f;
                            flag2 = false;
                        }
                        num2 = 12f / num2;
                        projPosX *= num2;
                        projPosY *= num2;
                        position.X += projPosX;
                        position.Y += projPosY;
                        projPosX = projectile.position.X + (float)projectile.width * 0.5f - position.X;
                        projPosY = projectile.position.Y + (float)projectile.height * 0.1f - position.Y;
                        if (num3 > 12f)
                        {
                            float num4 = 0.3f;
                            float num5 = Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y);
                            if (num5 > 16f)
                            {
                                num5 = 16f;
                            }
                            num5 = 1f - num5 / 16f;
                            num4 *= num5;
                            num5 = num3 / 80f;
                            if (num5 > 1f)
                            {
                                num5 = 1f;
                            }
                            num4 *= num5;
                            if (num4 < 0f)
                            {
                                num4 = 0f;
                            }
                            num5 = 1f - projectile.localAI[0] / 100f;
                            num4 *= num5;
                            if (projPosY > 0f)
                            {
                                projPosY *= 1f + num4;
                                projPosX *= 1f - num4;
                            }
                            else
                            {
                                num5 = Math.Abs(projectile.velocity.X) / 3f;
                                if (num5 > 1f)
                                {
                                    num5 = 1f;
                                }
                                num5 -= 0.5f;
                                num4 *= num5;
                                if (num4 > 0f)
                                {
                                    num4 *= 2f;
                                }
                                projPosY *= 1f + num4;
                                projPosX *= 1f - num4;
                            }
                        }
                        float rotation2 = (float)Math.Atan2((double)projPosY, (double)projPosX) - 1.57f;

                        Main.spriteBatch.Draw(
                            Main.fishingLineTexture, 
                            new Vector2(position.X - Main.screenPosition.X + (float)Main.fishingLineTexture.Width * 0.5f, position.Y - Main.screenPosition.Y + (float)Main.fishingLineTexture.Height * 0.5f), 
                            new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, 0, Main.fishingLineTexture.Width, (int)num)), 
                            GetLineColor(position), 
                            rotation2, 
                            new Vector2((float)Main.fishingLineTexture.Width * 0.5f, 0f), 
                            1f, 
                            SpriteEffects.None, 
                            0f
                        );
                    }
                }
            }
            return false;
        }
    }
}