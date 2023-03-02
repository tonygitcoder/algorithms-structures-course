namespace a_star;

using Kse.Algorithms.Samples;

public class PathFinder
{
    public HashSet<Point> GetShortestPath(string[,] map, Point start, Point goal)
    {
        var visitedPoints = new HashSet<Point> { new(start.Column, start.Row) };
        var distances = new Dictionary<Point, int> { { new Point(start.Column, start.Row), 0 } };
        
        var origins = new Dictionary<Point, Point>();
        
        while (!visitedPoints.Contains(goal) && visitedPoints.Count > 0)
        {
            var currentPoint = GetClosestPoint(distances, visitedPoints);
            visitedPoints.Add(currentPoint);
            
            if (currentPoint == goal)
            {
                Console.WriteLine("Path found!");
                break;
            }
            
            var neighbors = GetNeighbours(currentPoint.Column, currentPoint.Row, map);

            foreach (var neighbor in neighbors)
            {
                if (visitedPoints.Contains(neighbor)) continue;
                
                var tentativeDistance = distances[currentPoint] + 1;
                
                if (!distances.ContainsKey(neighbor) || tentativeDistance < distances[neighbor])
                {
                    distances[neighbor] = tentativeDistance;
                    origins[neighbor] = currentPoint;
                }
            }
        }
        
        var shortestPath = new HashSet<Point>();
        var step = goal;
        while (origins.ContainsKey(step))
        {
            shortestPath.Add(step);
            step = origins[step];
        }
        shortestPath.Add(start);

        return shortestPath;
    }

    /*int CalculateLinearDistance(Point a, Point b)
    {
        return Math.Abs(b.Column - a.Column) + Math.Abs(b.Row - a.Row);
    }*/

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