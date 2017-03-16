using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Reflection;

using XNATweener;

namespace FourChambers
{
    /* FlxSprite > Base Actor > Actor > Marksman
     *                        > EnemyActor > Bat
     * 
     */ 
    public class BaseActor : FlxSprite
    {
        /// <summary>
        /// Character's name;
        /// </summary>
        public string actorName;

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

        public List<string> thingsThatHaveHappenedToThisActor;

        public float price;
        public bool lockedForSelection;
        public int releaseTime = 0;

        public Tweener colorFlasher;

        public string customAnimation;

        public bool isRespawnable;

        public bool lockToOnScreen = false;

        public string deathSound = "";

        private int resetTimer = 100000;

        public int resetDurationToWait = 1;

        private float resetStorageX = 0.0f;

        private float resetStorageY = 0.0f;



        /// <summary>
        /// The base for Actors. Should remain pretty empty.
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        public BaseActor(int xPos, int yPos)
            : base(xPos, yPos)
        {
            isRespawnable = true;

            score = 0;
            actorName="BaseActor";

            acceleration.Y = Globals.GRAVITY;

            itemsThatCanKill = new List<string>();
            actorsThatCanCollectWhenDead = new List<string>();

            price = 0.99f;
            lockedForSelection = true;

            thingsThatHaveHappenedToThisActor = new List<string>();

            colorFlasher = new Tweener(0, 1, 1, Linear.EaseInOut);
            colorFlasher.PingPong = true;
            colorFlasher.Start();

            customAnimation = null;
            lockToOnScreen = false;

            resetTimer = 100000;
            resetDurationToWait = 1;

        }

        public override void kill()
        {

            Console.WriteLine("Kill: {0} ", GetType().ToString());

            if (deathSound != null && deathSound != "")
            {
                Console.WriteLine("Death Sound! : {0} ", deathSound);
                FlxG.play(deathSound);

            }

            colorFlasher.Start();

            base.kill();
        }

        public override void hurt(float Damage)
        {

            Console.WriteLine("Hurt: {0} ", GetType().ToString());

            if (deathSound != null && deathSound != "")
            {
                Console.WriteLine("Death Sound! : {0} ", deathSound);
                FlxG.play(deathSound);

            }

            base.hurt(Damage);
        }

        public void resetIn(float X, float Y, int Duration)
        {
            //Console.WriteLine(Duration);
            //Console.WriteLine("Reseting {0} -> {1}", resetTimer, resetDurationToWait);

            dead = false;
            visible = true;
            exists = true;

            this.resetTimer = 0;
            this.resetStorageX = X;
            this.resetStorageY = Y;
            this.resetDurationToWait = Duration;

            //Console.WriteLine("Reseting {0} -> {1}", resetTimer, resetDurationToWait);
        }

        public override void reset(float X, float Y)
        {
            base.reset(X, Y - (height-16));

            dead = false;
            visible = true;
            exists = true;

            color = Color.White;

            if (X >= FlxG.levelWidth / 2)
            {
                velocity.X = maxVelocity.X * -1;
            }
            else
            {
                velocity.X = maxVelocity.X ;
            }
                
            health = 1;

        }

        public override void overlapped(FlxObject obj)
        {
            /*
             * This checks if a custom overlap method has been written
             * For example a Zinger may have overlapWithBat(FlxObject obj) 
             * 
             * public void overlapWithObjectName(FlxObject obj)
             */
            if (GetType().GetMethod("overlapWith" + obj.GetType().ToString().Split('.')[1]) != null)
            {
                Type thisType = this.GetType();
                MethodInfo theMethod = thisType.GetMethod("overlapWith" + obj.GetType().ToString().Split('.')[1]);
                theMethod.Invoke(this, new object[] { obj });
            }

            if (itemsThatCanKill.Contains( obj.GetType().ToString() ))
            {
                if (!dead)
                {
                    Globals.arrowCombo++;

                    ((FlxSprite)obj).kill();
                    hurt(1);
                }
            }
            if (actorsThatCanCollectWhenDead.Contains( obj.GetType().ToString()))
            {
                if (dead)
                {
                    Globals.numberOfEnemiesToKillBeforeLevelOver--;

                    exists = false;

                    FlxG.play("sfx/Pickup_Coin25", 0.75f);

                    FlxG.quake.start(0.008f, ((20 - Globals.numberOfEnemiesToKillBeforeLevelOver) * 0.1f) * 0.2f);

                    SingleScreenLevel.hud.startTween();
                }
                else
                {
                    ((FlxSprite)obj).hurt(1);
                }
            }

            base.overlapped(obj);
        }
        override public void update()
        {
            
            //if (resetTimer<25)
            //    Console.WriteLine("Reseting {0} -> {1}", resetTimer, resetDurationToWait);

            if (resetTimer == resetDurationToWait)
            {

                
                
                reset(resetStorageX, resetStorageY);
            }
            resetTimer += 1;



            if (lockToOnScreen)
            {
                if (x < 3) x = 3;
                if (x > FlxG.width-16) x = FlxG.width-16;

            }
            colorFlasher.Update(FlxG.elapsedAsGameTime);

            if (dead)
            {
                color = new Color(1, colorFlasher.Position, colorFlasher.Position) ;
            }

            hurtTimer += FlxG.elapsed;
            
            base.update();

            if (FlxGlobal.cheatString != null)
            {
                if (FlxGlobal.cheatString.StartsWith("control" + GetType().ToString().Split('.')[1]))
                {
                    FlxG.write("Controlling " + GetType().ToString().Split('.')[1]);

                    isPlayerControlled = true;

                    FlxGlobal.cheatString = "";

                    //FlxG.follow(this, 7.0f);
                }
            }
        }
    }
}
