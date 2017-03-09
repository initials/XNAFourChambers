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
    class Door : FlxSprite
    {

        public int levelToGoTo;
        static public int debug_GoToLevel = 0;
        public FlxEmitter sparkles;

        public Door(int xPos, int yPos)
            : base(xPos, yPos)
        {
            renderOrder = 999;

            Texture2D Img = FlxG.Content.Load<Texture2D>("fourchambers/door_32x32");

            loadGraphic(Img, false, false, 32, 32);

            levelToGoTo = 1;

            addAnimation("idle", new int[] { 1 }, 10, false);
            addAnimation("pulse", new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 8, 9, 8, 9, 8, 9, 8, 0, 9, 8, 9, 8, 0, 9, 8, 9, 8, 0, 9, 8, 9, 8, 0, 9, 8, 9, 8, 0, 9, 8, 9, 8, 0, 9, 8, 9, 8, 0, 9, 8, 9, 8, 0, 9, 8, 9, 8, 0, }, 24, true);

            addAnimationCallback(pulse);

            play("idle");

            sparkles = new FlxEmitter();
            sparkles.setSize(6, 12);
            sparkles.setRotation();
            sparkles.setXSpeed(-255, 255);
            sparkles.setYSpeed(-255, 0);
            sparkles.gravity = Globals.GRAVITY / 10;
            sparkles.createSprites(FlxG.Content.Load<Texture2D>("fourchambers/doorSparkles3x3"), 75, true);
            sparkles.at(this);
            sparkles.x += 14;
            sparkles.y += 8;





        }
        public void pulse(string Name, uint Frame, int FrameIndex)
        {
            
            if (Name == "pulse" && FrameIndex == 4)
            {
                if (onScreen() )
                    FlxG.play("sfx/Door", 0.525f, false);
            }
            if (Name == "pulse" && Frame == 40)
            {
                //Console.WriteLine("Overlapped {0} {1}", Name, Frame);

                play("idle");
            }
        }

        public override void overlapped(FlxObject obj)
        {
            //Console.WriteLine("Overlapped {0} {1}", _curAnim.name, _curFrame);

            string overlappedWith = obj.GetType().ToString();

            if (overlappedWith == "FourChambers.Marksman")
            {

                if (Globals.numberOfEnemiesToKillBeforeLevelOver <= 0 && FlxControl.UP && obj.dead==false)
                {
                    obj.visible = false;
                    sparkles.start(false, 0.01f, 0);

                    //FlxG.bloom.Visible = true;
                    //FlxG.bloom.Settings = BloomPostprocess.BloomSettings.PresetSettings[3];

                    play("pulse");

                    if (FlxG.level == 7)
                    {
                        FlxG.fade.start(Color.Black, Globals.FADE_OUT_TIME, goToCharacterSelectScreen, false);
                    }
                    else
                    {
                        FlxG.fade.start(Color.Black, Globals.FADE_OUT_TIME, goToNextLevel, false);

                    }
                    return;
                }
                if (Globals.numberOfEnemiesToKillBeforeLevelOver <= 0 && FlxControl.DOWN)
                {
                    play("pulse");
                    FlxG.level--;
                    FlxG.level = Utils.LimitToRange(FlxG.level, 1, 7);
                    FlxG.state = new SingleScreenLevel();
                    return;
                }

            }

            base.overlapped(obj);
        }

        private static void goToNextLevel(object sender, FlxEffectCompletedEvent e)
        {
            FlxG.level++;
            FlxG.level = Utils.LimitToRange(FlxG.level, 1, 7);
            FlxG.state = new SingleScreenLevel();
            return;
        }

        private static void goToCharacterSelectScreen(object sender, FlxEffectCompletedEvent e)
        {
            FlxG.level = 1;
            FlxG.state = new CharacterSelectScreen();
            return;
        }

        override public void update()
        {
            if (Globals.numberOfEnemiesToKillBeforeLevelOver <= 0)
            {
                //FlxG.bloom.Visible = true;
                //FlxG.bloom.Settings = BloomPostprocess.BloomSettings.PresetSettings[1];

                play("pulse");
                
                
            }

            sparkles.update();
            base.update();

        }
        public override void render(SpriteBatch spriteBatch)
        {

            base.render(spriteBatch);
            sparkles.render(spriteBatch);
        }


    }
}
