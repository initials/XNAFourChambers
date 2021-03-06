﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Text;
using org.flixel;

namespace FourChambers
{
    public class Vampire : EnemyActor
    {
        private Texture2D ImgVampire;

        public Vampire(int xPos, int yPos)
            : base(xPos, yPos)
        {
            
            // Set up the stats for this actor.
            actorName = "Count Esperanza";
            score = 250;
            health = 50;
            runSpeed = 5;
            _jumpPower = -210.0f;
            _jumpInitialPower = -310.0f;
            _jumpMaxTime = 0.25f;
            _jumpInitialTime = 0.095f;
            maxVelocity.X = runSpeed * 4;
            maxVelocity.Y = 1000;
            //drag.X = runSpeed * 4;
            //drag.Y = runSpeed * 4;
            playbackFile = "FourChambers/ActorRecording/vampire.txt";
            timeDownAfterHurt = 2.5f;

            //Set the health bar max from here now that we know our health starting point.
            healthBar.max = (uint)health;

            // Load graphic and create animations.
            // Required anims:
            // walk, run, idle, attack, death, hurt, jump

            ImgVampire = FlxG.Content.Load<Texture2D>("fourchambers/characterSpriteSheets/Vampire_50x50");

            loadGraphic(ImgVampire, true, false, 50, 50);

            addAnimation("run", new int[] { 0, 1, 2, 3, 4, 5, 6 }, 16);
            addAnimation("walk", new int[] { 0, 1, 2, 3, 4, 5, 6 }, 8);
            addAnimation("idle", new int[] { 0,22,23,24,25,25,25,26,27,0,0,0,0,0,0,0,0,0,0 }, 16);
            addAnimation("attack", new int[] { 15,16,17,18,19,20,21 }, 16);

            
            addAnimation("hurt", new int[] { 7,0,1,2 }, 8, false);
            addAnimation("death", new int[] { 9,10,11,12,13,14 }, 8, false);

            //bounding box tweaks
            width = 6;
            height = 18;
            setOffset(22, 32);

            //offset.X = 4;
            //offset.Y = 1;

            deathSound = "sfx/vampire-hiss";
        }

        override public void update()
        {
            base.update();
        }


    }
}
