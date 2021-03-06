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
            isRespawnable = false;

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/spawner"), true, false, 16, 16);

            addAnimation("idle", new int[] { 0,1,2,3,2,1 }, (int)FlxU.random(8,12));
            play("idle");

            itemsThatCanKill = new List<string>() { "FourChambers.Arrow", "FourChambers.MeleeHitBox" };

            actorsThatCanCollectWhenDead = new List<string>() { };

            drag.X = drag.Y = 0;
            acceleration.Y = 0;
            health = 1;
            maxVelocity.Y = 350;


        }

        override public void update()
        {
            if ( debugName == "readyToPop" && !dead)
            {
                int totalEmitted = 0;

                List<FlxObject> zingers = SingleScreenLevel.actorsGrp.members.FindAll((FlxObject sp) => sp.GetType().ToString() == "FourChambers.Zinger");
                for (int i = 0; i < zingers.Count; i++)
                {
                    if (((Zinger)(zingers[i])).dead == true && totalEmitted <=2 )
                    {
                        ((Zinger)(zingers[i])).reset(x, y);
                        ((Zinger)(zingers[i])).homing = true;
                        ((Zinger)(zingers[i])).homingTarget = (FlxSprite)SingleScreenLevel.actorsGrp.members.Find((FlxObject sp) => sp.GetType().ToString() == "FourChambers.Marksman");
                        totalEmitted++;
                    }
                }
                kill();
            }


            base.update();

        }

        public void overlapWithZinger(FlxObject obj)
        {
            collide(obj);
            FlxU.collide(this, obj);

        }

        public override void overlapped(FlxObject obj)
        {
            //
            //acceleration.Y = FourChambers_Globals.GRAVITY;

            //Console.WriteLine("Zinger Nest has been overlapped, by: {0}", obj.GetType().ToString());
            if (itemsThatCanKill.Contains(obj.GetType().ToString()))
            {
                acceleration.Y = Globals.GRAVITY;
                @fixed = false;
                ((FlxSprite)obj).kill();
            }

            //base.overlapped(obj);
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
