using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNATweener;

namespace FourChambers
{
    public class Hud : FlxGroup
    {
        private FlxText pestsRemainingNumberText;
        private FlxText pestsRemainingText;

        //private FlxText pressToRestart;

        private Tweener scaleTween;
        private Tweener rotationTween;
        private bool shouldFollowTween;

        public Hud()
            : base()
        {
            scrollFactor.X = 0;
            scrollFactor.Y = 0;

            pestsRemainingNumberText = new FlxText((FlxG.width / 2) - 16 , 10, 50);
            pestsRemainingNumberText.setFormat(FlxG.Content.Load<SpriteFont>("flixel/initials/Munro"), 2, Color.White, FlxJustification.Left, Color.Black);
            add(pestsRemainingNumberText);

            pestsRemainingText = new FlxText((FlxG.width / 2) - 16, 1, 50);
            pestsRemainingText.setFormat(FlxG.Content.Load<SpriteFont>("flixel/initials/Munro"), 1, Color.White, FlxJustification.Center, Color.Black);

            add(pestsRemainingText);
            pestsRemainingText.text = "Collect\n\nMore Pests";



            //pressToRestart = new FlxText(4, FlxG.height-24 , 50);
            //pressToRestart.setFormat(FlxG.Content.Load<SpriteFont>("ui/BetterPixels"), 1, Color.White, FlxJustification.Left, Color.Black);
            //add(pressToRestart);
            //pressToRestart.text = "Press [R] / (Y) to Restart";
            //pressToRestart.visible = false;

            scaleTween = new Tweener(4, 2, 1.4f, Bounce.EaseOut);
            scaleTween.Reset();

            rotationTween = new Tweener(720, 0, 0.5f, Quadratic.EaseIn);
            rotationTween.Reset();

        }

        public void startTween()
        {
            scaleTween.Reset();
            rotationTween.Reset();


            shouldFollowTween = true;
            scaleTween.Start();
            rotationTween.Start();

            

            //pestsRemainingNumberText.scale = 3;
            pestsRemainingNumberText.angle = 45;
        }

        override public void update()
        {
//           FlxG.setHudText(3, "Time in Level: " + FlxG.elapsedTotal.ToString().Split('.')[0] + " Collect " + Globals.numberOfEnemiesToKillBeforeLevelOver.ToString() + " more pests. Arrow Combo: " + Globals.arrowCombo);

            if (Globals.numberOfEnemiesToKillBeforeLevelOver >= 1)
                pestsRemainingNumberText.text = Globals.numberOfEnemiesToKillBeforeLevelOver.ToString();
            else
            {
                if (FlxG.level == 7)
                {
                    //pestsRemainingText.x = FlxU.randomInt((FlxG.width / 2) - 17, (FlxG.width / 2) - 16);
                    //pestsRemainingText.y = FlxU.randomInt(9, 10);

                    pestsRemainingText.text = "Your shift is over but your boss called\nand wants you to come in early tomorrow.";
                }
                else
                {
                    pestsRemainingText.text = "Don't work for free. Clock out.";

                    pestsRemainingNumberText.text = "";

                    pestsRemainingText.x = FlxU.randomInt((FlxG.width / 2) - 17, (FlxG.width / 2) - 15);
                    pestsRemainingText.y = FlxU.randomInt(9, 12);
                }



            }
            if (FlxG.zoom == Globals.gameSizeGlobals["zoomCloseUp"])
            {
                pestsRemainingNumberText.text = "";
                pestsRemainingText.text = "";

                FlxG.setHudText(3, "Press [R] / (Y) to Restart");
                FlxG.setHudTextScale(3, 4);
                FlxG.setHudTextPosition(3, 8, FlxG.height - 8);

                //pressToRestart.visible = true;
                //pressToRestart.x = FlxG.mouse.screenX;
                //pressToRestart.y = FlxG.mouse.screenY;

                //Console.WriteLine("X {0} Y {1}", pressToRestart.x, pressToRestart.y);


            }
            if (shouldFollowTween)
            {
                pestsRemainingNumberText.scale = scaleTween.Position;
                pestsRemainingNumberText.angle = rotationTween.Position;
            }

            scaleTween.Update(FlxG.elapsedAsGameTime);
            rotationTween.Update(FlxG.elapsedAsGameTime);

            base.update();
        }
    }
}