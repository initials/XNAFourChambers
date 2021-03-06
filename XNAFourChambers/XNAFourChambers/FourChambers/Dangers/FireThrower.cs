﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace FourChambers
{
    class FireThrower : FlxSprite
    {

        public List<FlxObject> _fires;

        private Vector2[] angles;
        public int angleCount;
        private float shootTimer = 0.0f;
        public float shootEvery = 1.0f;
        private static int fireballCounter = 0;
        private XNATweener.Tweener tween;


        public FireThrower(int xPos, int yPos, List<FlxObject> Fires)
            : base(xPos, yPos)
        {

            angleCount = 0;

            Texture2D Img = FlxG.Content.Load<Texture2D>("fourchambers/fireThrower");

            loadGraphic(Img, false, false, 26, 26);

            addAnimation("play", new int[] { 0, 1, 2, 3, 4, 5, 6, 7 , 8}, 24, false);
            play("play");

            _fires = Fires;



            int vx = 150;
            int vy = 150;

            Vector2[] anglesx =
            {
                new Vector2(0, vy),
                new Vector2(vx, vy),
                new Vector2(vx, 0),
                new Vector2(vx, vy * -1),
                new Vector2(0, vy * -1),
                new Vector2(vx * -1, vy * -1),
                new Vector2(vx * -1, 0),
                new Vector2(vx * -1, vy),


            };

            angles = anglesx;

            tween = new XNATweener.Tweener(-25, 25, FlxU.random(0.8f, 1.2f), XNATweener.Quadratic.EaseInOut);
            tween.PingPong = true;
            tween.Start();


        }

        override public void update()
        {
            velocity.Y = tween.Position;

            tween.Update(FlxG.elapsedAsGameTime);

            shootTimer += FlxG.elapsed;

            //if (scale > 1.0f) scale -= 0.1f;
            //else scale = 1;

            if (shootTimer> shootEvery)
            {
                //Console.WriteLine("Shooting");

                shoot();
                shootTimer = 0.0f;

                //angle += 45;
            }
            base.update();

        }

        public void shoot()
        {
            //scale = 2;
            play("play", true);
            //Console.WriteLine("Fireball Count =? {0}", fireballCounter);

            int t = fireballCounter;
            _fires[t].x = x;
            _fires[t].y = y;
            _fires[t].velocity.X = angles[angleCount].X ;
            _fires[t].velocity.Y = angles[angleCount].Y;
            ((FlxSprite)(_fires[t])).scale = 0.2f;
            ((FlxSprite)(_fires[t])).dead = false ;
            ((FlxSprite)(_fires[t])).dead = true;

            angleCount++;
            fireballCounter++;
            if (angleCount > angles.Length - 1) angleCount = 0;
            if (fireballCounter > 19) fireballCounter = 0;

            //sound
            if (onScreen())
                FlxG.play("sfx/sony/whooshes/whoosh_wobble-001", 0.75f);



        }



    }
}
