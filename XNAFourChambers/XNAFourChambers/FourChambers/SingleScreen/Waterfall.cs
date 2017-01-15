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
    class Waterfall : FlxEmitter
    {

        public FlxSprite collider;

        public int colliderHeight;


        public Waterfall(int xPos, int yPos)
            : base(xPos, yPos)
        {
            setSize(32, 16);
            setRotation();
            setXSpeed(0, 0);
            setYSpeed(-15, 15);
            gravity = 98;
            createSprites(FlxG.Content.Load<Texture2D>("fourchambers/waterFallSparkle"), 300, true, 1, FlxU.random(0.02f,0.45f) );

            start(false, 0.0075f, 0);
            renderOrder = 1;

            collider = new FlxSprite(x, y + colliderHeight);
            collider.createGraphic(32, 16, Color.Red);
            collider.@fixed = true;

            foreach (var item in members)
            {
                ((FlxParticle)(item))._bounce = FlxU.random(0.02f, 0.45f);
            }

        }

        /// <summary>
        /// The Update Cycle. Called once every cycle.
        /// </summary>
        override public void update()
        {
            
            collider.update();
            FlxU.collide(this, collider);

            base.update();

            foreach (var item in members)
            {
                if (item.velocity.Y < 0)
                    ((FlxParticle)(item)).frame = 11;
            }
        }

        public override void render(SpriteBatch spriteBatch)
        {
            //collider.render(spriteBatch);

            base.render(spriteBatch);
        }
        //public override void emitParticle()
        //{
        //    velocity.Y = 0;
        //    base.emitParticle();
        //}
    }
}

        
