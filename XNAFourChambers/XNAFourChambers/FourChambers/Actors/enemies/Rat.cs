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
    class Rat : EnemyActor
    {
        public Rat(int xPos, int yPos)
            : base(xPos, yPos)
        {

            // Set up the stats for this actor.
            actorName = "Walkman";
            score = 50;
            health = 1;
            runSpeed = 120;
            _jumpPower = -110.0f;
            _jumpInitialPower = -110.0f;
            _jumpMaxTime = 0.15f;
            _jumpInitialTime = 0.045f;
            maxVelocity.X = runSpeed * 4;
            maxVelocity.Y = 1000;
            drag.X = runSpeed * 4;
            drag.Y = runSpeed * 4;
            playbackFile = "FourChambers/ActorRecording/rat.txt";
            timeDownAfterHurt = 2.5f;
            actorType = "rat";

            //Set the health bar max from here now that we know our health starting point.
            healthBar.max = (uint)health;

            // Load graphic and create animations.
            // Required anims:
            // walk, run, idle, attack, death, hurt, jump

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/characterSpriteSheets/Rat_20x20"), true, false, 20, 20);

            //addAnimation("run", new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }, 12);
            addAnimation("idle", new int[] {0 ,1,2,3,2 }, 12, true);
            //addAnimation("attack", new int[] { 2, 4 }, 18);

        }

        override public void update()
        {
            base.update();
        }
    }
}
