using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using org.flixel;

using System.Linq;
using System.Xml.Linq;

using XNATweener;
using System.IO;

namespace FourChambers
{
    public class GameSelectionMenuState : FlxState
    {

        private FlxText _menuItems;

        private FlxButton play;
        private FlxButton playProcedural;
        private FlxButton editName;
        private FlxButton playMultiPlayer;

        private FlxSprite bgSprite;

        private FlxSprite bgSprite2;


        private Tweener tween;



        private ColorTweener colTween;

        private Vector2 offsetButton;

        override public void create()
        {

            //FlxG.playMusic("music/" + FourChambers_Globals.MUSIC_MENU, 1.0f);

            FlxG.bloom.Settings = BloomPostprocess.BloomSettings.PresetSettings[2]; //6 is the super bright.
            FourChambers_Globals.hasMeleeWeapon = false;
            FourChambers_Globals.hasRangeWeapon = false;

            FlxG.backColor = new Color(0.2f, 0.2f, 0.2f);

            base.create();

            FlxG._game.hud.hudGroup = new FlxGroup();

            FlxG.resetHud();
            FlxG.showHud();

            tween = new Tweener(0, -240 , TimeSpan.FromSeconds(3.9f), Quadratic.EaseOut);
            //colTween = new ColorTweener(FlxColor.ToColor("#55B4FF"), FlxColor.ToColor("#ffffff"), 1.5f, Quadratic.EaseOut);
            //colTween.Play();

            // -350, -310
            //bgSprite = new FlxSprite(-350, 0, FlxG.Content.Load<Texture2D>("fourchambers/Fear"));
            //add(bgSprite);



            //Texture2D bgGraphic = FlxG.Content.Load<Texture2D>("fourchambers/" + levelAttrs["bgGraphic"]);
            bgSprite = new FlxSprite(-350, 0);
            bgSprite.loadGraphic("fourchambers/Fear");
            bgSprite.scrollFactor.X = 1.0f;
            bgSprite.scrollFactor.Y = 1.0f;
            bgSprite.boundingBoxOverride = false;
            bgSprite.allowColorFlicker = false;
            bgSprite.color = FlxColor.ToColor("#55B4FF");
            add(bgSprite);

            for (int i = 0; i < 6; i++)
            {
                FlxSprite cloud = new FlxSprite(FlxU.random(0, FlxG.width), FlxU.random(0, FlxG.height));
                cloud.loadGraphic("fourchambers/cloud", false, false, 160, 64);
                cloud.setScrollFactors(0.1f, 0.1f);

                cloud.setVelocity(FlxU.random(-5, 5), 0);
                cloud.alpha = 0.85f;
                add(cloud);

            }

            FlxG.mouse.show(FlxG.Content.Load<Texture2D>("initials/crosshair"));
            FlxG.mouse.cursor.offset.X = 5;
            FlxG.mouse.cursor.offset.Y = 5;
            
            _menuItems = new FlxText(0, 10, FlxG.width);
            _menuItems.setFormat(FlxG.Content.Load<SpriteFont>("initials/SpaceMarine"), 1, Color.White, FlxJustification.Center, Color.White);
            //_menuItems.text = "Four Chambers\n\nEnter name, use @ symbol to specify Twitter handle.\nPress enter when complete.";
            _menuItems.text = " ";
            _menuItems.shadow = Color.Black;
            add(_menuItems);

            //_nameEntry = new FlxText(10, FlxG.height, FlxG.width);
            //_nameEntry.setFormat(null, 1, Color.White, FlxJustification.Left, Color.White);
            //_nameEntry.text = "Username";
            //add(_nameEntry);

            play = new FlxButton(FlxG.width / 2 - 50, FlxG.height - 105, playGame, FlxButton.ControlPadA);
            play.loadGraphic((new FlxSprite()).loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/menuButton"), false, false, 100, 20), (new FlxSprite()).loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/menuButtonPressed"), false,false,100,20));
            play.loadText(new FlxText(2, 2, 100, "Play Game"), new FlxText(2, 2, 100, "Play Game!"));
            add(play);
            play.on = true;
            play.debugName = "playGame";

            playProcedural = new FlxButton(FlxG.width / 2 - 50, FlxG.height - 80, playGameTutorial, FlxButton.ControlPadA);
            playProcedural.loadGraphic((new FlxSprite()).loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/menuButton"), false, false, 100, 20), (new FlxSprite()).loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/menuButtonPressed"), false, false, 100, 20));
            playProcedural.loadText(new FlxText(2, 2, 100, "Multiplayer"), new FlxText(2, 2, 100, "Multiplayer!"));
            add(playProcedural);
            playProcedural.debugName = "Multiplayer";

            editName = new FlxButton(FlxG.width / 2 - 50, FlxG.height - 30, goToDataEntryState, FlxButton.ControlPadA);
            editName.loadGraphic((new FlxSprite()).loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/menuButton"), false, false, 100, 20), (new FlxSprite()).loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/menuButtonPressed"), false, false, 100, 20));
            editName.loadText(new FlxText(2, 2, 100, "Edit Name"), new FlxText(2, 2, 100, "Edit Name"));
            add(editName);
            editName.debugName = "editName";

            playMultiPlayer = new FlxButton(FlxG.width / 2 - 50, FlxG.height - 55, playMultiPlayerGame, FlxButton.ControlPadA);
            playMultiPlayer.loadGraphic((new FlxSprite()).loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/menuButton"), false, false, 100, 20), (new FlxSprite()).loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/menuButtonPressed"), false, false, 100, 20));
            playMultiPlayer.loadText(new FlxText(2, 2, 100, "Credits"), new FlxText(2, 2, 100, "Credits"));
            add(playMultiPlayer);
            playMultiPlayer.debugName = "credits";

            offsetButton = new Vector2(400, -10);

            FlxG.setHudGamepadButton(FlxHud.TYPE_XBOX, FlxButton.ControlPadA, (play.getScreenXY().X * FlxG.zoom) + offsetButton.X, (play.getScreenXY().Y * FlxG.zoom) + offsetButton.Y);

            FlxG.flash.start(Color.Black, 1.5f);

            FlxG.color(Color.White);

            try
            {
                FlxG.username = LoadFromDevice();
            }
            catch
            {
                Console.WriteLine("Cannot load name from file");
            }

            if (FlxG.username != "")
            {
                //_nameEntry.text = FlxG.username;
                FlxG.setHudText(3, "Username:\n"+FlxG.username);
                FlxG.setHudTextPosition(3, 50, FlxG.height - 30);
                FlxG.setHudTextScale(3, 2);
            }

            FlxSprite robot = new FlxSprite(220, 30);
            robot.loadGraphic("fourchambers/logo/FourChambersLogo", true, false, 800, 173);
            //add(robot);

            FlxG._game.hud.hudGroup.add(robot);



        }

        public string LoadFromDevice()
        {
            string value1 = File.ReadAllText("nameinfo.txt");
            return value1.Substring(0, value1.Length - 1);
        }

        public void setAllButtonsToOff()
        {
            play.on = false;
            playProcedural.on = false;
            editName.on = false;
            playMultiPlayer.on = false;
        }

        override public void update()
        {

            
            if ((FlxG.keys.justPressed(Keys.Escape) || FlxG.gamepads.isNewButtonPress(Buttons.Back)) && elapsedInState > 1.0f )
            {
                FlxG.Game.Exit();
                return;
            }

            if (FlxG.keys.justPressed(Keys.Up) || FlxG.gamepads.isNewButtonPress(Buttons.DPadUp) || FlxG.gamepads.isNewButtonPress(Buttons.LeftThumbstickUp ))
            {

                if (play.on)
                {
                    setAllButtonsToOff();
                    editName.on = true;
                    FlxG.setHudGamepadButton(FlxHud.TYPE_XBOX, FlxButton.ControlPadA, (editName.getScreenXY().X * FlxG.zoom) + offsetButton.X, (editName.getScreenXY().Y * FlxG.zoom) + offsetButton.Y);
                }
                else if (playProcedural.on)
                {
                    setAllButtonsToOff();
                    play.on = true;
                    FlxG.setHudGamepadButton(FlxHud.TYPE_XBOX, FlxButton.ControlPadA, (play.getScreenXY().X * FlxG.zoom) + offsetButton.X, (play.getScreenXY().Y * FlxG.zoom) + offsetButton.Y);
                }
                else if (playMultiPlayer.on)
                {
                    setAllButtonsToOff();
                    playProcedural.on = true;
                    FlxG.setHudGamepadButton(FlxHud.TYPE_XBOX, FlxButton.ControlPadA, (playProcedural.getScreenXY().X * FlxG.zoom) + offsetButton.X, (playProcedural.getScreenXY().Y * FlxG.zoom) + offsetButton.Y);
                }
                else if (editName.on)
                {
                    setAllButtonsToOff();
                    playMultiPlayer.on = true;
                    FlxG.setHudGamepadButton(FlxHud.TYPE_XBOX, FlxButton.ControlPadA, (playMultiPlayer.getScreenXY().X * FlxG.zoom) + offsetButton.X, (playMultiPlayer.getScreenXY().Y * FlxG.zoom) + offsetButton.Y);
                }
            }
            if (FlxG.keys.justPressed(Keys.Down) || FlxG.gamepads.isNewButtonPress(Buttons.DPadDown) || FlxG.gamepads.isNewButtonPress(Buttons.LeftThumbstickDown))
            {

                if (play.on)
                {
                    setAllButtonsToOff();
                    playProcedural.on = true;
                    FlxG.setHudGamepadButton(FlxHud.TYPE_XBOX, FlxButton.ControlPadA, (playProcedural.getScreenXY().X * FlxG.zoom) + offsetButton.X, (playProcedural.getScreenXY().Y * FlxG.zoom) + offsetButton.Y);
                }
                else if (playProcedural.on)
                {
                    setAllButtonsToOff();
                    playMultiPlayer.on = true;
                    FlxG.setHudGamepadButton(FlxHud.TYPE_XBOX, FlxButton.ControlPadA, (playMultiPlayer.getScreenXY().X * FlxG.zoom) + offsetButton.X, (playMultiPlayer.getScreenXY().Y * FlxG.zoom) + offsetButton.Y);
                }
                else if (playMultiPlayer.on)
                {
                    setAllButtonsToOff();
                    editName.on = true;
                    FlxG.setHudGamepadButton(FlxHud.TYPE_XBOX, FlxButton.ControlPadA, (editName.getScreenXY().X * FlxG.zoom) + offsetButton.X, (editName.getScreenXY().Y * FlxG.zoom) + offsetButton.Y);
                }
                else if (editName.on)
                {
                    setAllButtonsToOff();
                    play.on = true;
                    FlxG.setHudGamepadButton(FlxHud.TYPE_XBOX, FlxButton.ControlPadA, (play.getScreenXY().X * FlxG.zoom) + offsetButton.X, (play.getScreenXY().Y * FlxG.zoom) + offsetButton.Y);
                }
            }
            if (FlxG.gamepads.isNewButtonRelease(Buttons.A) && play._counter > 0.5f)
            {

                if (play.on)
                {
                    playGame();
                }
                else if (playProcedural.on)
                {
                    playGameTutorial();
                }
                else if (editName.on)
                {
                    goToDataEntryState();
                }
                else if (playMultiPlayer.on)
                {
                    playMultiPlayerGame();
                }
            }


            PlayerIndex pi;
            if (FlxG.gamepads.isNewButtonPress(Buttons.X, FlxG.controllingPlayer, out pi))
            {
                FlxG.joystickBeingUsed = true;
            }


            tween.Update(FlxG.elapsedAsGameTime);
            bgSprite.y = tween.Position;

            base.update();

            if (FlxG.keys.justPressed(Keys.F9))
            {
                //FlxG.bloom.Visible = !FlxG.bloom.Visible;
                FlxG.state = new TextTestState();
            }

            if (FlxG.username == "" || FlxG.username==null)
            {
				#if !__ANDROID__
                FlxG.state = new DataEntryState();
				#endif
            }

            if (FlxG.keys.justPressed(Keys.F2))
            {
                FlxG.level = 101;
                FlxG.score = 0;
                FlxG.hideHud();

                FlxG.state = new MultiPlayerPlayState();
                return;

            }
            if (FlxG.keys.justPressed(Keys.F3))
            {
                FlxG.level = 103;
                FlxG.score = 0;
                FlxG.hideHud();

                FlxG.state = new MultiPlayerBaseState();
                return;

            }
            if (FlxG.keys.justPressed(Keys.F4))
            {
                FlxG.level = 103;
                FlxG.score = 0;
                FlxG.hideHud();

                FlxG.state = new MultiPlayerCharacterSelect();
                return;

            }
            if (FlxG.keys.justPressed(Keys.F5))
            {
                FlxG.hideHud();

                FlxG.state = new AllLevels();
                return;

            }
            if (FlxG.keys.justPressed(Keys.F6))
            {
                FlxG.hideHud();

                FlxG.state = new MapState();
                return;

            }



        }

        /// <summary>
        /// 
        /// </summary>
        public void playGame()
        {
            FlxG.level = 1;
            FlxG.score = 0;
            FlxG.hideHud();

            //FlxG.transition.startFadeOut(0.1f,0,120);

            play = null;

            //if (FlxG.debug == false)
            //{
            //    FourChambers_Globals.startGame();
            //}
            FourChambers_Globals.startGame();

            FlxG.state = new BasePlayStateFromOel();

            //colTween.Play();

            return;

        }
        //playGameTutorial
        public void playGameTutorial()
        {
            //Console.WriteLine("Play Tutorial");

            //FlxG.level = -1;
            //FlxG.score = 0;
            //FlxG.hideHud();

            ////FlxG.transition.startFadeOut(0.1f,0,120);

            //FlxG.state = new BasePlayStateFromOelTutorial();


            FlxG.level = 103;
            FlxG.score = 0;
            FlxG.hideHud();
            FourChambers_Globals.startGame();
            FlxG.state = new MultiPlayerCharacterSelect();
            return;




        }

        public void playGameProcedural()
        {
            Console.WriteLine("Play Game Proc");

            FlxG.level = 1;
            FlxG.score = 0;
            FlxG.hideHud();

            //FlxG.transition.startFadeOut(0.1f,0,120);

            FlxG.state = new FourChambers.BasePlayStateFromOel();

        }
        public void goToDataEntryState()
        {
			#if !__ANDROID__
            FlxG.transition.resetAndStop();
            FlxG.state = new FourChambers.DataEntryState();
			#endif
        }

        public void playMultiPlayerGame()
        {
            //Console.WriteLine("Play MultiPlayer Game Proc");

            //FlxG.level = 101;
            //FlxG.score = 0;
            //FlxG.hideHud();

            ////FlxG.transition.startFadeOut(0.1f,0,120);

            FlxG.state = new Credits();
        }
    }
        
}
