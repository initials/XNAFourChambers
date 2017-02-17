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
    class Cloud : FlxSprite
    {
        public Cloud(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic("fourchambers/cloud");

            if (xPos < FlxG.width / 2)
            {
                acceleration.X = FlxU.random(-50, -130);
            }
            else
            {
                acceleration.X = FlxU.random(50, 130);
            }
        }

        /// <summary>
        /// The Update Cycle. Called once every cycle.
        /// </summary>
        override public void update()
        {
            base.update();
        }

    }
}