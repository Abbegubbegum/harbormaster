using System;
using System.Collections.Generic;
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

        public bool clicked = false;

        //Filled with a placeholder boat as default
        public Boat selectedBoat = new();

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
                    clicked = true;
                }
            }
            else
            {
                selectedBoat.selected = false;
                selectedBoat = new Boat();
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

        public void AddNode(List<Dock> docks)
        {
            foreach (Dock dock in docks)
            {
                //if it clicked on a dock, add a node to dock position and set up shit for the dock
                if (Raylib.CheckCollisionPointRec(clickPos, dock.hitBox) && selectedBoat.dockable)
                {
                    selectedBoat.OnPathToDock(dock);
                    return;
                }
            }
            selectedBoat.p.AddNode(clickPos);
        }

        public bool CheckBoatClick(Boat b)
        {
            if (Raylib.CheckCollisionPointCircle(clickPos, b.center, b.radius))
            {
                selectedBoat.selected = false;
                b.selected = true;
                selectedBoat = b;
                selectedBoat.p.Reset();
                return true;
            }
            return false;
        }
    }
}