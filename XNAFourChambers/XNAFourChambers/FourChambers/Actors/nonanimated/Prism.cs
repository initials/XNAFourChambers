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
    class Prism : FlxSprite
    {
        public Prism(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic("fourchambers/characterSpriteSheets/Prism_ss_14x20", true, false, 13, 20);
            addAnimation("play", new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 12, true);
            addAnimation("wrap", new int[] { 10, 11, 12, 13, 14, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 }, 12, true);

            addAnimationCallback(check);

            debugName = "";
        }

        override public void update()
        {
            base.update();
        }

        public void check(string Name, uint Frame, int FrameIndex)
        {

            string info = "Current animation: " + Name + " Frame: " + Frame + " FrameIndex: " + FrameIndex;
            if (FrameIndex == 0 && Name == "wrap")
            {
                debugName = "readyToGoToNextState";
            }
        }


    }
}
