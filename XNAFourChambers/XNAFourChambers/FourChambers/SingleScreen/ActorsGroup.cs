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
    class ActorsGroup : FlxGroup
    {
        
        List<Dictionary<string, string>> actorsAttrs;

        public ActorsGroup()
            : base()
        {

            actorsAttrs = new List<Dictionary<string, string>>();
            actorsAttrs = FlxXMLReader.readNodesFromOelFile(FourChambers_Globals.levelFile, "level/ActorsLayer");

            foreach (Dictionary<string, string> nodes in actorsAttrs)
            {
                bool pc = false;
                int localWidth = 0;
                int localHeight = 0;
                string PX = "";
                string PY = "";
                uint PT = 0;
                int PS = 0;
                float PC = 0.0f;
                //print nodes;

                //foreach (KeyValuePair<string, string> kvp in nodes)
                //{
                //    Console.Write("Key = {0}, Value = {1}, ",
                //        kvp.Key, kvp.Value);
                //}
                //Console.Write("\r\n");

                string nameOfNewActor = "FourChambers." + FlxU.firstLetterToUpper( nodes["Name"] );

                var type = Type.GetType(nameOfNewActor);

                //Console.WriteLine(nodes["Name"]);

                try
                {
                    if (nodes.ContainsKey("height") && nodes.ContainsKey("width"))
                    {
                        var myObject = (FlxSprite)Activator.CreateInstance(type, Convert.ToInt32(nodes["x"]), Convert.ToInt32(nodes["y"]), Convert.ToInt32(nodes["width"]), Convert.ToInt32(nodes["height"]));
                        add(myObject);
                    }
                    else if (nodes.ContainsKey("height"))
                    {
                        var myObject = (FlxSprite)Activator.CreateInstance(type, Convert.ToInt32(nodes["x"]), Convert.ToInt32(nodes["y"]), 16, Convert.ToInt32(nodes["height"]));
                        add(myObject);
                    }
                    else {
                    //if (nodes["Name"] == "marksman")
                    //{
                        var myObject = (FlxSprite)Activator.CreateInstance(type, Convert.ToInt32(nodes["x"]), Convert.ToInt32(nodes["y"]) - 16);
                        add(myObject);
                    //}
                    }
                }
                catch (Exception)
                {
                    
                }





            }
        }

        override public void update()
        {

            foreach (var item in members)
            {
                if (item.dead && !item.onScreen())
                {
                    //Respawn based on chance of respawn
                    //Console.WriteLine("Found a dead bee");
                    //dead = false;
                    //item.x = 68;
                    //item.y = 68;
                    //visible = true;
                    //((FlxSprite)item).play("fly");
                    //item.velocity.X = ((Zinger)item).runSpeed;
                }
            }

            base.update();
        }

    }
}