using System;
using System.Numerics;
using Raylib_cs;


public class Boat
{
    //OLD RECTANGLE SHIT WHICH I MIGHT DO SOMETIME BUT NOT NOW
    // public Rectangle rec = new Rectangle(100, 400, 25, 50);
    // public Vector2 center = new Vector2(rec.x + rec.width / 2, rec.y + rec.height / 2)
    // public int TriangleLenght { get; private set; } = 10;

    //Body 
    public Vector2 center = new Vector2(100, 400);
    public int r = 12;

    //Color 
    private Color c = Color.RED;
    private Color regularColor = Color.RED;
    private Color highlightColor = Color.ORANGE;
    public bool selected = false;

    //Movement 
    public Vector2 dir = new Vector2(0, 0);
    private float speed = 2f;

    //Pathfinding
    public Path p = new Path();
    private float pathCollisionMargin = 1f;


    public void Update()
    {
        //If there is a path
        if (p.nodes.Count != 0)
        {
            //If it reached the next node of the path, remove it
            if (Raylib.CheckCollisionPointCircle(p.nodes[0], center, pathCollisionMargin))
            {
                p.RemoveFirstNode();
            }
        }
        //If it still has a path
        if (p.nodes.Count != 0)
        {
            //Set direction towards next node in path
            SetDir(p.nodes[0]);
        }

        //Move it based on direction
        center.X += dir.X * speed;
        center.Y += dir.Y * speed;

        //Change color based on if its selected or not
        c = selected ? highlightColor : regularColor;
    }

    public void Draw()
    {
        //Draw Boat Circle
        Raylib.DrawCircleV(center, r, c);

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

    //Sets Direction of boat towards a Vector2
    public void SetDir(Vector2 target)
    {
        dir = Vector2.Normalize(target - center);
    }
}
