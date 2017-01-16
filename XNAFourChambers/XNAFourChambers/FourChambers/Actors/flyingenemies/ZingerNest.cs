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
    class ZingerNest : BaseActor
    {

        public ZingerNest(int xPos, int yPos)
            : base(xPos, yPos)
        {

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/spawner"), true, false, 16, 16);

            addAnimation("idle", new int[] { 0,1,2,3,2,1 }, (int)FlxU.random(8,12));
            play("idle");
            y += 16;

            itemsThatCanKill = new List<string>() { "FourChambers.Arrow", "FourChambers.MeleeHitBox" };

            //actorsThatCanCollectWhenDead = new List<string>() { "FourChambers.Marksman" };

            drag.X = drag.Y = 0;
            acceleration.Y = 0;
            health = 10;

        }

        override public void update()
        {
            
            base.update();

        }

        public override void overlapped(FlxObject obj)
        {
            //acceleration.Y = FourChambers_Globals.GRAVITY;

            //Console.WriteLine("Zinger Nest has been hit, accel: {0}", acceleration.Y);
            if (itemsThatCanKill.Contains(obj.GetType().ToString()))
            {
                acceleration.Y = FourChambers_Globals.GRAVITY;
                @fixed = false;

            }

            base.overlapped(obj);
        }

        public override void kill()
        {
            FlxG.play("sfx/harvesterAttack", 0.5f, false);
            //acceleration.Y = FourChambers_Globals.GRAVITY;
            base.kill();
        }

        public override void hitBottom(FlxObject Contact, float Velocity)
        {
            debugName = "readyToPop";

            //if (acceleration.Y == FourChambers_Globals.GRAVITY)
            //{
            //    exists = false;
            //    visible = false;
            //    dead = true;
            //}

            //kill();

            base.hitBottom(Contact, Velocity);
        }

    }
}
