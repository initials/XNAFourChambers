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
    class Spike : FlxTileblock
    {

        public Spike(int xPos, int yPos, int Width, int Height)
            : base(xPos, yPos, Width,Height)
        {
            auto = FlxTileblock.OFF;

            loadTiles("fourchambers/spike", 16, 16, 0);

        }

        override public void update()
        {


            base.update();

        }


    }
}
