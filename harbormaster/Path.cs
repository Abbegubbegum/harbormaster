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
        public bool disabled = false;
        // private Boat parentBoat;

        // public Path(Boat b)
        // {
        //     parentBoat = b;
        // }

        //Draws the rest of the path of the boat that is selected
        public void Draw()
        {
            if (nodes.Count > 1)
            {
                for (int i = 0; i < nodes.Count - 1; i++)
                {
                    Raylib.DrawLineV(nodes[i], nodes[i + 1], Color.GRAY);
                }
            }
        }

        //Adds a node to the end of the path
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


    }
}