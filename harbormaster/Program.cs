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

Mouse mouse = new Mouse();

Rectangle target = new Rectangle(600, 600, 30, 30);



while (!Raylib.WindowShouldClose())
{
    if (gameState == "game")
    {
        //LOGIC
        mouse.Update();
        // b.SetDir(mouse.pos);

        if (mouse.clickPos != new Vector2())
        {
            // b.SetDir(mouse.clickPos);
            if (Raylib.CheckCollisionPointCircle(mouse.clickPos, b.center, b.r))
            {
                mouse.selectedboat = b;
                b.selected = true;
            }
            else
            {
                //if (mouse.selectedboat finns)
                {
                    mouse.selectedboat.p.AddNode(mouse.clickPos);
                }
            }
        }

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


