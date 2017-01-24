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
    public class ArrowGroup : FlxGroup
    {
        public FlxEmitter _fire;
        public FlxGroup fires;

        public ArrowGroup()
            : base()
        {

            fires = new FlxGroup();
        }

        public override FlxObject add(FlxObject Object)
        {
            _fire = new FlxEmitter();
            _fire.setSize(1, 1);
            _fire.setRotation();
            _fire.setXSpeed(-44, 44);
            _fire.setYSpeed(-44, 44);
            _fire.gravity = 45;
            _fire.createSprites(FlxG.Content.Load<Texture2D>("fourchambers/arrowSparkles"), 25, true);
            _fire.start(false, 0.009f, 0);
            fires.add(_fire);

            ((Arrow)(Object))._fire = _fire;

            return base.add(Object);
        }

        /// <summary>
        /// The Update Cycle. Called once every cycle.
        /// </summary>
        override public void update()
        {
            
            fires.update();
            base.update();

            for (int i = 0; i < members.Count; i++)
            {
                if (members[i].dead == false)
                {
                    fires.members[i].x = members[i].x + (members[i].width / 2);
                    fires.members[i].y = members[i].y + (members[i].height / 2);
                }
                else
                {
                    //((FlxEmitter)(fires).members[i]).stop();
                }
            }

        }

        public override void render(SpriteBatch spriteBatch)
        {
            fires.render(spriteBatch);
            base.render(spriteBatch);
        }
    }
}
