
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
    public class PerLevelAdjustments
    {
        public static void update(ActorsGroup actorsGrp, LevelTiles levelTiles)
        {
            if (FlxG.level == 6)
            {
                int newColor = 256 - (int)Utils.getAllActorsOfType(actorsGrp.members, "FourChambers.Marksman")[0].y / 4;
                newColor = Utils.LimitToRange(newColor, 0, 256);

                //Console.WriteLine(newColor);

                setToColor(actorsGrp, levelTiles, newColor, newColor, newColor);
            }
        }
        public static void adjustForLevel(ActorsGroup actorsGrp, LevelTiles levelTiles)
        {
            // add zinger nests
            for (int i = 0; i < levelTiles.levelTiles.widthInTiles; i++)
            {
                for (int j = 0; j < levelTiles.levelTiles.heightInTiles; j++)
                {
                    //Console.WriteLine(levelTiles.levelTiles.getTile(i, j));

                    if (levelTiles.levelTiles.getTile(i, j) == 12)
                    {
                        if (FlxU.random() < 0.18)
                            actorsGrp.add(new ZingerNest(i * 16, (j * 16)+16));
                    }
                }
            }
            if (FlxG.level == 6)
            {
                //FlxG.color(Color.Black);

                FlxG.follow( Utils.getAllActorsOfType(actorsGrp.members, "FourChambers.Marksman")[0], 5.5f );
                FlxG.followBounds(0, 0, FlxG.levelWidth, FlxG.levelHeight, true);

                //setToColor(actorsGrp, levelTiles);
            }
            if (FlxG.level == 5)
            {
                setToColor(actorsGrp, levelTiles, 255, 22, 20);
            }
        }

        private static void setToColor(ActorsGroup actorsGrp, LevelTiles levelTiles, int R, int G, int B)
        {
            foreach (var item in actorsGrp.members)
            {
                try
                {
                    ((FlxSprite)(item)).color = new Color(R, G, B);
                }
                catch (Exception)
                {

                }

            }
            foreach (var item in levelTiles.members)
            {
                try
                {
                    if (item.GetType().Name != "bg")
                    {
                        ((FlxTilemap)(item)).color = new Color(R, G, B);
                    }
                }
                catch (Exception)
                {

                }

            }
        }
    }
}