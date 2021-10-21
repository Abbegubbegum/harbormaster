using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;


public class Path
{
    //List of all nodes 
    public List<Vector2> nodes = new List<Vector2>();

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
        nodes.Add(target);
    }

    //Removes the next node in the path
    public void RemoveFirstNode()
    {
        nodes.RemoveAt(0);
    }
}