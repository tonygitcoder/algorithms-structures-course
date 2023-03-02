namespace a_star;

using Kse.Algorithms.Samples;

public class PathFinder
{
    private readonly bool _astar;
    private readonly bool _addTraffic;
    private readonly int _maxWidth;
    private readonly int _maxHeight;
    private static Point _goal;

    public PathFinder(int maxWidth, int maxHeight, Point goal, bool addTraffic = false, bool astar = true)
    {
        _astar = astar;
        _maxWidth = maxWidth;
        _maxHeight = maxHeight;
        _addTraffic = addTraffic;
        _goal = goal;
    }
    
    public HashSet<Point> GetShortestPath(string[,] map, Point start, Point goal)
    {
        var origins = CalculateTotalDistances(start, goal, map);

        var shortestPath = new HashSet<Point>();

        Console.WriteLine($"origins.Count: {origins.Count}");

        var step = goal;
        do
        {
            step = origins[step];
            shortestPath.Add(step);
        } while (step != start);

        return shortestPath;
    }

    private Dictionary<Point, Point> CalculateTotalDistances(Point start, Point goal, string[,] map)
    {
        var visitedPoints = new HashSet<Point>{ new(start.Column, start.Row) };
        var distances = new Dictionary<Point, int>
        {
            { new Point(start.Column, start.Row), 0 }
        };
        var origins = new Dictionary<Point, Point>();

        for (var i = 0; i <= _maxHeight; i++)
        {
            for (var j = 0; j <= _maxWidth; j++)
            {
                if (!IsTraversable(new Point(j, i), map)) continue;
                
                // Console.WriteLine($"Progress: {(i+1)*(j+1) * 100 / map.Length}% ({(i+1)*(j+1)}/{map.Length})");

                var currentPoint = GetClosestPoint(distances, visitedPoints);
                visitedPoints.Add(currentPoint);

                var neighbours = GetNeighbours(currentPoint.Column, currentPoint.Row, map);
                foreach (var neighbour in neighbours)
                {
                    if (visitedPoints.Contains(neighbour)) continue;

                    var distance = distances[currentPoint];

                    if (_addTraffic)
                    {
                        distance += GetTrafficPenalty(neighbour, map);
                    }
                    else
                    {
                        distance++;
                    }
                    
                    var totalCost = distance;
                    if (_astar)
                    {
                        var heuristics = CalculateLinearDistance(neighbour, goal);
                        totalCost += heuristics;
                    }

                    if (distances.ContainsKey(neighbour))
                    {
                        if (totalCost >= distances[neighbour]) continue;

                        distances[neighbour] = totalCost;
                        origins[neighbour] = currentPoint;
                    }
                    else
                    {
                        distances[neighbour] = totalCost;
                        origins[neighbour] = currentPoint;
                    }
                }

                if (currentPoint == goal)
                {
                    Console.WriteLine("Path found!");
                    break;
                }
            }
        }

        return origins;
    }

    private int GetTrafficPenalty(Point point, string[,] map)
    {
        return IsTraversable(point, map) ? int.Parse(CheckPosition(point, map)) : 0;
    }

    private static int CalculateLinearDistance(Point a, Point b)
    {
        return Math.Abs(b.Column - a.Column) + Math.Abs(b.Row - a.Row);
    }

    private static Point GetClosestPoint(Dictionary<Point, int> distances, HashSet<Point> visitedPoints)
    {
        var closestPoint = new Point(0, 0);
        var closestDistance = int.MaxValue;
        
        foreach (var point in distances.Keys)
        {
            if (visitedPoints.Contains(point)) continue;
            
            var cond1 = distances[point] < closestDistance;
            var cond2 = distances[point] == closestDistance 
                        && CalculateLinearDistance(point, _goal) 
                        < CalculateLinearDistance(closestPoint, _goal);
            
            if (cond1 || cond2)
            {
                closestPoint = point;
                closestDistance = distances[point];
            }
        }

        return closestPoint;
    }

    private bool IsTraversable(Point point, string[,] map)
    {
        if (_addTraffic)
            return int.TryParse(CheckPosition(point, map), out _);

        return CheckPosition(point, map) == " ";
    }

    private List<Point> GetNeighbours(int column, int row, string[,] map)
    {
        var neighbours = new List<Point>();

        var topNeighbour = new Point(column, row - 1);
        if (IsTraversable(topNeighbour, map))
        {
            neighbours.Add(topNeighbour);
        }
        
        var bottomNeighbour = new Point(column, row + 1);
        
        if (IsTraversable(bottomNeighbour, map))
        {
            neighbours.Add(bottomNeighbour);
        }

        var leftNeighbour = new Point(column - 1, row);
        if (IsTraversable(leftNeighbour, map))
        {
            neighbours.Add(leftNeighbour);
        }

        var rightNeighbour = new Point(column + 1, row);
        if (IsTraversable(rightNeighbour, map))
        {
            neighbours.Add(rightNeighbour);
        }
        
        return neighbours;
    }

    private static string CheckPosition(Point point, string[,] map)
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