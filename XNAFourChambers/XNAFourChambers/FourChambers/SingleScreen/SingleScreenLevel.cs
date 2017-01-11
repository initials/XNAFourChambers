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

            indestructableTilemap = new LevelTiles();
            indestructableTilemap.collideMin = 0;
            indestructableTilemap.collideMax = 21;

            add(indestructableTilemap);

            FlxG.showBounds = !FlxG.showBounds;


            actorsGrp = new ActorsGroup();
            add(actorsGrp);


        }




        override public void update()
        {
            
            base.update();
            FlxU.collide(actorsGrp, indestructableTilemap);

            Console.WriteLine(indestructableTilemap.collideMax);


        }

    }
}
