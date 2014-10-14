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
    class Heart : FlxSprite
    {

        public Heart(int xPos, int yPos)
            : base(xPos, yPos)
        {
            setScrollFactors(0, 0);

            loadGraphic("fourchambers/heart_16x16", true, false, 16, 16);

            loadAnimationsFromGraphicsGaleCSV("content/fourchambers/heart_16x16.csv", null, null, false);

            play("full");
        }

        override public void update()
        {

            if (health >= 4) play("full");
            else if (health == 3) play("threequarter");
            else if (health == 2) play("half");
            else if (health == 1) play("quarter");
            else if (health == 0) play("empty ");

            base.update();

        }


    }
}
