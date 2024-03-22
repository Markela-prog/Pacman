using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

namespace MultithreadingProject
{
    internal class Player
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public char[,] Map { get; set; }
        public int score { get; set; }


        public Player(int x, int y, char[,] map)
        {
            this.X = x;
            this.Y = y;
            this.Map = map;
        }

        public void Move(char[,] map, ConsoleKeyInfo pressedKey)
        {
            int[] direction = GetDirection(pressedKey);

            int nextUserPositionX = X + direction[0];
            int nextUserPositionY = Y + direction[1];

            char nextPosition = map[nextUserPositionX, nextUserPositionY];

            if (nextPosition == ' ' || nextPosition == 'X' || nextPosition == 'o')
            {
                X = nextUserPositionX;
                Y = nextUserPositionY;


                CollectX(score, X, Y, map);
            }
        }

        private int[] GetDirection(ConsoleKeyInfo pressedKey)
        {
            int[] direction = { 0, 0 };

            if (pressedKey.Key == ConsoleKey.UpArrow)
            {
                direction[0] = -1;
            }
            else if (pressedKey.Key == ConsoleKey.DownArrow)
            {
                direction[0] = 1;
            }
            else if (pressedKey.Key == ConsoleKey.LeftArrow)
            {
                direction[1] = -1;
            }
            else if (pressedKey.Key == ConsoleKey.RightArrow)
            {
                direction[1] = 1;
            }

            return direction;
        }


        private void CollectX(int score, int userX, int userY, char[,] map)
        {
            if (map[userX, userY] == 'X')
            {
                map[userX, userY] = 'o';
                this.score++;
            }
        }


        
    }
}

