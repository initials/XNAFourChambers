using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
#if !WINDOWS_PHONE
//using Microsoft.Xna.Framework.GamerServices;
#endif
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using org.flixel;
using System.IO;

namespace Loader_Four
{
    /// <summary>
    /// Starts the game
    /// </summary>
    public class FlxFactory : Microsoft.Xna.Framework.Game
    {
        //graphics management
        public GraphicsDeviceManager _graphics;
        //other variables
        private FlxGame _flixelgame;

        //nothing much to see here, typical XNA initialization code
        public FlxFactory()
        {
            //set up the graphics device and the content manager
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            FlxG.zoom = FourChambers.Globals.DEBUG_ZOOM;

#if ! DEBUG

            FlxG.zoom=1;

#endif

            FlxG.resolutionWidth = 480 * FlxG.zoom;
            FlxG.resolutionHeight = 270 * FlxG.zoom;


            int newZoom = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / FlxG.resolutionWidth;

            int borderW = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width % FlxG.resolutionWidth;
            int borderH = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height % FlxG.resolutionHeight;



            int newW = newZoom * FlxG.resolutionWidth;
            int newH = newZoom * FlxG.resolutionHeight;
            Console.WriteLine("Will scale to : {0}x{1} - is widescreen?{2}", newW, newH, GraphicsAdapter.DefaultAdapter.IsWideScreen.ToString());

#if ! DEBUG
            FlxG.zoom = newZoom;

            FlxG.fullscreen = true;
            FlxG.resolutionWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width ;
            FlxG.resolutionHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

#endif
            /*
            FlxG.resolutionWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / div;
            FlxG.resolutionHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / div;
            */


            //int scaledUpH = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / FlxG.resolutionHeight;
            








            if (FlxG.fullscreen)
            {
                /*
                FlxG.borderWidth = borderW/2;
                FlxG.borderHeight = borderH/2;

                Console.WriteLine("BORDER WIDTH x:{0}-y:{1}", FlxG.borderWidth, FlxG.borderHeight);
                */

                //resX = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                //resY = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                //if (GraphicsAdapter.DefaultAdapter.IsWideScreen)
                //{
                //    //if user has it set to widescreen, let's make sure this
                //    //is ACTUALLY a widescreen resolution.
                //    if (((FlxG.resolutionWidth / 16) * 9) != FlxG.resolutionHeight)
                //    {
                //        FlxG.resolutionWidth = (FlxG.resolutionHeight / 9) * 16;
                //    }
                //}
            }

            //we don't need no new-fangled pixel processing
            //in our retro engine!
            _graphics.PreferMultiSampling = false;
            //set preferred screen resolution. This is NOT
            //the same thing as the game's actual resolution.
            _graphics.PreferredBackBufferWidth = FlxG.resolutionWidth;
            _graphics.PreferredBackBufferHeight = FlxG.resolutionHeight;
            //make sure we're actually running fullscreen if
            //fullscreen preference is set.
            if (FlxG.fullscreen && _graphics.IsFullScreen == false)
            {
                _graphics.ToggleFullScreen();
            }
            _graphics.ApplyChanges();

            Console.WriteLine("* Running Game at Settings: {0}x{1}\n -- Fullscreen?: {2}\n -- Preferrred: {3}x{4} \n -- Zoom: {5} \n -- FlxG.width/height {6} x {7} \n -- -- --", 
                FlxG.resolutionWidth, 
                FlxG.resolutionHeight, 
                FlxG.fullscreen, 
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, 
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height,
                FlxG.zoom,
                FlxG.width,
                FlxG.height);

            FlxG.Game = this;
#if !WINDOWS_PHONE
            //Components.Add(new GamerServicesComponent(this));
#endif
        }
        /// <summary>
        /// load up the master class, and away we go!
        /// </summary>
        protected override void Initialize()
        {
            //load up the master class, and away we go!

            //_flixelgame = new FlxGame();
            _flixelgame = new FlixelEntryPoint2(this);

            FlxG.bloom = new BloomPostprocess.BloomComponent(this);

            Components.Add(_flixelgame);
            Components.Add(FlxG.bloom);

            base.Initialize();
        }

    }

    #region Application entry point

    static class Program
    {
        //application entry point
        static void Main(string[] args)
        {
            using (FlxFactory game = new FlxFactory())
            {
                game.Run();
            }
        }
    }

    #endregion
}
