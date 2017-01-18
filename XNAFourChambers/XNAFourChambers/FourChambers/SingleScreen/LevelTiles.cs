/*
 * Add these to Visual Studio to quickly create new FlxSprites
 */

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
    class LevelTiles : FlxTilemap
    {
        private Dictionary<string, string> levelAttrs;
        private FlxTilemap bgTiles;
        private FlxTilemap fgTiles;

        private FlxSprite bg;
        private FireflyGroup fireflyGroup;
        public float transition = -1.0f;

        public LevelTiles()
            : base()
        {
            Console.WriteLine("Creating a custom LevelTiles tilemap");

            bg = new FlxSprite(0, 0, FlxG.Content.Load<Texture2D>("fourchambers/bg"));
            bg.alpha = 0.25f;

            collideMin = 0;
            collideMax = 21;
            collideIndex = 1;

            boundingBoxOverride = true;

            levelAttrs = new Dictionary<string, string>();
            levelAttrs = FlxXMLReader.readAttributesFromOelFile(Globals.levelFile, "level/collide");

            auto = FlxTilemap.STRING;

<<<<<<< HEAD
            levelTiles.loadMap(levelAttrs["collide"],
                FlxG.Content.Load<Texture2D>("fourchambers/" + levelAttrs["tileset"]),
                Globals.TILE_SIZE_X,
=======
            loadMap(levelAttrs["collide"], 
                FlxG.Content.Load<Texture2D>("fourchambers/" + levelAttrs["tileset"]), 
                Globals.TILE_SIZE_X, 
>>>>>>> parent of 62ff5c4... x
                Globals.TILE_SIZE_Y);


            levelAttrs = new Dictionary<string, string>();
            levelAttrs = FlxXMLReader.readAttributesFromOelFile(Globals.levelFile, "level/bg");

            bgTiles = new FlxTilemap();
            bgTiles.auto = FlxTilemap.STRING;

            bgTiles.loadMap(levelAttrs["bg"],
                FlxG.Content.Load<Texture2D>("fourchambers/" + levelAttrs["tileset"]),
                Globals.TILE_SIZE_X,
                Globals.TILE_SIZE_Y);
            bgTiles.alpha = 0.5f;


            levelAttrs = new Dictionary<string, string>();
            levelAttrs = FlxXMLReader.readAttributesFromOelFile(Globals.levelFile, "level/fg");
            fgTiles = new FlxTilemap();
            fgTiles.auto = FlxTilemap.STRING;

            fgTiles.loadMap(levelAttrs["fg"],
                FlxG.Content.Load<Texture2D>("fourchambers/" + levelAttrs["tileset"]),
                Globals.TILE_SIZE_X,
                Globals.TILE_SIZE_Y);
            



            fireflyGroup = new FireflyGroup();
            

        }

        /// <summary>
        /// The Update Cycle. Called once every cycle.
        /// </summary>
        override public void update()
        {
            base.update();
            fireflyGroup.update();
            if (transition >= 0)
            {
                for (int i = 0; i < FlxG.levelWidth / Globals.TILE_SIZE_X; i++)
                {
                    fgTiles.setTile(i, (int)transition, FlxU.randomInt(0, 250));
                }
                transition += 0.25f;


            }
        }

        public override void render(SpriteBatch spriteBatch)
        {
<<<<<<< HEAD
            base.render(spriteBatch);
=======
            bg.render(spriteBatch);
            bgTiles.render(spriteBatch);
            base.render(spriteBatch);
            fgTiles.render(spriteBatch);
            fireflyGroup.render(spriteBatch);
>>>>>>> parent of 62ff5c4... x
        }
    }
}