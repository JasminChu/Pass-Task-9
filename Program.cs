using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.Media;
using System.IO;


namespace Snake
{
    struct Position
    {
        public int row;
        public int col;
        public int color;
        public Position(int row, int col)
        {
            this.row = row;
            this.col = col;
            this.color = 0;
        }

        public Position(int row, int col, int color)
        {
            this.row = row;
            this.col = col;
            this.color = color;
        }
    }

    class Scoreboard
    {
        public static int origRow;
        public static int origCol;

        public static void WriteAt(string s, int x, int y)
        {
            Console.SetCursorPosition(origCol + x, origRow + y);
            Console.Write(s);
        }

        public static void WriteScore(int s, int x, int y)
        {
            Console.SetCursorPosition(origCol + x, origRow + y);
            Console.Write(s);
        }
    }


    class Program
    {
        private static int index;

        //draw the food
        static Position CreateFood(Position food, Random randomNumbersGenerator,
            Queue<Position> snakeElements, List<Position> obstacles)
        {
            do
            {
                food = new Position(randomNumbersGenerator.Next(6, Console.WindowHeight),
                    randomNumbersGenerator.Next(0, Console.WindowWidth));
            }
            //new food will be created if snake eat food OR obstacle has the same position with food
            while (snakeElements.Contains(food) || obstacles.Contains(food));
            Console.SetCursorPosition(food.col, food.row);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("♥♥");
            return food;
        }
        //JASMINNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN

        
        static Position CreateSupriseFood(Position food, Random randomNumbersGenerator,
           Queue<Position> snakeElements, List<Position> obstacles)
        {
            do
            {
                food = new Position(randomNumbersGenerator.Next(6, Console.WindowHeight),
                    randomNumbersGenerator.Next(0, Console.WindowWidth));
            }
            //new food will be created if snake eat food OR obstacle has the same position with food
            while (snakeElements.Contains(food) || obstacles.Contains(food));
            Console.SetCursorPosition(food.col, food.row);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("?");
            return food;
        }



        //draw obstacles
        static Position CreateObstacle(Position food, Position obstacle, Random randomNumbersGenerator,
            Queue<Position> snakeElements, List<Position> obstacles, ConsoleColor[] color)
        {
            do
            {
                obstacle = new Position(randomNumbersGenerator.Next(6, Console.WindowHeight),
                    randomNumbersGenerator.Next(0, Console.WindowWidth), randomNumbersGenerator.Next(color.Length));
            }
            while (snakeElements.Contains(obstacle) || //if snake eat the obstacle
                        obstacles.Contains(obstacle) ||         //if obstacles appear at the same position
                        (food.row != obstacle.row && food.col != obstacle.row));
            //the position of food and obstacle is different
            obstacles.Add(obstacle); //then obstacle will be generated
            Console.SetCursorPosition(obstacle.col, obstacle.row);
            Console.ForegroundColor = color[obstacle.color];
            Console.Write("\u2593");
            return obstacle;
        }
        //JASMINNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN

        //Print words at center
        static int PrintAtCenter(string printout, int height)
        {
            int width = decimal.ToInt32((Console.WindowWidth - printout.Length) / 2);
            height += 1;
            Console.SetCursorPosition(width, height);
            Console.WriteLine(printout);
            return height;
        }

        static bool collisionObstacle(int level, List<Position> obstacles, Position snakeNewHead)
        {
            if (level == 2)
            {
                Position snakeHead = snakeNewHead;
                snakeHead.col += 1;
                if (obstacles.Contains(snakeHead))
                {
                    return true;
                }

                snakeHead = snakeNewHead;
                snakeHead.col -= 1;
                if (obstacles.Contains(snakeHead))
                {
                    return true;
                }

                snakeHead = snakeNewHead;
                snakeHead.row += 1;
                if (obstacles.Contains(snakeHead))
                {
                    return true;
                }

                snakeHead = snakeNewHead;
                snakeNewHead.row -= 1;
                if (obstacles.Contains(snakeHead))
                {
                    return true;
                }
            }
            else if (level == 3)
            {
                Position snakeHead = snakeNewHead;
                snakeHead.col += 2;
                if (obstacles.Contains(snakeHead))
                {
                    return true;
                }

                snakeHead = snakeNewHead;
                snakeHead.col -= 2;
                if (obstacles.Contains(snakeHead))
                {
                    return true;
                }

                snakeHead = snakeNewHead;
                snakeHead.row += 2;
                if (obstacles.Contains(snakeHead))
                {
                    return true;
                }

                snakeHead = snakeNewHead;
                snakeNewHead.row -= 2;
                if (obstacles.Contains(snakeHead))
                {
                    return true;
                }
            }
            else if (level == 4)
            {
                Position snakeHead = snakeNewHead;
                snakeHead.col += 3;
                if (obstacles.Contains(snakeHead))
                {
                    return true;
                }

                snakeHead = snakeNewHead;
                snakeHead.col -= 3;
                if (obstacles.Contains(snakeHead))
                {
                    return true;
                }

                snakeHead = snakeNewHead;
                snakeHead.row += 3;
                if (obstacles.Contains(snakeHead))
                {
                    return true;
                }

                snakeHead = snakeNewHead;
                snakeNewHead.row -= 3;
                if (obstacles.Contains(snakeHead))
                {
                    return true;
                }
            }

            return false;
        }

        static bool collisionFood(int level, Position snakeNewHead, Position food)
        {
            if (level == 1)
            {
                if (food.col == snakeNewHead.col && food.row == snakeNewHead.row)
                {
                    return true;
                }
            }
            else if (level == 2)
            {
                Position snakeHead = snakeNewHead;
                snakeHead.col += 1;
                if (food.col == snakeHead.col && food.row == snakeHead.row)
                {
                    return true;
                }

                snakeHead = snakeNewHead;
                snakeHead.col -= 1;
                if (food.col == snakeHead.col && food.row == snakeHead.row)
                {
                    return true;
                }

                snakeHead = snakeNewHead;
                snakeHead.row += 1;
                if (food.col == snakeHead.col && food.row == snakeHead.row)
                {
                    return true;
                }

                snakeHead = snakeNewHead;
                snakeNewHead.row -= 1;
                if (food.col == snakeHead.col && food.row == snakeHead.row)
                {
                    return true;
                }
            }
            else if (level == 3)
            {
                Position snakeHead = snakeNewHead;
                snakeHead.col += 2;
                if (food.col == snakeHead.col && food.row == snakeHead.row)
                {
                    return true;
                }

                snakeHead = snakeNewHead;
                snakeHead.col -= 2;
                if (food.col == snakeHead.col && food.row == snakeHead.row)
                {
                    return true;
                }

                snakeHead = snakeNewHead;
                snakeHead.row += 2;
                if (food.col == snakeHead.col && food.row == snakeHead.row)
                {
                    return true;
                }

                snakeHead = snakeNewHead;
                snakeNewHead.row -= 2;
                if (food.col == snakeHead.col && food.row == snakeHead.row)
                {
                    return true;
                }
            }
            else if (level == 4)
            {
                Position snakeHead = snakeNewHead;
                snakeHead.col += 3;
                if (food.col == snakeHead.col && food.row == snakeHead.row)
                {
                    return true;
                }

                snakeHead = snakeNewHead;
                snakeHead.col -= 3;
                if (food.col == snakeHead.col && food.row == snakeHead.row)
                {
                    return true;
                }

                snakeHead = snakeNewHead;
                snakeHead.row += 3;
                if (food.col == snakeHead.col && food.row == snakeHead.row)
                {
                    return true;
                }

                snakeHead = snakeNewHead;
                snakeNewHead.row -= 3;
                if (food.col == snakeHead.col && food.row == snakeHead.row)
                {
                    return true;
                }
            }

            return false;
        }

        static void Main(string[] args)
        {
            //JASMINNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();


            Console.OutputEncoding = System.Text.Encoding.UTF8;
            //JASMINNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("WELCOME TO SNAKE GAME!");
            Console.WriteLine("Press Enter Key to Continue!");
            Console.Read();

            List<string> menuItems = new List<string>()
            {
                "Start Game",
                "Help",
                "Exit"
            };

            Console.CursorVisible = false;
            while (true)
            {
                //JASMINNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;

                string selectedMenuItem = drawMenu(menuItems);
                if (selectedMenuItem == "Start Game")
                {
                    Console.Clear();
                    Console.WriteLine("HELLO!"); Console.Read();
                    //JASMINNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN

                    //backgorund sound is played when the player start the game
                    SoundPlayer player = new SoundPlayer();
                    player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "/mainmenu.wav";
                    player.Play();

                    byte right = 0;
                    byte left = 1;
                    byte down = 2;
                    byte up = 3;
                    int lastFoodTime = 0;
                    int foodDissapearTime = 12000;
                    //----------------------------------------Life-----------------------------------
                    int life = 3;
                    //--------------------------------------level------------------------------------
                    int level = 1;

                    ConsoleColor[] color = { ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Gray, ConsoleColor.Blue };

                    int supriseFoodDissapearTime = 3390;
                    int negativePoints = 0;
                    int winningscore = 10;
                    int _scorecount = 0;
                    
                    Console.WriteLine("Name: ");
                    string winner = Console.ReadLine();
                    Console.Clear();    

                    if (File.Exists("winner.txt") == true)
                    {
                        string previouswinner = File.ReadAllText("winner.txt");
                        string[] splitBySpace = previouswinner.Split(' ');
                        string first = splitBySpace.ElementAt(0);
                        int last = Convert.ToInt32(splitBySpace.ElementAt(splitBySpace.Length - 1));
                        Scoreboard.WriteAt("Previous Winner: " + previouswinner, 0, 0);

                        if (first == winner)
                        {
                            _scorecount = last;
                        }

                        if (last >= winningscore)
                        {
                            winningscore = 10;
                        }
                        else if (last >= 10)
                        {
                            winningscore = 30;
                        }
                        else if (last >= 30)
                        {
                            winningscore = 100;
                        }
                    }
                    else if (File.Exists("winner.txt") == false)
                    {
                        File.Create("winner.txt");
                    }
                    File.WriteAllText("winner.txt", winner + " with score " + _scorecount);
                    Scoreboard.WriteAt("Your Current Score", 0, 1);
                    Scoreboard.WriteScore(_scorecount, 0, 2);
                    Scoreboard.WriteAt("Your Remains Life", 0, 3);
                    Scoreboard.WriteAt(life.ToString(), 0, 4);
                    Scoreboard.WriteAt("Your Level", 0, 5);
                    Scoreboard.WriteAt(level.ToString(), 0, 6);


                    //Array which is a linear data structure is used 
                    //position store direction (array)
                    Position[] directions = new Position[]
                    {
                                new Position(0, 1), // right
                                new Position(0, -1), // left
                                new Position(1, 0), // down
                                new Position(-1, 0), // up
                    };
                    double sleepTime = 100;
                    int direction = right;
                    Random randomNumbersGenerator = new Random();
                    Console.BufferHeight = Console.WindowHeight;

                    //Linked List which is a linear data structure is used 
                    //Creating a linkedlist 
                    //Using List class 
                    //list to store position of obstacles
                    //The obstacles are randomizd so it will appear randomly everytime user play it
                    List<Position> obstacles = new List<Position>()
                                {
                                    new Position(randomNumbersGenerator.Next(6, Console.WindowHeight),
                                        randomNumbersGenerator.Next(0, Console.WindowWidth),randomNumbersGenerator.Next(color.Length)),
                                    new Position(randomNumbersGenerator.Next(6, Console.WindowHeight),
                                        randomNumbersGenerator.Next(0, Console.WindowWidth),randomNumbersGenerator.Next(color.Length)),
                                    new Position(randomNumbersGenerator.Next(6, Console.WindowHeight),
                                        randomNumbersGenerator.Next(0, Console.WindowWidth),randomNumbersGenerator.Next(color.Length)),
                                    new Position(randomNumbersGenerator.Next(6, Console.WindowHeight),
                                        randomNumbersGenerator.Next(0, Console.WindowWidth),randomNumbersGenerator.Next(color.Length))
                                };
                    //For each loop
                    //Each obstacle in List(obstacles) to set the color, position
                    //Print out the obstacle
                    for (int j = 0; j < obstacles.Count(); j++)
                    {
                        //drawing obstacles
                        Console.ForegroundColor = color[obstacles[j].color];
                        Console.SetCursorPosition(obstacles[j].col, obstacles[j].row);
                        Console.Write("\u2593");
                    }

                    //creating snake body (5 "*")
                    //Queue which is a linear data structure is used
                    //Queue is like a container
                    //Enqueue is implementation of Queue to insert new element at the rear of the queue
                    //Set 5 items in snakeElements by setting the position (0,i)
                    //i increase every time until 5
                    //snakeElements used to store the snake body elements (*)
                    //Reduce the body length of snake to 3 units of * 
                    Queue<Position> snakeElements = new Queue<Position>();
                    for (int i = 0; i <= 3; i++)
                    {
                        snakeElements.Enqueue(new Position(7, i));
                    }

                    //The position is create randomly
                    //creating food in the game
                    Position food = new Position();
                    food = CreateFood(food, randomNumbersGenerator, snakeElements, obstacles);
                    lastFoodTime = Environment.TickCount;

                 
                    //The position is create randomly
                    //creating suprising food in the game
                    Position supriseFood = new Position();
                    supriseFood = CreateSupriseFood(supriseFood, randomNumbersGenerator, snakeElements, obstacles);
                    lastFoodTime = Environment.TickCount;

                    //drawing snake body ("*")
                    //set color and position of each of the part of body in snakeElements
                    foreach (Position position in snakeElements)
                    {
                        Console.SetCursorPosition(position.col, position.row);
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("*");
                    }

                    if ((level == 1 && _scorecount >= 3 && _scorecount < 5) || (level == 2 && _scorecount >= 6 && _scorecount < 8) || (level == 3 && _scorecount >= 9 && _scorecount < 10))
                    {
                        level += 1;
                        Scoreboard.WriteAt("Your Level", 0, 5);
                        Scoreboard.WriteAt(level.ToString(), 0, 6);
                        directions[0].col += 1;
                        directions[1].col -= 1;
                        directions[2].row += 1;
                        directions[3].row -= 1;
                    }

                    //The following code will run until the program stop
                    while (true)
                    {
                        negativePoints++;

                        //the movement of the snake
                        //When the user click the arrow key, if the snake direction is not same,
                        //it will change the snake direction
                        if (Console.KeyAvailable)
                        {
                            ConsoleKeyInfo userInput = Console.ReadKey();
                            if (userInput.Key == ConsoleKey.LeftArrow)
                            {
                                if (direction != right) direction = left;
                            }
                            if (userInput.Key == ConsoleKey.RightArrow)
                            {
                                if (direction != left) direction = right;
                            }
                            if (userInput.Key == ConsoleKey.UpArrow)
                            {
                                if (direction != down) direction = up;
                            }
                            if (userInput.Key == ConsoleKey.DownArrow)
                            {
                                if (direction != up) direction = down;
                            }

                            //Only arrow key can display
                            if (userInput.Key != ConsoleKey.LeftArrow || userInput.Key != ConsoleKey.RightArrow || userInput.Key != ConsoleKey.UpArrow || userInput.Key != ConsoleKey.DownArrow)
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.White;

                                if (File.Exists("winner.txt") == true)
                                {
                                    string previouswinner = File.ReadAllText("winner.txt");
                                    Scoreboard.WriteAt("Previous Winner: " + previouswinner, 0, 0);
                                }

                                Scoreboard.WriteAt("Your Current Score", 0, 1);
                                Scoreboard.WriteScore(_scorecount, 0, 2);
                                Scoreboard.WriteAt("Your Remains Life", 0, 3);
                                Scoreboard.WriteAt(life.ToString(), 0, 4);
                                Scoreboard.WriteAt("Your Level", 0, 5);
                                Scoreboard.WriteAt(level.ToString(), 0, 6);

                                foreach (Position position in snakeElements)
                                {
                                    Console.SetCursorPosition(position.col, position.row);
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.Write("*");
                                }

                                for (int j = 0; j < obstacles.Count(); j++)
                                {
                                    //drawing obstacles
                                    Console.ForegroundColor = color[obstacles[j].color];
                                    Console.SetCursorPosition(obstacles[j].col, obstacles[j].row);
                                    Console.Write("\u2593");
                                }

                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.SetCursorPosition(food.col, food.row);
                                Console.Write("♥♥");

                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.SetCursorPosition(supriseFood.col, supriseFood.row);
                                Console.Write("?");
                            }
                        }

                        //manage the position of the snake head if the snake exceed the width or height of the console window
                        //if the snake disappear at the bottom, it will reappear from the top
                        Position snakeHead = snakeElements.Last();
                        Position nextDirection = directions[direction];

                        Position snakeNewHead = new Position(snakeHead.row + nextDirection.row,
                            snakeHead.col + nextDirection.col);

                        //Game over when snake hits the console window
                        //the game will over if the snake eat its body OR eat the obstacles
                        //Stack which is a linear data structure is used
                        if (snakeNewHead.row < 6 && snakeNewHead.col < 40)
                        {
                            snakeNewHead.col = 0;
                            snakeNewHead.row = 7;
                            direction = right;
                        }

                        if (snakeElements.Contains(snakeNewHead) || obstacles.Contains(snakeNewHead)
                            || (snakeNewHead.row >= Console.WindowHeight) || (snakeNewHead.col >= Console.WindowWidth)
                            || (snakeNewHead.col < 0) || (snakeNewHead.row < 0) || collisionObstacle(level, obstacles, snakeNewHead) == true)
                        {
                            //Remove the obstacles which the snake has eaten
                            obstacles.Remove(snakeNewHead);

                            //Game over sound will display if the snake die
                            SoundPlayer player1 = new SoundPlayer();
                            player1.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "/die.wav";
                            player1.PlaySync();
                            direction = right;
                            snakeNewHead.row = 7;
                            snakeNewHead.col = 0;

                            //----------------------------------------life---------------------------------------
                            //If user still have life
                            if (life > 0)
                            {
                                //minus 1 life
                                life -= 1;
                                Scoreboard.WriteAt(life.ToString(), 0, 4);

                                //minus 1 score
                                if (_scorecount != 0)
                                {
                                    _scorecount -= 1;
                                    Scoreboard.WriteScore(_scorecount, 0, 2);

                                }

                                Position obstacle = new Position();
                                //generate new position for the obstacles
                                obstacle = CreateObstacle(food, obstacle, randomNumbersGenerator, snakeElements, obstacles,color);

                                if (player1.IsLoadCompleted == true)
                                {
                                    player.Play();
                                }
                            }
                            //displayed when game over
                            //------------------------------------------------GameOver----------------------------------------------------
                            else
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                string gameover = "Game over!";
                                string points = "Your points are: ";
                                string exit = "Press Enter to exit.";

                                int height = decimal.ToInt32((Console.WindowHeight) / 2) - 3;
                                int width = decimal.ToInt32((Console.WindowWidth - gameover.Length) / 2);
                                //print Game over and points
                                height = PrintAtCenter(gameover, height);
                                height = PrintAtCenter(points + _scorecount, height);

                                //------------------------------------------------Exit Game----------------------------------------------------

                                //Print Exit Game
                                height = PrintAtCenter(exit, height);

                                //Make a loop until user press enter key to exit the game
                                while (Console.ReadKey().Key != ConsoleKey.Enter)
                                {
                                    height = PrintAtCenter(exit, height);
                                }
                                Environment.Exit(0);
                            }
                        }

                        //Set the position of the snake
                        Console.SetCursorPosition(snakeHead.col, snakeHead.row);
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write("*");

                        //draw the snake head according to different direction
                        snakeElements.Enqueue(snakeNewHead);
                        Console.SetCursorPosition(snakeNewHead.col, snakeNewHead.row);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        if (direction == right) Console.Write(">");
                        if (direction == left) Console.Write("<");
                        if (direction == up) Console.Write("^");
                        if (direction == down) Console.Write("v");

                        //when the snake eat the food
                        //JASMINNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN
                        if ((collisionFood(level, snakeNewHead, food) == true) || (snakeNewHead.col == food.col && snakeNewHead.row == food.row) || (snakeNewHead.col == food.col + 1 && snakeNewHead.row == food.row))
                        {
                             if (snakeNewHead.col == food.col)
                                {
                                    Console.SetCursorPosition(food.col + 1, food.row);
                                    Console.Write("    ");
                                }

                                if (snakeNewHead.col == food.col+1)
                                {
                                    Console.SetCursorPosition(food.col, food.row);
                                    Console.Write("    ");
                                }
                            
                            Console.SetCursorPosition(food.col, food.row); //the cursor position will set to the food position.
                            Console.Write(" ");

                            _scorecount += 1;
                            Scoreboard.WriteAt("Your Current Score", 0, 1);
                            Scoreboard.WriteScore(_scorecount, 0, 2);

                            if (_scorecount == winningscore)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                string gamewon = "You have won the game!";
                                int height = decimal.ToInt32((Console.WindowHeight) / 2);
                                int width = decimal.ToInt32((Console.WindowWidth - gamewon.Length) / 2);

                                Console.SetCursorPosition(width, height);
                                Console.WriteLine("You have won the game!");
                                Console.SetCursorPosition(width, height + 1);
                                Console.WriteLine("Your points are: " + _scorecount);


                                Console.WriteLine("Winner saved into text file!");
                                File.WriteAllText("winner.txt", winner + " with score " + _scorecount);
                                string previouswinner = File.ReadAllText("winner.txt");
                                Console.WriteLine(previouswinner);

                                Console.WriteLine("Press Enter to exit.");

                                while (Console.ReadKey().Key != ConsoleKey.Enter)
                                {
                                    Console.WriteLine("Press Enter to exit.");
                                }
                                Environment.Exit(0);
                            }

                            //----------------------------------------level---------------------------------------
                            if ((level == 1 && _scorecount >= 3 && _scorecount < 5) || (level == 2 && _scorecount >= 6 && _scorecount < 8) || (level == 3 && _scorecount >= 9 && _scorecount < 10))
                            {
                                level += 1;
                                Scoreboard.WriteAt("Your Level", 0, 5);
                                Scoreboard.WriteAt(level.ToString(), 0, 6);
                                directions[0].col += 1;
                                directions[1].col -= 1;
                                directions[2].row += 1;
                                directions[3].row -= 1;
                            }

                            //feeding the snake
                            //generate new position for the food
                            food = CreateFood(food, randomNumbersGenerator, snakeElements, obstacles);
                            lastFoodTime = Environment.TickCount;
                            sleepTime--;

                            Position obstacle = new Position();
                            //generate new position for the obstacles
                            obstacle = CreateObstacle(food, obstacle, randomNumbersGenerator, snakeElements, obstacles,color);
                        }

                        
                        //when the snake eat the suprise food
                        else if (collisionFood(level, snakeNewHead, supriseFood) == true)
                        {
                            Console.SetCursorPosition(supriseFood.col, supriseFood.row); //the cursor position will set to the food position.
                            Console.Write(" ");

                            _scorecount += 2;
                            Scoreboard.WriteAt("Your Current Score", 0, 1);
                            Scoreboard.WriteScore(_scorecount, 0, 2);

                            if (_scorecount == winningscore)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                string gamewon = "You have won the game!";
                                int height = decimal.ToInt32((Console.WindowHeight) / 2);
                                int width = decimal.ToInt32((Console.WindowWidth - gamewon.Length) / 2);

                                Console.SetCursorPosition(width, height);
                                Console.WriteLine("You have won the game!");
                                Console.SetCursorPosition(width, height + 1);
                                Console.WriteLine("Your points are: " + _scorecount);


                                Console.WriteLine("Winner saved into text file!");
                                File.WriteAllText("winner.txt", winner + " with score " + _scorecount);
                                string previouswinner = File.ReadAllText("winner.txt");
                                Console.WriteLine(previouswinner);
                                Console.WriteLine("Press Enter to exit.");

                                while (Console.ReadKey().Key != ConsoleKey.Enter)
                                {
                                    Console.WriteLine("Press Enter to exit.");
                                }
                                Environment.Exit(0);
                            }
                            //----------------------------------------level---------------------------------------
                            if ((level == 1 && _scorecount >= 3 && _scorecount < 5) || (level == 2 && _scorecount >= 6 && _scorecount < 8) || (level == 3 && _scorecount >= 9 && _scorecount < 10))
                            {
                                level += 1;
                                Scoreboard.WriteAt("Your Level", 0, 5);
                                Scoreboard.WriteAt(level.ToString(), 0, 6);
                                directions[0].col += 1;
                                directions[1].col -= 1;
                                directions[2].row += 1;
                                directions[3].row -= 1;
                            }


                            Position obstacle = new Position();
                            //generate new position for the obstacles
                            obstacle = CreateObstacle(supriseFood, obstacle, randomNumbersGenerator, snakeElements, obstacles,color);
                        }

                        else
                        {
                            // moving...if didn't meet the conditions above then the snake will keep moving
                            Position last = snakeElements.Dequeue();
                            //The snake position will be set to the begining of the snakeElements 
                            //“Dequeue” which is used to remove and return the begining object
                            Console.SetCursorPosition(last.col, last.row);
                            Console.Write(" ");
                        }

                        //If the food appear at the console window (whole game time minus time of last food）
                        //is greater than the foodDissapearTime which intialise is 8000

                        //----------------------------------------------FoodRelocateTime--------------------------------------------------

                        //add another 5000 time to extend the food relocate time
                        if (Environment.TickCount - lastFoodTime >= foodDissapearTime)
                        {
                            negativePoints = negativePoints + 50;
                            Console.SetCursorPosition(food.col, food.row); //the cursor position will set to the food position.
                            Console.Write(" ");

                            food = CreateFood(food, randomNumbersGenerator, snakeElements, obstacles);
                            lastFoodTime = Environment.TickCount; //The lastFoodTime will reset to the present time
                        }

                        else if (Environment.TickCount - lastFoodTime >= supriseFoodDissapearTime)
                        {
                            negativePoints = negativePoints + 50;
                            Console.SetCursorPosition(supriseFood.col, supriseFood.row); //the cursor position will set to the food position.
                            Console.Write(" ");
                            supriseFood = CreateSupriseFood(supriseFood, randomNumbersGenerator, snakeElements, obstacles);
                            lastFoodTime = Environment.TickCount; //The lastFoodTime will reset to the present time
                        }

                        sleepTime -= 0.01;

                        Thread.Sleep((int)sleepTime);
                    }
                }

                else if (selectedMenuItem == "Help")
                {
                    //JASMINNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("WELCOME TO SNAKE GAME!");
                    Console.WriteLine("Keyboard Up = Up");
                    Console.WriteLine("Keyboard Down = Down");
                    Console.WriteLine("Keyboard Right = Right");
                    Console.WriteLine("Keyboard Left = Left");
                    Console.WriteLine("Press Enter Key to Continue!");
                    Console.Read();
                }

                else if (selectedMenuItem == "Exit")
                {
                    Environment.Exit(0);
                }

            }
        }

        static string drawMenu(List<string> items)
        {
            //JASMINNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();

            for (int i = 0; i < items.Count; i++)
            {
                if (i == index)
                {

                    //JASMINNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                   
                    Console.WriteLine(items[i]);
                }
                else
                {
                    Console.WriteLine(items[i]);
                }
                Console.ResetColor();
            }

            ConsoleKeyInfo ckey = Console.ReadKey();

            if (ckey.Key == ConsoleKey.DownArrow)
            {
                if (index == items.Count - 1)
                {
                    //index = 0; //Remove the comment to return to the topmost item in the list
                }
                else { index++; }
            }
            else if (ckey.Key == ConsoleKey.UpArrow)
            {
                if (index <= 0)
                {
                    //index = menuItem.Count - 1; //Remove the comment to return to the item in the bottom of the list
                }
                else { index--; }
            }
            else if (ckey.Key == ConsoleKey.Enter)
            {
                return items[index];
            }
            else
            {
                return "";
            }

            Console.Clear();
            return "";
        }

    }
    
}
