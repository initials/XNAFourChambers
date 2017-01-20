using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Text;
using org.flixel;

namespace FourChambers
{
    public class Warlock : Actor
    {
        public Warlock(int xPos, int yPos)
            : base(xPos, yPos)
        {
            actorName = "Terry";



            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/characterSpriteSheets/Warlock_50x50"), true, false, 50, 50);

            addAnimation("run", new int[] { 5, 6, 7, 8, 9 }, 12);
            addAnimation("walk", new int[] { 5, 6, 7, 8, 9 }, 12);
            addAnimation("idle", new int[] { 0, 1, 2, 3 }, 12);
            addAnimation("attack", new int[] { 11, 12, 13, 14, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16 }, 30);


            //Fix these...
            addAnimation("idleMelee", new int[] { 0, 1, 2, 3 }, 12);
            addAnimation("attackMelee", new int[] { 11, 12, 13, 14, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16 }, 60, true);
            addAnimation("jump", new int[] { 5, 6, 7 }, 3, true);
            addAnimation("jumpRange", new int[] { 5, 6, 7 }, 3, true);
            addAnimation("climb", new int[] { 0 }, 24, true);
            addAnimation("climbidle", new int[] { 0 }, 0, true);
            addAnimation("death", new int[] { 0 }, 4, false);
            addAnimation("hurt", new int[] { 0 }, 4, false);
            addAnimation("runRange", new int[] { 5, 6, 7, 8, 9 }, 12);
            addAnimation("idleRange", new int[] { 0,1,2,3 }, 4);

            //bounding box tweaks
            width = 5;
            height = 16;

            setOffset(21, 34);

            playerIndex = PlayerIndex.Two;

            runSpeed = 120;

            drag.X = runSpeed * 4;
            drag.Y = runSpeed * 4;



        }

        override public void update()
        {
            //SHOOTING
            if ((frame == 16 ) && attackingJoystick)
            {
                float rightX = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X;
                float rightY = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y;

                if (rightX == 0 && rightY == 0)
                {
                    if (facing == Flx2DFacing.Right)
                        ((WarlockFireBall)(_bullets[_curBullet])).shoot((int)x, (int)(y + (height / 12)), 600, -100);
                    else
                        ((WarlockFireBall)(_bullets[_curBullet])).shoot((int)x, (int)(y + (height / 12)), -600, -100);
                }
                else
                {
                    ((WarlockFireBall)(_bullets[_curBullet])).shoot((int)x, (int)(y + (height / 12)), (int)(rightX * 600), (int)(rightY *= -600));
                }
                if (rightX < 0)
                {
                    ((WarlockFireBall)(_bullets[_curBullet])).facing = Flx2DFacing.Left;
                }
                else
                {
                    ((WarlockFireBall)(_bullets[_curBullet])).facing = Flx2DFacing.Right;
                }

                if (++_curBullet >= _bullets.Count)
                    _curBullet = 0;

                attackingJoystick = false;
                attackingMouse = false;
                frame = 0;

            }

            base.update();

        }
    }
}
