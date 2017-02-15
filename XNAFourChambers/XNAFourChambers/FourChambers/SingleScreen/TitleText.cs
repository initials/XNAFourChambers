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
            t = new Vector2Tweener(new Vector2(xPos, yPos), new Vector2(destX, destY), timeFrame, XNATweener.Bounce.EaseOut);
            t.Play();

        }

        /// <summary>
        /// The Update Cycle. Called once every cycle.
        /// </summary>
        override public void update()
        {
            t.Update(FlxG.elapsedAsGameTime);
            base.update();
            x = t.Position.X;
            y = t.Position.Y;
        }

    }
}