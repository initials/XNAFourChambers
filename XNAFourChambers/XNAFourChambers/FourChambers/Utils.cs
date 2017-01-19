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
    public class Utils
    {
        public static int LimitToRange(int value, int inclusiveMinimum, int inclusiveMaximum)
        {
            if (value < inclusiveMinimum) { return inclusiveMinimum; }
            if (value > inclusiveMaximum) { return inclusiveMaximum; }
            return value;
        }
        public static void zoomIn()
        {
            FlxG.zoom = Globals.gameSizeGlobals["zoomCloseUp"];
            FlxG.width = Globals.gameSizeGlobals["widthCloseUp"];
            FlxG.height = Globals.gameSizeGlobals["heightCloseUp"];
            FlxG._game.Initialize();
        }
        public static void zoomOut()
        {
            FlxG.zoom = Globals.gameSizeGlobals["zoom"];
            FlxG.width = Globals.gameSizeGlobals["width"];
            FlxG.height = Globals.gameSizeGlobals["height"];
            FlxG._game.Initialize();
        }
    }
}

