using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using org.flixel;

namespace FourChambers
{
    /// <summary>
    /// 
    /// </summary>
    public class Arrow : FlxSprite
    {
        private Texture2D ImgBullet;

        private bool hasTouched;

        protected const string SndShoot = "sfx/arrowShoot1";
        protected const string SndHit = "sfx/arrowHit1";
        public bool explodesOnImpact = true;

        public FlxEmitter _fire;

        public int framesInAir = 0;
        
       

        public Arrow(int xPos, int yPos)
            : base(xPos, yPos)
        {

            ImgBullet = FlxG.Content.Load<Texture2D>("fourchambers/arrow_8x1");

            loadGraphic(ImgBullet, false, false, 8,1);

            hasTouched = false;

            width = 8;
            height = 1;
            offset.X = 0;
            offset.Y = 0;
            exists = false;

            addAnimation("explode", new int[] { 0 }, 30, false);
            addAnimation("normal", new int[] { 0 }, 0, false);

            play("normal");

            drag.X = 0;
            drag.Y = 0;

            acceleration.Y = 0;
            maxVelocity.X = 1000;
            maxVelocity.Y = 1000;

            
            //_fire = new FlxEmitter();
            //_fire.setSize(1, 1);
            //_fire.setRotation();
            //_fire.setXSpeed(-44, 44);
            //_fire.setYSpeed(-44, 44);
            //_fire.gravity = 45;
            //_fire.createSprites(FlxG.Content.Load<Texture2D>("fourchambers/arrowSparkles"), 25, true);

            damage = 15;

        }

        override public void render(SpriteBatch spriteBatch)
        {
            //_fire.render(spriteBatch);
            base.render(spriteBatch);
        }


        override public void update()
        {
            framesInAir++;

            //_fire.x = x + (width / 2);
            //_fire.y = y + (height / 2);

            if (hasTouched == false)
            {
                if (velocity.X > 0)
                {
                    double rot = Math.Atan2((float)velocity.Y, (float)velocity.X);
                    double degrees = rot * 180 / Math.PI;

                    angle = (float)degrees;
                }
                // reversing not working.
                else
                {
                    double rot = Math.Atan2((float)velocity.Y, (float)velocity.X);
                    double degrees = rot * 180 / Math.PI;

                    angle = (float)degrees;
                }
            }

            //_fire.update();
            base.update();

            if (onScreen() == false && !dead)
            {
                Globals.arrowCombo = 0;
                kill();
            }
        }

        override public void hitSide(FlxObject Contact, float Velocity) 
        {
            genericArrowHit();

        }

        private void genericArrowHit()
        {

            _fire.setXSpeed(-85, 85);
            _fire.setYSpeed(-85, 85);

            _fire.start(true, 0.5f, 13);

            play("explode");

            Globals.arrowCombo = 0;
            hasTouched = true;
            kill(); 


        }

        public override void hitLeft(FlxObject Contact, float Velocity)
        {
            genericArrowHit();

            base.hitLeft(Contact, Velocity);
        }

        public override void hitRight(FlxObject Contact, float Velocity)
        {
            genericArrowHit();

            base.hitRight(Contact, Velocity);
        }

        override public void hitBottom(FlxObject Contact, float Velocity) 
        {
            genericArrowHit();
        }
        override public void hitTop(FlxObject Contact, float Velocity) 
        {
            genericArrowHit();
        }
        override public void kill()
        {

            if (onScreen())
            {
                FlxG.play(SndHit, 0.5f, false);
                _fire.setXSpeed(-85, 85);
                _fire.setYSpeed(-85, 85);

                _fire.start(true, 5, 13);

            }


            if (dead) return;
            velocity.X = 0;
            velocity.Y = 0;
            
            play("explode");

            base.kill();
        }

        public void shoot(int X, int Y, int VelocityX, int VelocityY)
        {
            framesInAir = 0;

            //particles release at regular intervals;
            //_fire.start(false, 0.02f, 0);
            
            // Global counter for arrows fired.
            Globals.arrowsFired++;

            visible = true;
            FlxG.play(SndShoot, 1.0f, false);

            play("normal");
            
            base.reset(X, Y);
            solid = true;
            velocity.X = VelocityX;
            velocity.Y = VelocityY;
            hasTouched = false;
            dead = false;

        }

    }
}
