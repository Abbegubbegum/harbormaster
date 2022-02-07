using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;


namespace harbormaster

{
    class Program
    {
        static void Main(string[] args)
        {

            //RAYLIB SHIT
            const int windowWidth = 1020;
            const int windowHeight = 800;
            Raylib.InitWindow(windowWidth, windowHeight, "Harbor Master");
            Raylib.SetTargetFPS(60);

            //Game Variables
            string gameState = "game";
            int boatTimer = 10;
            int frameCount = 500;


            //Instanciate Mouse Class
            Mouse mouse = new();

            //Dock
            Dock d = new(windowWidth / 2);

            //Boat
            List<Boat> boats = new();

            //Boat queue
            Queue<Boat> boatQueue = new();

            for (int i = 0; i < 5; i++)
            {
                boatQueue.Enqueue(new RandomBoat());
            }

            // OLD QUEUE SHIT
            //Removal thing
            // Queue<Boat> boatRemovalList = new();


            while (!Raylib.WindowShouldClose())

            {
                //MAIN GAME
                if (gameState == "game")
                {
                    //---------LOGIC---------
                    //Incriment framecount
                    frameCount++;

                    //If it has reached boatTimer seconds, reset clock and add new random boat
                    if (frameCount / 60 == boatTimer && boatQueue.Count > 0)
                    {
                        frameCount = 0;
                        boats.Add(boatQueue.Dequeue());
                    }

                    if (boatQueue.Count == 0 && boats.Count == 0)
                    {
                        gameState = "end";
                    }

                    //Update all mouse inputs
                    mouse.Update();


                    //If mouse clicked somewhere this frame
                    if (mouse.clickPos != new Vector2())
                    {
                        //Temporary variable boatClick
                        bool boatClick = false;
                        //For each boat
                        foreach (var b in boats)
                        {
                            //If it clicked on boat, deselect previous boat and select that boat
                            if (mouse.CheckBoatClick(b))
                            {
                                boatClick = true;
                            }
                        }
                        //If it didn't click on a boat, add nodes to the selected boats path
                        if (!boatClick)
                        {
                            mouse.AddNode(d);
                        }
                    }

                    //Boat Movement
                    for (int i = 0; i < boats.Count; i++)
                    {
                        boats[i].Update();

                        //Check each boat thats infront in the list, therefore avoiding duplicate checks
                        for (int j = i + 1; j < boats.Count; j++)
                        {
                            //if they crashed into eachother, end game
                            if (boats[i].CheckBoatCrash(boats[j]))
                            {
                                gameState = "end";
                            }
                        }

                        //If boat is offscreen
                        if (boats[i].center.X - boats[i].radius > windowWidth || boats[i].center.X + boats[i].radius < 0 || boats[i].center.Y - boats[i].radius > windowHeight)
                        {
                            //If the boat isn't invincible from just spawning
                            if (!boats[i].invincible)
                            {
                                //If it still hadn't been docked, you lose
                                if (boats[i].dockable)
                                {
                                    gameState = "end";
                                }
                                //Else remove that boat from the game 
                                else
                                {
                                    // OLD QUEUE SHIT
                                    // boatRemovalList.Enqueue(boats[i]);

                                    //Remove that boat from the game
                                    boats.RemoveAt(i);
                                    //Shift the pointer to adjust
                                    i--;
                                }
                            }
                        }
                    }

                    // OLD QUEUE SHIT
                    //Remove all boats added to the removal queue
                    // while (boatRemovalList.Count > 0)
                    // {
                    //     boats.Remove(boatRemovalList.Dequeue());
                    // }

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
        }
    }
}