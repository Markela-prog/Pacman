using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MultithreadingProject
{
    internal class Bot
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Bot(int x, int y, char[,] map)
        {
            X = x;
            Y = y;

        }

        public void MoveTowardsPlayer(Player player, char[,] map)
        {
            // Calculate the path to the player using A*
            List<Point> path = CalculatePathToPlayer(player, map);

            // If a path was found, move to the next position
            if (path.Count > 1)
            {
                Point nextPosition = path[1];
                //Console.SetCursorPosition(40, 15);
                //Console.WriteLine($"Bot is trying to move from ({X}, {Y}) to ({nextPosition.X}, {nextPosition.Y})");
                X = nextPosition.X;
                Y = nextPosition.Y;
            }
        }


        private List<Point> CalculatePathToPlayer(Player player, char[,] map)
        {
            // The set of nodes already evaluated
            List<Point> closedSet = new List<Point>();

            // The set of currently discovered nodes that are not evaluated yet
            List<Point> openSet = new List<Point> { new Point(X, Y) };

            // For each node, which node it can most efficiently be reached from
            Dictionary<Point, Point> cameFrom = new Dictionary<Point, Point>();

            // For each node, the cost of getting from the start node to that node
            Dictionary<Point, double> gScore = new Dictionary<Point, double>
            {
                [new Point(X, Y)] = 0
            };

            // For each node, the total cost of getting from the start node to the goal
            // by passing by that node
            Dictionary<Point, double> fScore = new Dictionary<Point, double>
            {
                [new Point(X, Y)] = HeuristicCostEstimate(new Point(X, Y), new Point(player.X, player.Y))
            };

            while (openSet.Count > 0)
            {
                // the node in openSet having the lowest fScore[] value
                Point current = openSet.OrderBy(point => fScore.ContainsKey(point) ? fScore[point] : double.MaxValue).First();

                if (current.X == player.X && current.Y == player.Y)
                {
                    return ReconstructPath(cameFrom, current);
                }

                openSet.Remove(current);
                closedSet.Add(current);

                foreach (Point neighbor in GetNeighbors(current, map))
                {
                    if (closedSet.Contains(neighbor))
                    {
                        continue;
                    }

                    // The distance from start to a neighbor
                    double tentativeGScore = (gScore.ContainsKey(current) ? gScore[current] : double.MaxValue) + 1;

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                    else if (tentativeGScore >= (gScore.ContainsKey(neighbor) ? gScore[neighbor] : double.MaxValue))
                    {
                        continue;
                    }

                    // This path is the best until now. Record it!
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + HeuristicCostEstimate(neighbor, new Point(player.X, player.Y));

                    List<Point> path = ReconstructPath(cameFrom, current);
                    //Console.WriteLine($"Calculated path of length {path.Count}");
                    foreach (Point point in path)
                    {
                       // Console.WriteLine($"Path point: ({point.X}, {point.Y})");
                    }
                }
            }

            // Open set is empty but goal was never reached
            return new List<Point>();
        }

        private double HeuristicCostEstimate(Point a, Point b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        private List<Point> ReconstructPath(Dictionary<Point, Point> cameFrom, Point current)
        {
            List<Point> path = new List<Point> { current };

            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                path.Insert(0, current);
            }

            return path;
        }

        private List<Point> GetNeighbors(Point point, char[,] map)
        {
            List<Point> neighbors = new List<Point>();

            if (point.X - 1 >= 0 && map[point.X - 1, point.Y] != '#')
            {
                neighbors.Add(new Point(point.X - 1, point.Y));
            }

            if (point.X + 1 < map.GetLength(0) && map[point.X + 1, point.Y] != '#')
            {
                neighbors.Add(new Point(point.X + 1, point.Y));
            }

            if (point.Y - 1 >= 0 && map[point.X, point.Y - 1] != '#')
            {
                neighbors.Add(new Point(point.X, point.Y - 1));
            }

            if (point.Y + 1 < map.GetLength(1) && map[point.X, point.Y + 1] != '#')
            {
                neighbors.Add(new Point(point.X, point.Y + 1));
            }

            return neighbors;
        }
    



    public void Move(char[,] map)
        {
            while (true)
            {
                int[] direction = GetRandomDirection();

                int nextPositionX = X + direction[0];
                int nextPositionY = Y + direction[1];

                char nextPosition = map[nextPositionX, nextPositionY];

                if (nextPosition == ' ' || nextPosition == 'X' || nextPosition == 'o')
                {
                    X = nextPositionX;
                    Y = nextPositionY;
                }
                Thread.Sleep(500);
            }
        }

        private int[] GetRandomDirection()
        {
            Random random = new Random();
            int[] direction = { 0, 0 };
            int rand = random.Next(1, 5);
            if (rand == 1)
            {
                direction[0] = -1;
            }
            else if (rand == 2)
            {
                direction[0] = 1;
            }
            else if (rand == 3)
            {
                direction[1] = -1;
            }
            else if (rand == 4)
            {
                direction[1] = 1;
            }

            return direction;

        }
    }

}