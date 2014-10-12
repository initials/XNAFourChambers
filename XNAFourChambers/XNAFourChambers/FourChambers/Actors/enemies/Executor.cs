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
    public class Executor : EnemyActor
    {

        public Executor(int xPos, int yPos)
            : base(xPos, yPos)
        {

            // Set up the stats for this actor.
            actorName = "Master Jaymes";
            score = 250;
            health = 75;
            runSpeed = 120;
            _jumpPower = -110.0f;
            _jumpInitialPower = -110.0f;
            _jumpMaxTime = 0.15f;
            _jumpInitialTime = 0.045f;
            maxVelocity.X = runSpeed * 4;
            maxVelocity.Y = 1000;
            drag.X = runSpeed * 4;
            drag.Y = runSpeed * 4;
            playbackFile = "FourChambers/ActorRecording/executor.txt";
            timeDownAfterHurt = 2.5f;
            actorType = "executor";

            //Set the health bar max from here now that we know our health starting point.
            healthBar.max = (uint)health;

            // Load graphic and create animations.
            // Required anims:
            // walk, run, idle, attack, death, hurt, jump

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/characterSpriteSheets/Executor_50x50"), true, false, 50, 50);

            loadAnimationsFromGraphicsGaleCSV("content/fourchambers/characterSpriteSheets/Executor_50x50.csv");

            //addAnimation("run", new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 12);
            //addAnimation("walk", new int[] { 1, 2, 3, 4, 5, 6, 7, 8 }, 8);
            //addAnimation("idle", new int[] { 0,11,12,13,14,15 }, 12);
            //addAnimation("attack", new int[] { 0, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28,28,28,28,28,28 }, 18);

            //addAnimation("hurt", new int[] { 29,30,31,32,33,34,35 }, 12, false);
            //addAnimation("death", new int[] { 29, 30, 31, 32, 33, 34,35 }, 12, false);

            addAnimation("jump", new int[] { 2, 3, 6, 7 }, 3, true);

            //bounding box tweaks
            width = 7;
            height = 20;

            setOffset(21, 30);

            //offset.X = 7;
            //offset.Y = 1;

        }

        override public void update()
        {



            base.update();

        }


    }
}
