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
    class Waterfall : FlxEmitter
    {
        public FlxSprite collider;

        public Waterfall(int xPos, int yPos, int Width, int Height)
            : base(xPos, yPos)
        {
            setSize(Width, 16);
            setRotation();
            setXSpeed(0, 0);
            setYSpeed(-15, 15);
            gravity = 98;
            for (int i = 0; i < 25; i++)
            {
                WaterfallParticle p = new WaterfallParticle(FlxU.random(0.02f, 0.35f));
                add(p);
            }
            start(false, 0.065f, 0);

            renderOrder = 1;

            collider = new FlxSprite(xPos, yPos + Height);
            collider.createGraphic(32, 16, Color.Red);
            collider.@fixed = true;

        }

        /// <summary>
        /// The Update Cycle. Called once every cycle.
        /// </summary>
        override public void update()
        {
            
            collider.update();
            FlxU.collide(this, collider);

            base.update();

        }

        public override void render(SpriteBatch spriteBatch)
        {
            base.render(spriteBatch);
        }

    }
}

        
