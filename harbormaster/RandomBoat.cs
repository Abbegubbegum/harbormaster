using System;
using System.Numerics;

namespace harbormaster
{
    public class RandomBoat : Boat
    {

        private Random r = new Random();
        public RandomBoat() : base()
        {
            //Randomizes one of the 3 edges for simplicity
            int edge = r.Next(0, 3);

            //Randomizes the position on a line parallell of the random edge but offset with margin
            switch (edge)
            {
                case 0:
                    center = new Vector2(-outsideMargin, r.Next(0 + radius, windowHeight - radius));
                    break;

                case 1:
                    center = new Vector2(windowWidth + outsideMargin, r.Next(0 + radius, windowHeight - radius));
                    break;

                case 2:
                    center = new Vector2(r.Next(0 + radius, windowWidth - radius), windowHeight + outsideMargin);
                    break;
            }

            //Set movement direction to the center of the screen to make sure it comes on screen
            SetDirToTarget(new Vector2(windowWidth / 2, windowHeight / 2));
        }
    }
}