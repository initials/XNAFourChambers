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
    class SpawnPoint : FlxSprite
    {

        public SpawnPoint(int xPos, int yPos)
            : base(xPos, yPos)
        {
            createGraphic(16, 16, Color.Green);
            alpha = 0.1f;
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