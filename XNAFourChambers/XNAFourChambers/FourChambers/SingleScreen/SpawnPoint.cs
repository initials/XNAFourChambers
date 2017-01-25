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
            createGraphic(16, 16, Color.Green);
            alpha = 0.1f;
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
                //Console.WriteLine("actors: {0}", item.GetType().ToString());

                if (item.dead && !item.exists)
                {
                    if (FlxU.random() < chance || FlxG.keys.justPressed(Keys.F4))
                    {

                        //Respawn based on chance of respawn
                        item.reset(x, y);

                        Console.WriteLine("Respawning {0} at {1} x {2}", item.GetType().ToString(), x, y);

                        //if (FlxG.keys.justPressed(Keys.F4))
                        //    break;

                    }
                }
            }


            base.update();
        }

    }
}