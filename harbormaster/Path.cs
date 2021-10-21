using System;
using System.Numerics;
using System.Collections.Generic;
using Raylib_cs;


public class Path
{
    public List<Vector2> nodes = new List<Vector2>() { new Vector2(-1000, -1000) };

    public void Update()
    {

    }

    public void Draw()
    {

    }

    public void AddNode(Vector2 target)
    {
        if (nodes[0] == new Vector2(-1000, -1000))
        {
            nodes[0] = target;
        }
        else
        {
            nodes.Add(target);
        }
    }
}