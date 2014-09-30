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

        FlxSprite actor;
        MapActor mActor;


        FlxTileblock hud;

        Tweener tween;
        TextBox t;
        FlxGroup events;



        override public void create()
        {
            base.create();

            events = new FlxGroup();
            add(events);

            mapAttrs = new Dictionary<string, string>();
            mapAttrs = FlxXMLReader.readAttributesFromOelFile("ogmoLevels/worldMap.oel", "level/DestructableTerrain");

            map = new FlxTilemap();
            map.auto = FlxTilemap.STRING;
            map.loadMap(mapAttrs["DestructableTerrain"], FlxG.Content.Load<Texture2D>("fourchambers/" + mapAttrs["tileset"]), FourChambers_Globals.TILE_SIZE_X, FourChambers_Globals.TILE_SIZE_Y);
            add(map);

            mActor = new MapActor(64, 64);
            add(mActor);


            //actor = new FlxSprite(0, 0);
            //actor.loadGraphic("fourchambers/blowharder", true, false, 16, 16);
            //actor.addAnimation("static", new int[] { 742 }, 12, true);
            //actor.play("static");
            //add(actor);

            FlxG.follow(actor, 10.0f);
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


            /*
             * 
             * destructableAttrs = new Dictionary<string, string>();
            destructableAttrs = FlxXMLReader.readAttributesFromOelFile(levelFile, "level/DestructableTerrain");
             * 
             * 
             * destructableTilemap = new FlxTilemap();
            destructableTilemap.auto = FlxTilemap.STRING;
            destructableTilemap.loadMap(destructableAttrs["DestructableTerrain"], FlxG.Content.Load<Texture2D>("fourchambers/" + destructableAttrs["tileset"]), FourChambers_Globals.TILE_SIZE_X, FourChambers_Globals.TILE_SIZE_Y);
            destructableTilemap.boundingBoxOverride = true;
            allLevelTiles.add(destructableTilemap);
             */


            //foreach (var item in mapAttrs["DestructableTerrain"])
            //{
            //    Console.WriteLine(item);
            //}

            //hud = new FlxTileblock(32, 8, 160, 32);
            //hud.auto = FlxTileblock.HUDELEMENT;
            //hud.loadTiles("ui/_sheet_window_18", 16, 16, 0);
            //add(hud);


            t = new TextBox(16, FlxG.height - 32, FlxG.width-32, 32, "...");
            add(t);

            tween = new Tweener(FlxG.height + 32, FlxG.height - 32, 0.75f, XNATweener.Cubic.EaseIn);
            tween.Start();


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
                t.visible = true;
                t.setText("Go to Level " + FlxG.level.ToString());
            }
            else
            {
                t.visible = true;
                t.setText(command);


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
             t.visible = true;
            ((EventSprite)e.Object2).runCallback();

            FlxG.level = ((EventSprite)e.Object2).value1;

            if (((EventSprite)e.Object2).repeats >= 0)
                ((EventSprite)e.Object2).hurt(((EventSprite)e.Object2).repeats);

            return true;
        }

        override public void update()
        {
            t.visible = false;
            FlxU.overlap(mActor, events, eventCallback);

            //int tile = map.getTile((int)(actor.x / 16), (int)(actor.y / 16));
            //if (tile == 2983 || tile == 2984)
            //{
            //    tween.Start();
            //}

            //if (FlxG.mouse.justPressed())
            //{
            //    tween.Reset();
            //    tween.Start();
            //}
            //if (FlxG.mouse.justPressed())
            //{
            //    //int tile = map.getTile((int)(actor.x/16), (int)(actor.y/16));
            //    Console.WriteLine(tile);
            //    if (tile==2983) FlxG.level = 6;
            //    FlxG.state = new BasePlayStateFromOel();
            //}

            //if (FlxControl.UPJUSTPRESSED)
            //{
            //    actor.y -= 8;
            //}
            //if (FlxControl.DOWNJUSTPRESSED)
            //{
            //    actor.y += 8;
            //}
            //if (FlxControl.LEFTJUSTPRESSED)
            //{
            //    actor.x -= 8;
            //}
            //if (FlxControl.RIGHTJUSTPRESSED)
            //{
            //    actor.x += 8;
            //}


            //if (FlxControl.UP)
            //{
            //    actor.y -= 1;
            //}
            //if (FlxControl.DOWN)
            //{
            //    actor.y += 1;
            //}
            //if (FlxControl.LEFT)
            //{
            //    actor.x -= 1;
            //}
            //if (FlxControl.RIGHT)
            //{
            //    actor.x += 1;
            //}

            if (FlxControl.ACTIONJUSTPRESSED)
            {
                FlxG.transition.startFadeOut(0.25f, 45, 120);
            }

            if (FlxG.transition.complete)
            {
                FlxG.state = new BasePlayStateFromOel();
                return;
            }

            //t.y = tween.Position;

            tween.Update(FlxG.elapsedAsGameTime);
            base.update();
        }


    }
}
