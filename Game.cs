using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace MultithreadingProject
{
    internal class Game
    {
        private Player player;
        private List<Bot> bots;
        private char[,] map;
        private String difficulty;
        
        ConsoleKeyInfo pressedKey = new ConsoleKeyInfo();

        public Game(Player player, List<Bot> bots, char[,] map, String difficulty)
        {
            this.player = player;
            this.map = map;
            this.bots = bots;
            this.difficulty = difficulty;
            switch (difficulty)
            {
                case "easy":
                    this.bots = new List<Bot> { new Bot(4, 5, map) };
                    break;
                case "normal":
                    this.bots = new List<Bot> { new Bot(4, 5, map), new Bot(15, 5, map) };
                    break;
                case "hard":
                    this.bots = new List<Bot> { new Bot(4, 5, map), new Bot(4, 5, map), new Bot(4, 5, map), new Bot(4, 5, map), new Bot(4, 5, map) };
                    break;
                default:
                    throw new ArgumentException("Invalid difficulty level");
            }
        }

        public void RunGame()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    pressedKey = Console.ReadKey(true);

                }
            }, cancellationToken);

            Thread[] threads = new Thread[GetNumBotThreads(difficulty)];
            for (int i = 0; i < bots.Count; i++)
            {
                Bot tempBot = bots[i];
                Thread botThread = new Thread(() =>
                {
                    while (true)
                    {
                        tempBot.MoveTowardsPlayer(player, map);
                        Thread.Sleep(300); // delay to prevent the loop from running too fast
                    }
                });
                botThread.Start();
                threads[i] = botThread;
            }


            // start the game loop
            while (!cancellationToken.IsCancellationRequested)
            {
                player.Move(map, pressedKey);
                Console.SetCursorPosition(30, 17);
                Console.Write("Bots alive - " + GetNumBotThreads(difficulty));

                Map.DrawMap(map, player, bots);

                Console.SetCursorPosition(30, 15);
                Console.Write("Score: " + player.score);


                // check if the player has collided with a bot
                foreach (Bot bot in bots)
                {
                    if (player.X == bot.X && player.Y == bot.Y)
                    {
                        cancellationTokenSource.Cancel();
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Game over!");
                    
                        Thread.Sleep(2000);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }
                }

                if (Map.CountX(map) == 0)
                {
                    cancellationTokenSource.Cancel();
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" Congratz you won the game!\n Your score - " + player.score);
                    Thread.Sleep(2000);
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
               
                Thread.Sleep(100);
            }
        }

        private int GetNumBotThreads(String diff)
        {
            switch (diff)
            {
                case "easy":
                    return 1;
                case "normal":
                    return 2;
                case "hard":
                    return 5;
                default:
                    throw new ArgumentException("Invalid difficulty level");
            }
        }

     
    }
}