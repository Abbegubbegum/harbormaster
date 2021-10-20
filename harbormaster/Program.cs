using System;
using System.Numerics;
using Raylib_cs;

const int windowHeight = 1020;
const int windowWidth = 800;

Boat b = new Boat();


Raylib.InitWindow(windowHeight, windowWidth, "Harbor Master");

while (!Raylib.WindowShouldClose())
{
    //LOGIC





    //DRAWING
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.WHITE);
    b.Draw();
    Raylib.EndDrawing();
}


