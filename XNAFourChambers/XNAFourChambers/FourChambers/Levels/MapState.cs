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

        FlxTileblock hud;

        Tweener tween;

        override public void create()
        {
            base.create();

            mapAttrs = new Dictionary<string, string>();
            mapAttrs = FlxXMLReader.readAttributesFromOelFile("ogmoLevels/worldMap.oel", "level/DestructableTerrain");

            map = new FlxTilemap();
            map.auto = FlxTilemap.STRING;
            map.loadMap(mapAttrs["DestructableTerrain"], FlxG.Content.Load<Texture2D>("fourchambers/" + mapAttrs["tileset"]), FourChambers_Globals.TILE_SIZE_X, FourChambers_Globals.TILE_SIZE_Y);
            add(map);


            actor = new FlxSprite(0, 0);
            actor.loadGraphic("fourchambers/blowharder", true, false, 16, 16);
            actor.addAnimation("static", new int[] { 742 }, 12, true);
            actor.play("static");
            add(actor);

            FlxG.follow(actor, 10.0f);
            FlxG.followBounds(0, 0, 5000, 5000);

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

            hud = new FlxTileblock(32, 8, 160, 32);
            hud.auto = FlxTileblock.HUDELEMENT;
            hud.loadTiles("ui/_sheet_window_18", 16, 16, 0);
            add(hud);


            TextBox t = new TextBox(32, 100, 160, 32, "This level is...");
            add(t);

            tween = new Tweener(-200, 32, 1.5f, XNATweener.Bounce.EaseOut);
            tween.Start();
            
        }

        override public void update()
        {
            int tile = map.getTile((int)(actor.x / 16), (int)(actor.y / 16));
            if (tile == 2983 || tile == 2984)
            {
                tween.Start();
            }

            if (FlxG.mouse.justPressed())
            {
                tween.Reset();
                tween.Start();
            }
            //if (FlxG.mouse.justPressed())
            //{
            //    //int tile = map.getTile((int)(actor.x/16), (int)(actor.y/16));
            //    Console.WriteLine(tile);
            //    if (tile==2983) FlxG.level = 6;
            //    FlxG.state = new BasePlayStateFromOel();
            //}

            if (FlxControl.UPJUSTPRESSED)
            {
                actor.y -= 8;
            }
            if (FlxControl.DOWNJUSTPRESSED)
            {
                actor.y += 8;
            }
            if (FlxControl.LEFTJUSTPRESSED)
            {
                actor.x -= 8;
            }
            if (FlxControl.RIGHTJUSTPRESSED)
            {
                actor.x += 8;
            }


            if (FlxControl.UP)
            {
                actor.y -= 1;
            }
            if (FlxControl.DOWN)
            {
                actor.y += 1;
            }
            if (FlxControl.LEFT)
            {
                actor.x -= 1;
            }
            if (FlxControl.RIGHT)
            {
                actor.x += 1;
            }


            hud.x = tween.Position;

            tween.Update(FlxG.elapsedAsGameTime);
            base.update();
        }


    }
}
