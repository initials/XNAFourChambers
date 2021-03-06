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
    class Seraphine : BaseActor
    {
        public bool concern;

        public Seraphine(int xPos, int yPos)
            : base(xPos, yPos)
        {
            actorName = "Jennifer Twist";

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/characterSpriteSheets/Seraphine_50x50"), true, false, 50,50);

            addAnimation("fly", new int[] {0,1,2,3,4,5,6,7,8,9 }, 18);
            addAnimation("jump", new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 18);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 0, 1, 2 }, 12);

            addAnimation("walk", new int[] { 0, 1, 2 }, 12);
            addAnimation("run", new int[] { 0, 1, 2 }, 12);
            addAnimation("hurt", new int[] { 10 }, 12);
            addAnimation("death", new int[] { 10,11,12,13,14,15 }, 12, false);

            addAnimation("concern", new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 7, 7, 8, 8, 8, 9, 9, 9, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 17, 18, 19, 20, 21, 22, 23, 24, 20, 21, 22, 23, 24, 20, 21, 22, 23, 24, 20, 21, 22, 23, 24, 20, 21, 22, 23, 24, 20, 21, 22, 23, 24, 20, 21, 22, 23, 24, 20, 21, 22, 23, 24, 20, 21, 22, 23, 24, 20, 21, 22, 23, 24, 20, 21, 22, 23, 24, 20, 21, 22, 23, 24, 20, 21, 22, 23, 24, 20, 21, 22, 23, 24, 20, 21, 22, 23, 24, 20, 21, 22, 23, 24, 20, 21, 22, 23, 24, 20, 21, 22, 23, 24, 20, 21, 22, 23, 24, 20, 21, 22, 23, 24, 20, 21, 22, 23, 24, 20, 21, 22, 23, 24, 20, 21, 22, 23, 24, 20, 21, 22, 23, 24, 20, 21, 22, 23, 24, 20, 21, 22, 23, 24 }, 24, false);

            //basic player physics
            runSpeed = 120;
            //drag.X = runSpeed * 4;
            acceleration.Y = Globals.GRAVITY;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 25;

            width = 10;
            height = 20;

            setOffset(20, 30);

            concern = false;

            itemsThatCanKill = new List<string>() { "FourChambers.Arrow", "FourChambers.MeleeHitBox" };

            actorsThatCanCollectWhenDead = new List<string>() { "FourChambers.Marksman" };


        }

        override public void update()
        {
            if (!concern)
            {
                if (dead && onFloor)
                {
                    play("death");
                }
                else if (dead)
                {
                    play("hurt");
                }
                else if (!onFloor)
                {
                    play("fly");
                }
                else
                {
                    play("idle");
                }
            }
            else if (concern)
            {
                if (!onFloor)
                {
                    play("fly");
                }
                else
                {
                    play("concern");
                }
            }

            base.update();
        }

        public override void kill()
        {
            acceleration.Y = Globals.GRAVITY;
            dead = true;
            //base.kill();
        }
    }
}
