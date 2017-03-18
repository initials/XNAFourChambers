using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNATweener;


namespace FourChambers
{
    public class NokiaPhone : FlxGroup
    {
        public int numberOfTimesShown;
        public int timeVisible;
        public List<string> textStories;
        public FlxText textMessage;

        public NokiaPhone()
            : base()
        {

            textStories = new List<string>() { 
                "From: Your Boss\nMessage: Can u work\non the weekend?\nkthx", 
                "Can u spray the\nBrouwer's backyard\nIt's full of hornets", 
                "Don't forget\nDo it in costume",
                "Our customers\nAren't paying for\nsome normie to",
                "exterminate\ntheir pests"
            };

            setScrollFactors(0, 0);

            //FlxSprite bg = new FlxSprite(0, 0);
            //bg.createGraphic(1000, 1000, Color.Black);
            //add(bg);

            FlxSprite e = new FlxSprite(0, 0);
            e.loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/NokiaScreen"), true, false, 84, 48);
            e.addAnimation("flicker", 
                new int[] { 3, FlxU.randomInt(0, 2), FlxU.randomInt(0, 2), 
                FlxU.randomInt(0, 2), FlxU.randomInt(0, 2), 
                FlxU.randomInt(0, 2), FlxU.randomInt(0, 4), 
                FlxU.randomInt(0, 4), FlxU.randomInt(0, 4), 0, 1, 2, 3 }, 3);

            e.play("flicker");
            e.setScrollFactors(0, 0);
            e.scale = 4;
            add(e);

            DateTime time = DateTime.Now;

            FlxText t = new FlxText(-64, 84*2, 84);
            t.text = time.ToShortTimeString();
            t.color = Color.Black;
            t.offset.X = -84;
            t.offset.Y = 64;
            t.scale = 2;
            add(t);

            textMessage = new FlxText(0, 0, 48);
            textMessage.scale = 2;
            textMessage.text = textStories[0];
            textMessage.color = Color.Black;
            textMessage.alignment = FlxJustification.Left;
            add(textMessage);
            textMessage.offset.X = 92;
            textMessage.offset.Y = 32;



            //x = (84 + 84/2);

            //y = 48 + 48/2;

            x = FlxG.width / 2 - 84/2;
            y = FlxG.height / 2 - 48 / 2;

            numberOfTimesShown = 0;
        }

        public void setVisible()
        {
            FlxG.bloom.Visible = true;
            FlxG.play("sfx/smstone");
            visible = true;

            numberOfTimesShown++;
            timeVisible = 0;
        }

        public void setInvisible()
        {

            FlxG.bloom.Visible = false;
            visible = false;

        }

        public void moveStoryAlong()
        {
            int index = textStories.FindIndex(x => x.StartsWith(textMessage.text));

            if (textStories.Count <= index + 1)
                setInvisible();
            else
                textMessage.text = textStories[index + 1];


        }

        override public void update()
        {
            timeVisible++;

            base.update();

            if (FlxG.keys.NUMPADFOUR)
            {
                x--;
                Console.WriteLine("x/y - x:{0}--y{1}", x, y);
            }
            if (FlxG.keys.NUMPADSIX)
            {
                x++;
                Console.WriteLine("x/y - x:{0}--y{1}", x, y);
            }
            if (FlxG.keys.NUMPADEIGHT)
            {
                y--;
                Console.WriteLine("x/y - x:{0}--y{1}", x, y);
            }
            if (FlxG.keys.NUMPADTWO)
            {
                y++;
                Console.WriteLine("x/y - x:{0}--y{1}", x, y);
            }


            foreach (var item in members)
            {
                //item.x = x;
                //item.y = y;
                item.x = x - ((FlxSprite)(item)).offset.X;
                item.y = y - ((FlxSprite)(item)).offset.Y;

            }

            if (timeVisible < 25)
            {
                FlxG.bloom.blurAmount = FlxU.random(0.0f, 0.50f);
                FlxG.bloom.bloomThreshold = FlxU.random(0.5f, 1);
                FlxG.bloom.bloomSaturation = FlxU.random(0.5f, 1);
                FlxG.bloom.bloomIntensity = FlxU.random(0.5f, 1);

                FlxG.bloom.usePresets = false;
            }
            else
            {
                FlxG.bloom.usePresets = true;
                FlxG.bloom.Settings = BloomPostprocess.BloomSettings.PresetSettings[1];
            }

        }
    }
}