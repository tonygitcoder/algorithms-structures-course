using Kse.Algorithms.Samples;

internal class Program
{
    public static void Main(string[] args)
    {
        var generator = new MapGenerator(new MapGeneratorOptions()
        {
            Height = 35,
            Width = 90,
        });

        string[,] map = generator.Generate();

        var shortestPath = GetShortestPath(map, new Point(0, 0), new Point(89, 34));
        new MapPrinter().Print(map, shortestPath);

        List<Point> GetShortestPath(string[,] map, Point start, Point goal)
        {
            map[start.Column, start.Row] = "A";
            map[goal.Column, goal.Row] = "B";
            
            var visitedPoints = new List<Point>();
            var distances = new Dictionary<Point, int>
            {
                { new Point(start.Column, start.Row), 0 }
                // { new Point(start.Column, start.Row), 0 }
            };
            var origins = new Dictionary<Point, Point>();
            
            CalculateTotalDistances();
            
            // Console.WriteLine("Distances:" + distances.Count);

            // TODO: return path
            return null;

            Point GetPoint(int i)
            {
                return new Point(i % map.GetLength(0), i / map.GetLength(0));
            }
            
            void CalculateTotalDistances()
            {
                while (visitedPoints.Count < map.Length)
                {
                    var currentPoint = GetClosestPoint(distances);
                    visitedPoints.Add(currentPoint);
                    
                    map[currentPoint.Column, currentPoint.Row] = "-";

                    var linDist = CalculateLinearDistance(currentPoint, goal);

                    var neighbours = GetNeighbours(currentPoint.Column, currentPoint.Row, map);
                    foreach (var neighbour in neighbours)
                    {
                        if (visitedPoints.Contains(neighbour)) continue;
                        
                        // TODO: bugfix not spreading the maze
                        var distance = distances[currentPoint] + linDist + 1;

                        if (distances.ContainsKey(neighbour))
                        {
                            if (distance > distances[neighbour])
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
                }
            }
        }

        int CalculateLinearDistance(Point a, Point b)
        {
            return Math.Abs(b.Column - a.Column) + Math.Abs(b.Row - a.Row);
        }
        
        int CalculateTotalDistance(Point start, Point point)
        {
            
            
            // TODO: return
            return 0;
        }
        
        Point GetClosestPoint(Dictionary<Point, int> distances)
        {
            var closestPoint = new Point(0, 0);
            var closestDistance = 0;
            foreach (var point in distances.Keys)
            {
                // Console.WriteLine(distances[point]);
                if (distances[point] > closestDistance)
                {
                    closestPoint = point;
                    closestDistance = distances[point];
                }
            }
            // Console.WriteLine($"Closest point: {closestPoint}, distance: {closestDistance}");
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
}