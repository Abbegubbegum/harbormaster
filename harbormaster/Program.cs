using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

//RAYLIB SHIT
const int windowWidth = 1020;
const int windowHeight = 800;
Raylib.InitWindow(windowWidth, windowHeight, "Harbor Master");
Raylib.SetTargetFPS(60);

//Game Variables
string gameState = "game";

//Instanciate Mouse Class
Mouse mouse = new Mouse();


//Dock
Dock d = new Dock(windowWidth / 2);

//Boat
List<Boat> boats = new List<Boat>() { new Boat(100, 400, 1, 1000), new Boat(400, 200, 5, 1) };



while (!Raylib.WindowShouldClose())
{
    //MAIN GAME
    if (gameState == "game")
    {
        //---------LOGIC---------

        //Update all mouse inputs
        mouse.Update();


        //If mouse clicked somewhere this frame
        if (mouse.clickPos != new Vector2())
        {
            bool boatClick = false;
            for (int i = 0; i < boats.Count; i++)
            {
                //If it clicked on boat, select that boat
                if (Raylib.CheckCollisionPointCircle(mouse.clickPos, boats[i].center, boats[i].r))
                {
                    mouse.selectedboat.selected = false;
                    mouse.selectedboat = boats[i];
                    boats[i].selected = true;
                    boatClick = true;
                }
            }
            //If it didn't click on a boat
            if (!boatClick)
            {
                //if it clicked on a dock, add a node to dock position
                if (Raylib.CheckCollisionPointRec(mouse.clickPos, d.hitBox))
                {
                    mouse.selectedboat.p.AddNode(new Vector2(d.center.X, d.center.Y + (12 + 12 + 5)));
                    mouse.selectedboat.p.AddNode(d.center);
                }
                //else add regular node
                else
                {
                    mouse.selectedboat.p.AddNode(mouse.clickPos);
                }
            }
        }

        //Boat Movement
        for (int i = 0; i < boats.Count; i++)
        {
            boats[i].Update();

            //Check each boat against each boat
            for (int j = 0; j < boats.Count; j++)
            {
                //if they aren't the same, check if they crashed into eachother
                if (!boats[i].Equals(boats[j]))
                {
                    if (Raylib.CheckCollisionCircles(boats[i].center, boats[i].r, boats[j].center, boats[j].r))
                    {
                        boats[i].destroyed = true;
                        boats[j].destroyed = true;
                        gameState = "end";
                    }
                }
            }
        }



        //---------DRAWING---------
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.SKYBLUE);

        //Boats
        for (int i = 0; i < boats.Count; i++)
        {
            boats[i].Draw();
        }

        //Dock
        d.Draw();

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


