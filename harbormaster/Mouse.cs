using System;
using System.Numerics;
using Raylib_cs;

namespace harbormaster
{
    public class Mouse
    {
        //Mouse position
        public Vector2 pos;

        //Filled with an empty vector as default
        public Vector2 clickPos = new();

        //Filled with a placeholder boat as default
        public Boat selectedBoat = new(false);

        //Margin for acceptable distance between clicks before registering
        private readonly int newClickMargin = 3;


        public void Update()
        {
            //Updates mouse position
            pos = new Vector2(Raylib.GetMouseX(), Raylib.GetMouseY());

            //If mouse click is down this frame and is far enough away from last click, save the position, otherwise empty it
            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
            {
                if ((pos - clickPos).Length() >= newClickMargin)
                {
                    clickPos = pos;
                }
                else
                {
                    clickPos = new Vector2();
                }
            }
            else
            {
                selectedBoat.selected = false;
                selectedBoat = new Boat(false);
            }

            //If right mouse is pressed, empty selected boat completely
            // if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_RIGHT_BUTTON))
            // {
            //     selectedBoat.selected = false;
            //     selectedBoat = new Boat(false);
            // }

            //If middle mouse is pressed, remove last node on selected boat path
            // if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_MIDDLE_BUTTON))
            // {
            //     selectedBoat.p.RemoveLastNode();
            // }


        }

        public void AddNode(Dock d)
        {
            //if it clicked on a dock, add a node to dock position and set up shit for the dock
            if (Raylib.CheckCollisionPointRec(clickPos, d.hitBox) && selectedBoat.dockable)
            {
                selectedBoat.OnPathToDock(d);
            }
            //Else add regular node
            else if (!Raylib.CheckCollisionPointRec(clickPos, d.hitBox))
            {
                selectedBoat.p.AddNode(clickPos);
            }
        }
    }
}