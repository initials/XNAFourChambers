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
        private FlxText pestsRemainingBelowText;


        private FlxText pressToRestart;

        private Tweener scaleTween;
        private Tweener rotationTween;
        private bool shouldFollowTween;

        public Hud()
            : base()
        {
            scrollFactor.X = 0;
            scrollFactor.Y = 0;

            pestsRemainingNumberText = new FlxText((FlxG.width / 2) - 16 , 10, 50);
            pestsRemainingNumberText.setFormat(FlxG.Content.Load<SpriteFont>(Globals.HUD_FONT), 2, Color.White, FlxJustification.Left, Color.Black);
            add(pestsRemainingNumberText);

            pestsRemainingText = new FlxText((FlxG.width / 2) - 32, 1, 50);
            pestsRemainingText.setFormat(FlxG.Content.Load<SpriteFont>(Globals.HUD_FONT), 1, Color.White, FlxJustification.Center, Color.Black);

            add(pestsRemainingText);
            pestsRemainingText.text = "Collect";


            pestsRemainingBelowText = new FlxText((FlxG.width / 2) - 32, pestsRemainingNumberText.y + 20, 50);
            pestsRemainingBelowText.setFormat(FlxG.Content.Load<SpriteFont>(Globals.HUD_FONT), 1, Color.White, FlxJustification.Center, Color.Black);

            add(pestsRemainingBelowText);
            pestsRemainingBelowText.text = "More Pests";


            //pressToRestart = new FlxText(4, 4, 150);
            //pressToRestart.setFormat(FlxG.Content.Load<SpriteFont>(Globals.HUD_FONT), 1, Color.White, FlxJustification.Left, Color.Black);
            //add(pressToRestart);
            //pressToRestart.text = "Press [R] / (Y) to Restart";
            //pressToRestart.visible = false;
            //pressToRestart.setScrollFactors(0, 0);


            scaleTween = new Tweener(4, 2, 1.4f, Bounce.EaseOut);
            scaleTween.Reset();

            rotationTween = new Tweener(720, 0, 0.5f, Quadratic.EaseIn);
            rotationTween.Reset();

            for (int i = 0; i < Globals.numberOfEnemiesToKillBeforeLevelOver; i++)
            {
                EnemiesRemainingIndicator e = new EnemiesRemainingIndicator(1 + (4 * i), 1);
                e.setScrollFactors(0, 0);
                add(e);
                e.scale = 0.25f;
            }

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

            int count = 0;
            foreach (var item in members)
            {
                if (item.GetType().ToString() == "FourChambers.EnemiesRemainingIndicator")
                {
                    if (count >= Globals.numberOfEnemiesToKillBeforeLevelOver)
                    {
                        ((FlxSprite)(item)).play("dead");

                    }
                    count++;
                }
            }
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
                    pestsRemainingText.text = "Your shift is over but your boss called\nand wants you to come in early tomorrow.";
                    pestsRemainingBelowText.text = "";
                    pestsRemainingNumberText.text = "";
                }
                else
                {
                    pestsRemainingText.text = "Don't work for free. Clock out.";
                    pestsRemainingBelowText.text = "";
                    pestsRemainingNumberText.text = "";

                    pestsRemainingText.x = FlxU.randomInt((FlxG.width / 2) - 17, (FlxG.width / 2) - 15);
                    pestsRemainingText.y = FlxU.randomInt(9, 12);
                }
            }
            if (FlxG.zoom == Globals.gameSizeGlobals["zoomCloseUp"] && FlxG.fade.exists==false)
            {
                pestsRemainingNumberText.text = "";
                pestsRemainingText.text = "";

                FlxG.setHudText(3, "Press [R] / (Y) to Exterminate Pests In Full Costume. Press [ESC] / (Back) to select new costume. ");
                FlxG.setHudTextScale(3, 3);
                FlxG.setHudTextPosition(3, 8, FlxG.height - 16);

                //pressToRestart.visible = true;

                //pressToRestart.visible = true;
                //pressToRestart.x = FlxG.mouse.screenX;
                //pressToRestart.y = FlxG.mouse.screenY;

                //Console.WriteLine("X {0} Y {1}", pressToRestart.x, pressToRestart.y);


                //turn off enemy indicators
                foreach (var item in members)
                {
                    if (item.GetType().ToString() == "FourChambers.EnemiesRemainingIndicator")
                    {
                        ((FlxSprite)(item)).kill();
                    }
                }
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