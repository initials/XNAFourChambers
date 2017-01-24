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



        public Waterfall(int xPos, int yPos, int Width, int Height)
            : base(xPos, yPos)
        {
            setSize(Width, 16);
            setRotation();
            setXSpeed(0, 0);
            setYSpeed(-15, 15);
            gravity = 98;
            //createSprites(FlxG.Content.Load<Texture2D>("fourchambers/waterFallSparkle"), 170, true, 1, FlxU.random(0.02f,0.45f) );
            for (int i = 0; i < 150; i++)
            {
                WaterfallParticle p = new WaterfallParticle(FlxU.random(0.02f, 0.35f));
                add(p);
            }
            //start(false, 0.05f, 0);
            start(false, 0.015f, 0);

            renderOrder = 1;

            collider = new FlxSprite(xPos, yPos + Height);
            collider.createGraphic(32, 16, Color.Red);
            collider.@fixed = true;

            //foreach (var item in members)
            //{
            //    ((FlxParticle)(item))._bounce = FlxU.random(0.02f, 0.35f);
            //    if (FlxU.random() > 0.9f)
            //    {
            //        FlxSprite p = emitParticleAndReturnSprite();
            //        p.y += FlxU.randomInt(0, Height);
            //        p.velocity.Y = FlxU.random(20, 98);
            //        p.frame = FlxU.randomInt(0, 10);

            //    }
            //}

        }

        /// <summary>
        /// The Update Cycle. Called once every cycle.
        /// </summary>
        override public void update()
        {
            
            collider.update();
            FlxU.collide(this, collider);
            //FlxU.collide(this, SingleScreenLevel.actorsGrp.members.Find((FlxObject item) => item.GetType().ToString() == "FourChambers.Marksman"));

            //FlxU.collide(this, SingleScreenLevel.actorsGrp);

            base.update();

            //foreach (var item in members)
            //{
            //    if (item.velocity.Y < 0)
            //    {
            //        ((FlxParticle)(item)).frame = 11;
            //    }
            //}

        }

        public override void render(SpriteBatch spriteBatch)
        {
            //collider.render(spriteBatch);

            base.render(spriteBatch);
        }

    }
}

        
