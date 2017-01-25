
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
        public static void adjustForLevel(ActorsGroup actorsGrp, LevelTiles levelTiles)
        {
            // add zinger nests
            for (int i = 0; i < levelTiles.levelTiles.widthInTiles; i++)
            {
                for (int j = 0; j < levelTiles.levelTiles.heightInTiles; j++)
                {
                    Console.WriteLine(levelTiles.levelTiles.getTile(i, j));

                    if (levelTiles.levelTiles.getTile(i, j) == 12)
                    {
                        if (FlxU.random() < 0.18)
                            actorsGrp.add(new ZingerNest(i * 16, (j * 16)+16));
                    }
                }
            }

            if (FlxG.level == 5)
            {
                foreach (var item in actorsGrp.members)
                {
                    try
                    {
                        ((FlxSprite)(item)).color = new Color(0,0,0, ((FlxSprite)(item)).alpha);
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
                            ((FlxTilemap)(item)).color = new Color(0,0,0, ((FlxTilemap)(item)).alpha);
                        }
                    }
                    catch (Exception)
                    {

                    }

                }
            }
        }
    }
}