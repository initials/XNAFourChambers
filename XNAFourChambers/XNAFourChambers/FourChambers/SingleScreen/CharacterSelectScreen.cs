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
    public class CharacterSelectScreen : FlxState
    {
        private LevelTiles levelTilemap;
        private ActorsGroup actorsGrp;
        private FlxSprite prism;
        private int currentCharacterSelected = 0;

        override public void create()
        {
            base.create();

            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("fourchambers/crosshair"));

            FlxG.elapsedTotal = 0;

            FourChambers_Globals.numberOfEnemiesToKillBeforeLevelOver = 20;

            FlxG.showHud();
            FlxG.setHudTextScale(1, FlxG.zoom);

            FourChambers_Globals.levelFile = "ogmoLevels/characterSelect.oel";

            Dictionary<string, string> levelAttrs = new Dictionary<string, string>();

            levelAttrs = FlxXMLReader.readAttributesFromOelFile(FourChambers_Globals.levelFile, "level");

            FlxG.playMp3("music/" + levelAttrs["music"], 0.250f);

            FlxG.levelWidth = Convert.ToInt32(levelAttrs["width"]);
            FlxG.levelHeight = Convert.ToInt32(levelAttrs["height"]);

            Console.WriteLine("Level Width: " + FlxG.levelWidth + " Level Height: " + FlxG.levelHeight);

            levelTilemap = new LevelTiles();
            add(levelTilemap);

            actorsGrp = new ActorsGroup();
            add(actorsGrp);

            foreach (var item in actorsGrp.members)
            {
                item.velocity.X = 0;
                ((BaseActor)(item)).isPlayerControlled = false;
            }

            prism = new FlxSprite((int)actorsGrp.members[currentCharacterSelected].x, (int)actorsGrp.members[currentCharacterSelected].y - 24);
            prism.loadGraphic("fourchambers/characterSpriteSheets/Prism_ss_14x20", true, false, 13, 20);
            prism.addAnimation("play", new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, 12, true);
            prism.play("play",true);
            add(prism);

            actorsGrp.members = actorsGrp.members.OrderBy(d => d.x).ToList();
        }

        override public void update()
        {
            prism.x = actorsGrp.members[currentCharacterSelected].x;
            prism.y = actorsGrp.members[currentCharacterSelected].y - 24;

            if (FlxControl.LEFTJUSTPRESSED)
                currentCharacterSelected--;
            if (FlxControl.RIGHTJUSTPRESSED)
                currentCharacterSelected++;

            if (FlxControl.ACTIONJUSTPRESSED && FlxG.elapsedTotal>0.5f)
            {
                FlxG.state = new SingleScreenLevel();
                return;
            }

            FlxU.collide(actorsGrp, levelTilemap);

            base.update();

            if (((BaseActor)(actorsGrp.members[currentCharacterSelected])).lockedForSelection == false)
                FlxG.setHudText(1, actorsGrp.members[currentCharacterSelected].GetType().ToString().Split('.')[1] + " ready!");
            else
                FlxG.setHudText(1, actorsGrp.members[currentCharacterSelected].GetType().ToString().Split('.')[1] + " [LOCKED], $" + ((BaseActor)(actorsGrp.members[currentCharacterSelected])).price.ToString() + " to unlock");

            //FlxG.setHudTextPosition(1, prism.x, prism.y - 24);

        }

    }
}
