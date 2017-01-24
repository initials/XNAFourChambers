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
    class Rat : EnemyActor
    {
        public Rat(int xPos, int yPos)
            : base(xPos, yPos)
        {

            // Set up the stats for this actor.
            actorName = "Walkman";
            score = 50;
            health = 1;
            runSpeed = 5;
            _jumpPower = -110.0f;
            _jumpInitialPower = -110.0f;
            _jumpMaxTime = 0.09f;
            _jumpInitialTime = 0.045f;
            maxVelocity.X = runSpeed * 4;
            maxVelocity.Y = 1000;
            //drag.X = runSpeed * 4;
            //drag.Y = runSpeed * 4;
            playbackFile = "FourChambers/ActorRecording/rat.txt";
            timeDownAfterHurt = 2.5f;
 
            //Set the health bar max from here now that we know our health starting point.
            healthBar.max = (uint)health;

            // Load graphic and create animations.
            // Required anims:
            // walk, run, idle, attack, death, hurt, jump

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/characterSpriteSheets/Rat_50x50"), true, false, 50, 50);
            
            width = 10;
            height = 10;

            setOffset(20, 40);

            addAnimation("run", new int[] { 0, 1, 1, 1, 2, 3, 2 }, 24);
            addAnimation("walk", new int[] { 0, 1, 1, 1, 2, 3, 2 }, 12);
            addAnimation("jump", new int[] { 0, 1, 1, 1, 2, 3, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 24);


            addAnimation("idle", new int[] { 0 }, 12, true);
            //addAnimation("attack", new int[] { 2, 4 }, 18);
            addAnimation("death", new int[] { 4,5,6,7,8,7,8,7,8,7,8,7,6 }, 12, false);
            addAnimation("hurt", new int[] { 4, 5, 6, 7, 8, 7, 8, 7, 8, 7, 8, 7, 6 },12, false);

            //addAnimationCallback(jump);

            itemsThatCanKill = new List<string>() { "FourChambers.Arrow", "FourChambers.MeleeHitBox"};

            actorsThatCanCollectWhenDead = new List<string>() { "FourChambers.Marksman" };

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Frame"></param>
        /// <param name="FrameIndex"></param>
        public void jump(string Name, uint Frame, int FrameIndex)
        {
            string info = "Current animation: " + Name + " Frame: " + Frame + " FrameIndex: " + FrameIndex;
            Console.WriteLine(info);

            //if (Frame == 1 && Name=="idle")
            //{
            //    if (FlxU.random() > 0.5f)
            //    {
            //        velocity.Y = -180;
            //        velocity.X = 130;
            //    }
            //    else
            //    {
            //        velocity.Y = -200;
            //        velocity.X = -110;
            //    }
            //}
        }

        override public void update()
        {
            if (onFloor && !dead && !colorFlickering() )
            {
                if (FlxU.random() > 0.99f)
                {
                    velocity.Y = -180;
                    velocity.X = 130;
                }
                else if (FlxU.random() > 0.98f)
                {
                    velocity.Y = -200;
                    velocity.X = -110;
                }
            }


            base.update();
        }
    }
}
