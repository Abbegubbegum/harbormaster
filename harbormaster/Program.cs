using System;
using System.Numerics;
using Raylib_cs;

//RAYLIB SHIT
const int windowHeight = 1020;
const int windowWidth = 800;
Raylib.InitWindow(windowHeight, windowWidth, "Harbor Master");
Raylib.SetTargetFPS(60);

//Game Variables
string gameState = "game";

//Instanciate Mouse Class
Mouse mouse = new Mouse();

//Boat
Boat b = new Boat();

//Placeholder
Rectangle target = new Rectangle(600, 600, 30, 30);



while (!Raylib.WindowShouldClose())
{
    //MAIN GAME
    if (gameState == "game")
    {
        //---------LOGIC---------

        //Update all mouse positions
        mouse.Update();

        // b.SetDir(mouse.pos);

        //If mouse clicked somewhere
        if (mouse.clickPos != new Vector2())
        {
            // b.SetDir(mouse.clickPos);

            //If it clicked on boat, select that boat
            if (Raylib.CheckCollisionPointCircle(mouse.clickPos, b.center, b.r))
            {
                mouse.selectedboat = b;
                b.selected = true;
            }
            //Else add node to selected nodes path
            else
            {
                mouse.selectedboat.p.AddNode(mouse.clickPos);
            }
        }

        //Boat Movement
        b.Update();

        //Placeholder
        if (Raylib.CheckCollisionCircleRec(b.center, b.r, target))
        {
            gameState = "end";
        }


        //---------DRAWING---------
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.WHITE);

        //Boats
        b.Draw();

        //Placeholders
        Raylib.DrawRectangleRec(target, Color.DARKBLUE);
        Raylib.DrawLineV(b.center, b.center + (b.dir * 10), Color.BLACK);

        Raylib.EndDrawing();
    }
    //END SCREEN
    else
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.WHITE);

        Raylib.DrawText("END", 100, 100, 84, Color.BLACK);

        Raylib.EndDrawing();
    }
}


