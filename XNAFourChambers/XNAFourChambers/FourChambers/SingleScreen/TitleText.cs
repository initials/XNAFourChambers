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
        private float timing;
        private int _delay;

        public TitleText(float xPos, float yPos, float Width, float destX, float destY, float timeFrame, int Delay)
            : base(xPos, yPos, Width)
        {
            //setScrollFactors(2, 2);

            //x += FlxG.scroll.X * (1 - scrollFactor.X);
            //y += FlxG.scroll.Y * (1 - scrollFactor.Y);

            t = new Vector2Tweener(new Vector2(xPos, yPos), new Vector2(destX, destY), timeFrame, XNATweener.Bounce.EaseOut);
            t.Play();

            timing = timeFrame;

            _delay = Delay;

        }

        /// <summary>
        /// The Update Cycle. Called once every cycle.
        /// </summary>
        override public void update()
        {
            if (FlxG.elapsedFrames>_delay)
                t.Update(FlxG.elapsedAsGameTime);

            base.update();

            x = t.Position.X;
            y = t.Position.Y;

            if (y < 0)
                visible = false;
            
        }

        public void pushTextOffToLeft()
        {
            t = new Vector2Tweener(new Vector2(x, y), new Vector2(x - 450, y), timing, XNATweener.Cubic.EaseIn);
            t.Play();
        }

    }
}