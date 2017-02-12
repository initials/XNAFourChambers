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
    class FlyingEnemy : BaseActor
    {
        public FlxObject homingTarget;
        public bool homing = false;

        /// <summary>
        /// used for tracking the amount of time dead for restarts.
        /// </summary>
        public float timeDead;

        //public int score;

        protected float chanceOfWingFlap = 0.023f;

        protected float speedOfWingFlapVelocity = -40;

        
        public FlyingEnemy(int xPos, int yPos)
            : base(xPos, yPos)
        {
            originalPosition.X = xPos;
            originalPosition.Y = yPos;

            acceleration.Y = 50;
            maxVelocity.X = 1000;
            maxVelocity.Y = 1000;
            velocity.X = 100;

            chanceOfWingFlap += FlxU.random(0.005, 0.009);
            speedOfWingFlapVelocity += FlxU.random(0, 3);

        }
        override public void hitSide(FlxObject Contact, float Velocity)
        {
            velocity.X = velocity.X * -1;
        }
        override public void update()
        {
            if (customAnimation != null)
            {
                play(customAnimation, false);
            }

            if (path == null)
            {
                if (dead)
                {
                    timeDead += FlxG.elapsed;
                    acceleration.Y = Globals.GRAVITY;
                }
                else
                {
                    timeDead = 0;
                    acceleration.Y = 50;
                }

                if (dead == false)
                {
                    if (FlxU.random() < chanceOfWingFlap)
                    {
                        velocity.Y = speedOfWingFlapVelocity;
                    }
                }
            }

            base.update();

            if (velocity.X > 0)
            {
                facing = Flx2DFacing.Right;
            }
            else
            {
                facing = Flx2DFacing.Left;
            }

            if (x > FlxG.levelWidth+20) x = 1;
            if (x < -20) x = FlxG.levelWidth - 1;

            if (homing && homingTarget != null)
            {
                float rightX1 = homingTarget.x;
                float rightY1 = homingTarget.y - (FlxU.random(-20, 40));

                float xDiff = x - rightX1;
                float yDiff = y - rightY1;

                double degrees = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;

                double radians = Math.PI / 180 * degrees;

                double velocity_x = Math.Cos((float)radians);
                double velocity_y = Math.Sin((float)radians);

                Vector2 targetVel = new Vector2((float)velocity_x * FlxU.randomInt(-400, -200), (float)velocity_y * FlxU.randomInt(-400, -200));

                if (velocity.X < targetVel.X) velocity.X += FlxU.randomInt(0, 40);
                if (velocity.X > targetVel.X) velocity.X -= FlxU.randomInt(0, 40);
                if (velocity.Y < targetVel.Y) velocity.Y += FlxU.randomInt(0, 40);
                if (velocity.Y > targetVel.Y) velocity.Y -= FlxU.randomInt(0, 40);

                if (homingTarget.dead == true)
                {
                    homing = false;
                    homingTarget = null;
                }
            }

        }
        public override void kill()
        {
            play("death");
            dead = true;
            angularVelocity = 500;
            angularDrag = 700;
            drag.X = 1000;
            acceleration.Y = Globals.GRAVITY;

            homing = false;
            homingTarget = null;

            //FlxG.score += score * FourChambers_Globals.arrowCombo;

            //base.kill();

            
        }

        public override void reset(float X, float Y)
        {
            dead = false;
            visible = true;
            exists = true;
            acceleration.Y = 50;
            base.reset(X, Y);
        }


    }
}
