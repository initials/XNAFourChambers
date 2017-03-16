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
        private TitleText t;
        private FlxText infoText;

        private FlxObject followObject;

        private NokiaPhone nokiaPhone;


        override public void create()
        {
            base.create();

            //FlxG.bloom.Visible = false;

            followObject = new FlxObject(400, 592 / 2, 1, 1);
            add(followObject);

            FlxG.followBounds(0, 0, 2500, 2500, true);
            FlxG.follow(followObject, 5);

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

            for (int i = 0; i < 25; i++)
            {
                Cloud c = new Cloud((int)FlxU.random(0, FlxG.width) + 160, (int)FlxU.random(0, FlxG.height) + 160);
                add(c);

            }

            infoText = new FlxText(0,0,200);

            infoText.setFormat(Globals.HUD_FONT, 1, Color.White, Color.Black, FlxJustification.Left);
            infoText.setScrollFactors(1, 1);
            add(infoText);
            infoText.x = prism.x;
            infoText.y = prism.y+56;

            nokiaPhone = new NokiaPhone();
            add(nokiaPhone);
            nokiaPhone.visible = false;

        }

        private void addText()
        {
            int i = 1;
            foreach (var item in Globals.GAME_NAME.Split(' '))
            {
                t = new TitleText(FlxG.width / 2 - 100, FlxG.height + (i * 14), 200, FlxG.width / 2 - 100, 8 + (i * 14), 2.0f + (i*0.2f));
                t.setFormat(Globals.TITLE_FONT, 1, Color.White, Color.Black, FlxJustification.Center);
                t.text = item.ToString();
                add(t);

                i++;
            }
        }

        override public void update()
        {
            prism.x = (actorsGrp.members[currentCharacterSelected].x + actorsGrp.members[currentCharacterSelected].width/2) - (prism.width/2);
            prism.y = actorsGrp.members[currentCharacterSelected].y - 36;

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

            if (FlxG.keys.justPressed(Keys.Y))
            {
                showTextMessage(null,null);
            }

            currentCharacterSelected = Utils.LimitToRange(currentCharacterSelected, 0, actorsGrp.members.Count-1);

            if (FlxControl.ACTIONJUSTPRESSED && FlxG.elapsedTotal>0.5f)
            {
                if (((BaseActor)(actorsGrp.members[currentCharacterSelected])).lockedForSelection == false && FlxG.fade.exists == false)
                {
                    prism.play("wrap");

                    FlxG.fade.start(Color.Black, 1.5f, goToNextState, true);

                    FlxG.play("sfx/Door");

                    //levelTilemap.transition = 0;

                    FlxG.quake.start(0.00525f, 1.7f);
                    
                    followObject.velocity.X = 45;
                    followObject.acceleration.X = 75;

                    foreach (var item in this.defaultGroup.members)
                    {
                        if (item.GetType().ToString() == "FourChambers.TitleText")
                        {
                            ((TitleText)(item)).pushTextOffToLeft();
                        }
                    }

                }
                else if (FlxG.fade.exists == true)
                {

                }
                else if (nokiaPhone.visible==true)
                {
                    FlxG.fade.start(Color.Black, 1.5f, goToNextState, true);
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

            }

            FlxU.collide(actorsGrp, levelTilemap);

            base.update();

            if (((BaseActor)(actorsGrp.members[currentCharacterSelected])).lockedForSelection == false)
            {
                infoText.text = actorsGrp.members[currentCharacterSelected].GetType().ToString().Split('.')[1] + " ready!";
            }
            else
            {
                infoText.text = actorsGrp.members[currentCharacterSelected].GetType().ToString().Split('.')[1] + " [LOCKED] Proposed Price: $" + ((BaseActor)(actorsGrp.members[currentCharacterSelected])).price.ToString() + " ";
            }
            if (FlxG.debug)
            {
                runDebugKeyPresses();
            }
            if (FlxG.elapsedTotal > 1.0f)
            {
                if (FlxG.keys.justPressed(Keys.Escape) || FlxG.gamepads.isNewButtonPress(Buttons.Back))
                {
                    FlxG.fade.start(Color.Black, 1.5f, exitGame, false);

                    foreach (var item in this.defaultGroup.members)
                    {
                        if (item.GetType().ToString() == "FourChambers.TitleText")
                        {
                            ((TitleText)(item)).text = "";
                        }
                    }
                }
            }
        }

        public void showTextMessage(object sender, FlxEffectCompletedEvent e)
        {
            FlxG.fade.exists = false;
            
            nokiaPhone.setVisible();
            
            FlxG.stopMp3();

        }

        public void goToNextState(object sender, FlxEffectCompletedEvent e)
        {
            
            FlxG.state = new SingleScreenLevel();
            return;
        }

        public void goToNextState()
        {

            FlxG.state = new SingleScreenLevel();
            return;
        }

        public static void exitGame(object sender, FlxEffectCompletedEvent e)
        {
            FlxG.Game.Exit();
            return;
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
