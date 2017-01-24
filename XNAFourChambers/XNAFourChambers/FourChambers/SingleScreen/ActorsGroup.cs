﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.flixel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace FourChambers
{
    public class ActorsGroup : FlxGroup
    {
        
        List<Dictionary<string, string>> actorsAttrs;

        public ActorsGroup()
            : base()
        {

            actorsAttrs = new List<Dictionary<string, string>>();
            actorsAttrs = FlxXMLReader.readNodesFromOelFile(Globals.levelFile, "level/ActorsLayer");

            foreach (Dictionary<string, string> nodes in actorsAttrs)
            {
                string nameOfNewActor = "FourChambers." + FlxU.firstLetterToUpper( nodes["Name"] );

                var type = Type.GetType(nameOfNewActor);

                Console.WriteLine("Making " + nameOfNewActor);

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
                        var sprite = (FlxSprite)Activator.CreateInstance(type, Convert.ToInt32(nodes["x"]), Convert.ToInt32(nodes["y"]) );
                        add(sprite);
                        if (!sprite.onScreen())
                        {
                            sprite.dead = true;
                            sprite.exists = false;
                            sprite.x = -100;
                            sprite.y = -100;

                        }

                        if (nameOfNewActor == "FourChambers.Sign")
                        {
                            ((Sign)(sprite)).message = nodes["message"].ToString();
                        }

                        //Console.WriteLine(nodes.ContainsKey("message"));

                        //Console.WriteLine(sprite.GetType().GetProperty("message") != null );

                        //if ( nodes.ContainsKey("message") == true &&  sprite.GetType().GetProperty("message") != null) 
                        //{
                        //    Console.WriteLine("writing the sign message");
                        //    ((Sign)(sprite)).message = nodes["message"].ToString();
                        //}
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Failed to Make " + nameOfNewActor);
                }
            }

            //// sort members by y to draw correctly.
            members = members.OrderBy(d => d.renderOrder).ToList();

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
                    if (FlxU.random() < chance || FlxG.keys.justPressed(Keys.F4))
                    {
                        // Get list of respawn Point
                        List<FlxObject> spawnPoints = members.FindAll((FlxObject sp) => sp.GetType().ToString() == "FourChambers.SpawnPoint");

                        FlxSprite randomPosition = (FlxSprite)spawnPoints[FlxU.randomInt(0, spawnPoints.Count)];

                        //Respawn based on chance of respawn
                        item.reset(randomPosition.x, randomPosition.y);

                        Console.WriteLine("Respawning {0} at {1} x {2}", item.GetType().ToString(), randomPosition.x, randomPosition.y);

                        //if (FlxG.keys.justPressed(Keys.F4))
                        //    break;

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

                if (FlxG.elapsedTotal < 5)
                {
                    FlxSprite gloom = (FlxSprite)members.Find((FlxObject sp) => sp.GetType().ToString() == "FourChambers.Gloom");
                    if (gloom.onScreen() == false)
                    {
                        List<FlxObject> spawnPoints = members.FindAll((FlxObject sp) => sp.GetType().ToString() == "FourChambers.SpawnPoint");
                        FlxSprite randomPosition = (FlxSprite)spawnPoints[FlxU.randomInt(0, spawnPoints.Count)];
                        gloom.reset(randomPosition.x, randomPosition.y);
                    }

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