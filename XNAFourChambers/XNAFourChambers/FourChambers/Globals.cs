﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace FourChambers
{
    public class Globals
    {
        public const string GAME_NAME = "The Four Chambers of the Human Heart";

        public const string HUD_FONT = "flixel/initials/Munro";
        public const string TITLE_FONT = "ui/PixelFraktur";

#if DEBUG
        public const float FADE_OUT_TIME = 0.25f;
#else
        public const float FADE_OUT_TIME = 1.25f;
#endif

        public const string SAVE_FILE_NAME = "turbo.z80";

        public const float CHANCE_OF_ZINGER_NEST = 0.18f;

        public const int TILE_SIZE_X = 16;
        public const int TILE_SIZE_Y = 16;

        public const float GRAVITY = 820.0f;

        public const int DEBUG_ZOOM = 2;


        /*
        public static int BUILD_TYPE = 0;

        public const int BUILD_TYPE_RELEASE = 0;
        public const int BUILD_TYPE_PRESS = 1;

        public static bool DEMO_VERSION = false;
        */



        public static int PLAYER_ACTOR = 1;

        public const int PLAYER_MARKSMAN = 1;
        public const int PLAYER_MISTRESS = 2;
        public const int PLAYER_WARLOCK = 3;


        public const string MUSIC_MENU = "FourChambers_Dramatic";
        public const string MUSIC_TUTORIAL = "FourChambers_WarmerMoreSynths";

        public static int previousLevel = 1;

        public static bool hasMeleeWeapon = true;
        public static bool hasRangeWeapon = true;

        //public static int[] collectedTreasures = { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };

        public static List<int> treasuresCollected = new List<int> { };


        public static Dictionary<int, int> treasuresCollectedPersistant = new Dictionary<int, int>();


        public static int[] multiplayerSelectedCharacters = { 0,0,0,0 };


        /// <summary>
        /// Releases a lot of Homing Zingers.
        /// </summary>
        public static bool PIRATE_COPY = false;

        /// <summary>
        /// Keeps track of whether you can use the Seraphine to fly in a level.
        /// </summary>
        public static bool seraphineHasBeenKilled = false;

        /// <summary>
        /// Keeps track of how many things in a row you've hit with arrows.
        /// </summary>
        public static int arrowCombo = 0;

        public static int arrowsFired = 0;

        public static int arrowsHitTarget = 0;

        public static bool canDestroyTerrain = false;

        public static string levelFile;

        /// <summary>
        /// The number of arrows fired from the bow
        /// </summary>
        public static int arrowsToFire = 1;

        /// <summary>
        /// How fast to shoot the arrow.
        /// </summary>
        public static int arrowPower = 0;

        public static int swordPower = 0;

        /// <summary>
        /// 
        /// </summary>
        public static bool goldenRun = false;

        /// <summary>
        /// gif will eliminate extra color bending, so that GIFs can be made with minimal colors.
        /// </summary>
        public static bool gif = false;

        public static float health = 3;

        public static bool invincible = false;

        public static Vector2 lastMapLocation = new Vector2(80, 122);

        public static int numberOfEnemiesToKillBeforeLevelOver;

        public static Dictionary<string, int> gameSizeGlobals = new Dictionary<string, int>();

        /// <summary>
        /// Allows the FlxConsole to run commands.
        /// </summary>
        /// <param name="Cheat">Name of the cheat you want to run.</param>
        public static void runCheat(string Cheat)
        {
            if (Cheat == "arrows") FlxG.log("Current Arrows to Fire: " + Globals.arrowsToFire);
            else if (Cheat.StartsWith("arrows")) Globals.arrowsToFire = Convert.ToInt32(Cheat[Cheat.Length - 1].ToString());
            else if (Cheat.StartsWith("whatisgame")) FlxG.log("Four Chambers");
            else if (Cheat.StartsWith("liketheangels")) Globals.seraphineHasBeenKilled = false;
            else if (Cheat.StartsWith("bigmoney")) FlxG.score += 20000;
            else if (Cheat.StartsWith("nobugs")) FlxG.debug = false;
            else if (Cheat == "bounds") FlxG.showBounds = true;
            else if (Cheat == "nobounds") FlxG.showBounds = false;
            else if (Cheat.StartsWith("level"))
            {
                Globals.arrowsToFire = Convert.ToInt32(Cheat[Cheat.Length - 1].ToString());
            }
            else if (Cheat == "nouse") Globals.invincible = true;
            FlxGlobal.cheatString = Cheat;

        }


        /// <summary>
        /// This is called at the beginning of every game. Reset all globals here.
        /// </summary>
        public static void startGame()
        {

            //FlxG.score = 0;
            Globals.seraphineHasBeenKilled = true;

            //FlxG.level = 1;

            //health = 3;   

            Globals.arrowCombo = 0;
            Globals.arrowPower = 1;
            //FourChambers_Globals.arrowsToFire = 1;

            //FourChambers_Globals.availableLevels = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            //int newLevel = (int)FlxU.random(0, FourChambers_Globals.availableLevels.Count);
            //FlxG.level = FourChambers_Globals.availableLevels[newLevel];
            //Console.WriteLine("startGame() " + FourChambers_Globals.availableLevels[newLevel] + "  New Level:  " + newLevel + " " + availableLevels.Count );
            //FourChambers_Globals.availableLevels.RemoveAt(newLevel);


        }

        public  static  void writeGameProgressToFile()
        {

            string progress = "";
            for (int i = 0; i < 307; i++)
            {
                if (Globals.treasuresCollectedPersistant.ContainsKey(i))
                {
                    if (Globals.treasuresCollectedPersistant[i]==1)
                        progress += "1,";
                    else
                        progress += "0,";
                }
                else
                {
                    progress += "0,";
                }
            }
            progress += "$" + FlxG.score + "," + FlxG.level;

            Console.WriteLine("Writing Game Progress to File: {0}", FlxG.score);

            FlxU.saveToDevice(progress, Globals.SAVE_FILE_NAME);
            //FlxU.saveToDevice(progress, "gui.txt");
            //if (FlxG.debug)
            //    FlxU.saveToDevice(progress, "gui_DEBUG.dll");
            //else
            //    FlxU.saveToDevice(progress, "gui.dll");
            
        }

        public static void readGameProgressToFile()
        {
            string progress = FlxU.loadFromDevice(Globals.SAVE_FILE_NAME);

            string[] vsplit = progress.Split('$');

            string[] elements0 = vsplit[0].Split(',');
            string[] elements1 = vsplit[1].Split(',');

            for (int i = 0; i < 306; i++)
            {
                if (elements0[i]=="1")
                {
                    Globals.treasuresCollectedPersistant[i] = 1;
                }
                else
                {
                    Globals.treasuresCollectedPersistant[i] = 0;
                }
            }

            FlxG.score = Convert.ToInt32(elements1[0]);

            Console.WriteLine("Reading Game Progress - Highest Level available {0} -- Money == ${1}", elements1[1], elements1[0]);


        }

        public static void getLevelFileName()
        {
            string levelFile;
            if (FlxG.level >= 1)
            {
                levelFile = "ogmoLevels/level" + FlxG.level.ToString() + ".oel";
            }
            else if (FlxG.level == -1)
            {
                levelFile = "ogmoLevels/levelTutorial.oel";
            }
            else
            {
                Console.WriteLine("Unknown level, loading level : " + FlxG.level.ToString());

                levelFile = "ogmoLevels/level" + FlxG.level.ToString() + ".oel";
            }

            Console.WriteLine("Loading BasePlayStateFromOel Level: " + levelFile);

            Globals.levelFile = levelFile;

        }
    }
}
