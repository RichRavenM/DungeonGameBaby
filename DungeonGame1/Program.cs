using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame1
{
    class Program
    {
        public static Player currentPlayer = new Player();
        public static bool mainLoop = true;
        static void Main(string[] args)
        {
            Start();
            Encounters.FirstEncounter();
            while(mainLoop)
            {
                Encounters.RandomEncounter();
            }
        }

        static void Start()
        {
            Console.WriteLine("Rich's Dungeon Game");
            Console.WriteLine("Name:");
            currentPlayer.name = Console.ReadLine();
            Console.Clear();
            Console.WriteLine($"You awake in a cold, stone, dark room. You feel dazed and are having trouble remembering\nanything about your past.");
            if(currentPlayer.name == "")
            {
                Console.WriteLine("You can't even remember your own name...");
            }
            else
            {
               Console.WriteLine($"You know your name is {currentPlayer.name}");
            }
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("You stumble around in the darkness until you find a door handle. You feel some resistance as\nyou turn the door handle, but the rust lock breaks with little effort. You see your captor\nstanding with his back to you outside the door.");
        }
    }
}