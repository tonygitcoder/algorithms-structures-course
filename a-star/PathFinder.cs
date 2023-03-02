namespace a_star;

using Kse.Algorithms.Samples;

public class PathFinder
{
    public HashSet<Point> GetShortestPath(string[,] map, Point start, Point goal)
    {
        // map[start.Column, start.Row] = "A";
        // map[goal.Column, goal.Row] = "B";
    
        var origins = CalculateTotalDistances(start, goal, map);
    
        var shortestPath = new HashSet<Point>();

        var step = goal;
        Console.WriteLine($"Path: {step.Column}, {step.Row}");
        do
        {
            step = origins[step];
            shortestPath.Add(step);
        } while (step != start);
    

        return shortestPath;
    }
    
    Dictionary<Point, Point> CalculateTotalDistances(Point start, Point goal, string[,] map)
        {
            // With HashSet program execution time decreased by ~100 times
            // comparing with List, where Contains() method was used,
            // it took O(n) time to find an element in the list.
            // now it is O(1) time with HashSet.
            var visitedPoints = new HashSet<Point>{ new(start.Column, start.Row) };
            var distances = new Dictionary<Point, int>
            {
                { new Point(start.Column, start.Row), 0 }
            };
            
            var origins = new Dictionary<Point, Point>();
            
            for (var i = 0; i <= map.Length; i++)
            {
                Console.Clear();
                Console.WriteLine($"Progress: {i*100/map.Length}% ({i}/{map.Length})");
                
                var currentPoint = GetClosestPoint(distances, visitedPoints);
                visitedPoints.Add(currentPoint);
                
                // For debug
                // map[currentPoint.Column, currentPoint.Row] = "-";   

                var linDist = CalculateLinearDistance(currentPoint, goal);

                var neighbours = GetNeighbours(currentPoint.Column, currentPoint.Row, map);
                foreach (var neighbour in neighbours)
                {
                    if (visitedPoints.Contains(neighbour)) continue;
                    
                    if (neighbour == goal)
                    {
                        Console.WriteLine("Path found!");
                    }
                    
                    var distance = distances[currentPoint] + 1;

                    if (distances.ContainsKey(neighbour))
                    {
                        // TODO: for a*
                        // if (distances[neighbour] + linDist  < distance + CalculateLinearDistance(neighbour, goal))
                        if (distances[neighbour] < distance)
                        {
                            distances[neighbour] = distance;
                            origins[neighbour] = currentPoint;
                        }
                    }
                    else
                    {
                        distances[neighbour] = distance;
                        origins[neighbour] = currentPoint;
                    }
                }

                if (currentPoint == goal)
                {
                    Console.WriteLine("Path found!");
                }
            }

            return origins;
        }
    
    int CalculateLinearDistance(Point a, Point b)
    {
        return Math.Abs(b.Column - a.Column) + Math.Abs(b.Row - a.Row);
    }

    Point GetClosestPoint(Dictionary<Point, int> distances, HashSet<Point> visitedPoints)
    {
        var closestPoint = new Point(0, 0);
        var closestDistance = int.MaxValue;
        
        foreach (var point in distances.Keys)
        {
            if (visitedPoints.Contains(point)) continue;
            // TODO: for a*
            if (distances[point] >= closestDistance) continue;
            
            closestPoint = point;
            closestDistance = distances[point];
        }

        return closestPoint;
    }

    List<Point> GetNeighbours(int column, int row, string[,] map)
    {
        var neighbours = new List<Point>();
        
        bool IsTraversable(Point point) => CheckPosition(point, map) == " ";
        
        
        var topNeighbour = new Point(column, row - 1);
        if (IsTraversable(topNeighbour))
        {
            neighbours.Add(topNeighbour);
        }
        
        var bottomNeighbour = new Point(column, row + 1);
        if (IsTraversable(bottomNeighbour))
        {
            neighbours.Add(bottomNeighbour);
        }

        var leftNeighbour = new Point(column - 1, row);
        if (IsTraversable(leftNeighbour))
        {
            neighbours.Add(leftNeighbour);
        }

        var rightNeighbour = new Point(column + 1, row);
        if (IsTraversable(rightNeighbour))
        {
            neighbours.Add(rightNeighbour);
        }
        
        return neighbours;
    }
    
    string CheckPosition(Point point, string[,] map)
    {
        var leftBorder = point.Column < 0;
        var rightBorder = point.Column >= map.GetLength(0);
        var topBorder = point.Row < 0;
        var bottomBorder = point.Row >= map.GetLength(1);
        
        // TODO: catch exception
        if (leftBorder || rightBorder || topBorder || bottomBorder) return "";

        return map[point.Column, point.Row];
    }
}