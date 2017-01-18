
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
            if (FlxG.level == 4)
            {
                foreach (var item in actorsGrp.members)
                {
                    try
                    {
                        ((FlxSprite)(item)).color = Color.Black;
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
                            ((FlxTilemap)(item)).color = Color.Black;
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