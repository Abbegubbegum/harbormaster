using System;
using System.Numerics;
using Raylib_cs;

namespace harbormaster
{
    public class Mouse
    {
        public Vector2 pos;
        ///Filled with an empty vector as default
        public Vector2 clickPos = new();

        //Filled with a placeholder boat as default
        public Boat selectedBoat = new(false);


        public void Update()
        {
            //Updates mouse position
            pos = new Vector2(Raylib.GetMouseX(), Raylib.GetMouseY());

            //If mouse is clicked this frame, save the position, otherwise empty it
            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_LEFT_BUTTON))
            {
                clickPos = pos;
            }
            else
            {
                clickPos = new Vector2();
            }

            //If right mouse is clicked, empty selected boat completely
            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_RIGHT_BUTTON))
            {
                selectedBoat.selected = false;
                selectedBoat = new Boat(false);
            }

            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_MIDDLE_BUTTON))
            {
                selectedBoat.p.RemoveLastNode();
            }


        }

        public void OnDockClick(Dock d)
        {
            selectedBoat.p.AddNode(new Vector2(d.center.X, d.center.Y + (12 + 12 + 5)));
            selectedBoat.p.AddNode(d.center);
            selectedBoat.OnPathToDock();
        }
    }
}