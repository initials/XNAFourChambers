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
    class ImpactText : FlxGroup
    {

        public ImpactText(String[] Texts)
        {
            members = new List<FlxObject>();

            int count = 0;
            int total = Texts.Length;
            foreach (String t in Texts)
            {
                FlxG.write("Text string " + t);

                FlxText localText = new FlxText(0, 10 + (count * 30), FlxG.width);
                localText.text = t;
                localText.setFormat(FlxG.Content.Load<SpriteFont>("initials/SpaceMarine"), 1, Color.White, FlxJustification.Center, Color.White);

                add(localText);

                count++;

            }


        }

        override public void update()
        {


            base.update();

        }


    }
}
