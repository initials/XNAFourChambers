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
    public class GenericEmitter : FlxEmitter
    {
        public static int minFrame;
        public static int maxFrame;

        public GenericEmitter(int xPos, int yPos, int Width, int Height)
            : base(xPos, yPos)
        {
            setSize(1, 1);
            setRotation();
            setXSpeed(0, 0);
            setYSpeed(-15, 15);
            gravity = 98;
            for (int i = 0; i < 35; i++)
            {
                GenericParticle p = new GenericParticle(FlxU.random(0.02f, 0.35f));
                add(p);
            }
            //start(false, 0.02f, 0);

            renderOrder = 1;
            minFrame = 0;
            maxFrame = 0;

        }

        /// <summary>
        /// The Update Cycle. Called once every cycle.
        /// </summary>
        override public void update()
        {
            base.update();
        }

        public override void render(SpriteBatch spriteBatch)
        {
            //collider.render(spriteBatch);

            base.render(spriteBatch);
        }

    }
}


