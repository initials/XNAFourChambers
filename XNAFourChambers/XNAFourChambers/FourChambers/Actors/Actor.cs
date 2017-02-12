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
    public class Actor : BaseActor
    {
        // A bunch of helpers for the allActors sprite sheet.

        #region Constants for frame numbers
        // Row:  0
        public const int FR_soldier = 0;
        public const int FR_paladin = 1;
        public const int FR_executor = 2;
        public const int FR_savage = 3;
        public const int FR_marksman = 4;
        public const int FR_monk = 5;
        public const int FR_assassin = 6;
        public const int FR_corsair = 7; 
        // Row:  1
        public const int FR_wizard = 8;
        public const int FR_priest = 9;
        public const int FR_warlock = 10;
        public const int FR_druid = 11; 
        public const int FR_merchant = 12;
        public const int FR_mechanic = 13;
        public const int FR_artist = 14; 
        public const int FR_gormet = 15;
        // Row:  2
        public const int FR_chicken = 16;
        public const int FR_rat = 17;
        public const int FR_toad = 18;
        public const int FR_feline = 19;
        public const int FR_sheep = 20;
        public const int FR_cow = 21;
        public const int FR_horse = 22;
        public const int FR_deer = 23;
        // Row:  3
        public const int FR_snake = 24;
        public const int FR_bat = 25;
        public const int FR_zinger = 26;
        public const int FR_spider = 27;
        public const int FR_wolf = 28;
        public const int FR_grizzly = 29;
        public const int FR_lion = 30;
        public const int FR_unicorn = 31;
        // Row:  4
        public const int FR_troll = 32;
        public const int FR_cyclops = 33;
        public const int FR_dwarf = 34;
        public const int FR_goblin = 35;
        public const int FR_ogre = 36;
        public const int FR_nymph = 37;
        public const int FR_mermaid = 38;
        public const int FR_centaur = 39; 
        // Row:  5
        public const int FR_tauro = 40;
        public const int FR_treant = 41;
        public const int FR_fungant = 42;
        public const int FR_zombie = 43;
        public const int FR_mummy = 44;
        public const int FR_deathclaw = 45; 
        public const int FR_bloatedzombie = 46; 
        public const int FR_vampire = 47;
        // Row:  6
        public const int FR_bogbeast = 48; 
        public const int FR_blight = 49;
        public const int FR_willowisp = 50;
        public const int FR_phantom = 51;
        public const int FR_gloom = 52;
        public const int FR_nightmare = 53; 
        public const int FR_skeleton = 54;
        public const int FR_grimwarrior = 55;
        // Row:  7
        public const int FR_harvester = 56;
        public const int FR_lich = 57;
        public const int FR_imp = 58;
        public const int FR_devil = 59;
        public const int FR_mephisto = 60;
        public const int FR_kerberos = 61;
        public const int FR_glutton = 62;
        public const int FR_tormentor = 63;
        // Row:  8
        public const int FR_succubus = 64;
        public const int FR_gorgon = 65;
        public const int FR_ifrit = 66;
        public const int FR_embersteed = 67; 
        public const int FR_seraphine = 68;
        public const int FR_bombling = 69;
        public const int FR_drone = 70; 
        public const int FR_mimick = 71;
        // Row:  9
        public const int FR_automaton = 72; 
        public const int FR_golem = 73;
        public const int FR_gelantine = 74; 
        public const int FR_floatingeye = 75; 
        public const int FR_prism = 76;
        public const int FR_djinn = 77;
        public const int FR_sphinx = 78;
        public const int FR_chimera = 79;
        #endregion

        /// <summary>
        /// Deadzone for the joystick on this character.
        /// </summary>
        public const float DEADZONE = 0.5f;

        //public int runSpeed = 120;

        public List<FlxObject> _bullets;

        public FlxGroup allProjectiles;

        public int _curBullet;
        /// <summary>
        /// Tells the Actor whether to play the attack animation
        /// </summary>
        public bool attackingMouse = false;

        /// <summary>
        /// Tells the Actor whether to play the attack animation
        /// </summary>
        public bool attackingJoystick = false;


        public bool attackingMelee = false;

        public bool hasRangeWeapon = true;
        public bool hasMeleeWeapon = true;

        /// <summary>
        /// used for tracking the amount of time dead for restarts.
        /// </summary>
        public float timeDead;

        /// <summary>
        /// holds the amount of time character has been jumping.
        /// </summary>
        private float _jump = 0.0f;
        /// <summary>
        /// How high the character will jump.
        /// </summary>
        private float _jumpPower = -180.0f;

        private float _jumpInitialPower = -140.0f;

        private float _jumpMaxTime = 0.25f;

        private float _jumpInitialTime = 0.065f;

        /// <summary>
        /// How many frames have passed since the character left the ground.
        /// </summary>
        public float framesSinceLeftGround;

        public string lastAttack = "range";

        public FlxEmitter grass;
        private bool _wasInAir = false;

        /// <summary>
        /// An actor at x,y
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        public Actor(int xPos, int yPos)
            : base(xPos,yPos)
		{
            //basic player physics
            
            //drag.X = runSpeed * 4;
            //drag.Y = runSpeed * 4;
            acceleration.Y = Globals.GRAVITY;
            maxVelocity.X = runSpeed;
            maxVelocity.Y = 1000;

            isPlayerControlled = false;
            health = Globals.health;

            grass = new FlxEmitter(xPos, yPos);
            grass.createSprites("fourchambers/grassParticles", 50, true, 0.0f, 0.0f);
            grass.setXSpeed(-10, 10);
            grass.setYSpeed(-20, 0);
            grass.start(false, 0.01f,1);
            grass.setSize(5, 1);
            grass.gravity = 22;

            debugName = "";

        }

        public void stopAttacking(string Name, uint Frame, int FrameIndex)
        {
            //attacking = false;
        }

        public void updateInputs()
        {
            PlayerIndex pi;

            //if (playerIndex != PlayerIndex.One)
            //{
            //    return;
            //}
            
            // Running pushes walk speed higher.
            if (FlxG.gamepads.isButtonDown(Buttons.RightTrigger, playerIndex, out pi) )
            {
                lastAttack = "range";
                maxVelocity.X = runSpeed * 2;
                attackingMelee = false;
            }
            else
            {
                maxVelocity.X = runSpeed;
                
            }

            //
            acceleration.X = 0;

            // Walking left.
            if (((FlxG.keys.A && playerIndex == PlayerIndex.One) || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickLeft, playerIndex, out pi) || FlxG.gamepads.isButtonDown(Buttons.DPadLeft, playerIndex, out pi)) && !isClimbingLadder)
            {
                lastAttack = "range";
                attackingJoystick = false;
                attackingMouse = false;
                facing = Flx2DFacing.Left;
                acceleration.X -= drag.X;
                attackingMelee = false;
                debugName = "";
            }
            //Walking right.
            else if (((FlxG.keys.D && playerIndex == PlayerIndex.One) || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickRight, playerIndex, out pi) || FlxG.gamepads.isButtonDown(Buttons.DPadRight, playerIndex, out pi)) && !isClimbingLadder)
            {
                lastAttack = "range";
                attackingJoystick = false;
                attackingMouse = false;
                facing = Flx2DFacing.Right;
                acceleration.X += drag.X;
                attackingMelee = false;
                debugName = "";
            }

            // ladders
            if (((FlxG.keys.W && playerIndex == PlayerIndex.One) || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickUp, playerIndex, out pi) || FlxG.gamepads.isButtonDown(Buttons.DPadUp, playerIndex, out pi)) && canClimbLadder && !FlxG.gamepads.isButtonDown(Buttons.A, playerIndex, out pi))
            {
                lastAttack = "range";
                x = ladderPosX + width;

                velocity.Y = -100;
                isClimbingLadder = true;
                attackingMelee = false;

                debugName = "";

                // on a ladder, snap to nearest 16
            }
            else if (((FlxG.keys.S && playerIndex == PlayerIndex.One) || FlxG.gamepads.isButtonDown(Buttons.LeftThumbstickDown, playerIndex, out pi) || FlxG.gamepads.isButtonDown(Buttons.DPadDown, playerIndex, out pi)) && canClimbLadder && !FlxG.gamepads.isButtonDown(Buttons.A, playerIndex, out pi))
            {
                lastAttack = "range";
                x = ladderPosX + width;

                velocity.Y = 100;
                isClimbingLadder = true;
                attackingMelee = false;

                debugName = "";

            }
            else
            {
                //isClimbingLadder = false;
            }

            // Jumping. 
            if ((_jump >= 0 || framesSinceLeftGround < 10 || isClimbingLadder) && (((FlxG.keys.W && playerIndex == PlayerIndex.One) && canFly == false && !isClimbingLadder) || (FlxG.keys.SPACE && playerIndex == PlayerIndex.One) || FlxG.gamepads.isButtonDown(Buttons.A, playerIndex, out pi)))
            {
                lastAttack = "range";
                if (framesSinceLeftGround < 10)
                {
                    _jump = 0.0f;
                    framesSinceLeftGround = 10000;
                }
                if (isClimbingLadder)
                {
                    _jump = 0.0f;
                    isClimbingLadder = false;
                }

                attackingJoystick = false;
                attackingMouse = false;
                attackingMelee = false;

                _jump += FlxG.elapsed;
                if (_jump > _jumpMaxTime) _jump = -1;
            }
            else
            {
                _jump = -1;
            }
            if (_jump > 0)
            {
                if (_jump < _jumpInitialTime)
                    velocity.Y = _jumpInitialPower;
                else
                    velocity.Y = _jumpPower;
            }
            
            //Console.WriteLine("jump= " + _jump + " " + canClimbLadder);

            // Attacking
            if (FlxG.keys.justPressed(Keys.M) && playerIndex == PlayerIndex.One)
            {
                lastAttack = "range";

                attackingMouse = true;
                attackingMelee = false;
            }
            if (hasRangeWeapon && FlxG.gamepads.isNewButtonPress(Buttons.RightShoulder, playerIndex, out pi))
            {
                lastAttack = "range";

                attackingJoystick = true;
                attackingMelee = false;
            }


            //FlxG.gamepads.isButtonDown(Buttons.X, playerIndex, out pi)

            if (((GamePad.GetState(playerIndex).ThumbSticks.Right.X > DEADZONE ||
                GamePad.GetState(playerIndex).ThumbSticks.Right.Y > DEADZONE ||
                GamePad.GetState(playerIndex).ThumbSticks.Right.X < DEADZONE * -1.0f ||
                GamePad.GetState(playerIndex).ThumbSticks.Right.Y < DEADZONE * -1.0f) &&

                FlxG.gamepads.isNewButtonPress(Buttons.RightShoulder, playerIndex, out pi)

                ) && hasRangeWeapon)
            {
                lastAttack = "range";
                attackingJoystick = true;
                attackingMelee = false;
            }
            //else
            //{
            //    attackingJoystick = false;
            //}

            // Attacking using mouse.
            if ( ( FlxG.mouse.justPressedLeftButton() && playerIndex == PlayerIndex.One)  && hasRangeWeapon)
            {
                lastAttack = "range";
                attackingMouse = true;
                attackingMelee = false;
            }

            // Attacking using mouse.
            //if ()
            //{
            //    lastAttack = "melee";
            //    attackingMouse = false;
            //    attackingMelee = true;
            //}

            //else
            //{
            //    attackingMouse = false;
            //}

            if (hasMeleeWeapon && ((FlxG.keys.O && playerIndex == PlayerIndex.One) || FlxG.gamepads.isButtonDown(Buttons.X, playerIndex, out pi) || FlxG.mouse.pressedRightButton()))
            {
                lastAttack = "melee";
                attackingMelee = true;
            }


            if ((FlxG.keys.C && playerIndex == PlayerIndex.One) && hasRangeWeapon)
            {
                lastAttack = "range";
                attackingMouse = true;
                attackingMelee = false;
            }

            // update direction based on attacking direction.
            if (FlxG.gamepads.isButtonDown(Buttons.RightThumbstickLeft, playerIndex, out pi))
            {
                facing = Flx2DFacing.Left;
            }
            if (FlxG.gamepads.isButtonDown(Buttons.RightThumbstickRight, playerIndex, out pi))
            {
                facing = Flx2DFacing.Right;
            }
            // TO DO: Same above for mouse.

        }

        public void updateAnims()
        {
            List<FlxAnim> anim = animations.Where(p => p.name == debugName).ToList();

            if (customAnimation != null)
            {
                play(customAnimation);
            }
            else if (dead)
            {
                play("death");
            }
            else if (hurtTimer < timeDownAfterHurt)
            {
                play("hurt");
            }
            else if (debugName != "" && anim.Count != 0)
            {
                play(debugName);
            }
            else if (isClimbingLadder)
            {
                if (velocity.Y == 0)
                {
                    play("climbidle");
                }
                else
                {
                    play("climb");
                }
            }
            else if (attackingMouse || attackingJoystick)
            {
                play("attack");
            }
            else if (attackingMelee)
            {
                play("attackMelee");

            }
            else if (velocity.Y != 0)
            {
                if (hasRangeWeapon) play("jumpRange");
                else play("jump");
            }
            else if (velocity.X == 0)
            {
                if (lastAttack == "melee") play("idleMelee");
                else if (hasRangeWeapon) play("idleRange");
                else play("idle");
            }
            else if (FlxU.abs(velocity.X) > 1)
            {
                if (hasRangeWeapon == true) play("runRange");
                else play("run");
            }
            else
            {
                if (lastAttack == "melee") play("idleMelee");
                else if (hasRangeWeapon == true) play("idleRange");
                else play("idle");
            }

            //if (GetType().ToString() == "FourChambers.Warlock")
            //{
            //    Console.WriteLine(_curAnim.name);
            //}
        }

        override public void update()
        {
            if (dead) timeDead += FlxG.elapsed;
            else timeDead = 0;

            Globals.health = health;

            // Calculate how many frames since the player left the ground

            if (onFloor)
            {
                framesSinceLeftGround = 0;
            }
            else
            {
                framesSinceLeftGround++;
            }

            if (isClimbingLadder || customAnimation!=null)
            {
                acceleration.Y = 0;
            }
            else
            {
                acceleration.Y = Globals.GRAVITY;
            }

            //MOVEMENT

            if (isPlayerControlled && hurtTimer > timeDownAfterHurt)
            {
                updateInputs();
            }
            //else // is not player controlled.
            //{
            //    acceleration.Y = 0;
            //}

            if (hurtTimer < timeDownAfterHurt)
            {
                acceleration.X = 0;
                velocity.X = 0;
            }

            //ANIMATION
            updateAnims();

            grass.at(this);
            
            grass.x -= 9;
            grass.y -= 2;
            
            grass.update();

            if ((onFloor && Math.Abs(velocity.X) > 0))
            {
                //grass.start(false, 0.01f, 2);
                //Console.WriteLine("On Floor");

                
            }
            else if (_wasInAir && onFloor)
            {
                grass.start(true, 0.5f, 12);
                //Console.WriteLine("On Floor/Was in Air");


                _wasInAir = false;
            }
            else
            {
                grass.stop();
            }





            if (framesSinceLeftGround > 1)
            {
                _wasInAir = true;
            }
            base.update();

            // Just landed.
            //if (_wasInAir && framesSinceLeftGround == 2)
            //{
            //    _wasInAir = false;

            //}
            

            if (canClimbLadder == false) isClimbingLadder = false;
            canClimbLadder = false;



        }

        public override void hitBottom(FlxObject Contact, float Velocity)
        {
            _jump = 0.0f;
            isClimbingLadder = false;
            base.hitBottom(Contact, Velocity);
        }

        override public void hitSide(FlxObject Contact, float Velocity)
        {


            if (!isPlayerControlled)
                velocity.X = velocity.X * -1;

            base.hitSide( Contact,  Velocity);
        }

        public override void hurt(float Damage)
        {
            if (Globals.invincible==false)
            {
                velocity.X = 0;

                hurtTimer = 0.0f;

                colorFlicker(timeDownAfterHurt * 2);

                base.hurt(Damage);
            }
        }

        public override void kill()
        {
            if (dead == false)
                FlxG.write(GetType().ToString() + " is dead. ");

            play("death");

            dead = true;

            //flicker(1.5f);

            isPlayerControlled = false;

            velocity.X = 0;
            acceleration.X = 0;

            //base.kill();

        }

        public override void render(SpriteBatch spriteBatch)
        {
            

            base.render(spriteBatch);
            grass.render(spriteBatch);
        }


    }

}
