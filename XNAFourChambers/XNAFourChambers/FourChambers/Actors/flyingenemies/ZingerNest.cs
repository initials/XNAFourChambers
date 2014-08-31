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
    class ZingerNest : BaseActor
    {

        public ZingerNest(int xPos, int yPos, FlxGroup zingers)
            : base(xPos, yPos)
        {

            loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/spawner"), true, false, 16, 16);

            addAnimation("idle", new int[] { 0,1,2,3,2,1 }, (int)FlxU.random(8,12));
            play("idle");



        }

        override public void update()
        {

            acceleration.Y = 0;

            base.update();

        }


    }
}
