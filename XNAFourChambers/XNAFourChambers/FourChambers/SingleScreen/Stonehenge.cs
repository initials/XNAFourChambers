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
    public class Stonehenge : FlxSprite
    {

        public Stonehenge(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic("fourchambers/Stonehenge_100x100", true, false, 100, 100);

            width = 32;
            height = 32;
            setOffset(34, 68);

            addAnimation("stay", new int[] { 0 }, 12, false);
            addAnimation("fall", new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, 6, true);

            play("stay");
        }

        override public void update()
        {
            base.update();
        }

    }
}