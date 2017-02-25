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

                string nameOfNewActor = "FourChambers." + FlxU.firstLetterToUpper(nodes["Name"]);

                var type = Type.GetType(nameOfNewActor);
                if (nameOfNewActor == "FourChambers.Door")
                {
                    var newDoor = (FlxSprite)Activator.CreateInstance(type, Convert.ToInt32(nodes["x"]), Convert.ToInt32(nodes["y"]));
                    add(newDoor);
                }
            }

            foreach (Dictionary<string, string> nodes in actorsAttrs)
            {
                string nameOfNewActor = "FourChambers." + FlxU.firstLetterToUpper( nodes["Name"] );

                var type = Type.GetType(nameOfNewActor);

                Console.WriteLine("Making " + nameOfNewActor);

                try
                {
                    if (nameOfNewActor == "FourChambers.Door")
                    {
                        //ignore
                    }
                    else if (nodes.ContainsKey("event") && nodes.ContainsKey("height") && nodes.ContainsKey("width"))
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
                    else if (nodes.ContainsKey("spriteName"))
                    {
                        var sprite = (FlxObject)Activator.CreateInstance(Type.GetType("FourChambers." + FlxU.firstLetterToUpper(nodes["spriteName"])), Convert.ToInt32(nodes["x"]), Convert.ToInt32(nodes["y"]));
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
                        if (!sprite.onScreen() )
                        {
                            if (sprite is BaseActor)
                            {
                                if (((BaseActor)sprite).isPlayerControlled==false)
                                {
                                    sprite.dead = true;
                                    sprite.exists = false;
                                    sprite.x = -100;
                                    sprite.y = -100;
                                }
                            }
                            else
                            {
                                sprite.dead = true;
                                sprite.exists = false;
                                sprite.x = -100;
                                sprite.y = -100;
                            }

                            Console.WriteLine(" -> Not on screen. killing: " + nameOfNewActor);

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

            // seems to do weird things to climbing the ladder.
            //members = members.OrderBy(d => d.renderOrder).ToList();

        }

        override public void update()
        {

            
            foreach (var item in members)
            {
                
                

                //if (FlxG.elapsedTotal < 5)
                //{
                //    FlxSprite gloom = (FlxSprite)members.Find((FlxObject sp) => sp.GetType().ToString() == "FourChambers.Gloom");
                //    if (gloom.onScreen() == false)
                //    {
                //        List<FlxObject> spawnPoints = members.FindAll((FlxObject sp) => sp.GetType().ToString() == "FourChambers.SpawnPoint");
                //        FlxSprite randomPosition = (FlxSprite)spawnPoints[FlxU.randomInt(0, spawnPoints.Count)];
                //        gloom.reset(randomPosition.x, randomPosition.y);
                //    }

                //}
            }
            
            base.update();
        }

        public override void render(SpriteBatch spriteBatch)
        {
            base.render(spriteBatch);
        }

    }
}