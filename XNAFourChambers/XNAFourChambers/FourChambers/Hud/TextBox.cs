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
    class TextBox : FlxTileblock
    {
        public string text;

        public FlxText flxText;

        public TextBox(int xPos, int yPos, int xWidth, int yHeight, string Text)
            : base(xPos, yPos, xWidth, yHeight)
        {
            flxText = new FlxText(xPos, yPos, xWidth);
            flxText.alignment = FlxJustification.Left;
            flxText.text = Text;

            auto = FlxTileblock.HUDELEMENT;
            loadTiles("ui/_sheet_window_18", 16, 16, 0);


        }

        public void setText(string Text)
        {
            flxText.text = Text;
        }

        override public void update()
        {

            flxText.update();

            base.update();

        }

        public override void render(SpriteBatch spriteBatch)
        {
            
            
            base.render(spriteBatch);

            flxText.render(spriteBatch);


        }


    }
}
