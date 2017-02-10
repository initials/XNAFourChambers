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
    class Hud : FlxGroup
    {
        private FlxText pestsRemainingNumberText;
        private FlxText pestsRemainingText;



        public Hud()
            : base()
        {
            
            pestsRemainingNumberText = new FlxText((FlxG.width / 2) - 16 , 10, 50);
            pestsRemainingNumberText.setFormat(FlxG.Content.Load<SpriteFont>("ui/BetterPixels"), 2, Color.White, FlxJustification.Left, Color.Black);
            add(pestsRemainingNumberText);

            pestsRemainingText = new FlxText((FlxG.width / 2) - 16, 1, 50);
            pestsRemainingText.setFormat(FlxG.Content.Load<SpriteFont>("ui/BetterPixels"), 1, Color.White, FlxJustification.Left, Color.Black);
            add(pestsRemainingText);
            pestsRemainingText.text = "Collect\n\nMore Pests";






        }

        /// <summary>
        /// The Update Cycle. Called once every cycle.
        /// </summary>
        override public void update()
        {
//           FlxG.setHudText(3, "Time in Level: " + FlxG.elapsedTotal.ToString().Split('.')[0] + " Collect " + Globals.numberOfEnemiesToKillBeforeLevelOver.ToString() + " more pests. Arrow Combo: " + Globals.arrowCombo);

            if (Globals.numberOfEnemiesToKillBeforeLevelOver >= 1)
                pestsRemainingNumberText.text = Globals.numberOfEnemiesToKillBeforeLevelOver.ToString();
            else
            {
                pestsRemainingText.text = "Don't work for free. Clock out.";
                pestsRemainingNumberText.text = "";

                pestsRemainingText.x = FlxU.randomInt((FlxG.width / 2) - 17, (FlxG.width / 2) - 15);
                pestsRemainingText.y = FlxU.randomInt(9,12);



            }

            base.update();
        }
    }
}