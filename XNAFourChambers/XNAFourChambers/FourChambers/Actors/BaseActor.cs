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
    /* FlxSprite > Base Actor > Actor > Marksman
     *                        > EnemyActor > Bat
     * 
     * 
     * 
     * 
     */ 
    public class BaseActor : FlxSprite
    {
        /// <summary>
        /// Character's name;
        /// </summary>
        public string actorName;

        /// <summary>
        /// A quick helper for finding the type of actor. Must be set up manually.
        /// </summary>
        public string actorType;
        
        /// <summary>
        /// Score for killing this creature.
        /// </summary>
        public int score;

        /// <summary>
        /// Location of the file that holds the attack data.
        /// </summary>
        public string playbackFile = "FourChambers/ActorRecording/file.txt";

        public bool flying = false;
        public bool canFly = false;

        public bool canClimbLadder = false;

        public bool isClimbingLadder = false;

        public float timeDownAfterHurt = 1.0f;

        public float ladderPosX = 0;

        /// <summary>
        /// Determines whether or not game inputs affect charactetr.
        /// </summary>
        public bool isPlayerControlled;

        public float hurtTimer = 550.0f;

        public int runSpeed = 120;

        public PlayerIndex playerIndex = PlayerIndex.One;

        public List<string> itemsThatCanKill;

        public List<string> actorsThatCanCollectWhenDead;

        public float price;
        public bool lockedForSelection;


        /// <summary>
        /// The base for Actors. Should remain pretty empty.
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        public BaseActor(int xPos, int yPos)
            : base(xPos, yPos)
        {
            score = 0;
            actorName="BaseActor";
            actorType = "BaseActor";
            acceleration.Y = Globals.GRAVITY;

            itemsThatCanKill = new List<string>();
            actorsThatCanCollectWhenDead = new List<string>();

            price = 0.99f;
            lockedForSelection = true;

        }

        public override void hurt(float Damage)
        {
            //Console.WriteLine("Health of {0} - {1}", actorType, health);

            base.hurt(Damage);
        }

        public override void reset(float X, float Y)
        {
            dead = false;
            visible = true;
            exists = true;
            color = Color.White;
            if (X>FlxG.width/2)
                velocity.X = maxVelocity.X * -1;
            else
                velocity.X = maxVelocity.X;
            base.reset(X, Y);
            health = 1;
        }

        public override void overlapped(FlxObject obj)
        {
            if (itemsThatCanKill.Contains( obj.GetType().ToString() ))
            {
                if (!dead)
                {
                    //Console.WriteLine("I am a {0} itemsThatCanKill Not dead, obj: {1}", GetType().ToString() ,obj.GetType().ToString());

                    Globals.arrowCombo++;

                    ((FlxSprite)obj).kill();
                    hurt(1);
                }
            }
            if (actorsThatCanCollectWhenDead.Contains( obj.GetType().ToString()))
            {
                if (dead)
                {
                    //Console.WriteLine("I am a {0} actorsThatCanCollectWhenDead Dead, obj: {1}", GetType().ToString(), obj.GetType().ToString());

                    Globals.numberOfEnemiesToKillBeforeLevelOver--;

                    exists = false;

                    FlxG.play("sfx/Pickup_Coin25", 0.75f);

                    FlxG.quake.start(0.008f, ((20 - Globals.numberOfEnemiesToKillBeforeLevelOver) * 0.1f) * 0.2f);
                }
                else
                {
                    //Console.WriteLine("I am a {0} actorsThatCanCollectWhenDead Not dead, obj: {1}", GetType().ToString(), obj.GetType().ToString());

                    ((FlxSprite)obj).hurt(1);
                }
            }

            

            base.overlapped(obj);
        }
        override public void update()
        {
            if (dead)
            {
                color = new Color(1.0f, 0.75f, 0.75f);
            }

            hurtTimer += FlxG.elapsed;
            
            base.update();

            if (FlxGlobal.cheatString != null)
            {
                if (FlxGlobal.cheatString.StartsWith("control" + actorType))
                {
                    FlxG.write("Controlling " + actorType);

                    isPlayerControlled = true;

                    FlxGlobal.cheatString = "";

                    FlxG.follow(this, 7.0f);
                }
            }

        }


    }
}
