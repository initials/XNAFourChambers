﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace FourChambers
{
    public class Credits : FlxState
    {
        FlxSprite bgSprite;

        FlxText credits;

        override public void create()
        {
            base.create();

            FlxG.resetHud();
            FlxG.hideHud();

            bgSprite = new FlxSprite(-350, 0);
            bgSprite.loadGraphic("fourchambers/Fear");
            bgSprite.scrollFactor.X = 1.0f;
            bgSprite.scrollFactor.Y = 1.0f;
            bgSprite.boundingBoxOverride = false;
            bgSprite.allowColorFlicker = false;
            bgSprite.color = FlxColor.ToColor("#55B4FF");
            add(bgSprite);

            for (int i = 0; i < 6; i++)
            {
                FlxSprite cloud = new FlxSprite(FlxU.random(0, FlxG.width), FlxU.random(0, FlxG.height));
                cloud.loadGraphic("fourchambers/cloud", false, false, 160, 64);
                cloud.setScrollFactors(0.1f, 0.1f);

                cloud.setVelocity(FlxU.random(-5, 5), 0);
                cloud.alpha = 0.85f;
                add(cloud);

            }


            credits = new FlxText(80, 210, FlxG.width);
            credits.setFormat(null, 1, Color.White, FlxJustification.Center, Color.White); //FlxG.Content.Load<SpriteFont>("initials/SpaceMarine")
            //_menuItems.text = "Four Chambers\n\nEnter name, use @ symbol to specify Twitter handle.\nPress enter when complete.";
            credits.text = "Credits";
            credits.shadow = Color.Black;
            credits.alignment = FlxJustification.Left;
            add(credits);
            credits.setScrollFactors(1, 1);
            
            addString("A game by:");
            addString("initials");
            addString(" ");
            addString("Art by Stephanie Chiew");
            addString(" ");
            addString("Logo by Auro Cyanide");
            addString(" ");
            addString("Score by ");
            addString("Girls With Depression ");
            addString(" ");
            addString("Additional Music by ");
            addString("Ralph Hilton aka");
            addString("Rotary Dial Sins");
            addString(" ");
            addString("Minifigs by svh440");
            addString(" ");
            addString("Environment Pixel Art by ");
            addString("Surt");
            addString("");
        }

        public void addString(string Credit)
        {
            credits.text += "\n";
            credits.text += Credit;

        }

        override public void update()
        {

            credits.y -= 1.0f;

            if (this.elapsedInState > 1.0f)
            {
                if (FlxControl.ACTIONJUSTPRESSED || FlxG.mouse.justPressed() )
                {


                }

            }

            base.update();
        }


    }
}
