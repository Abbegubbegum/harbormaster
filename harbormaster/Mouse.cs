using System;
using System.Numerics;
using Raylib_cs;


public class Mouse
{
    public Vector2 pos;
    ///Filled with an empty vector as default
    public Vector2 clickPos = new Vector2();

    //Filled with a placeholder boat as default
    public Boat selectedboat = new Boat(0, 0, 0, 0);


    public void Update()
    {
        //Updates mouse position
        pos = new Vector2(Raylib.GetMouseX(), Raylib.GetMouseY());

        //If mouse is clicked this fram, save the position, otherwise empty it
        if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
        {
            clickPos = pos;
        }
        else
        {
            clickPos = new Vector2();
        }

        if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_RIGHT_BUTTON))
        {
            selectedboat.selected = false;
            selectedboat = new Boat(0, 0, 0, 0);
        }


    }
}
