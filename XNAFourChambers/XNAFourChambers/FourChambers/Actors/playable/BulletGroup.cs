/*
 * Add these to Visual Studio to quickly create new FlxSprites
 */

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
    class BulletGroup : FlxGroup
    {

        public BulletGroup()
            : base()
        {
            for (int i = 0; i < 20; i++)
            {
                Arrow arrow = new Arrow(-1000, 1000);
                add(arrow);
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
