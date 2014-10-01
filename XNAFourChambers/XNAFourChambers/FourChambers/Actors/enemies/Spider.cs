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
    class Spider : EnemyActor
    {
        public Spider(int xPos, int yPos)
            : base(xPos, yPos)
        {

            // Set up the stats for this actor.
            actorName = "Tarantulatis";
            score = 250;
            health = 35;
            runSpeed = 25;
            _jumpPower = -110.0f;
            _jumpInitialPower = -110.0f;
            _jumpMaxTime = 0.15f;
            _jumpInitialTime = 0.045f;
            maxVelocity.X = runSpeed * 4;
            maxVelocity.Y = 1000;
            drag.X = runSpeed * 4;
            drag.Y = runSpeed * 4;
            playbackFile = "FourChambers/ActorRecording/spider.txt";
            timeDownAfterHurt = 2.5f;
            actorType = "spider";

            //Set the health bar max from here now that we know our health starting point.
            healthBar.max = (uint)health;

            // Load graphic and create animations.
            // Required anims:
            // walk, run, idle, attack, death, hurt, jump

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/characterSpriteSheets/Spider_50x50"), true, false, 50, 50);

            //addAnimation("run", new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }, 12);
            addAnimation("idle", generateFrameNumbersBetween(10,20), 12);
            addAnimation("death", generateFrameNumbersBetween(0,9), 12, false);
            addAnimation("hurt", generateFrameNumbersBetween(0, 9), 12, false);

            addAnimation("walk", generateFrameNumbersBetween(21,26), 12, false);
            addAnimation("run", generateFrameNumbersBetween(21, 26), 12, false);
            
            addAnimation("attack", generateFrameNumbersBetween(28,32), 6, false);


            //addAnimation("attack", new int[] { 2, 4 }, 18);

            //bounding box tweaks
            width = 20;
            height = 20;
            offset.X = 15;
            offset.Y = 30;

        }

        override public void update()
        {
            base.update();
        }
    }
}
