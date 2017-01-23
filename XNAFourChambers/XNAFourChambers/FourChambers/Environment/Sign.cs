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
    class Sign : FlxSprite
    {
        public string message;
        //public List<string> currentOverlappingObjects;

        public TextBox textBox;

        public Sign(int xPos, int yPos)
            : base(xPos, yPos)
        {
            loadGraphic("fourchambers/auto_surt2_16x16", true, false, 16, 16);
            frame = 592;
            message = "";

            textBox = new TextBox(40, 0, 400, 32, message, 8);
            //textBox.writeOn = true;
            
            renderOrder = 1;

        }

        /// <summary>
        /// The Update Cycle. Called once every cycle.
        /// </summary>
        override public void update()
        {

            base.update();
            textBox.update();
            
            
        }

        public override void render(SpriteBatch spriteBatch)
        {
            textBox.render(spriteBatch);
            base.render(spriteBatch);
        }

        public override void overlapped(FlxObject obj)
        {
            if (obj.GetType().ToString() == "FourChambers.Marksman")
            {
                textBox.setText(message);
                
            }

            base.overlapped(obj);
        }
    }
}
