using System;
using System.Numerics;
using Raylib_cs;

namespace harbormaster
{
    public class Boat
    {
        //OLD RECTANGLE SHIT WHICH I MIGHT DO SOMETIME BUT NOT NOW
        // public Rectangle rec = new Rectangle(100, 400, 25, 50);
        // public Vector2 center = new Vector2(rec.x + rec.width / 2, rec.y + rec.height / 2)
        // public int TriangleLenght { get; private set; } = 10;
        //Global Variables
        private const int windowWidth = 1020;
        private const int windowHeight = 800;

        //Body 
        public Vector2 center;
        public int radius = 12;

        //Color 
        private Color c = Color.RED;
        private Color fullRegularColor = Color.RED;
        private Color fullHighlightColor = Color.ORANGE;
        private Color dockedColor = Color.YELLOW;
        private Color finishedRegularColor = Color.LIME;
        private Color finishedHighlightedColor = Color.GREEN;
        public bool selected = false;

        //Movement 
        public Vector2 dir;
        private float speed = 2f;

        //Pathfinding
        public Path p;
        private readonly float pathCollisionMargin = 1f;

        //Game
        public bool destroyed = false;
        public bool docked = false;
        public bool dockable = true;
        private int dockTimer = 0;
        private int dockTime = 5;
        public bool invincible = true;

        //DEBUGGING
        public bool outsideArea = false;



        //Spawning
        Random r = new();
        private int outsideMargin = 20;

        public Boat(bool randomize, int x = 0, int y = 0, int dirx = 0, int diry = 0)
        {
            if (randomize)
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
            else
            {
                center = new Vector2(x, y);

                dir = Vector2.Normalize(new Vector2(dirx, diry));
            }

            p = new Path(this);
        }

        public void Update()
        {
            //If the boat is on the screen make it not invincible
            if (center.X + radius < windowWidth && center.X - radius > 0 && center.Y + radius < windowHeight && invincible)
            {
                invincible = false;
            }



            //If there isn't a path
            if (p.nodes.Count == 0)
            {
                //If the path is disabled meaning that we had a path towards a dock but not anymore, the boat should be docked
                if (p.disabled == true)
                {
                    docked = true;
                    dockable = false;
                    dir = new Vector2(0, 0);
                    dockTimer++;
                }
            }
            //Else if it still isn't docked
            else if (!docked)
            {
                //If it reached the next node of the path, remove it
                if (Raylib.CheckCollisionPointCircle(p.nodes[0], center, pathCollisionMargin))
                {
                    p.RemoveFirstNode();
                }
            }
            //If it still has a path
            if (p.nodes.Count != 0 && !docked)
            {
                //Set direction towards next node in path
                SetDirToTarget(p.nodes[0]);
            }

            //Move it based on direction
            center.X += dir.X * speed;
            center.Y += dir.Y * speed;


            //If the number of frames it has been docked is equal to the full time in seconds, undock the boat
            if (dockTimer == dockTime * 60)
            {
                docked = false;
                p.Toggle();
                dir = new Vector2(0, 1);
                dockTimer = 0;
            }



            //Change color based on different values
            c = docked ? dockedColor : dockable ? selected ? fullHighlightColor : fullRegularColor : selected ? finishedHighlightedColor : finishedRegularColor;
        }



        public void Draw()
        {
            //Draw Boat Circle
            Raylib.DrawCircleV(center, radius, c);

            //OLD RECTANGLE SHIT WHICH I MIGHT DO SOMETIME BUT NOT NOW
            // Raylib.DrawRectangleRec(rec, c);
            // Raylib.DrawTriangleLines(new Vector2(rec.x + rec.width, rec.y), new Vector2(rec.x + rec.width / 2, rec.y - TriangleLenght), new Vector2(rec.x, rec.y), Color.BLACK);

            //You guessed it... if there is a path
            if (p.nodes.Count != 0)
            {
                //Draw a line to the current path target
                Raylib.DrawLineV(center, p.nodes[0], Color.BLACK);

                //If the boat is selected, draw the rest of the path
                if (selected)
                {
                    p.Draw();
                }
            }

        }

        //Sets Direction of boat towards a target Vector2
        public void SetDirToTarget(Vector2 target)
        {
            dir = Vector2.Normalize(target - center);
        }

        //Called when the boat gets a path towards a dock
        public void OnPathToDock()
        {
            p.Toggle();
        }
    }
}