using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNATweener;

namespace FourChambers
{
    class TitleText : FlxText
    {
        public Vector2Tweener t;

        public TitleText(float xPos, float yPos, float Width, float destX, float destY, float timeFrame)
            : base(xPos, yPos, Width)
        {
            //setScrollFactors(2, 2);

            //x += FlxG.scroll.X * (1 - scrollFactor.X);
            //y += FlxG.scroll.Y * (1 - scrollFactor.Y);

            t = new Vector2Tweener(new Vector2(xPos, yPos), new Vector2(destX, destY), timeFrame, XNATweener.Bounce.EaseOut);
            t.Play();

        }

        /// <summary>
        /// The Update Cycle. Called once every cycle.
        /// </summary>
        override public void update()
        {
            if (FlxG.elapsedFrames>50)
                t.Update(FlxG.elapsedAsGameTime);

            base.update();

            x = t.Position.X;
            y = t.Position.Y;
            
        }

        public void pushTextOffToLeft()
        {
            t = new Vector2Tweener(new Vector2(x, y), new Vector2(x-450, y), 2.0f, XNATweener.Cubic.EaseIn);
            t.Play();
        }

    }
}