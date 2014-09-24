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
    class Hill : FlxTileblock
    {

        public Hill(int xPos, int yPos)
            : base(xPos, yPos, 2560, 256)
        {
            width = 2560;
            height = 256;

            auto = RANDOM;
            //loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/bghill"), true, false, 256, 256);
            loadTiles(FlxG.Content.Load<Texture2D>("fourchambers/bghill"), 256,256,0);

        }

        override public void update()
        {

            if (FlxG.keys.H)
            {
                y -= 5;
                Console.WriteLine("Y {0}", y);
            }

            base.update();
        }


    }
}
