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
    public class MultiPlayerCharacterSelect : FlxState
    {
        List<int> selectable;
        int[] current = new int[4];
        FlxGroup icons;

        override public void create()
        {
            base.create();

            icons = new FlxGroup();

            selectable = new List<int> { 
                EnemyActor.FR_marksman, 
                EnemyActor.FR_succubus, 
                EnemyActor.FR_paladin, 
                EnemyActor.FR_unicorn, 
                EnemyActor.FR_vampire, 
                EnemyActor.FR_warlock, 
                EnemyActor.FR_corsair,
                EnemyActor.FR_automaton, 
                EnemyActor.FR_executor, 
                EnemyActor.FR_gloom, 
                EnemyActor.FR_harvester, 
                EnemyActor.FR_mummy, 
                EnemyActor.FR_seraphine, 
                EnemyActor.FR_mistress,
                EnemyActor.FR_medusa, 
                EnemyActor.FR_zombie,
                EnemyActor.FR_tormentor
            };
            current = new int[] { 0, 0, 0, 0 };

            //FourChambers_Globals.MUSIC_LEVEL11;

            for (int i = 0; i < 4; i++)
            {
                FlxSprite bg = new FlxSprite((FlxG.width / 4) * i, 0);
                Color c = Color.Black;
                switch (i)
                {   
                    case 0:
                        c = Color.DeepSkyBlue;
                        break;
                    case 1:
                        c = Color.MediumPurple;
                        break;
                    case 2:
                        c = Color.MediumVioletRed;
                        break;
                    case 3:
                        c = Color.Orange;
                        break;

                    default:
                        break;
                }

                bg.createGraphic(FlxG.width / 4, FlxG.height, c);
                
                add(bg);

                FlxSprite e = new FlxSprite((FlxG.width / 4) * i, 130);
                e.loadGraphic(FlxG.Content.Load<Texture2D>("fourchambers/allActors"), true, false, 26, 26);
                e.frame = EnemyActor.FR_marksman;
                e.scale = 4;
                icons.add(e);

            }



            add(icons);

        }

        override public void update()
        {

            if (FlxG.keys.justPressed(Keys.Right) || FlxG.keys.justPressed(Keys.D))
            {
                current[0]++;
                if (current[0] >= selectable.Count)
                {
                    current[0] = 0;
                }
                ((FlxSprite)(icons.members[0])).frame = selectable[current[0]];
            }
            if (FlxG.keys.justPressed(Keys.Left) || FlxG.keys.justPressed(Keys.A))
            {
                current[0]--;
                if (current[0] < 0)
                {
                    current[0] = selectable.Count - 1;
                }
                ((FlxSprite)(icons.members[0])).frame = selectable[current[0]];
            }

            PlayerIndex pi;
            for (PlayerIndex i = PlayerIndex.One; i <= PlayerIndex.Four; i++)
            {
                int playerIndexAsInt = 1;

                if (i == PlayerIndex.One) playerIndexAsInt = 0;
                else if (i == PlayerIndex.Two) playerIndexAsInt = 1;
                else if (i == PlayerIndex.Three) playerIndexAsInt = 2;
                else if (i == PlayerIndex.Four) playerIndexAsInt = 3;

                if (FlxG.gamepads.isNewButtonPress(Buttons.DPadLeft, i, out pi))
                {
                    Console.WriteLine( "DPad Left ");
                    current[playerIndexAsInt]--;
                    if (current[playerIndexAsInt] < 0)
                    {
                        current[playerIndexAsInt] = selectable.Count - 1;
                    }
                    ((FlxSprite)(icons.members[playerIndexAsInt])).frame = selectable[current[playerIndexAsInt]];


                }
                if (FlxG.gamepads.isNewButtonPress(Buttons.DPadRight, i, out pi))
                {
                    Console.WriteLine("DPad Right " );
                    current[playerIndexAsInt]++;
                    if (current[playerIndexAsInt] >= selectable.Count)
                    {
                        current[playerIndexAsInt] = 0;
                    }
                    ((FlxSprite)(icons.members[playerIndexAsInt])).frame = selectable[current[playerIndexAsInt]];

                }


            }


            //if (FlxG.keys.F4)
            //{
            //    for (int i = 1; i < 4; i++)
            //    {
            //        current[i] = (int)FlxU.random(0, selectable.Count);
            //        ((FlxSprite)(icons.members[i])).frame = selectable[current[i]];
            //    }
                

            //}

            if (FlxControl.ACTIONJUSTPRESSED)
            {

                int co = 0;
                foreach (var item in icons.members)
                {
                    FourChambers_Globals.multiplayerSelectedCharacters[co] = current[co];
                    co++;
                }

                FlxG.state = new MultiPlayerBaseState();
                return;
            }

            base.update();
        }


    }
}
