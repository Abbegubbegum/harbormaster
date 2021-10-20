using System;
using System.Numerics;
using Raylib_cs;


public class Boat
{
    public Rectangle rec = new Rectangle(100, 400, 12, 24);
    public Color c = Color.ORANGE;
    private int triangleLength = 10;

    public void Update()
    {

    }

    public void Draw()
    {
        Raylib.DrawRectangleRec(rec, c);
        Raylib.DrawTriangle(new Vector2(rec.x + rec.width, rec.y), new Vector2(rec.x + rec.width / 2, rec.y + triangleLength), new Vector2(rec.x, rec.y), c);
    }
}
