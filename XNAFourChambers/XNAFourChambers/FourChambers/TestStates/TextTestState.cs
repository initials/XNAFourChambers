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
    public class TextTestState : FlxState
    {

        override public void create()
        {
            base.create();
            
            ImpactText im = new ImpactText(new String[] { "element", "bang2", "adsf awer", "bang5" });
            add(im);
        }

        override public void update()
        {
            base.update();
        }


    }
}
