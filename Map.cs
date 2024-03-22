using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadingProject
{
    internal class Map
    {
        public static void DrawMap(char[,] map, Player player, List<Bot> bots)
        {
            Console.SetCursorPosition(0, 0);

            // create a copy of the original map to draw the player and bots on
            char[,] updatedMap = (char[,])map.Clone();

            updatedMap[player.X, player.Y] = '@';

            foreach (Bot bot in bots)
            {
                updatedMap[bot.X, bot.Y] = '!';
            }

            // draw the updated map to the console
            for (int y = 0; y < updatedMap.GetLength(0); y++)
            {
                for (int x = 0; x < updatedMap.GetLength(1); x++)
                {
                    Console.Write(updatedMap[y, x]);
                }
                Console.Write("\n");
            }


        }

        public static char[,] ReadMap(string path)
        {
            string[] file = File.ReadAllLines(path);

            char[,] map = new char[GetMaxLengthOfLine(file), file.Length];

            for (int x = 0; x < map.GetLength(0); x++)
                for (int y = 0; y < map.GetLength(1); y++)
                    map[x, y] = file[y][x];

            return map;
        }

        private static int GetMaxLengthOfLine(string[] lines)
        {
            int maxLength = lines[0].Length;

            foreach (var line in lines)
            {
                if (line.Length > maxLength)
                    maxLength = line.Length;
            }
            return maxLength;
        }

        public static void GenerateX(char[,] map)
        {
            Random random = new Random();

            for (int i = 0; i < random.Next(20, 30);)
            {

                int randomX = random.Next(map.GetLength(0));
                int randomY = random.Next(map.GetLength(1));

                if (map[randomX, randomY] != '#' && map[randomX, randomY] != 'o' && map[randomX, randomY] != 'X' && map[randomX, randomY] != '@')
                {

                    map[randomX, randomY] = 'X';
                    i++;
                }

            }
        }

        public static int CountX(char[,] map)
        {
            int countX = 0;
            for (int i = 0; i < map.GetLength(0); i++)
            {

                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 'X')
                    {
                        countX++;
                    }
                }


            }
            return countX;
        }
    }
}
