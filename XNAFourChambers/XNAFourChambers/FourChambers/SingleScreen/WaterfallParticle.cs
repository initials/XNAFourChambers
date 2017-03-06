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
    class WaterfallParticle : FlxParticle
    {
        public WaterfallParticle(float Bounce)
            : base(Bounce)
        {
            loadGraphic("fourchambers/waterFallSparkle", true, false, 3, 3);
            frame = FlxU.randomInt(0, 10);

            exists = false;

        }

        override public void update()
        {
            if (velocity.Y < 0)
            {
                frame = 11;
            }

            base.update();
        }
        public override void onEmit()
        {
            velocity.Y = 1;
            if (frame==11)
                frame = FlxU.randomInt(0, 10);
            base.onEmit();
        }
    }
}