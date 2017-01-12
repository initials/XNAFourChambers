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
    public class SingleScreenLevel : FlxState
    {
        private LevelTiles indestructableTilemap;
        private ActorsGroup actorsGrp;

        override public void create()
        {
            base.create();

            FourChambers_Globals.getLevelFileName();

            Dictionary<string, string> levelAttrs = new Dictionary<string, string>();

            levelAttrs = FlxXMLReader.readAttributesFromOelFile(FourChambers_Globals.levelFile, "level");

            FlxG.levelWidth = Convert.ToInt32(levelAttrs["width"]);
            FlxG.levelHeight = Convert.ToInt32(levelAttrs["height"]);

            Console.WriteLine("Level Width: " + FlxG.levelWidth + " Level Height: " + FlxG.levelHeight);

            indestructableTilemap = new LevelTiles();
            add(indestructableTilemap);

            actorsGrp = new ActorsGroup();
            add(actorsGrp);



        }




        override public void update()
        {
            FlxU.collide(actorsGrp, indestructableTilemap);
            
            base.update();


        }

    }
}
