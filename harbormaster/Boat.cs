using System;
using System.Numerics;
using Raylib_cs;


public class Boat
{
    public Vector2 center = new Vector2(100, 400);
    public int r = 12;
    private Color c = Color.ORANGE;
    public int TriangleLenght { get; private set; } = 10;
    public Vector2 dir = new Vector2(0, 0);
    private float speed = 0.2f;

    public void Update()
    {
        center.X += dir.X * speed;
        center.Y += dir.Y * speed;
    }

    public void Draw()
    {
        Raylib.DrawCircleV(center, r, c);
        // Raylib.DrawRectangleRec(rec, c);
        // Raylib.DrawTriangleLines(new Vector2(rec.x + rec.width, rec.y), new Vector2(rec.x + rec.width / 2, rec.y - TriangleLenght), new Vector2(rec.x, rec.y), Color.BLACK);
    }
}
