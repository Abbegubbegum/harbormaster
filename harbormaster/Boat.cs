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
        protected const int windowWidth = 1020;
        protected const int windowHeight = 800;

        //Body 
        public Vector2 center;
        public int radius = 12;

        //Colors for different states 
        private Color c = Color.RED;
        private Color fullRegularColor = Color.RED;
        private Color fullHighlightColor = Color.ORANGE;
        private Color dockedColor = Color.YELLOW;
        private Color finishedRegularColor = Color.LIME;
        private Color finishedHighlightedColor = Color.GREEN;

        //Movement 
        public Vector2 dir;
        private readonly float speed = 1f;

        //Pathfinding
        public Path p;
        private readonly float pathCollisionMargin = 1f;

        //Different States
        public bool selected = false;
        public bool crashed = false;
        public bool docked = false;
        public bool dockable = true;
        public bool invincible = true;

        //Docktiming stuff
        private int dockTimer = 0;
        private readonly int dockTime = 5;

        public Boat(int x = 0, int y = 0, int dirx = 0, int diry = 0)
        {

            center = new Vector2(x, y);

            dir = Vector2.Normalize(new Vector2(dirx, diry));


            //Add the path with a link to this boat object
            p = new Path(this);
        }

        public void Update()
        {
            //Incriment plz
            dockTimer++;

            //If the boat is on the screen make it not invincible
            if (center.X + radius < windowWidth && center.X - radius > 0 && center.Y + radius < windowHeight && invincible)
            {
                invincible = false;
            }


            if (!docked)
            {
                //If there is a path
                if (p.nodes.Count > 0)
                {
                    //If it reached the next node of the path, remove it
                    if (Raylib.CheckCollisionPointCircle(p.nodes[0], center, pathCollisionMargin))
                    {
                        p.RemoveFirstNode();
                    }

                    //If there still is a path after that, Set direction towards next node in path
                    if (p.nodes.Count > 0)
                    {
                        SetDirToTarget(p.nodes[0]);
                    }
                }
                //If there isn't a path at this point and the path is disabled meaning that we had a path towards a dock but not anymore
                if (p.nodes.Count == 0 && p.disabled)
                {
                    DockBoat();
                }
            }

            //Move it based on direction
            center.X += dir.X * speed;
            center.Y += dir.Y * speed;

            //If the number of frames it has been docked is equal to the full time in seconds, undock the boat
            if (dockTimer == dockTime * 60 && docked)
            {
                docked = false;
                dockable = false;
                p.disabled = false;
                dir = new Vector2(0, 1);
                dockTimer = 0;
            }

            //Change color based on different values
            c = docked ? dockedColor : dockable ? selected ? fullHighlightColor : fullRegularColor : selected ? finishedHighlightedColor : finishedRegularColor;
        }

        private void DockBoat()
        {
            docked = true;
            dockable = false;
            dir = new Vector2(0, 0);
            dockTimer = 0;
            dockTimer++;
            p.disabled = true;
        }

        public void Draw()
        {
            //Draw Boat Circle
            Raylib.DrawCircleV(center, radius, c);

            //OLD RECTANGLE SHIT WHICH I MIGHT DO SOMETIME BUT NOT NOW
            // Raylib.DrawRectangleRec(rec, c);
            // Raylib.DrawTriangleLines(new Vector2(rec.x + rec.width, rec.y), new Vector2(rec.x + rec.width / 2, rec.y - TriangleLenght), new Vector2(rec.x, rec.y), Color.BLACK);

            //If there is a path draw it
            if (p.nodes.Count != 0)
            {
                p.Draw();
            }

        }

        //Sets Direction of boat towards a target Vector2
        public void SetDirToTarget(Vector2 target)
        {
            dir = Vector2.Normalize(target - center);
        }

        //Called when the boat gets a pathed towards a dock
        public void OnPathToDock(Dock d)
        {
            p.AddNode(new Vector2(d.center.X, d.center.Y + (12 + 12 + 5)));
            p.AddNode(d.center);
            p.disabled = true;
        }

        public bool CheckBoatCrash(Boat b)
        {
            if (Raylib.CheckCollisionCircles(center, radius, b.center, b.radius))
            {
                crashed = true;
                b.crashed = true;
                return true;
            }

            return false;
        }
    }
}