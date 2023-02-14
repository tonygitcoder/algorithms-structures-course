using Kse.Algorithms.Samples;

internal class Program
{
    public static void Main(string[] args)
    {
        const int maxHeight = 11;
        const int maxWidth = 35;
        
        var generator = new MapGenerator(new MapGeneratorOptions()
        {
            Height = maxHeight,
            Width = maxWidth,
        });

        string[,] map = generator.Generate();
        
        // var maxDistance = 0;
        // var maxDistancePoint = new Point(0, 0);

        var shortestPath
            = GetShortestPath(map, 
                new Point(0, 0), 
                new Point(maxWidth-1, maxHeight-1));
        new MapPrinter().Print(map, shortestPath);
        

        List<Point> GetShortestPath(string[,] map, Point start, Point goal)
        {
            map[start.Column, start.Row] = "A";
            map[goal.Column, goal.Row] = "B";
            
            var visitedPoints = new List<Point>{ new Point(start.Column, start.Row) };
            var distances = new Dictionary<Point, int>
            {
                { new Point(start.Column, start.Row), 0 }
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
                int index = 0;
                while (visitedPoints.Count < map.Length)
                {
                    Console.WriteLine($"Index: {index++}");
                    //Console.WriteLine("distances: " + string.Join(", ", distances.Keys.Select(p => $"{p.Row} {p.Column}")));
                    var currentPoint = GetClosestPoint(distances, visitedPoints);
                    visitedPoints.Add(currentPoint);
                    
                    map[currentPoint.Column, currentPoint.Row] = "-";

                    var linDist = CalculateLinearDistance(currentPoint, goal);

                    var neighbours = GetNeighbours(currentPoint.Column, currentPoint.Row, map);
                    foreach (var neighbour in neighbours)
                    {
                        if (visitedPoints.Contains(neighbour)) continue;
                        
                        // TODO: bugfix not spreading the maze
                        var distance = distances[currentPoint] + 1;
                        // if (distance>maxDistance)
                        // {
                        //     maxDistancePoint = neighbour;
                        // }

                        if (distances.ContainsKey(neighbour))
                        {
                            if (distances[neighbour]  < distance) //distance + linDist > CalculateLinearDistance(neighbour, goal)
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

        Point GetClosestPoint(Dictionary<Point, int> distances, List<Point> visitedPoints)
        {
            var closestPoint = new Point(0, 0);
            var closestDistance = int.MaxValue;
            
            foreach (var point in distances.Keys)
            {
                if (visitedPoints.Contains(point)) continue;
                if (distances[point] < closestDistance)
                {
                    //Console.WriteLine($"Point: {point.Row} {point.Column}, distance: {distances[point]}");
                    closestPoint = point;
                    closestDistance = distances[point];
                }
            }
            // Console.WriteLine($"Closest point: {closestPoint}, distance: {closestDistance}");
            return closestPoint;

            // return maxDistancePoint;
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