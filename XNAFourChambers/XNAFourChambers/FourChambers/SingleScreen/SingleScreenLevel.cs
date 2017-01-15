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
        private LevelTiles levelTilemap;
        private ActorsGroup actorsGrp;

        override public void create()
        {
            base.create();

            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("fourchambers/crosshair"));

            FlxG.elapsedTotal = 0;

            // set level details

            FourChambers_Globals.numberOfEnemiesToKillBeforeLevelOver = 20;

            FlxG.showHud();

            FourChambers_Globals.getLevelFileName();

            Dictionary<string, string> levelAttrs = new Dictionary<string, string>();

            levelAttrs = FlxXMLReader.readAttributesFromOelFile(FourChambers_Globals.levelFile, "level");

            FlxG.levelWidth = Convert.ToInt32(levelAttrs["width"]);
            FlxG.levelHeight = Convert.ToInt32(levelAttrs["height"]);

            Console.WriteLine("Level Width: " + FlxG.levelWidth + " Level Height: " + FlxG.levelHeight);

            levelTilemap = new LevelTiles();
            add(levelTilemap);

            actorsGrp = new ActorsGroup();
            add(actorsGrp);

            

        }




        override public void update()
        {
            FlxU.collide(actorsGrp, levelTilemap);
            FlxU.overlap(actorsGrp, actorsGrp, overlapCallback);
            
            collideArrows();
            
            base.update();

            FlxG.setHudText(1, "Time in Level: " + FlxG.elapsedTotal.ToString().Split('.')[0] + " Collect " + FourChambers_Globals.numberOfEnemiesToKillBeforeLevelOver.ToString() + " more pests" );

            if (FlxG.keys.R)
            {
                FlxG.level = 1;
                FlxG.state = new SingleScreenLevel();
                return;
            }
            if (FlxG.keys.P)
            {
                FourChambers_Globals.numberOfEnemiesToKillBeforeLevelOver = 0;
                foreach (var item in actorsGrp.members)
                {
                    if (!(item is Marksman))
                    {
                        item.dead = true;
                    }
                }
            }

        }

        private void collideArrows()
        {
            foreach (var item in actorsGrp.members)
            {
                if (item is Marksman)
                {
                    FlxU.overlap(((Marksman)(item)).arrows, actorsGrp, runOverlapOnObject2);
                    FlxU.collide(((Marksman)(item)).arrows, levelTilemap);

                }
            }
        }

        protected bool overlapWithLadder(object Sender, FlxSpriteCollisionEvent e)
        {
            if (e.Object1 is BaseActor)
            {
                if (!((BaseActor)(e.Object1)).flying)
                {
                    ((BaseActor)(e.Object1)).ladderPosX = e.Object2.x;
                    ((BaseActor)(e.Object1)).canClimbLadder = true;
                }
            }
            return true;
        }

        protected bool overlapCallback(object Sender, FlxSpriteCollisionEvent e)
        {
            ((FlxSprite)e.Object1).overlapped(((FlxSprite)e.Object2));
            ((FlxSprite)e.Object2).overlapped(((FlxSprite)e.Object1));

            //if can climb ladder and ladder

            if ((e.Object1 is Marksman) && (e.Object2 is Ladder))
            {
                overlapWithLadder(Sender, e);
            }
            if ((e.Object1 is Marksman) && (e.Object2 is Door))
            {
                //prepare for next level
                //overlapWithLadder(Sender, e);
                //FlxG.level++;

                //FlxG.state = new SingleScreenLevel();
                
                //return;
                foreach (var item in ((Door)(e.Object2)).sparkles.members)
                {
                    ((FlxSprite)(item)).scale+= FlxU.random();
                    ((FlxSprite)(item)).angle+=FlxU.random();
                    if (((FlxSprite)(item)).scale > 30)
                    {
                        FlxG.level++;

                        FlxG.state = new SingleScreenLevel();
                        break;
                    }

                }
                

            }


            return true;
        }


        protected bool runOverlapOnObject2(object Sender, FlxSpriteCollisionEvent e)
        {

            //((Arrow)e.Object1).kill();
            ((FlxSprite)e.Object2).overlapped(((FlxSprite)e.Object1));

            return true;
        }


    }
}
