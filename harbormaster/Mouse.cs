using System;
using System.Numerics;
using Raylib_cs;


public class Mouse
{
    public Vector2 pos;
    public Vector2 clickPos = new Vector2();
    public Boat selectedboat;


    public void Update()
    {
        pos = new Vector2(Raylib.GetMouseX(), Raylib.GetMouseY());

        if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
        {
            clickPos = pos;
        }
        else
        {
            clickPos = new Vector2();
        }
    }

    public void Draw()
    {

    }
}
