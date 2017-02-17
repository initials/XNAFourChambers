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
        private Prism prism;
        private int currentCharacterSelected = 0;

        override public void create()
        {
            base.create();

            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("fourchambers/crosshair"));

            FlxG.elapsedTotal = 0;

            Globals.numberOfEnemiesToKillBeforeLevelOver = 20;

            FlxG.showHud();
            

            Globals.levelFile = "ogmoLevels/characterSelect.oel";

            Dictionary<string, string> levelAttrs = new Dictionary<string, string>();

            levelAttrs = FlxXMLReader.readAttributesFromOelFile(Globals.levelFile, "level");

            FlxG.playMp3("music/" + levelAttrs["music"], 0.250f);

            FlxG.levelWidth = Convert.ToInt32(levelAttrs["width"]);
            FlxG.levelHeight = Convert.ToInt32(levelAttrs["height"]);

            Console.WriteLine("Level Width: " + FlxG.levelWidth + " Level Height: " + FlxG.levelHeight);

            levelTilemap = new LevelTiles();
            add(levelTilemap);

            addText();

            actorsGrp = new ActorsGroup();
            add(actorsGrp);

            foreach (var item in actorsGrp.members)
            {
                item.velocity.X = 0;
                ((BaseActor)(item)).isPlayerControlled = false;
            }

            prism = new Prism((int)actorsGrp.members[currentCharacterSelected].x, (int)actorsGrp.members[currentCharacterSelected].y - 24);
            add(prism);

            actorsGrp.members = actorsGrp.members.OrderBy(d => d.x).ToList();

            FlxG.setHudTextScale(1, FlxG.zoom);
            FlxG.setHudTextPosition(1, prism.x, prism.y - 24);

            //FlxG.showBounds = true;

            for (int i = 0; i < 25; i++)
            {
                Cloud c = new Cloud((int)FlxU.random(0, FlxG.height), (int)FlxU.random(0, FlxG.width));
                add(c);

            }
        }

        private void addText()
        {
            int i = 1;
            foreach (var item in Globals.GAME_NAME.Split(' '))
            {
                TitleText t = new TitleText(FlxG.width / 2 - 100, FlxG.height + (i * 14), 200, FlxG.width / 2 - 100, 8 + (i * 14), 2.0f + (i*0.2f));
                t.setFormat("ui/PixelFraktur", 1, Color.White, Color.Black, FlxJustification.Center);
                t.text = item.ToString();
                add(t);

                i++;

            }
        }




        override public void update()
        {
            prism.x = (actorsGrp.members[currentCharacterSelected].x + actorsGrp.members[currentCharacterSelected].width/2) - (prism.width/2);
            prism.y = actorsGrp.members[currentCharacterSelected].y - 24;

            if (FlxControl.LEFTJUSTPRESSED)
            {
                currentCharacterSelected--;
                FlxG.play("sfx/Pickup_Coin");
            }
            if (FlxControl.RIGHTJUSTPRESSED)
            {
                currentCharacterSelected++;
                FlxG.play("sfx/Pickup_Coin");
            }
            currentCharacterSelected = Utils.LimitToRange(currentCharacterSelected, 0, actorsGrp.members.Count-1);

            if (FlxControl.ACTIONJUSTPRESSED && FlxG.elapsedTotal>0.5f)
            {
                if (((BaseActor)(actorsGrp.members[currentCharacterSelected])).lockedForSelection == false)
                {
                    prism.play("wrap");

                    FlxG.fade.start(Color.Black, 1.5f);
                    FlxG.play("sfx/Door");
                    FlxG.setHudTextPosition(1, int.MaxValue, int.MaxValue);
                    levelTilemap.transition = 0;
                    FlxG.quake.start(0.00525f, 1.7f);
                }
                else
                {
                    FlxG.log("Go to Steam in game purchase");
                    FlxG.quake.start(0.025f, 0.7f);
                    FlxG.play("sfx/Hit_Hurt5");

                }
            }

            if (prism.debugName == "readyToGoToNextState")
            {
                FlxG.setHudText(1, "");
                FlxG.state = new SingleScreenLevel();
                return;
            }

            FlxU.collide(actorsGrp, levelTilemap);

            base.update();

            if (((BaseActor)(actorsGrp.members[currentCharacterSelected])).lockedForSelection == false)
                FlxG.setHudText(1, actorsGrp.members[currentCharacterSelected].GetType().ToString().Split('.')[1] + " ready!");
            else
                FlxG.setHudText(1, actorsGrp.members[currentCharacterSelected].GetType().ToString().Split('.')[1] + " [LOCKED], $" + ((BaseActor)(actorsGrp.members[currentCharacterSelected])).price.ToString() + " to unlock");

            if (FlxG.debug)
            {
                runDebugKeyPresses();
            }

        }
        private void runDebugKeyPresses()
        {
            if (FlxG.keys.R || FlxG.gamepads.isNewButtonPress(Buttons.Y))
            {
                FlxG.state = new CharacterSelectScreen();
                return;
            }
        }

    }
}
