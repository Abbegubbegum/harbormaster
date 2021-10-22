using System;
using System.Collections.Generic;
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
List<Boat> boats = new List<Boat>() { new Boat(100, 400, 1, 1000), new Boat(400, 200, 5, 1) };

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


        //If mouse clicked somewhere
        if (mouse.clickPos != new Vector2())
        {
            bool boatClick = false;
            for (int i = 0; i < boats.Count; i++)
            {
                //If it clicked on boat, select that boat
                if (Raylib.CheckCollisionPointCircle(mouse.clickPos, boats[i].center, boats[i].r))
                {
                    mouse.selectedboat = boats[i];
                    boats[i].selected = true;
                    boatClick = true;
                }
            }
            if (!boatClick)
            {
                mouse.selectedboat.p.AddNode(mouse.clickPos);
            }
        }

        //Boat Movement
        for (int i = 0; i < boats.Count; i++)
        {
            boats[i].Update();

            //Placeholder
            if (Raylib.CheckCollisionCircleRec(boats[i].center, boats[i].r, target))
            {
                gameState = "end";
            }
        }



        //---------DRAWING---------
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.WHITE);

        //Boats
        for (int i = 0; i < boats.Count; i++)
        {
            boats[i].Draw();
        }

        //Placeholders
        Raylib.DrawRectangleRec(target, Color.DARKBLUE);

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


