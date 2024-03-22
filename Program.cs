// See https://aka.ms/new-console-template for more information
using MultithreadingProject;


Console.CursorVisible = false;
char[,] map;
bool result = true;
bool exit = false;
object lockObject = new object();
Console.SetBufferSize(120, 30);
string difficulty = "easy";

while (!exit)
{
    Menu();
}
void Menu()
{
    Console.Clear();
    Console.WriteLine("1) First map");
    Console.WriteLine("2) Second map");
    Console.WriteLine("3) Random map");
    Console.WriteLine("4) Exit");
    Console.Write("Choose option: ");

    switch (Console.ReadLine())
    {
        case "1":
            Console.Clear();
            Console.WriteLine("1) Easy");
            Console.WriteLine("2) Normal");
            Console.WriteLine("3) Hard");

            Console.Write("Choose option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    difficulty = "easy";
                    break;
                case "2":
                    difficulty = "normal";
                    break;
                case "3":
                    difficulty = "hard";
                    break;
                default:
                    break;
            }
            map = Map.ReadMap("map1.txt");
            Map.GenerateX(map);
            Player player = new Player(1, 1,map);
            List<Bot> bots = new List<Bot>();
            Game game = new Game(player,bots,map,difficulty);
            game.RunGame();
            Console.Clear();
            break;
        case "2":
            if (IsTutorialComplete(result))
            {
                
                Console.Clear();
                Console.WriteLine("1) Easy");
                Console.WriteLine("2) Normal");
                Console.WriteLine("3) Hard");
                
                Console.Write("Choose option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        difficulty = "easy";
                        break;
                    case "2":
                        difficulty = "normal";
                        break;
                    case "3":
                        difficulty = "hard";
                        break;
                    default:
                        break;
                }
                map = Map.ReadMap("map2.txt");
                Map.GenerateX(map);
                Player player2 = new Player(1, 1, map);
                List<Bot> bots2 = new List<Bot>();
                Game game2 = new Game(player2, bots2, map, difficulty);
                game2.RunGame();
                Console.Clear();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("You have to complete first map!");
                Thread.Sleep(2000);
            }
            break;
        case "3":
            if (IsTutorialComplete(result))
            {
                Console.Clear();
                Console.WriteLine("1) Easy");
                Console.WriteLine("2) Normal");
                Console.WriteLine("3) Hard");

                Console.Write("Choose option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        difficulty = "easy";
                        break;
                    case "2":
                        difficulty = "normal";
                        break;
                    case "3":
                        difficulty = "hard";
                        break;
                    default:
                        break;
                }

                Random random = new Random();
                string randomMap = "map" + random.Next(1, 3).ToString() + ".txt";
                map = Map.ReadMap(randomMap);
                Map.GenerateX(map);
                Player player3 = new Player(1, 1, map);
                List<Bot> bots3 = new List<Bot>();
                Game game3 = new Game(player3, bots3, map, difficulty);
                game3.RunGame();
                Console.Clear();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("You have to complete first map!");
                Thread.Sleep(2000);
            }
            break;
        case "4":
            exit = true;
            break;
        default:
            break;
    }
}

bool IsTutorialComplete(bool result)
{
    return result;
}

