using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/*
 *             actor = new FlxSprite(0, 0);
            actor.loadGraphic("fourchambers/blowharder", true, false, 16, 16);
            actor.addAnimation("static", new int[] { 742 }, 12, true);
            actor.play("static");
            add(actor);
 * 
 */ 

namespace FourChambers
{
    class MapActor : FlxSprite
    {
        float runSpeed;

        public MapActor(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic("fourchambers/blowharderCharacter", true, true, 16, 16);

            addAnimation("idle", new int[] { 0 }, 12, true);
            addAnimation("walkDown", new int[] { 0, 1, 2, 1 }, 12, true);
            addAnimation("walkRight", new int[] { 3,4,5,4 }, 12, true);
            addAnimation("walkUp", new int[] { 6,7,8,7 }, 12, true);



            play("idle");

            setDrags(320, 320);

            width = 8;
            height = 2;
            setOffset(4, 14);

            runSpeed = 90;

        }

        override public void update()
        {

            if (velocity.X == 0 && velocity.Y == 0)
            {
                play("idle");
                //facing = Flx2DFacing.Left;
            }


            if (FlxControl.UP)
            {
                this.velocity.Y = runSpeed * -1;
                play("walkUp");
            }
            else if (FlxControl.DOWN)
            {
                this.velocity.Y = runSpeed;
                play("walkDown");
            }
            else if (FlxControl.LEFT)
            {
                this.velocity.X = runSpeed * -1;
                play("walkRight");
                facing = Flx2DFacing.Left;
            }
            else if (FlxControl.RIGHT)
            {
                this.velocity.X = runSpeed;
                play("walkRight");
                facing = Flx2DFacing.Right;
            }




            base.update();

        }


    }
}
