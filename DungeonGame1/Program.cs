using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Media;

namespace DungeonGame1
{
    class Program
    {
        public static Player currentPlayer = new Player();
        public static bool mainLoop = true;
        public static Random rand = new Random();
        static void Main(string[] args)
        {
            if (!Directory.Exists("saves"))
            {
                Directory.CreateDirectory("saves");
            }
            Console.WriteLine("Press any key to start");
            Console.ReadKey();
            Console.Clear();
            currentPlayer = Load(out bool newP);
            if (newP)
            {
                Encounters.FirstEncounter();
            }
            while (mainLoop)
            {
                Encounters.RandomEncounter();
            }
        }

        static Player NewStart(int i)
        {
            Player p = new Player();
            Console.WriteLine("Rich's Dungeon Game");
            Console.WriteLine("Name:");
            while (true)
            {
                p.name = Console.ReadLine();
                if (p.name == "")
                {
                    Console.WriteLine("Please choose a valid name!");
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine("Choose your class: Ninja -- Berserker -- Druid");
            bool isValid = false;
            while (!isValid)
            {
                isValid = true;
                string input = Console.ReadLine().ToLower();

                switch (input)
                {
                    case "ninja":
                        p.currentClass = Player.PlayerClass.Ninja;
                        break;
                    case "berserker":
                        p.currentClass = Player.PlayerClass.Berserker;
                        break;
                    case "druid":
                        p.currentClass = Player.PlayerClass.Druid;
                        break;
                    default:
                        Console.WriteLine("Please choose and existing class");
                        isValid = false;
                        break;
                }

            }
            p.id = i;

            Console.Clear();
            Print($"You awake in a cold, stone, dark room. You feel dazed and are having trouble remembering\nanything about your past.");



            Print($"You know your name is {p.name}");

            Console.ReadKey();
            Console.Clear();
            Print("You stumble around in the darkness until you find a door handle. You feel some resistance as\nyou turn the door handle, but the rust lock breaks with little effort. You see your captor\nstanding with his back to you outside the door.");

            return p;
        }

        public static void Quit()
        {
            Console.Clear();
            Console.WriteLine("Would you like to save first: Y/N");
            while (true)
            {
                string input = Console.ReadLine();
                if (input.ToUpper() == "Y")
                {
                    Save();
                    Environment.Exit(0);
                }
                else if (input.ToUpper() == "N")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Please enter Y or N:");
                }
            }
        }

        public static void Save()
        {
            BinaryFormatter binForm = new BinaryFormatter();
            string path = $"saves{Path.DirectorySeparatorChar}{currentPlayer.id.ToString()}";
            FileStream file = File.Open(path, FileMode.OpenOrCreate);
            binForm.Serialize(file, currentPlayer);
            file.Close();
        }

        public static Player Load(out bool newP)
        {
            newP = false;
            Console.Clear();
            Console.WriteLine("Load Player file:");
            string[] paths = Directory.GetFiles("saves");
            List<Player> players = new List<Player>();

            int idCount = 0;

            BinaryFormatter binForm = new BinaryFormatter();


            foreach (string p in paths)
            {
                FileStream file = File.Open(p, FileMode.Open);
                Player player = (Player)binForm.Deserialize(file);
                file.Close();
                players.Add(player);
            }

            idCount = players.Count;
            while (true)
            {
                Console.Clear();
                foreach (Player p in players)
                {
                    Console.WriteLine($"{p.id}:\t{p.name}");
                }

                Console.WriteLine("Please input player id (as id:#) or name. Alternatively, type 'create' to create a new character:");
                string[] data = Console.ReadLine().Split(':');

                try
                {
                    if (data[0] == "id")
                    {
                        if (int.TryParse(data[1], out int id))
                        {
                            foreach (Player p in players)
                            {
                                if (p.id == id)
                                {
                                    return p;
                                }
                                Console.WriteLine("There is no player with that id!");
                                Console.ReadKey();
                            }

                        }
                        else
                        {
                            Console.WriteLine("The id needs to be in the format id:#, with '#' being the player id\nPress any key to continue.");
                            Console.ReadKey();
                        }
                    }
                    else if (data[0] == "create")
                    {
                        Player newPlayer = NewStart(idCount);
                        newP = true;
                        return newPlayer;
                    }
                    else
                    {
                        foreach (Player p in players)
                        {
                            if (p.name == data[0])
                            {
                                return p;
                            }
                            else
                            {
                                Console.WriteLine("There is no player with that name!");
                                Console.ReadKey();
                            }
                        }
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("The id needs to be in the format id:#, with '#' being the player id\nPress any key to continue.");
                    Console.ReadKey();

                }
            }
        }

        public static void Print(string text, int speed = 40)
        {
            SoundPlayer soundplayer = new SoundPlayer($"sounds{Path.DirectorySeparatorChar}typewriter-1.wav");
            soundplayer.PlayLooping();
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(speed);
            }
            soundplayer.Stop();
            Console.WriteLine();
        }

        public static void PrintForEncounter(string text, int speed = 40)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(speed);
            }
            Console.WriteLine();
        }
    }
}