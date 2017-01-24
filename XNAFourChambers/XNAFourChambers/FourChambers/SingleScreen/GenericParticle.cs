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
    public class GenericParticle : FlxParticle
    {

        public GenericParticle(float Bounce)
            : base(Bounce)
        {
            loadGraphic("fourchambers/customSparkles", true, false, 1, 1);

            exists = false;


        }

        /// <summary>
        /// The Update Cycle. Called once every cycle.
        /// </summary>
        override public void update()
        {
            base.update();
        }

        public override void onEmit()
        {
            frame = FlxU.randomInt(GenericEmitter.minFrame, GenericEmitter.maxFrame);
            base.onEmit();
        }
    }
}