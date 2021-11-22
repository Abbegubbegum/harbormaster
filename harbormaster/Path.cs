using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;

namespace harbormaster
{
    public class Path
    {
        //List of all nodes 
        public List<Vector2> nodes = new();

        //If more nodes can be added or not
        public bool disabled = false;

        //Reference to parent boat
        private Boat parentBoat;

        public Path(Boat b)
        {
            parentBoat = b;
        }

        //Draws the path with black if boat is selected or gray if not
        public void Draw()
        {
            //Draw line from boat to first node
            Raylib.DrawLineV(parentBoat.center, nodes[0], parentBoat.selected ? Color.BLACK : Color.GRAY);

            //Draw the rest of the lines between the nodes
            for (int i = 0; i < nodes.Count - 1; i++)
            {
                Raylib.DrawLineV(nodes[i], nodes[i + 1], parentBoat.selected ? Color.BLACK : Color.GRAY);
            }
        }

        //Adds a node to the end of the path aslong as the path isn't disabled
        public void AddNode(Vector2 target)
        {
            if (!disabled)
            {
                nodes.Add(target);
            }
        }

        public void RemoveFirstNode()
        {
            nodes.RemoveAt(0);
        }

        public void RemoveLastNode()
        {
            nodes.RemoveAt(nodes.Count - 1);
            disabled = false;
        }

        public void Toggle()
        {
            disabled = !disabled;
        }

        public void Reset()
        {
            nodes.Clear();
        }
    }
}