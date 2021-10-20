using System;
using System.Numerics;
using Raylib_cs;

//RAYLIB SHIT
const int windowHeight = 1020;
const int windowWidth = 800;
Raylib.InitWindow(windowHeight, windowWidth, "Harbor Master");
Raylib.SetTargetFPS(60);

string gameState = "game";

Boat b = new Boat();

Rectangle target = new Rectangle(600, 600, 30, 30);

Vector2 mousePos;

while (!Raylib.WindowShouldClose())
{
    if (gameState == "game")
    {
        //LOGIC
        mousePos = new Vector2(Raylib.GetMouseX(), Raylib.GetMouseY());
        b.dir = Vector2.Normalize(mousePos - b.center) * 10;

        b.Update();

        if (Raylib.CheckCollisionCircleRec(b.center, b.r, target))
        {
            gameState = "end";
        }


        //DRAWING
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.WHITE);
        b.Draw();
        Raylib.DrawRectangleRec(target, Color.DARKBLUE);
        Raylib.DrawLineV(b.center, b.center + b.dir, Color.BLACK);

        Raylib.EndDrawing();
    }
    else
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.WHITE);

        Raylib.DrawText("END", 100, 100, 84, Color.BLACK);

        Raylib.EndDrawing();
    }
}


