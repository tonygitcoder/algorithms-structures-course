namespace a_star;

using Kse.Algorithms.Samples;

public class PathFinder
    {
    public HashSet<Point> GetShortestPath(string[,] map, Point start, Point goal)
    {
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

            var neighbours = GetNeighbours(currentPoint.Column, currentPoint.Row, map);
            foreach (var neighbour in neighbours)
            {
                if (visitedPoints.Contains(neighbour)) continue;

                var distance = distances[currentPoint] + 1;
                var heuristics = distance + CalculateLinearDistance(neighbour, goal);
                if (distances.ContainsKey(neighbour))
                {
                    if (heuristics >= distances[neighbour]) continue;

                    distances[neighbour] = heuristics;
                    origins[neighbour] = currentPoint;
                }
                else
                {
                    distances[neighbour] = heuristics;
                    origins[neighbour] = currentPoint;
                }
            }

            if (currentPoint == goal)
            {
                Console.WriteLine("Path found!");
                break;
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