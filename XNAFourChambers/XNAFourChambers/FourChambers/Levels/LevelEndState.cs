using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace FourChambers
{
    public class LevelEndState : FlxState
    {
        FlxText t;

        FlxText t2;

        override public void create()
        {
            base.create();

            t = new FlxText(10, 10, FlxG.width);
            t.text = "PLACEHOLDER DEATH SCREEN\n";
            add(t);

            t2 = new FlxText(10, 110, FlxG.width);
            t2.text = "Money: $" + FlxG.score + "\nBest Combo: \nLevel\netc" ;
            t2.alignment = FlxJustification.Left;
            add(t2);
        }

        override public void update()
        {


            if (elapsedInState > 2.0f && t.text == "PLACEHOLDER DEATH SCREEN\n")
            {
                t.text += "Press ACTION to restart";

            }

            if (FlxControl.ACTIONJUSTPRESSED && elapsedInState > 2.0f)
            {
                FlxG.state = new GameSelectionMenuState();
            }

            base.update();
        }


    }
}
