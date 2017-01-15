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
    class Ladder : FlxTileblock
    {

        public Ladder(int X, int Y, int Width, int Height)
            : base(X, Y, Width, Height)
        {

            auto = FlxTileblock.RANDOM;
            loadTiles(FlxG.Content.Load<Texture2D>("fourchambers/ladderTiles_16x16"), FourChambers_Globals.TILE_SIZE_X, FourChambers_Globals.TILE_SIZE_Y, 0);

            renderOrder = 1;
        }

        /// <summary>
        /// The Update Cycle. Called once every cycle.
        /// </summary>
        override public void update()
        {
            base.update();
        }
        public override void overlapped(FlxObject obj)
        {
            if (obj.GetType().ToString() == "FourChambers.Arrow")
            {
                
            }
            else
            {

            }
            base.overlapped(obj);
        }

        public override void hurt(float Damage)
        {
            base.hurt(Damage);
        }

        public override void kill()
        {

            base.kill();
        }
    }
}
