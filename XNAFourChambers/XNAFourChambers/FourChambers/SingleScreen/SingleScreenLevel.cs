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

            // set level details

            FourChambers_Globals.numberOfEnemiesToKillBeforeLevelOver = 20;

            FlxG.showHud();

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
            FlxU.overlap(actorsGrp, actorsGrp, overlapCallback);

            collideArrows();
            
            base.update();

            FlxG.setHudText(1, "Time in Level: " + FlxG.elapsedTotal.ToString().Split('.')[0] + " Enemies To Kill: " + FourChambers_Globals.numberOfEnemiesToKillBeforeLevelOver.ToString() );


        }

        private void collideArrows()
        {
            foreach (var item in actorsGrp.members)
            {
                if (item is Marksman)
                {
                    FlxU.overlap(((Marksman)(item)).arrows, actorsGrp, eventCallback);
                }
            }
        }

        protected bool overlapCallback(object Sender, FlxSpriteCollisionEvent e)
        {
            ((FlxSprite)e.Object1).overlapped(((FlxSprite)e.Object2));
            ((FlxSprite)e.Object2).overlapped(((FlxSprite)e.Object1));

            return true;
        }

        protected bool eventCallback(object Sender, FlxSpriteCollisionEvent e)
        {

            //((Arrow)e.Object1).kill();
            ((FlxSprite)e.Object2).overlapped(((FlxSprite)e.Object1));

            return true;
        }


    }
}
