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
int boatTimer = 10;
int frameCount = 0;

//Instanciate Mouse Class
Mouse mouse = new();

//Dock
Dock d = new(windowWidth / 2);

//Boats
List<Boat> activeBoats = new() { new Boat(true) };
Queue<Boat> boatQueue = new Queue<Boat>();

//Initialize Boat Queue
for (int i = 0; i < 5; i++)
{
    boatQueue.Enqueue(new Boat(true));
}


//Removal thing
Queue<Boat> boatRemovalList = new();


while (!Raylib.WindowShouldClose())
{
    //MAIN GAME
    if (gameState == "game")
    {
        //---------LOGIC---------
        //Incriment framecount
        frameCount++;

        //If it has reached boatTimer seconds, reset clock and add new random boat
        if (frameCount / 60 == boatTimer)
        {
            frameCount = 0;
            activeBoats.Add(boatQueue.Dequeue());
        }

        //If there are no more boats, the game is finished
        if (activeBoats.Count == 0)
        {
            gameState = "complete";
        }

        //Update all mouse inputs
        mouse.Update();


        //If mouse clicked somewhere this frame
        if (mouse.clickPos != new Vector2())
        {
            //Temporary variable boatClick
            bool boatClick = false;

            //For each boat
            foreach (var b in activeBoats)
            {
                //If it clicked on boat, deselect previous boat and select that boat
                if (Raylib.CheckCollisionPointCircle(mouse.clickPos, b.center, b.radius))
                {
                    mouse.selectedBoat.selected = false;
                    b.selected = true;
                    mouse.selectedBoat = b;
                    boatClick = true;
                    mouse.selectedBoat.p.Reset();
                }
            }
            //If it didn't click on a boat, add nodes to the selected boats path
            if (!boatClick)
            {
                mouse.AddNode(d);
            }
        }

        //Boat Movement
        foreach (var b in activeBoats)
        {
            b.Update();

            //Check each boat against each boat
            foreach (var b2 in activeBoats)
            {
                //if they aren't the same, check if they crashed into eachother
                if (!b.Equals(b2))
                {
                    if (Raylib.CheckCollisionCircles(b.center, b.radius, b2.center, b2.radius))
                    {
                        b.destroyed = true;
                        b2.destroyed = true;
                        gameState = "end";
                    }
                }
            }

            //If boat is offscreen
            if (b.center.X - b.radius > windowWidth || b.center.X + b.radius < 0 || b.center.Y - b.radius > windowHeight)
            {
                //If the boat isn't invincible from just spawning
                if (!b.invincible)
                {
                    //If it still hadn't been docked, you lose
                    if (b.dockable)
                    {
                        b.outsideArea = true;
                        gameState = "end";
                    }
                    //Else remove that boat from the game 
                    else
                    {
                        boatRemovalList.Enqueue(b);
                    }
                }
            }
        }

        //Remove all boats added to the removal queue
        while (boatRemovalList.Count > 0)
        {
            activeBoats.Remove(boatRemovalList.Dequeue());
        }

        //---------DRAWING---------
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.SKYBLUE);

        //Boats
        foreach (var b in activeBoats)
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


