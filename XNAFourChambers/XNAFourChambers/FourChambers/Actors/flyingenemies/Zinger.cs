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
    class Zinger : BaseActor
    {
        protected float chanceOfWingFlap = 0.023f;

        protected float speedOfWingFlapVelocity = -40;

        public bool homing = false;
        public FlxSprite homingTarget;

        public Zinger(int xPos, int yPos)
            : base(xPos, yPos)
        {

            actorName = "Zinger";
            health = 1;
            score = 100;

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/characterSpriteSheets/Zinger_ss_12x14"), true, false, 12, 14);

            addAnimation("fly", new int[] { 0, 1 }, FlxU.randomInt(15,30) );
            addAnimation("death", new int[] { 2,3,4 }, 8, false);
            play("fly");

            //bounding box tweaks
            //width = 10;
            //height = 10;
            //offset.X = 1;
            //offset.Y = 4;

            chanceOfWingFlap += FlxU.random(0.005, 0.009);
            speedOfWingFlapVelocity += FlxU.random(0,3);

            originalPosition.X = xPos;
            originalPosition.Y = yPos;

            runSpeed = 30;
            acceleration.Y = 50;
            maxVelocity.X = FlxU.random(20,50);
            maxVelocity.Y = FlxU.random(20, 50);
            //velocity.X = 100;

            itemsThatCanKill = new List<string>() { "FourChambers.Arrow", "FourChambers.MeleeHitBox"};

            actorsThatCanCollectWhenDead = new List<string>() { "FourChambers.Marksman" };

        }

        public override void reset(float X, float Y)
        {
            //Console.WriteLine("Resetting a Zinger");
            homing = false;

            velocity.X = FlxU.random(0, maxVelocity.X);
            velocity.Y = FlxU.random(0, maxVelocity.Y);

            visible = true;
            dead = false;

            play("fly");

            base.reset(X, Y);

            acceleration.Y = 50;
        }

        override public void update()
        {

            if (dead == false)
            {
                if (FlxU.random() < chanceOfWingFlap)
                {
                    velocity.Y = speedOfWingFlapVelocity;
                }
            }

            base.update();

            if (homing && homingTarget != null)
            {
                float rightX1 = homingTarget.x;
                float rightY1 = homingTarget.y - (FlxU.random(-20,40));

                float xDiff = x - rightX1;
                float yDiff = y - rightY1;

                double degrees = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;

                double radians = Math.PI / 180 * degrees;

                double velocity_x = Math.Cos((float)radians);
                double velocity_y = Math.Sin((float)radians);

                //if (FlxU.random() > 0.5f)
                //    velocity_x = 0;

                // original
                //velocity.X = (float)velocity_x * -400;
                //velocity.Y = (float)velocity_y * -400;

                Vector2 targetVel = new Vector2((float)velocity_x * FlxU.randomInt(-400, -200), (float)velocity_y * FlxU.randomInt(-400, -200));

                if (velocity.X < targetVel.X) velocity.X += FlxU.randomInt(0,40);
                if (velocity.X > targetVel.X) velocity.X -= FlxU.randomInt(0, 40);
                if (velocity.Y < targetVel.Y) velocity.Y += FlxU.randomInt(0, 40);
                if (velocity.Y > targetVel.Y) velocity.Y -= FlxU.randomInt(0, 40);

                if (homingTarget.dead == true)
                {
                    homingTarget = null;
                }
            }


            if (velocity.X > 0)
            {
                facing = Flx2DFacing.Right;
            }
            else
            {
                facing = Flx2DFacing.Left;
            }

            if (x > FlxG.levelWidth)
            {
                x = 1;
                //Console.WriteLine(x.ToString() );

            }
            if (x < 0)
            {
                x = FlxG.levelWidth - 1;
                //Console.WriteLine(x.ToString());
            }
            if (y < 0)
            {
                velocity.Y = 95;
                //Console.WriteLine(x.ToString());
            }

        }
        public override void kill()
        {
            homing = false;
            homingTarget = null;
            //FlxG.score += score * FourChambers_Globals.arrowCombo;

            play("death");
            dead = true;
            acceleration.Y = Globals.GRAVITY;
            velocity.X = 0;
            maxVelocity.Y = 1000;

            //base.kill();
        }
        override public void hitSide(FlxObject Contact, float Velocity)
        {
            velocity.X = velocity.X * -1;
        }
        public override void overlapped(FlxObject obj)
        {
            base.overlapped(obj);
        }
    }
}
