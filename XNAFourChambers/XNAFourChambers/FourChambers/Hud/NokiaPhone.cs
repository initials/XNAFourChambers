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
        public NokiaPhone()
            : base()
        {
            setScrollFactors(0, 0);

            //FlxSprite bg = new FlxSprite(0, 0);
            //bg.createGraphic(1000, 1000, Color.Black);
            //add(bg);

            FlxSprite e = new FlxSprite(0, 0, FlxG.Content.Load<Texture2D>("fourchambers/NokiaScreen"));
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

            FlxText newText = new FlxText(0, 0, 48);
            newText.scale = 2;
            newText.text = "From: Your Boss\nMessage: Can u work\non the weekend?\nkthx";
            newText.color = Color.Black;
            add(newText);
            newText.offset.X = -32;
            newText.offset.Y = 32;

            
            
            //FlxG.bloom.Settings = BloomPostprocess.BloomSettings.PresetSettings[1];


            x = (84*4) - (FlxG.width/2) + 84;
            y = 100;
        }

        public void setVisible()
        {
            FlxG.bloom.Visible = true;
            FlxG.play("sfx/smstone");
            visible = true;
            

        }

        override public void update()
        {

            foreach (var item in members)
            {
                //item.x = x;
                //item.y = y;
                item.x = x - ((FlxSprite)(item)).offset.X;
                item.y = y - ((FlxSprite)(item)).offset.Y;

            }

            if (FlxG.elapsedFrames < 65)
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