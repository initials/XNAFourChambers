#region File Description
//-----------------------------------------------------------------------------
// Flixel for XNA.
// Original repo : https://github.com/StAidan/X-flixel
// Extended and edited repo : https://github.com/initials/XNAMode
//-----------------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;

namespace Loader_Four
{
    /// <summary>
    /// Flixel enters here.
    /// <code>FlxFactory</code> refers to it as the "masterclass".
    /// </summary>
    public class FlixelEntryPoint2 : FlxGame
    {
        public FlixelEntryPoint2(Game game)
            : base(game)
        {
            /*
            Post build zipper
            cd ..
            C:\_Files\programs\7-Zip\7z a -tzip FourChambers.zip Release\ -r
            */
            int w = FlxG.resolutionWidth / FlxG.zoom;
            int h = FlxG.resolutionHeight / FlxG.zoom;
            FourChambers.Globals.startGame();

            initGame(w, h, new FourChambers.CharacterSelectScreen(), new Color(15, 15, 15), true, new Color(5, 5, 5));
            
            //initGame(w, h, new FourChambers.SingleScreenLevel(), new Color(15, 15, 15), true, new Color(5, 5, 5));

            //initGame(w, h, new FourChambers.AnimationCycleState(), new Color(15, 15, 15), true, new Color(5, 5, 5));




            FlxG.debug = false;

            FourChambers.Globals.gif = true;
            FourChambers.Globals.BUILD_TYPE = FourChambers.Globals.BUILD_TYPE_RELEASE;
            FourChambers.Globals.DEMO_VERSION = false;
            FourChambers.Globals.PIRATE_COPY = false;
            FlxG.level = 1;
            FlxG.score = 0;

            FlxG.hudFont = FourChambers.Globals.HUD_FONT;

#if DEBUG
            FlxG.level = 1;
            FlxG.debug = true;
#endif
#if DEMO
            FourChambers_Globals.BUILD_TYPE = FourChambers_Globals.BUILD_TYPE_RELEASE;
            FourChambers_Globals.DEMO_VERSION = true;
#endif
#if FULL
            FourChambers_Globals.gif = false;
            FourChambers_Globals.BUILD_TYPE = FourChambers_Globals.BUILD_TYPE_RELEASE;
            FourChambers_Globals.DEMO_VERSION = false;
            FourChambers_Globals.PIRATE_COPY = false;
#endif
#if PIRATE
            FourChambers.FourChambers_Globals.PIRATE_COPY = true;
#endif
#if PRESS
            FourChambers.FourChambers_Globals.BUILD_TYPE = FourChambers_Globals.BUILD_TYPE_PRESS;
#endif
#if MAKEGIF
            FourChambers.FourChambers_Globals.gif = true;
#endif

            Console.WriteLine("Beggining Game at FlxG.width/height {0} x {1}", FlxG.width, FlxG.height);

            FourChambers.Globals.gameSizeGlobals.Add("zoom", FlxG.zoom);
            FourChambers.Globals.gameSizeGlobals.Add("zoomCloseUp", FlxG.zoom*3);

            FourChambers.Globals.gameSizeGlobals.Add("width", FlxG.width);
            FourChambers.Globals.gameSizeGlobals.Add("height", FlxG.height);

            FourChambers.Globals.gameSizeGlobals.Add("widthCloseUp", FlxG.width/3);
            FourChambers.Globals.gameSizeGlobals.Add("heightCloseUp", FlxG.height/3);


        }
    }
}
