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
    class PowerUpThrower : FlxSprite
    {

        public PowerUpThrower(int xPos, int yPos)
            : base(xPos, yPos)
        {
            visible = false;

        }

        override public void update()
        {


            base.update();

        }


    }
}
