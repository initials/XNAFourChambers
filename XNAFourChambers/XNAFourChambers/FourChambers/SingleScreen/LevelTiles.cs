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
    public class LevelTiles : FlxGroup
    {
        private Dictionary<string, string> levelAttrs;

        private FlxTilemap bgTiles;
        private FlxTilemap fgTiles;
        public FlxTilemap levelTiles;

        private FlxSprite bg;
        private FireflyGroup fireflyGroup;
        public float transition = -1.0f;

        public LevelTiles()
            : base()
        {
            //setScrollFactors(0, 0);

            bg = new FlxSprite(0, 0, FlxG.Content.Load<Texture2D>("fourchambers/bg"));
            bg.alpha = 0.55f;
            bg.setScrollFactors(0.01f, 0.01f);
            bg.@fixed = true;
            bg.moves = false;
            add(bg);

            levelAttrs = new Dictionary<string, string>();
            levelAttrs = FlxXMLReader.readAttributesFromOelFile(Globals.levelFile, "level/bg");

            bgTiles = new FlxTilemap();
            bgTiles.auto = FlxTilemap.STRING;

            bgTiles.loadMap(levelAttrs["bg"],
                FlxG.Content.Load<Texture2D>("fourchambers/" + levelAttrs["tileset"]),
                Globals.TILE_SIZE_X,
                Globals.TILE_SIZE_Y);
            bgTiles.alpha = 0.5f;
            bgTiles.setScrollFactors(0.2f, 0.2f);
            add(bgTiles);


            levelAttrs = new Dictionary<string, string>();
            levelAttrs = FlxXMLReader.readAttributesFromOelFile(Globals.levelFile, "level/collide");

            levelTiles = new FlxTilemap();
            levelTiles.collideMin = 0;
            levelTiles.collideMax = 21;
            levelTiles.collideIndex = 1;
            levelTiles.boundingBoxOverride = true;
            levelTiles.auto = FlxTilemap.STRING;
            levelTiles.loadMap(levelAttrs["collide"], 
                FlxG.Content.Load<Texture2D>("fourchambers/" + levelAttrs["tileset"]), 
                Globals.TILE_SIZE_X, 
                Globals.TILE_SIZE_Y);
            add(levelTiles);


            levelAttrs = new Dictionary<string, string>();
            levelAttrs = FlxXMLReader.readAttributesFromOelFile(Globals.levelFile, "level/fg");
            fgTiles = new FlxTilemap();
            fgTiles.auto = FlxTilemap.STRING;

            fgTiles.loadMap(levelAttrs["fg"],
                FlxG.Content.Load<Texture2D>("fourchambers/" + levelAttrs["tileset"]),
                Globals.TILE_SIZE_X,
                Globals.TILE_SIZE_Y);
            add(fgTiles);

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
            base.render(spriteBatch);
        }
    }
}