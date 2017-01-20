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
    class Sign : FlxSprite
    {
        public string message;

        public Sign(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic("fourchambers/auto_surt2_16x16", true, false, 16, 16);
            frame = 592;
            message = "";
        }

        /// <summary>
        /// The Update Cycle. Called once every cycle.
        /// </summary>
        override public void update()
        {
            base.update();
        }

        public override void overlapped(FlxObject obj)
        {
            if (obj.GetType().ToString() == "FourChambers.Marksman")
            {
                FlxG.setHudText(3, message);
            }

            base.overlapped(obj);
        }
    }
}
