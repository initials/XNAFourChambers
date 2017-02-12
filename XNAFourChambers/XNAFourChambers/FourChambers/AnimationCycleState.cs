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
    public class AnimationCycleState : FlxState
    {
        public Marksman m;

        override public void create()
        {
            base.create();
            //FlxG.mouse.show("Mode/cursor");

            FlxSprite bg = new FlxSprite(0, 0);
            bg.createGraphic(1920,1080, new Color(77,189,252));
            add(bg);

            m = new Marksman(30,50);
            m.customAnimation = "runRange";
            add(m);

            Mistress mi = new Mistress(80, 50);
            mi.customAnimation = "run";
            mi.acceleration.Y = 0;
            add(mi);

            Warlock w = new Warlock(130, 50);
            w.customAnimation = "run";
            add(w);


            Drone d = new Drone(30, 100);
            d.customAnimation = "fly";
            d.setVelocity(0, 0);
            add(d);

            Automaton a = new Automaton(30, 100);
            a.customAnimation = "walk";
            a.acceleration.Y = 0;
            add(a);

            Corsair c = new Corsair(80, 100);
            c.customAnimation = "run";
            c.acceleration.Y = 0;
            add(c);

            Executor e = new Executor(130, 100);
            e.customAnimation = "walk";
            e.acceleration.Y = 0;
            add(e);

            Gloom g = new Gloom(30, 150);
            g.customAnimation = "walk";
            g.acceleration.Y = 0;
            add(g);

            Harvester h = new Harvester(80, 150);
            h.customAnimation = "walk";
            h.acceleration.Y = 0;
            add(h);

            //Medusa m = new Medusa(

            Vampire v = new Vampire(130, 150);
            v.customAnimation = "walk";
            v.acceleration.Y = 0;
            add(v);

            

        }
        override public void update()
        {
            //Console.WriteLine("{0}", m.acceleration.Y);

            base.update();
        }

        protected bool overlapped(object Sender, FlxSpriteCollisionEvent e)
        {
            ((FlxObject)(e.Object1)).overlapped(e.Object2);
            ((FlxObject)(e.Object2)).overlapped(e.Object1);
            return true;
        }

    }
}
