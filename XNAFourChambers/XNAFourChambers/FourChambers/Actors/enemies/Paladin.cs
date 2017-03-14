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
    public class Paladin : EnemyActor
    {

        public Paladin(int xPos, int yPos)
            : base(xPos, yPos)
        {
            // Set up the stats for this actor.
            actorName = "Johnny Football Hero";
            score = 250;
            health = 15;
            runSpeed = 6;
            _jumpPower = -150.0f;
            _jumpInitialPower = -150.0f;
            _jumpMaxTime = 0.10f;
            _jumpInitialTime = 0.055f;
            maxVelocity.X = runSpeed * 4;
            maxVelocity.Y = 1000; 
            
            
            playbackFile = "FourChambers/ActorRecording/paladin.txt";
            timeDownAfterHurt = 2.5f;

            //Set the health bar max from here now that we know our health starting point.
            healthBar.max = (uint)health;

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/characterSpriteSheets/paladin_50x50"), true, false, 50, 50);

            addAnimation("run", new int[] {0, 1, 2, 3, 4, 5, 6, 7 }, 18);
            addAnimation("walk", new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }, 12);
            addAnimation("idle", new int[] { 0,6,7,7,7,6,0 }, 6);
            addAnimation("attack", new int[] { 8,9,9,0 }, 24, false);
            addAnimation("death", new int[] { 10 }, 4, false);

            //bounding box tweaks
            width = 10;
            height = 20;
            setOffset(20, 30);

            itemsThatCanKill = new List<string>() { "FourChambers.Arrow", "FourChambers.MeleeHitBox" };

            actorsThatCanCollectWhenDead = new List<string>() { "FourChambers.Marksman" };

            deathSound = "sfx/screech5";



        }

        override public void update()
        {
            base.update();
        }


    }
}
