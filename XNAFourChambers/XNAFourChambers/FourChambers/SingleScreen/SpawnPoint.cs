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
    class SpawnPoint : FlxSprite
    {

        public SpawnPoint(int xPos, int yPos)
            : base(xPos, yPos)
        {
            //createGraphic(16, 16, Color.Green);

            loadGraphic("fourchambers/portal", true, false, 8, 8);
            addAnimation("pulse", new int[] { 0, 1, 2, 3, 0 }, 18, false);
            play("pulse", true);

            alpha = 1.0f;
        }

        /// <summary>
        /// The Update Cycle. Called once every cycle.
        /// </summary>
        override public void update()
        {
            float chance = 0.00251f;

            if (FlxG.elapsedTotal < 15) chance = 0.000251f;
            else if (FlxG.elapsedTotal < 30) chance = 0.000451f;
            else if (FlxG.elapsedTotal < 45) chance = 0.001651f;

            foreach (var item in SingleScreenLevel.actorsGrp.members)
            {
                if (item.dead && !item.exists && item is BaseActor)
                {
                    if (((FlxU.random() < chance || FlxG.keys.justPressed(Keys.F4)) && FlxG.elapsedTotal > ((BaseActor)item).releaseTime) && ((BaseActor)item).isRespawnable)
                    {
                        Console.WriteLine("Respawning {0} at {1} x {2}, release time was {3}, elapsed total {4}, dead {5}, exists {6}",
                            item.GetType().ToString(), x, y, ((BaseActor)(item)).releaseTime, FlxG.elapsedTotal, item.dead, item.exists);

                        //Respawn based on chance of respawn
                        //item.reset(x+4, y+4);
                        ((BaseActor)(item)).resetIn(x+4, y+4, 10);

                        play("pulse", true);
                    }
                }
            }


            base.update();
        }

    }
}