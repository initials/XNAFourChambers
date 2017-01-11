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
    class LevelTiles : FlxTilemap
    {
        private Dictionary<string, string> indestructableAttrs;

        public LevelTiles()
            : base()
        {
            Console.WriteLine("Creating a custom LevelTiles tilemap");

            

            collideMin = 0;
            collideMax = 21;
            collideIndex = 1;

            boundingBoxOverride = true;

            indestructableAttrs = new Dictionary<string, string>();
            indestructableAttrs = FlxXMLReader.readAttributesFromOelFile(FourChambers_Globals.levelFile, "level/DestructableTerrain");

            auto = FlxTilemap.STRING;

            loadMap(indestructableAttrs["DestructableTerrain"], 
                FlxG.Content.Load<Texture2D>("fourchambers/" + indestructableAttrs["tileset"]), 
                FourChambers_Globals.TILE_SIZE_X, 
                FourChambers_Globals.TILE_SIZE_Y);

            



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
