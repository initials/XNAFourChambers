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
namespace XNAFourChambers.FourChambers.SingleScreen
{
    class Timer : FlxObject
    {
        public float timeInLevel;

        public Timer()
            : base()
        {

        }

        public void reset()
        {
            timeInLevel = 0;
        }

        override public void update()
        {
            timeInLevel += FlxG.elapsed;
            
            base.update();
        }
    }
}