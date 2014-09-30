using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

using XNATweener;

namespace FourChambers
{
    public class MapState : FlxState
    {
        Dictionary<string, string> mapAttrs;
        FlxTilemap map;

        FlxTilemap collisionMap;

        MapActor mActor;

        Tweener tween;
        TextBox t;
        FlxGroup events;

        FlxSprite logo;
        FlxSprite fadeIn;

        override public void create()
        {
            base.create();

            FourChambers_Globals.readGameProgressToFile();


            events = new FlxGroup();
            add(events);

            mapAttrs = new Dictionary<string, string>();
            mapAttrs = FlxXMLReader.readAttributesFromOelFile("ogmoLevels/worldMap.oel", "level/DestructableTerrain");

            map = new FlxTilemap();
            map.auto = FlxTilemap.STRING;
            map.loadMap(mapAttrs["DestructableTerrain"], FlxG.Content.Load<Texture2D>("fourchambers/" + mapAttrs["tileset"]), FourChambers_Globals.TILE_SIZE_X, FourChambers_Globals.TILE_SIZE_Y);
            add(map);

            mapAttrs = FlxXMLReader.readAttributesFromOelFile("ogmoLevels/worldMap.oel", "level/IndestructableTerrain");

            collisionMap = new FlxTilemap();
            collisionMap.auto = FlxTilemap.STRING;
            collisionMap.loadMap(mapAttrs["IndestructableTerrain"], FlxG.Content.Load<Texture2D>("fourchambers/" + mapAttrs["tileset"]), FourChambers_Globals.TILE_SIZE_X, FourChambers_Globals.TILE_SIZE_Y);
            add(collisionMap);

            mActor = new MapActor(64, 64);
            add(mActor);

            FlxG.follow(mActor, 10.0f);
            FlxG.followBounds(0, 0, 5000, 5000);

            List<Dictionary<string, string>>  actorsAttrs = new List<Dictionary<string, string>>();
            actorsAttrs = FlxXMLReader.readNodesFromOelFile("ogmoLevels/worldMap.oel", "level/ActorsLayer");

            foreach (Dictionary<string, string> nodes in actorsAttrs)
            {
                if (nodes["Name"] == "_event")
                {
                    buildEvent(Convert.ToInt32(nodes["x"]), 
                        Convert.ToInt32(nodes["y"]), 
                        Convert.ToInt32(nodes["width"]), 
                        Convert.ToInt32(nodes["height"]), 
                        Convert.ToInt32(nodes["repeat"]),
                        nodes["event"],
                        Convert.ToInt32(nodes["value1"]),
                        Convert.ToInt32(nodes["value2"]));

                }
            }

            t = new TextBox(16, FlxG.height - 32, FlxG.width-32, 32, "...");
            //t.writeOn = true;
            add(t);

            tween = new Tweener(FlxG.height + 32, FlxG.height - 32, 0.75f, XNATweener.Cubic.EaseIn);
            tween.Start();


            fadeIn = new FlxSprite(0,0);
            fadeIn.createGraphic(FlxG.width, FlxG.height, Color.Black);
            add(fadeIn);


            logo = new FlxSprite(0, 0);
            logo.loadGraphic("fourchambers/logo/FourChambersLogo", true, false, 800, 173);
            add(logo);

            //FlxG._game.hud.hudGroup.add(logo);



            
        }

        public void buildEvent(int x = 0, int y = 0, int width = 0, int height = 0, int repeat = -1, string eventOrQuote = "", int Value1=0, int Value2=0)
        {
            if (eventOrQuote == " .. ")
            {

            }
            else
            {
                EventSprite s2 = new EventSprite(x, y, eventSpriteRun, repeat, eventOrQuote);
                s2.createGraphic(width, height, Color.Red);
                s2.value1 = Value1;
                s2.value2 = Value2;
                events.add(s2);
            }

        }

        public void eventSpriteRun(string command)
        {
            #region commands
            
            //t.inflateX = 10;
            //t.inflateY = 10;

            if (command.StartsWith("quake"))
            {
                FlxG.quake.start(0.01f, 1.0f);
            }
            else if (command.StartsWith("level"))
            {
                if (t.visible==false)
                    t.setText("Go to Level " + FlxG.level.ToString());
                t.visible = true;
                
            }
            else
            {
                if (t.visible == false)
                    t.setText(command);
                t.visible = true;
                


                //if (!tween.Running)
                //{
                //    tween.Reset();
                //    tween.Start();
                //}
            }

            #endregion
        }
        
        protected bool eventCallback(object Sender, FlxSpriteCollisionEvent e)
        {
            ((EventSprite)e.Object2).runCallback();

            FlxG.level = ((EventSprite)e.Object2).value1;

            if (((EventSprite)e.Object2).repeats >= 0)
                ((EventSprite)e.Object2).hurt(((EventSprite)e.Object2).repeats);

            return true;
        }

        override public void update()
        {


            logo.alpha -= 0.01f;
            fadeIn.alpha -= 0.005f;

            t.visible = false;
            FlxU.overlap(mActor, events, eventCallback);
            FlxU.collide(mActor, collisionMap);

            //int tile = map.getTile((int)(actor.x / 16), (int)(actor.y / 16));

            if (FlxControl.ACTIONJUSTPRESSED && elapsedInState > 1.0f)
            {
                FlxG.transition.startFadeOut(0.25f, 45, 120);
            }

            if (FlxG.transition.complete)
            {
                FourChambers_Globals.startGame();
                FlxG.state = new BasePlayStateFromOel();
                return;
            }

            //if (FlxG.keys.justPressed(Keys.D6) && FlxG.debug)
            //{
            //    FlxU.openURL("https://twitter.com/intent/tweet?hashtags=fourchambers&original_referer=https%3A%2F%2Fabout.twitter.com%2Fresources%2Fbuttons&text=Four%20Chambers%20of%20the%20Human%20Heart&tw_p=tweetbutton&url=http%3A%2F%2Fwww.initialsgames.com%2Ffourchambers&via=initials_games");

            //}

            //t.y = tween.Position;

            tween.Update(FlxG.elapsedAsGameTime);
            base.update();
        }


    }
}
