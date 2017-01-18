﻿using System;
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

            Globals.numberOfEnemiesToKillBeforeLevelOver = 20;

            FlxG.showHud();
            FlxG.setHudTextScale(1, FlxG.zoom);
            FlxG.setHudTextPosition(1, 16, 8);

            Globals.getLevelFileName();

            Dictionary<string, string> levelAttrs = new Dictionary<string, string>();

            levelAttrs = FlxXMLReader.readAttributesFromOelFile(Globals.levelFile, "level");

            FlxG.playMp3("music/" + levelAttrs["music"], 0.550f);

            FlxG.levelWidth = Convert.ToInt32(levelAttrs["width"]);
            FlxG.levelHeight = Convert.ToInt32(levelAttrs["height"]);

            Console.WriteLine("Level Width: " + FlxG.levelWidth + " Level Height: " + FlxG.levelHeight);

            levelTilemap = new LevelTiles();
            add(levelTilemap);

            actorsGrp = new ActorsGroup();
            add(actorsGrp);

            PerLevelAdjustments.adjustForLevel(actorsGrp,levelTilemap);

        }

        override public void update()
        {
            FlxU.collide(actorsGrp, levelTilemap.levelTiles);
            FlxU.overlap(actorsGrp, actorsGrp, overlapCallback);
            
            collideArrows();
            
            base.update();

            FlxG.setHudText(1, "Time in Level: " + FlxG.elapsedTotal.ToString().Split('.')[0] + " Collect " + Globals.numberOfEnemiesToKillBeforeLevelOver.ToString() + " more pests. Arrow Combo: " + Globals.arrowCombo );

            if (FlxG.keys.R)
            {
                FlxG.level = 1;
                FlxG.state = new SingleScreenLevel();
                return;
            }
            if (FlxG.keys.P)
            {
                Globals.numberOfEnemiesToKillBeforeLevelOver = 0;
                Globals.arrowCombo = 20;

                actorsGrp.members.Find((FlxObject item) => item.GetType().ToString() == "FourChambers.Marksman").x = actorsGrp.members.Find((FlxObject item) => item.GetType().ToString() == "FourChambers.Door").x;
                actorsGrp.members.Find((FlxObject item) => item.GetType().ToString() == "FourChambers.Marksman").y = actorsGrp.members.Find((FlxObject item) => item.GetType().ToString() == "FourChambers.Door").y;

                foreach (var item in actorsGrp.members)
                {
                    if (!(item is Marksman))
                    {
                        //item.dead = true;
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
                    FlxU.overlap(((Marksman)(item)).allProjectiles, actorsGrp, runOverlapOnObject2);
                    FlxU.collide(((Marksman)(item)).allProjectiles, levelTilemap.levelTiles);

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
