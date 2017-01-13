/*
 * Add these to Visual Studio to quickly create new FlxStates
 */

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;
using System.Linq;
using System.Xml.Linq;
namespace FourChambers
{
    public class FireflyGroup : FlxGroup
    {
        public FireflyGroup()
            : base()
        {

            //plots down some clusters of fireflies.
            for (int i = 0; i < 5; i++)
            {
                int xp = (int)FlxU.random(0, FlxG.levelWidth);
                int yp = (int)FlxU.random(0, FlxG.levelHeight);

                for (int j = 0; j < 25; j++)
                {
                    Firefly f = new Firefly(xp + (int)FlxU.random(-30, 30), yp - (int)FlxU.random(-30, 30));
                    add(f);
                    //f.color = FlxColor.ToColor(levelAttrs["fireflyColor"]);
                }
            }
        }
    }
}
        