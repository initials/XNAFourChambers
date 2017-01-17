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
            actorsAttrs = FlxXMLReader.readNodesFromOelFile(Globals.levelFile, "level/ActorsLayer");

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
                    
                    if (nodes.ContainsKey("event") && nodes.ContainsKey("height") && nodes.ContainsKey("width"))
                    {
                        var sprite = (FlxObject)Activator.CreateInstance(Type.GetType("FourChambers." + FlxU.firstLetterToUpper(nodes["event"])), 
                            Convert.ToInt32(nodes["x"]), 
                            Convert.ToInt32(nodes["y"]),
                            Convert.ToInt32(nodes["width"]), 
                            Convert.ToInt32(nodes["height"]));

                        add(sprite);
                    }
                    else if (nodes.ContainsKey("event"))
                    {
                        var sprite = (FlxObject)Activator.CreateInstance(Type.GetType("FourChambers." + FlxU.firstLetterToUpper(nodes["event"])), Convert.ToInt32(nodes["x"]), Convert.ToInt32(nodes["y"]));
                        add(sprite);
                        //if (nodes["event"] == "waterfall")
                        //{
                        //    ((Waterfall)(sprite)).colliderHeight = Convert.ToInt32(nodes["height"]);
                        //    ((Waterfall)(sprite)).collider.y += Convert.ToInt32(nodes["height"]);
                        //}

                    }
                    else if (nodes.ContainsKey("height") && nodes.ContainsKey("width"))
                    {
                        var sprite = (FlxSprite)Activator.CreateInstance(type, Convert.ToInt32(nodes["x"]), Convert.ToInt32(nodes["y"]), Convert.ToInt32(nodes["width"]), Convert.ToInt32(nodes["height"]));
                        add(sprite);
                    } 
                    else if (nodes.ContainsKey("height"))
                    {
                        var sprite = (FlxSprite)Activator.CreateInstance(type, Convert.ToInt32(nodes["x"]), Convert.ToInt32(nodes["y"]), 16, Convert.ToInt32(nodes["height"]));
                        add(sprite);
                    }
                    else {
                    //if (nodes["Name"] == "marksman")
                    //{
                        var sprite = (FlxSprite)Activator.CreateInstance(type, Convert.ToInt32(nodes["x"]), Convert.ToInt32(nodes["y"]) - 16);
                        add(sprite);
                        if (!sprite.onScreen())
                        {
                            sprite.dead = true;
                            sprite.exists = false;
                            sprite.x = -100;
                            sprite.y = -100;

                        }


                    //}
                    }
                }
                catch (Exception)
                {
                    
                }
            }

            // return a sorted list of members based on render order

            //int i = 0;
            //FlxObject o;
            //int ml = members.Count;


            //// sort members by y to draw correctly.
            //members = members.OrderBy(d => d.renderOrder).ToList();

        }

        override public void update()
        {
            float chance = 0.00251f;

            if (FlxG.elapsedTotal < 15) chance = 0.000251f;
            else if (FlxG.elapsedTotal < 30) chance = 0.000451f;
            else if (FlxG.elapsedTotal < 45) chance = 0.001651f;
            
            foreach (var item in members)
            {
                if (item.dead && !item.exists)
                {
                    if (FlxU.random() < chance)
                    {
                        // Get list of respawn Point
                        List<FlxObject> spawnPoints = members.FindAll((FlxObject sp) => sp.GetType().ToString() == "FourChambers.SpawnPoint");

                        FlxObject randomPosition = spawnPoints[FlxU.randomInt(0, spawnPoints.Count)];

                        //Respawn based on chance of respawn
                        item.reset(randomPosition.x, randomPosition.y);
                    }
                }
                if (item is ZingerNest && item.debugName=="readyToPop"  && !item.dead)
                {
                    List<FlxObject> zingers = members.FindAll((FlxObject sp) => sp.GetType().ToString() == "FourChambers.Zinger");
                    for (int i = 0; i < zingers.Count; i++)
                    {
                        if (((Zinger)(zingers[i])).dead == true)
                        {
                            ((Zinger)(zingers[i])).reset(item.x, item.y);
                            ((Zinger)(zingers[i])).homing = true;
                            ((Zinger)(zingers[i])).homingTarget = (FlxSprite)members.Find((FlxObject sp) => sp.GetType().ToString() == "FourChambers.Marksman");

                        }
                    }
                    item.kill();
                }
            }
            
            base.update();
        }

        public override void render(SpriteBatch spriteBatch)
        {
            

            base.render(spriteBatch);
        }

    }
}