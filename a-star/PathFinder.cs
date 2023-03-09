namespace a_star;

using Kse.Algorithms.Samples;

public class PathFinder
{
    private readonly bool _astar;
    private readonly bool _addTraffic;
    private readonly int _carSpeed;

    public PathFinder(int carSpeed = 0, bool addTraffic = false, bool astar = true)
    {
        _astar = astar;
        _addTraffic = addTraffic;
        _carSpeed = carSpeed;
    }
    
    public HashSet<Point> GetShortestPath(string[,] map, Point start, Point goal, Dictionary<Point, Point> origins)
    {
        int GetVelocity(int n) => _carSpeed - (n - 1) * 6;
        var totalTime = 0f;

        var shortestPath = new HashSet<Point>();

        var distance = 0;
        var step = goal;
        do
        {
            distance++;
            step = origins[step];
            shortestPath.Add(step);

            if (!_addTraffic) continue;
            var velocity = GetVelocity(int.Parse(map[step.Column, step.Row]));
            totalTime += (float)_carSpeed / velocity;

            // Console.WriteLine($"Velocity: {vel} km/h | Time: {(float)carSpeed / vel} hours");
        } while (step != start);

        if (!_addTraffic) return shortestPath;
        Console.WriteLine($"Map distance: {distance} km");
        Console.WriteLine($"Total time: {totalTime} hours with {_carSpeed} km/h speed");

        return shortestPath;
    }
    
    public struct Costs
    {
        public int HCost { get; set; }
        public int GCost { get; set; }
            
        public Costs(int hCost = int.MaxValue, int gCost = int.MaxValue)
        {
            HCost = hCost;
            GCost = gCost;
        }
    }

    public Dictionary<Point, Point> CalculateTotalDistances(Point start, Point goal, string[,] map)
    {
        var origins = new Dictionary<Point, Point>();
        var distances = new PriorityQueue<Point, int>();
        var costs = new Dictionary<Point, Costs> { { start, new Costs(0, 0) } };
        
        distances.Enqueue(new Point(start.Column, start.Row), 0);

        while (distances.Count > 0)
        {
            distances.TryDequeue(out var currentPoint, out var currentDistance);

            if (currentPoint == goal) break;

            var neighbours = GetNeighbours(currentPoint.Column, currentPoint.Row, map);
            foreach (var neighbour in neighbours)
            {
                var currentGCost = currentDistance - costs[currentPoint].HCost;
                
                var neighbourHCost = _astar ? CalculateLinearDistance(neighbour, goal) : 0;
                var neighbourGCost = currentGCost;
                neighbourGCost += _addTraffic ? GetTrafficPenalty(neighbour, map) : 1;
                
                Console.WriteLine($"currentHCost: {costs[currentPoint].HCost} | neighbourHCost: {neighbourHCost} | neighbourGCost: {neighbourGCost} | currentGCost: {costs[currentPoint].GCost}");
                // Console.WriteLine($"gCost: {neighbourGCost} | currentPoint.GCost: {currentPoint.GCost}");

                var neighbourIsVisited = origins.ContainsKey(neighbour) || origins.ContainsValue(neighbour);
                
                if (!costs.ContainsKey(neighbour))
                    costs.Add(neighbour, new Costs());
                
                var neighbourIsFurther = costs[currentPoint].GCost >= costs[neighbour].GCost;
                
                if (neighbourIsVisited & neighbourIsFurther) continue;
                var neighbourCost = new Costs
                {
                    HCost = neighbourHCost,
                    GCost = neighbourGCost
                };
                
                costs[neighbour] = neighbourCost;

                distances.Enqueue(neighbour, neighbourGCost + neighbourHCost);
                origins[neighbour] = currentPoint;
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