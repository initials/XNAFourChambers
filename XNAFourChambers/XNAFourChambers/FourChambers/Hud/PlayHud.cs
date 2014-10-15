using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

using org.flixel;
using XNATweener;


namespace FourChambers
{
    class PlayHud : FlxGroup
    {
        /// <summary>
        /// use setArrowsRemaining(Member)
        /// </summary>
        public FlxText arrowsRemaining;

        //public FlxText healthText;
        /// <summary>
        /// Use score.text = "" to set the score.
        /// </summary>
        public FlxText score;
        public FlxText combo;

        public FlxText nestsRemaining;
        
        private Tweener tweenPos;
        private Tweener tweenScale;

        public FlxSprite currentAnimatedObj;

        private float ypos;

        public LevelBeginText comboOnScreen;
        public FlxSprite hudGraphic;

        public Heart heart;

        public FlxBar swordPowerBar;
        public FlxBar arrowPowerBar;

        public FlxSprite swordPower;
        public FlxSprite arrowPower;


        //FlxBar bar2;
        //FlxBar bar3;


        public PlayHud()
        {

            currentAnimatedObj = new FlxSprite();

            ypos = FlxG.height - 30; //* FlxG.zoom

			#if __ANDROID__

			ypos -= 60;

			#endif

            //hudGraphic = new FlxSprite(0,0, FlxG.Content.Load<Texture2D>("fourchambers/hudElements"));
            //hudGraphic.scrollFactor.X = 0;
            //hudGraphic.scrollFactor.Y = 0;
            //hudGraphic.scale = 2;
            //add(hudGraphic);

            //hudGraphic.x = hudGraphic.width / 2;
            ////hudGraphic.y = ypos - hudGraphic.height /2;
            //hudGraphic.health = 100;

            heart = new Heart(2, (int)ypos);
            add(heart);

            //arrowsRemaining = new FlxText(65, ypos-5, 100);
            //arrowsRemaining.setFormat(null, 2, Color.White, FlxJustification.Left, Color.Black);
            //arrowsRemaining.text = "00";
            //add(arrowsRemaining);

            //combo = new FlxText(140, ypos - 10, 100);
            //combo.setFormat(null, 2, Color.White, FlxJustification.Left, Color.Black);
            //combo.text = "Combo: 0x";
            //add(combo);

            score = new FlxText(0, ypos - 10, FlxG.width-10);
            score.setFormat(null, 2, Color.White, FlxJustification.Right, Color.Black);
            score.text = "Score: 000000";
            add(score);

            swordPowerBar = new FlxBar(20, (int)ypos+10 , FlxBar.FILL_LEFT_TO_RIGHT, 20, 2, null, " ", 0, 12, false);
            swordPowerBar.loadCustomEmptyGraphic("ui/bar_01");
            swordPowerBar.emptyBar.setScrollFactors(0, 0);
            swordPowerBar.filledBar.setScrollFactors(0, 0);
            swordPowerBar.emptyBar.setOffset(2, 3);
            add(swordPowerBar);

            swordPower = new FlxSprite((int)swordPowerBar.emptyBar.x, (int)swordPowerBar.emptyBar.y-16);
            swordPower.loadGraphic("fourchambers/pickups_16x16", false, false, 16, 16);
            //swordPower.x = (int)swordPowerBar.x;
            //swordPower.y = (int)swordPowerBar.y;
            swordPower.frame = (PowerUp.FR_Sword);
            swordPower.setScrollFactors(0, 0);
            add(swordPower);




            arrowPowerBar = new FlxBar(50, (int)ypos+10, FlxBar.FILL_LEFT_TO_RIGHT, 20, 2, null, " ", 0, 12, false);
            arrowPowerBar.loadCustomEmptyGraphic("ui/bar_01");
            arrowPowerBar.emptyBar.setScrollFactors(0, 0);
            arrowPowerBar.filledBar.setScrollFactors(0, 0);
            arrowPowerBar.emptyBar.setOffset(2, 3);
            add(arrowPowerBar);

            arrowPower = new FlxSprite((int)arrowPowerBar.emptyBar.x, (int)arrowPowerBar.emptyBar.y-16);
            arrowPower.loadGraphic("fourchambers/pickups_16x16", false, false, 16, 16);
            //arrowPower.x = (int)arrowPowerBar.x;
            //arrowPower.y = (int)arrowPowerBar.y;
            arrowPower.frame = (PowerUp.FR_Arrows);
            arrowPower.setScrollFactors(0, 0);
            add(arrowPower);

        }

        public override void update()
        {
            //tweenScale.Update(FlxG.elapsedAsGameTime);
            //tweenPos.Update(FlxG.elapsedAsGameTime);
            
            //if (currentAnimatedObj != null)
            //{
            //    currentAnimatedObj.scale = tweenScale.Position;
            //    currentAnimatedObj.y = tweenPos.Position;
            //}

            int i2 = 0;
            int l2 = this.members.Count;
            
            while (i2 < l2)
            {
                if (this.members[i2].GetType().ToString() == "org.flixel.FlxText")
                {
                    if ((this.members[i2] as FlxText).scale > 2)
                    {
                        (this.members[i2] as FlxText).scale -= 0.1f;
                    }
                    else if ((this.members[i2] as FlxText).scale < 2)
                    {
                        (this.members[i2] as FlxText).scale = 2;
                    }
                }

                i2++;
            }

            base.update();
        }

        ///// <summary>
        ///// Use this to activate a member of the treasures.
        ///// </summary>
        ///// <param name="Member"></param>
        //public void collectTreasure (int Member)
        //{
        //    ((FlxSprite)this.members[Member]).play("on");
        //    currentAnimatedObj = ((FlxSprite)members[Member]);

        //    tweenScale = new Tweener(10, 1, TimeSpan.FromSeconds(1.0f), Linear.EaseOut);
        //    //tweenScale.Ended += StartDrop;
        //    tweenScale.Delay = 1.0f;

        //    tweenPos = new Tweener(100, ypos, TimeSpan.FromSeconds(2.0f), Bounce.EaseOut);
        //    tweenPos.Delay = 2.0f;
        //}

        //public void setArrowsRemaining(int ArrowsRemaining)
        //{
        //    if (ArrowsRemaining <= 4)
        //    {
        //        arrowsRemaining.color = Color.Red;
        //        arrowsRemaining.text = "0" + ArrowsRemaining.ToString();
        //    }
        //    else if (ArrowsRemaining <= 9)
        //    {
        //        arrowsRemaining.color = Color.Orange;
        //        arrowsRemaining.text = "0"+ ArrowsRemaining.ToString();
        //    }
        //    else if (ArrowsRemaining <= 99)
        //    {
        //        arrowsRemaining.color = Color.White;
        //        arrowsRemaining.text = ArrowsRemaining.ToString();
        //    }
        //    else
        //    {
        //        arrowsRemaining.color = Color.White;
        //        arrowsRemaining.text = "99+";
        //    }
        //}




    }
}
