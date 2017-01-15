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
    public class AllLevels : FlxState
    {
        FlxSprite f;

        FlxTilemap destructableTilemap;
        FlxGroup g;
        FlxGroup g2;
        FlxGroup doors;


        int c;

        override public void create()
        {
            base.create();
            c = 0;
            int lastLength = 0;
            g = new FlxGroup();
            g2 = new FlxGroup();
            doors = new FlxGroup();


            for (int i = 0; i < 22; i++)
            {
                string levelFile = "ogmoLevels/level" + i.ToString() + ".oel";

                Dictionary<string, string>  destructableAttrs = new Dictionary<string, string>();
                destructableAttrs = FlxXMLReader.readAttributesFromOelFile(levelFile, "level/collide");

                destructableTilemap = new FlxTilemap();
                destructableTilemap.auto = FlxTilemap.STRING;
                destructableTilemap.loadMap(destructableAttrs["collide"], FlxG.Content.Load<Texture2D>("fourchambers/" + destructableAttrs["tileset"]), 16,16);
                destructableTilemap.boundingBoxOverride = true;
                g.add(destructableTilemap);
                destructableTilemap.visible = false;
                //lastLength += destructableTilemap.widthInTiles;


                destructableAttrs = FlxXMLReader.readAttributesFromOelFile(levelFile, "level/Incollide");

                destructableTilemap = new FlxTilemap();
                destructableTilemap.auto = FlxTilemap.STRING;
                destructableTilemap.loadMap(destructableAttrs["Incollide"], FlxG.Content.Load<Texture2D>("fourchambers/" + destructableAttrs["tileset"]), 16, 16);
                destructableTilemap.boundingBoxOverride = true;
                g2.add(destructableTilemap);
                destructableTilemap.visible = false;
                //lastLength += destructableTilemap.widthInTiles;

                List<Dictionary<string, string>> actorsAttrs = new List<Dictionary<string, string>>();
                actorsAttrs = FlxXMLReader.readNodesFromOelFile(levelFile, "level/ActorsLayer");
                foreach (Dictionary<string, string> nodes in actorsAttrs)
                {
                    if (nodes["Name"] == "door")
                    {
                        FlxSprite d = new FlxSprite(Convert.ToInt32(nodes["x"]),Convert.ToInt32(nodes["y"]));
                        d.createGraphic(24, 24, Color.Red);
                        doors.add(d);
                        d.debugName = i.ToString();


                        FlxText t = new FlxText(Convert.ToInt32(nodes["x"]), Convert.ToInt32(nodes["y"]) - 32, 100);
                        t.alignment = FlxJustification.Left;
                        t.text = nodes["levelToGoTo"];
                        t.scale = 3;
                        doors.add(t);
                        t.debugName = i.ToString();

                        Console.WriteLine("Adding Door");
                    }
                }



            }

            add(g);
            add(g2);
            add(doors);







            f = new FlxSprite(0, 0);
            f.loadGraphic("surt/race_or_die", true, false, 4, 4);
            add(f);

            FlxG.follow(f, 10.0f);
            FlxG.followBounds(0, 0, int.MaxValue, int.MaxValue);
        }

        override public void update()
        {
            if (FlxControl.RIGHTJUSTPRESSED)
            {
                g.members[c].visible = false;
                g2.members[c].visible = false;

                c++;
                g.members[c].visible = true;
                g2.members[c].visible = true;

                foreach (FlxObject item in doors.members)
                {
                    item.visible = false;

                    if (item.debugName == c.ToString())
                    {
                        item.visible = true;
                    }

                }
            }
            if (FlxControl.LEFTJUSTPRESSED)
            {
                g.members[c].visible = false;
                g2.members[c].visible = false;
                c--;
                g.members[c].visible = true;
                g2.members[c].visible = true;
                foreach (FlxObject item in doors.members)
                {
                    item.visible = false;

                    if (item.debugName == c.ToString())
                    {
                        item.visible = true;
                    }

                }
            }

            base.update();
        }


    }
}
