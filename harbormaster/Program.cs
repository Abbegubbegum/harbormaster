using System;
using System.Collections.Generic;
using System.Numerics;
using harbormaster;
using Raylib_cs;

//RAYLIB SHIT
const int windowWidth = 1020;
const int windowHeight = 800;
Raylib.InitWindow(windowWidth, windowHeight, "Harbor Master");
Raylib.SetTargetFPS(60);

//Game Variables
string gameState = "game";

//Instanciate Mouse Class
Mouse mouse = new();


//Dock
Dock d = new(windowWidth / 2);

//Boat
List<Boat> boats = new() { new Boat(100, 400, 1, 1000), new Boat(400, 200, 5, 1) };

//Removal thing
List<Boat> boatRemovalList = new();

while (!Raylib.WindowShouldClose())
{
    //MAIN GAME
    if (gameState == "game")
    {
        //---------LOGIC---------
        //If there are no more boats, the game is finished
        if (boats.Count == 0)
        {
            gameState = "complete";
        }

        //Update all mouse inputs
        mouse.Update();


        //If mouse clicked somewhere this frame
        if (mouse.clickPos != new Vector2())
        {
            bool boatClick = false;
            foreach (var b in boats)
            {
                //If it clicked on boat, select that boat
                if (Raylib.CheckCollisionPointCircle(mouse.clickPos, b.center, b.r))
                {
                    mouse.selectedBoat.selected = false;
                    mouse.selectedBoat = b;
                    b.selected = true;
                    boatClick = true;
                }
            }
            //If it didn't click on a boat
            if (!boatClick)
            {
                //if it clicked on a dock, add a node to dock position and set up shit for the dock
                if (Raylib.CheckCollisionPointRec(mouse.clickPos, d.hitBox) && mouse.selectedBoat.dockable)
                {
                    mouse.selectedBoat.p.AddNode(new Vector2(d.center.X, d.center.Y + (12 + 12 + 5)));
                    mouse.selectedBoat.p.AddNode(d.center);
                    mouse.selectedBoat.OnPathToDock();
                }
                //else add regular node
                else if (!Raylib.CheckCollisionPointRec(mouse.clickPos, d.hitBox))
                {
                    mouse.selectedBoat.p.AddNode(mouse.clickPos);
                }
            }
        }

        //Boat Movement
        foreach (var b1 in boats)
        {
            b1.Update();

            //Check each boat against each boat
            foreach (var b2 in boats)
            {
                //if they aren't the same, check if they crashed into eachother
                if (!b1.Equals(b2))
                {
                    if (Raylib.CheckCollisionCircles(b1.center, b1.r, b2.center, b2.r))
                    {
                        b1.destroyed = true;
                        b2.destroyed = true;
                        gameState = "end";
                    }
                }
            }

            //If boat is offscreen
            if (b1.center.X - b1.r > windowWidth || b1.center.X + b1.r < 0 || b1.center.Y - b1.r > windowHeight)
            {
                //If it still hadn't been docked, you lose
                if (b1.dockable)
                {
                    gameState = "end";
                }
                //Else remove that boat from the list
                else
                {
                    boatRemovalList.Add(b1);
                }
            }
        }


        for (int i = 0; i < boatRemovalList.Count; i++)
        {
            boats.Remove(boatRemovalList[i]);
        }

        boatRemovalList.RemoveRange(0, boatRemovalList.Count);



        //---------DRAWING---------
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.SKYBLUE);

        //Boats
        foreach (var b in boats)
        {
            b.Draw();
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

        Raylib.DrawText(gameState, 100, 100, 84, Color.BLACK);

        Raylib.EndDrawing();
    }
}


