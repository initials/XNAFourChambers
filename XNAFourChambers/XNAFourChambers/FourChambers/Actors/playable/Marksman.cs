using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace FourChambers
{
    public class Marksman : Actor
    {
        public static int _curArrow;

        /// <summary>
        /// arrows left
        /// </summary>
        public int arrowsRemaining = 0;

        public MeleeHitBox meleeHitBox;

        public bool hasUsedJoystickToAim = false;

        private double _degrees;

        private Vector2 lastJoystickDirection;

        public Marksman(int xPos, int yPos, List<FlxObject> Bullets)
            : base(xPos, yPos)
        {
            actorName = "Marqu";

            _bullets = Bullets;

            if (FourChambers_Globals.PLAYER_ACTOR == FourChambers_Globals.PLAYER_MARKSMAN)
            {

                loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/characterSpriteSheets/Marksman_50x50"), true, false, 50, 50);

                addAnimation("run", new int[] { 38, 39, 40, 41, 42, 43, 44, 45, 46, 47 }, 12);
                addAnimation("idle", new int[] { 38 }, 12);
                addAnimation("idleMelee", new int[] { 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 73, 57, 57, 57, 57, 57, 57, 57, 57, 57, 57, 57, 57, 57 }, 12);
                addAnimation("attack", new int[] { 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 }, 60, true);
                addAnimation("attackMelee", new int[] { 0, 28, 28, 28, 29, 29, 29, 30, 30, 30, 31, 31, 31, 32, 32, 32, 33, 33, 33, 34, 34, 34, 35, 35, 35, 36, 36, 36, 36, 36, 36 }, 60, true);

                addAnimation("jump", new int[] { 39, 40, 41, 42, 43, 44 }, 3, true);
                addAnimation("jumpRange", new int[] { 3, 4, 5, 6, 7, 8, 9 }, 3, true);
                addAnimation("climb", new int[] { 20, 21, 22, 23, 24, 25, 24, 23, 22, 21 }, 24, true);
                addAnimation("climbidle", new int[] { 20 }, 0, true);
                addAnimation("death", new int[] { 26, 27 }, 4, false);
                addAnimation("hurt", new int[] { 26, 27 }, 4, false);

                addAnimation("runRange", new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 12);
                addAnimation("idleRange", new int[] { 48, 49, 50, 51, 52, 53, 54, 55, 56 }, 12);
            }
            else if (FourChambers_Globals.PLAYER_ACTOR == FourChambers_Globals.PLAYER_MISTRESS)
            {

                loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/characterSpriteSheets/Mistress_50x50"), true, false, 50, 50);

                addAnimation("run", new int[] { 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, 12);
                addAnimation("walk", new int[] { 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, 8);
                addAnimation("jump", new int[] { 7 }, 12);

                addAnimation("runRange", new int[] { 25, 26, 27, 28, 29, 30, 31, 32, 33, 34 }, 12);
                addAnimation("walkRange", new int[] { 25, 26, 27, 28, 29, 30, 31, 32, 33, 34 }, 8);
                addAnimation("jumpRange", new int[] { 25 }, 12);

                addAnimation("idleMelee", new int[] { 0 }, 12);
                addAnimation("attackMelee", new int[] { 0, 1, 2, 3, 4, 5, 6, 6, 6, 0 }, 30, true);
                addAnimation("hurt", new int[] { 17, 18, 19, 20, 21, 22, 23, 24 }, 12);
                addAnimation("death", new int[] { 17, 18, 19, 20, 21, 22, 23, 24 }, 12);

                addAnimation("climb", new int[] { 20, 21, 22, 23, 24, 25, 24, 23, 22, 21 }, 24, true);
                addAnimation("climbidle", new int[] { 20 }, 0, true);

            }
            else if (FourChambers_Globals.PLAYER_ACTOR == FourChambers_Globals.PLAYER_WARLOCK)
            {

                loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/characterSpriteSheets/Warlock_50x50"), true, false, 50, 50);

                addAnimation("run", new int[] { 5, 6, 7, 8, 9 }, 12);
                addAnimation("walk", new int[] { 5, 6, 7, 8, 9 }, 12);
                addAnimation("idle", new int[] { 0, 1, 2, 3 }, 12);
                addAnimation("attack", new int[] { 11, 12, 13, 14, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16 }, 30);


                //Fix these...
                addAnimation("idleMelee", new int[] { 0, 1, 2, 3 }, 12);
                addAnimation("attackMelee", new int[] { 11, 12, 13, 14, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16, 15, 16 }, 60, true);
                addAnimation("jump", new int[] { 5,6,7 }, 3, true);
                addAnimation("jumpRange", new int[] { 5, 6, 7 }, 3, true);
                addAnimation("climb", new int[] { 0 }, 24, true);
                addAnimation("climbidle", new int[] { 0 }, 0, true);
                addAnimation("death", new int[] { 0 }, 4, false);
                addAnimation("hurt", new int[] { 0 }, 4, false);
                addAnimation("runRange", new int[] { 5, 6, 7, 8, 9 }, 12);
                addAnimation("idleRange", new int[] { 0 }, 12);
            }


            addAnimationCallback(footstep);

            //bounding box tweaks
            width = 5;
            height = 16;

            setOffset(21, 34);

            //offset.X = 13;
            //offset.Y = 8;

            //basic player physics
            int runSpeed = 120;
            drag.X = runSpeed * 4;
            acceleration.Y = FourChambers_Globals.GRAVITY;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 1000;

            arrowsRemaining = 0;

            meleeHitBox = new MeleeHitBox(xPos, yPos);
            meleeHitBox.width = 5;
            meleeHitBox.height = 5;

            lastJoystickDirection = new Vector2(0, 0);

            timeDownAfterHurt = 1.5f;


            hasMeleeWeapon = FourChambers_Globals.hasMeleeWeapon;
            hasRangeWeapon = FourChambers_Globals.hasRangeWeapon;

            health = 4;

        }

        public void adjustMeleeBox()
        {
            if (attackingMelee)
            {
                meleeHitBox.width = 5;
                meleeHitBox.height = 5;
                // position the hit box of the whip.

                if (facing == Flx2DFacing.Right)
                {
                    switch (_curFrame)
                    {
                        case 4:
                            meleeHitBox.dead = false;
                            meleeHitBox.x = x + 14;
                            meleeHitBox.y = y;
                            break;
                        case 5:
                            meleeHitBox.dead = false;
                            meleeHitBox.x = x + 16;
                            meleeHitBox.y = y + 2;
                            break;
                        case 6:
                            meleeHitBox.dead = false;
                            meleeHitBox.width = 7;
                            meleeHitBox.height = 7;
                            meleeHitBox.x = x + 28;
                            meleeHitBox.y = y + 3;
                            break;
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                            meleeHitBox.dead = false;
                            meleeHitBox.width = 10;
                            meleeHitBox.height = 17;
                            meleeHitBox.x = x + _curFrame;
                            meleeHitBox.y = (y - 18) + (_curFrame);
                            break;
                        case 30:
                            attackingMelee = false;
                            break;
                        default:
                            meleeHitBox.dead = true;
                            meleeHitBox.width = 10;
                            meleeHitBox.height = 10;
                            meleeHitBox.x = x;
                            meleeHitBox.y = y;
                            break;
                    }
                }
                if (facing == Flx2DFacing.Left)
                {
                    switch (_curFrame)
                    {
                        case 4:
                            meleeHitBox.dead = false;
                            meleeHitBox.x = x - 10;
                            meleeHitBox.y = y;
                            break;
                        case 5:
                            meleeHitBox.dead = false;
                            meleeHitBox.x = x - 12;
                            meleeHitBox.y = y + 2;
                            break;
                        case 6:
                            meleeHitBox.dead = false;
                            meleeHitBox.width = 7;
                            meleeHitBox.height = 7;
                            meleeHitBox.x = x - 24;
                            meleeHitBox.y = y + 3;
                            break;
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                            meleeHitBox.dead = false;
                            meleeHitBox.width = 10;
                            meleeHitBox.height = 17;
                            meleeHitBox.x = x - _curFrame;
                            meleeHitBox.y = (y - 18) +  (_curFrame);
                            break;
                        case 30:
                            attackingMelee = false;
                            break;

                        default:
                            meleeHitBox.dead = true;
                            meleeHitBox.width = 10;
                            meleeHitBox.height = 10;
                            meleeHitBox.x = x;
                            meleeHitBox.y = y;
                            break;
                    }
                }
            }
            else
            {
                meleeHitBox.dead = true;
                meleeHitBox.x = x;
                meleeHitBox.y = y;
                meleeHitBox.width = 0;
                meleeHitBox.height = 0;
            }
        }

        override public void update()
        {

            adjustMeleeBox();

            float rightX11 = GamePad.GetState(playerIndex).ThumbSticks.Right.X;
            float rightY11 = GamePad.GetState(playerIndex).ThumbSticks.Right.Y;

			#if __ANDROID__

			//rightY11 *= -1;

			#endif

            if (rightX11 != 0 || rightY11 != 0 || hasUsedJoystickToAim)
            {
                hasUsedJoystickToAim = true;

                float xDiff = 0 - rightX11;
                float yDiff = 0 - rightY11;

                if (rightX11 == 0 && rightY11 == 0)
                {

                }
                else
                {
                    _degrees = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;
                }

                double radians = Math.PI / 180 * _degrees;

                Vector2 rotpoint = FlxU.rotatePoint(x - 50, y, x, y, (float)_degrees * -1);
                FlxG.mouse.cursor.x = rotpoint.X;
                FlxG.mouse.cursor.y = rotpoint.Y;

            }

            if (FlxG.mouse.pressed() && playerIndex == PlayerIndex.One)
            {
                hasUsedJoystickToAim = false;
            }

            if (hasRangeWeapon && ((_curFrame == 8 || _curFrame == 9 || _curFrame == 10) && attackingJoystick) || (FlxG.gamepads.isNewButtonPress(Buttons.RightShoulder) && velocity.X != 0) )
            {
                //Console.WriteLine("Shooting Arrow " + FlxG.elapsedTotal + " This is the frame of the Marksman animation" + _curFrame);

                float rightX = GamePad.GetState(playerIndex).ThumbSticks.Right.X;
                float rightY = GamePad.GetState(playerIndex).ThumbSticks.Right.Y;

				#if __ANDROID__

				//rightY *= -1;

				#endif
                
                // No Right Stick so do a generic shoot...
                if (arrowsRemaining >= 1)
                {
                    for (int i = 0; i < FourChambers_Globals.arrowsToFire; i++)
                    {
                        if (rightX == 0 && rightY == 0)
                        {
                            //if (facing == Flx2DFacing.Right)
                            //    ((Arrow)(_bullets[_curArrow])).shoot((int)x, (int)(y + (height / 2)), 600, -100 + (i * 40));
                            //else
                            //    ((Arrow)(_bullets[_curArrow])).shoot((int)x, (int)(y + (height / 2)), -600, -100 + (i * 40));

                            //Console.WriteLine(12 * (int)(x - FlxG.mouse.cursor.x) * -1);
                            
                            int yVel = (int)(12 * (int)(y - FlxG.mouse.cursor.y) * -1);
                            int yVelAdjusted = yVel - (i * 40);
                            ((Arrow)(_bullets[_curArrow])).shoot((int)x, (int)(y + (height / 2)), 12 * (int)(x - FlxG.mouse.cursor.x) * -1, yVelAdjusted);
                            
                        }
                        // use the right stick to fire a weapon
                        else
                        {
                            int yVel = (int)(rightY * -600);
                            int yVelAdjusted = yVel - (i * 40);

                            ((Arrow)(_bullets[_curArrow])).shoot((int)x, (int)(y + (height / 2)), (int)(rightX * 600), yVelAdjusted);
                        }




                        if (rightX < 0)
                        {
                            ((Arrow)(_bullets[_curArrow])).facing = Flx2DFacing.Left;
                        }
                        else
                        {
                            ((Arrow)(_bullets[_curArrow])).facing = Flx2DFacing.Right;
                        }

                        if (++_curArrow >= _bullets.Count)
                            _curArrow = 0;
                    }
                    arrowsRemaining--;
                }


                attackingJoystick = false;
                attackingMouse = false;
                _curFrame = 0;

            }

            // use the mouse position to fire a bullet.
            if ((_curFrame == 8 || _curFrame == 9 || _curFrame == 10) && (attackingMouse) && hasRangeWeapon)
            {
                //Console.WriteLine("Shooting Arrow " + FlxG.elapsedTotal + " This is the frame of the Marksman animation" + _curFrame);

                float rightX1 = FlxG.mouse.x;
                float rightY1 = FlxG.mouse.y;

                float xDiff = x - rightX1;
                float yDiff = y - rightY1;

                double degrees = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;

                double radians = Math.PI / 180 * degrees;

                double velocity_x = Math.Cos((float)radians);
                double velocity_y = Math.Sin((float)radians);

                if (arrowsRemaining >= 1)
                {
                    for (int i = 0; i < FourChambers_Globals.arrowsToFire; i++)
                    {
                        int yVel = (int)(velocity_y * (-550 - (FourChambers_Globals.arrowPower* 50)));
                        int yVelAdjusted = yVel - (i * 40);
                        int xVel = (int)(velocity_x * (-550 - (FourChambers_Globals.arrowPower*50)));


                        //Console.WriteLine("Y Velocity = {0} {1}", xVel, yVelAdjusted);

                        ((Arrow)(_bullets[_curArrow])).shoot((int)x, (int)(y + (height / 2)), xVel, yVelAdjusted);
                        

                        if (++_curArrow >= _bullets.Count)
                            _curArrow = 0;

                    }
                }
                arrowsRemaining--;
                if (rightX1 - x < 0)
                {
                    facing = Flx2DFacing.Left;

                    //Console.WriteLine("Left");
                }
                else
                {
                    facing = Flx2DFacing.Right;

                    //Console.WriteLine("Right");
                }


                attackingJoystick = false;
                attackingMouse = false;
                _curFrame = 0;

            }

            PlayerIndex pi;

            if (FourChambers_Globals.seraphineHasBeenKilled == false && canFly)
            {
                if (FlxG.gamepads.isButtonDown(Buttons.Y, playerIndex, out pi) || FlxG.keys.W)
                {
                    velocity.Y = -100;
                    //FlxG.bloom.Visible = true;
                    flying = true;

                }
                else
                {
                    //velocity.Y = FourChambers_Globals.GRAVITY;
                    //FlxG.bloom.Visible = false;
                    flying = false;
                }
            }

            if (FlxGlobal.cheatString == "weapons")
            {
                hasMeleeWeapon = true;
                hasRangeWeapon = true;

                FourChambers_Globals.hasMeleeWeapon = true;
                FourChambers_Globals.hasRangeWeapon = true;
            }

            // For just landing.
            bool wasInAir = false;
            if (framesSinceLeftGround>1)
            {
                wasInAir = true;
            }

            base.update();

            // Just landed.
            if (wasInAir && framesSinceLeftGround == 0)
            {
                FlxG.play("sfx/sony/footsteps/footstep_grass_boots_jog-001", 1.0f, false);

                //Console.WriteLine("----------------------> WAS IN AIR");

            }
        }

        public void footstep(string Name, uint Frame, int FrameIndex)
        {

            //sfx
            if (Name.StartsWith("run") && (Frame == 2 || Frame==7))
            {
                //Console.WriteLine("Footstep F {0} FI {1}", Frame, FrameIndex);
                FlxG.play("sfx/sony/footsteps/footstep_grass_boots_jog-00" + ((int)FlxU.random(1, 10)).ToString(), 0.75f);
            }
        }

        public override void render(SpriteBatch spriteBatch)
        {
            base.render(spriteBatch);
            meleeHitBox.render(spriteBatch);
        }
    }
}
