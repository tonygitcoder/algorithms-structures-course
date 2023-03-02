namespace Kse.Algorithms.Samples
{
    using System;
    using System.Collections.Generic;

    public class MapPrinter
    {
        public void PrintMaze(string[,] maze, Point start, Point goal, HashSet<Point> path = null)
        {
            PrintTopLine(maze);
            for (var row = 0; row < maze.GetLength(1); row++)
            {
                Console.Write($"{row}\t");
                for (var column = 0; column < maze.GetLength(0); column++)
                {
                    var pathExists = path != null;
                    var pointInPath = pathExists && path.Contains(new Point(column, row));
                    var pointIsGoal = new Point(column, row) == goal;
                    var pointIsStart = new Point(column, row) == start;
                    
                    if (pointInPath || pointIsGoal || pointIsStart)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Green;
                        if (start == new Point(column, row))
                        {
                            Console.Write("A");
                        }
                        else if (goal == new Point(column, row))
                        {
                            Console.Write("B");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Write(maze[column, row]);
                        }

                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(maze[column, row]);
                    }
                }
                
                Console.WriteLine();
            }

            void PrintTopLine(string[,] maze)
            {
                Console.Write($" \t");
                for (int i = 0; i < maze.GetLength(0); i++)
                {
                    Console.Write(i % 10 == 0 ? i / 10 : " ");
                }

                Console.Write($"\n \t");
                for (int i = 0; i < maze.GetLength(0); i++)
                {
                    Console.Write(i % 10);
                }

                Console.WriteLine("\n");
            }
        }
    }
}
