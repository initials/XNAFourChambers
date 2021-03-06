﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

namespace FourChambers
{
    public class SingleScreenLevel : FlxState
    {
        private LevelTiles levelTilemap;
        public static ActorsGroup actorsGrp;
        private Seraphine seraphine;
        private Gloom gloom;

        public static GenericEmitter particles;
        public static Hud hud;

        public static NokiaPhone nokiaPhone;

        override public void create()
        {
            base.create();

            FlxG.bloom.Visible = false;

            FlxObject f = new FlxObject(400, 592 / 2, 1, 1);
            add(f);

            FlxG.followBounds(0, 0, 2500, 2500, true);
            FlxG.follow(f, 5);


            if (FlxG.debug)
            {
                Console.WriteLine(" - F9 - Advance to next level");
                Console.WriteLine(" - R  - Reset To level 1");
                Console.WriteLine("Creating Level: {0}", FlxG.level);
            }


            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("fourchambers/crosshair"));

            FlxG.elapsedTotal = 0;

            // set level details

            Globals.numberOfEnemiesToKillBeforeLevelOver = 20;

            FlxG.showHud();
            FlxG.setHudTextScale(1, 1);
            FlxG.setHudTextPosition(1, 1, 1);

            Globals.getLevelFileName();

            Dictionary<string, string> levelAttrs = new Dictionary<string, string>();

            levelAttrs = FlxXMLReader.readAttributesFromOelFile(Globals.levelFile, "level");

            FlxG.playMp3("music/" + levelAttrs["music"], 0.550f);

            FlxG.levelWidth = Convert.ToInt32(levelAttrs["width"]);
            FlxG.levelHeight = Convert.ToInt32(levelAttrs["height"]);

            Console.WriteLine("Level Width: " + FlxG.levelWidth + " Level Height: " + FlxG.levelHeight);

            levelTilemap = new LevelTiles();
            add(levelTilemap);

            actorsGrp = new ActorsGroup();
            add(actorsGrp);

            seraphine = new Seraphine(-100, -100);
            actorsGrp.add(seraphine);

            gloom = new Gloom(-100, -100);
            actorsGrp.add(gloom);
            gloom.dead = true;
            gloom.exists = false;

            particles = new GenericEmitter(20, 20, 1, 1);
            add(particles);

            Utils.zoomOut();

            PerLevelAdjustments.adjustForLevel(actorsGrp, levelTilemap);
            PerLevelAdjustments.update(actorsGrp, levelTilemap);

            hud = new Hud();
            add(hud);

            nokiaPhone = new NokiaPhone();
            add(nokiaPhone);
            nokiaPhone.visible = false;

            TitleText t = new TitleText(FlxG.width / 2 - 100, FlxG.height / 2 - 50, 200, FlxG.width / 2 - 100, -50, 1.0f, 150);
            t.setFormat(Globals.TITLE_FONT, 1, Color.White, Color.Black, FlxJustification.Center);
            t.text = string.Format("Level {0}\n{1}", FlxG.level, levelAttrs["levelName"]);
            add(t);


        }

        public FlxObject getPlayerCharacter()
        {
            return actorsGrp.members.Find((FlxObject item) => item.GetType().ToString() == "FourChambers.Marksman");
        }

        override public void update()
        {
            FlxObject player = getPlayerCharacter();

            if (player!=null && player.dead && seraphine.concern == false)
            {
                seraphine.concern = true;
                seraphine.x = actorsGrp.members.Find((FlxObject item) => item.GetType().ToString() == "FourChambers.Marksman").x - 20;
                seraphine.y = actorsGrp.members.Find((FlxObject item) => item.GetType().ToString() == "FourChambers.Marksman").y - 30;
            }

            FlxU.collide(actorsGrp, levelTilemap.levelTiles);

            if (FlxG.fade.exists == false)
                FlxU.overlap(actorsGrp, actorsGrp, overlapCallback);
            
            collideArrows();

            PerLevelAdjustments.update(actorsGrp, levelTilemap);

            if (FlxG.debug)
            {
                //FlxG.setHudText(1, "Time in Level: " + FlxG.elapsedTotal.ToString().Split('.')[0] + " Collect " + Globals.numberOfEnemiesToKillBeforeLevelOver.ToString() + " more pests. Arrow Combo: " + Globals.arrowCombo + " Globals.arrowsFired: " + Globals.arrowsFired + " State: " + FlxG.state.ToString());
                
                runDebugKeyPresses();
            }

            base.update();

            if (FlxG.elapsedTotal>1.0f)
                runKeyPresses();
        }

        private void runKeyPresses()
        {
            FlxObject m = actorsGrp.members.Find((FlxObject item) => item.GetType().ToString() == "FourChambers.Marksman");
            if (m == null)
                return;

            if (FlxG.keys.justPressed(Keys.Escape) || FlxG.gamepads.isNewButtonPress(Buttons.Back))
            {
                FlxG.fade.start(Color.Black, Globals.FADE_OUT_TIME, goToCharacterSelectScreen, false);
                return;
            }

            if (m.dead==true)
            {
                FlxG.mouse.hide();

                if (FlxG.keys.R || FlxG.gamepads.isNewButtonPress(Buttons.Y))
                {
                    FlxG.setHudText(3, " ");

                    FlxG.fade.start(Color.Black, Globals.FADE_OUT_TIME, restartScene, false);
                    return;
                }
            }
        }





        private void runDebugKeyPresses()
        {
            //if (FlxG.keys.R || FlxG.gamepads.isNewButtonPress(Buttons.Y))
            //{
            //    Globals.arrowCombo = 0;
            //    FlxG.follow(null, 0);
            //    Utils.zoomOut();

            //    FlxG.state = new SingleScreenLevel();

            //    FlxG.setHudText(3, "");

            //    return;
            //}
            if (FlxG.keys.F8)
            {
                Globals.numberOfEnemiesToKillBeforeLevelOver = 5;
                hud.startTween();
            }
            if (FlxG.keys.F9)
            {
                Globals.numberOfEnemiesToKillBeforeLevelOver = 0;
                hud.startTween();
                Globals.arrowCombo = 20;

                actorsGrp.members.Find((FlxObject item) => item.GetType().ToString() == "FourChambers.Marksman").x = actorsGrp.members.Find((FlxObject item) => item.GetType().ToString() == "FourChambers.Door").x;
                actorsGrp.members.Find((FlxObject item) => item.GetType().ToString() == "FourChambers.Marksman").y = actorsGrp.members.Find((FlxObject item) => item.GetType().ToString() == "FourChambers.Door").y;

                foreach (var item in actorsGrp.members)
                {
                    if (!(item is Marksman))
                    {
                        //item.dead = true;
                    }
                }
            }
            if (FlxG.keys.justPressed(Keys.F1))
            {
                Utils.zoomOut();

                FlxG.follow(actorsGrp.members.Find((FlxObject item) => item.GetType().ToString() == "FourChambers.Marksman"), 1);
                FlxG.followBounds(0, 0, 480, 320, false);

            }
            if (FlxG.keys.justPressed(Keys.F2))
            {
                Utils.zoomIn();

                FlxG.follow(actorsGrp.members.Find((FlxObject item) => item.GetType().ToString() == "FourChambers.Marksman"), 1);
                FlxG.followBounds(0, 0, 480, 320, false);

            }
            if (FlxG.keys.justPressed(Keys.F3))
            {
                FlxG.color(Color.Tomato);
            }
            if (FlxG.keys.justPressed(Keys.F4))
            {

            }
        }

        private void collideArrows()
        {
            foreach (var item in actorsGrp.members)
            {
                if (item is Marksman)
                {
                    FlxU.overlap(((Marksman)(item)).allProjectiles, actorsGrp, runOverlapOnObject2);
                    FlxU.collide(((Marksman)(item)).allProjectiles, levelTilemap.levelTiles);

                }
            }
        }

        protected bool overlapWithLadder(object Sender, FlxSpriteCollisionEvent e)
        {
            if (e.Object1 is BaseActor)
            {
                if (!((BaseActor)(e.Object1)).flying)
                {
                    ((BaseActor)(e.Object1)).ladderPosX = e.Object2.x;
                    ((BaseActor)(e.Object1)).canClimbLadder = true;
                }
            }
            return true;
        }

        protected bool overlapCallback(object Sender, FlxSpriteCollisionEvent e)
        {
            ((FlxSprite)e.Object1).overlapped(((FlxSprite)e.Object2));
            ((FlxSprite)e.Object2).overlapped(((FlxSprite)e.Object1));

            //if can climb ladder and ladder

            if ((e.Object1 is Marksman) && (e.Object2 is Ladder))
            {
                overlapWithLadder(Sender, e);
            }

            return true;
        }


        protected bool runOverlapOnObject2(object Sender, FlxSpriteCollisionEvent e)
        {
            //((Arrow)e.Object1).kill();

            //Add bonus for long shots, 20+ frames
            //if (e.Object1 is Arrow)
            //    Console.WriteLine("Frames in air: {0} ", ((Arrow)e.Object1).framesInAir);

            ((FlxSprite)e.Object2).overlapped(((FlxSprite)e.Object1));

            return true;
        }

        public static void goToNextState(object sender, FlxEffectCompletedEvent e)
        {
            

            FlxG.state = new SingleScreenLevel();
            return;
        }

        public static void goToCharacterSelectScreen(object sender, FlxEffectCompletedEvent e)
        {
            Globals.arrowCombo = 0;
            FlxG.follow(null, 0);
            Utils.zoomOut();

            FlxG.level = 1;
            FlxG.state = new CharacterSelectScreen();

            FlxG.setHudText(3, "");

            return;
        }

        private static void restartScene(object sender, FlxEffectCompletedEvent e)
        {
            Globals.arrowCombo = 0;
            FlxG.follow(null, 0);
            Utils.zoomOut();

            //FlxG.level = 1;
            FlxG.state = new SingleScreenLevel();

            FlxG.setHudText(3, "");

            return;
        }


    }
}
