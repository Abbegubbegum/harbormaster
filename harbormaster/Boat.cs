using System;
using System.Numerics;
using Raylib_cs;


public class Boat
{
    // public Rectangle rec = new Rectangle(100, 400, 25, 50);
    // public Vector2 center = new Vector2(rec.x + rec.width / 2, rec.y + rec.height / 2)
    public Vector2 center = new Vector2(100, 400);
    public int r = 12;
    private Color c = Color.RED;
    private Color regularColor = Color.RED;
    private Color highlightColor = Color.ORANGE;
    public int TriangleLenght { get; private set; } = 10;
    public Vector2 dir = new Vector2(0, 0);
    private float speed = 0.2f;
    public Path p = new Path();
    public bool selected = false;

    public void Update()
    {
        if (p.nodes[0] != new Vector2(-1000, -1000))
        {
            SetDir(p.nodes[0]);
        }

        center.X += dir.X * speed;
        center.Y += dir.Y * speed;

        if (selected)
        {
            c = highlightColor;
        }
        else
        {
            c = regularColor;
        }
    }

    public void Draw()
    {
        Raylib.DrawCircleV(center, r, c);
        // Raylib.DrawRectangleRec(rec, c);
        // Raylib.DrawTriangleLines(new Vector2(rec.x + rec.width, rec.y), new Vector2(rec.x + rec.width / 2, rec.y - TriangleLenght), new Vector2(rec.x, rec.y), Color.BLACK);
    }

    public void SetDir(Vector2 target)
    {
        dir = Vector2.Normalize(target - center) * 10;
    }
}
