﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace FourChambers
{
    class TextBox : FlxTileblock
    {
        public string text;

        public FlxText flxText;

        public bool writeOn = false;
        private int writeOnChar = 0;

        public TextBox(int xPos, int yPos, int xWidth, int yHeight, string Text, int Style)
            : base(xPos, yPos, xWidth, yHeight)
        {

            setScrollFactors(0, 0);

            flxText = new FlxText(xPos, yPos, xWidth, 100, "", Color.White, FlxG.Content.Load<SpriteFont>("flixel/initials/Munro"),1,FlxJustification.Left,0);

            flxText.setFormat(FlxG.Content.Load<SpriteFont>("flixel/initials/Munro"), 1, Color.White, FlxJustification.Left, Color.Black);

            //flxText.alignment = FlxJustification.Center;
            flxText.text = Text;
            flxText.setScrollFactors(0, 0);

            string STRstyle = "";

            if (Style <= 9)
                STRstyle = "0" + Style.ToString();
            else
                STRstyle = Style.ToString();

            auto = FlxTileblock.HUDELEMENT;
            loadTiles("ui/_sheet_window_" + STRstyle, 16, 16, 0);

            text = Text;

        }

        public void setText(string Text)
        {
            string newText = Text;

            if (newText.Length > 60)
            {
                newText = newText.Insert(60, "\n");
            }

            //Console.WriteLine(newText); 

            if (writeOn == true)
            {
                text = Text;
                writeOnChar = 0;
            }
            else
            {
                flxText.text = newText;
            }
        }

        override public void update()
        {



            flxText.x = x+6;
            flxText.y = y+2;

            flxText.update();

            base.update();

            if (writeOn)
            {
                if (writeOnChar < text.Length - 1)
                {
                    flxText.text = text.Substring(0, writeOnChar);
                    writeOnChar++;
                }
            }

        }

        public override void render(SpriteBatch spriteBatch)
        {
            
            
            base.render(spriteBatch);

            flxText.render(spriteBatch);


        }


    }
}
