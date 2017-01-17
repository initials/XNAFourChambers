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

        XNATweener.Tweener tween;
        int current = 0;

        FlxGroup powerups;
        FlxGroup powerupsUncollected;

        override public void create()
        {
            base.create();

            tween = new XNATweener.Tweener(0,1,0.115f, XNATweener.Bounce.EaseOut);
            tween.Start();

            powerups = new FlxGroup();
            powerupsUncollected = new FlxGroup();

            // Add random treasures
            /*
            if (FlxG.debug)
            {
                for (int i = 0; i < 50; i++)
                {

                    int rr = (int)FlxU.random(0, 304);

                    //Console.WriteLine("Adding {0}", rr);

                    //FourChambers_Globals.treasuresCollected.Add((int)FlxU.random(0, 304));
                    if (FourChambers_Globals.treasuresCollectedPersistant.ContainsKey(rr)==false)
                    {
                        
                        FourChambers_Globals.treasuresCollectedPersistant.Add(rr, 1);

                    }
                    else if (FourChambers_Globals.treasuresCollectedPersistant.ContainsKey(rr) == true)
                    {

                        FourChambers_Globals.treasuresCollectedPersistant[rr] = 1;

                    }


                }

                FourChambers_Globals.writeGameProgressToFile();

            }
             */



            int xp = 0;
            int yp = 20;

            //foreach (var item in FourChambers_Globals.treasuresCollected)
            for (int i = 0; i < 307; i++)
            {
                //t2.text += item.ToString() + ", ";

                PowerUp p = new PowerUp(xp, yp);
                p.TypeOfPowerUp(i);
                
               
                p.allowColorFlicker = false;

                if (Globals.treasuresCollectedPersistant.ContainsKey(i))
                {
                    if (Globals.treasuresCollectedPersistant[i] == 1)
                    {
                        p.scale = 0;
                        p.color = Color.White;
                        powerups.add(p);
                    }
                    else
                    {
                        p.color = Color.Black;
                        powerupsUncollected.add(p);
                    }
                }
                else
                {
                    p.color = Color.Black;
                    powerupsUncollected.add(p);
                }

                xp += 16;
                if (xp > 400)
                {
                    xp = 0;
                    yp += 16;
                }
            }

            add(powerups);
            add(powerupsUncollected);


            t = new FlxText(10, 10, FlxG.width);
            t.setFormat(FlxG.Content.Load<SpriteFont>("flixel/initials/Munro"), 1, Color.White, FlxJustification.Left, Color.Black);

            t.text = "PLACEHOLDER DEATH SCREEN\n";
            add(t);

            t2 = new FlxText(10, 110, FlxG.width);
            t2.setFormat(FlxG.Content.Load<SpriteFont>("flixel/initials/Munro"), 1, Color.White, FlxJustification.Left, Color.Black);
            t2.text = "Money: $" + FlxG.score + "\nBest Combo: \nTreasures: ";
            t2.alignment = FlxJustification.Left;
            add(t2);

            FlxG.playMp3("music/gwd/SlowedLoop", 1.0f);

        }

        override public void update()
        {

            tween.Update(FlxG.elapsedAsGameTime);



            


            if (tween.hasEnded )
            {
                if (current < powerups.members.Count - 1)
                {
                    ((FlxSprite)(powerups.members[current])).scale = tween.Position;
                    //Console.WriteLine("Current {0} Count {1}", current, powerups.members.Count);

                    if (((FlxSprite)( powerups.members[current])).color==Color.White) 
                        FlxG.play("sfx/Pickup_Coin");

                    current++;
                    tween.Reset();
                    tween.Start();
                }

            }


            if (elapsedInState > 2.0f && t.text == "PLACEHOLDER DEATH SCREEN\n")
            {
                t.text += "Press ACTION to restart";

            }

            if (FlxControl.ACTIONJUSTPRESSED && elapsedInState > 2.0f)
            {
                FlxG.state = new MapState();
            }

            base.update();
        }


    }
}
