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
                        if (Convert.ToInt32(nodes["y"])==0)
                        {
                            //myObject.dead = true;
                            //myObject.x = -100;
                            //myObject.y = -100;

                        }


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
            float chance = 0.00251f;

            if (FlxG.elapsedTotal > 15) chance = 0.0251f;
            if (FlxG.elapsedTotal > 30) chance = 0.0451f;
            if (FlxG.elapsedTotal > 45) chance = 0.0851f;
            
            foreach (var item in members)
            {
                if (item.dead && !item.exists)
                {
                    if (FlxU.random() < chance)
                    {
                        //Respawn based on chance of respawn
                        item.reset(8, 64);
                    }
                }
            }
            
            base.update();
        }

    }
}