using System;
using System.Numerics;
using Raylib_cs;

namespace harbormaster
{
    public class Dock
    {
        //Body
        private Rectangle wallTop;
        private Rectangle wallRight;
        private Rectangle wallLeft;
        public Rectangle hitBox;
        private Color c = Color.BROWN;
        private readonly int wallThickness = 6;
        private readonly int boatRadius = 12;
        private readonly int margin = 5;

        public Vector2 center;

        //Weard shit creating everything from an x position since it is always on the top edge of the game
        public Dock(int x)
        {

            wallTop = new Rectangle(x - (boatRadius + margin + wallThickness), 0, (boatRadius + margin + wallThickness) * 2, wallThickness);
            wallLeft = new Rectangle(wallTop.x, wallTop.y + wallThickness, wallThickness, (boatRadius + margin) * 2);
            wallRight = new Rectangle(wallTop.x + wallTop.width - wallThickness, wallTop.y + wallThickness, wallThickness, (boatRadius + margin) * 2);
            hitBox = new Rectangle(wallTop.x, wallTop.y, wallRight.x + wallRight.width - wallTop.x, wallTop.y + wallTop.height + wallRight.height);

            center = new Vector2(x, wallThickness + margin + boatRadius);
        }

        public void Draw()
        {
            Raylib.DrawRectangleRec(wallTop, c);
            Raylib.DrawRectangleRec(wallLeft, c);
            Raylib.DrawRectangleRec(wallRight, c);
        }

    }
}