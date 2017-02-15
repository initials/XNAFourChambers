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
    class Gloom : EnemyActor
    {

        public Gloom(int xPos, int yPos)
            : base(xPos, yPos)
        {

            // Set up the stats for this actor.
            actorName = "Stubbsy From Accounting";
            score = 250;
            health = 5;
            runSpeed = 5;

            _jumpPower = -110.0f;
            _jumpInitialPower = -110.0f;
            _jumpMaxTime = 0.15f;
            _jumpInitialTime = 0.045f;
            
            maxVelocity.X = runSpeed * 4;
            maxVelocity.Y = 58;
            
            playbackFile = "FourChambers/ActorRecording/gloom.txt";
            timeDownAfterHurt = 2.5f;

            //Set the health bar max from here now that we know our health starting point.
            healthBar.max = (uint)health;

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/characterSpriteSheets/Gloom_13x26"), true, false, 13, 26);

            addAnimation("run", new int[] { 0, 1, 2, 3, 4, 5,6,7 }, 12);
            addAnimation("jump", new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }, 12);

            addAnimation("walk", new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }, 8);
            addAnimation("idle", new int[] { 0 }, 12);
            addAnimation("attack", new int[] { 0, 1, 2 }, 12);
            addAnimation("death", new int[] { 8 }, 1, false);

            //bounding box tweaks
            width = 7;
            height = 26;
            offset.X = 2;
            offset.Y = 0;

            itemsThatCanKill = new List<string>() { "FourChambers.Arrow", "FourChambers.MeleeHitBox" };

            actorsThatCanCollectWhenDead = new List<string>() { "FourChambers.Marksman" };

            releaseTime = 900;

        }

        override public void update()
        {
            if (!dead && FlxU.random() < 0.005f && SingleScreenLevel.actorsGrp != null && onScreen())
            {
                Console.WriteLine("Release the bats!");

                //get all bats and release them.
                foreach (Bat b in Utils.getAllActorsOfType(SingleScreenLevel.actorsGrp.members, "FourChambers.Bat"))
                {
                    if (b.dead && !b.exists)
                    {
                        b.reset(this.x, this.y);
                        b.homing = true;
                        b.homingTarget = SingleScreenLevel.actorsGrp.members.Find((FlxObject item) => item.GetType().ToString() == "FourChambers.Marksman");
                        
                        

                        if (thingsThatHaveHappenedToThisActor.Contains("ReleasedBats")==false)
                        {
                            FlxG.play("sfx/bat");
                            FlxG.quake.start(0.005f, 0.5f);
                            FlxObject s = SingleScreenLevel.actorsGrp.members.Find((FlxObject item) => item.GetType().ToString() == "FourChambers.Stonehenge");
                            if (s != null)
                                ((FlxSprite)s).play("fall");
                        }

                        thingsThatHaveHappenedToThisActor.Add("ReleasedBats");
                    }
                }
            }

            base.update();
        }
    }
}
