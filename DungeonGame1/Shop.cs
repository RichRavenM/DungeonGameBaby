using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame1
{
    public class Shop
    {
        static int armourModifier;
        static int weaponModifier;
        static int difficultyModifier;


        public static void LoadShop(Player p)
        {
            /*
               armourModifier = p.armourValue;
               weaponModifier = p.weaponValue;
               difficultyModifier = p.mods;
            */
            RunShop(p);
        }

        public static void RunShop(Player p)
        {
            SoundPlayer soundplayer = new SoundPlayer($"sounds{Path.DirectorySeparatorChar}shop.wav");
            soundplayer.PlayLooping();
            Console.WriteLine("Welcome to Graham's Wonder Emporium! How may I assist you today?");

            int potionPrice;
            int armourPrice;
            int weaponPrice;
            int difficultyPrice;

            while (true)
            {
                potionPrice = 20 + 10 * p.mods;
                armourPrice = 100 * p.armourValue;
                weaponPrice = 100 * (p.weaponValue + 1);
                difficultyPrice = 300 + 100 * p.mods;
                Console.Clear();
                Console.Clear();
                Console.WriteLine(@" Graham's Wonder Emporium ");
                Console.WriteLine("==========================");
                Console.WriteLine(@$"(W)eapon:          ${weaponPrice}     ");
                Console.WriteLine(@$"(A)rmour:          ${armourPrice}     ");
                Console.WriteLine(@$"(P)otion:          ${potionPrice}     ");
                Console.WriteLine(@$"(D)ifficulty Mod:  ${difficultyPrice}     ");
                Console.WriteLine("==========================");
                Console.WriteLine(@"          (S)ave         ");
                Console.WriteLine(@"          (E)xit         ");
                Console.WriteLine(@"      (Q)uit the game    ");

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(@"       Player Stats       ");
                Console.WriteLine("==========================");
                Console.WriteLine(@$"Current Health:    {p.health}     ");
                Console.WriteLine(@$"Coins:            ${p.coins}     ");
                Console.WriteLine(@$"Weapon Strength:   {p.weaponValue}     ");
                Console.WriteLine(@$"Armour Value:      {p.armourValue}     ");
                Console.WriteLine(@$"Potions:           {p.potion}     ");
                Console.WriteLine(@$"Difficulty Mod:    {p.mods}     ");
                Console.WriteLine("==========================");

                string input = Console.ReadLine().ToLower();

                if (input == "w" || input == "weapon")
                    TryBuy("weapon", weaponPrice, p);
                else if (input == "a" || input == "armour")
                    TryBuy("armour", armourPrice, p);
                else if (input == "p" || input == "potion")
                    TryBuy("potion", potionPrice, p);
                else if (input == "d" || input == "difficulty mod")
                    TryBuy("difficulty", difficultyPrice, p);
                else if (input == "s" || input == "save")
                    Program.Save();
                else if (input == "q" || input == "quit")
                    Program.Quit();
                else if (input == "e" || input == "exit")
                {
                    soundplayer.Stop();
                    Encounters.RandomEncounter();

                }

            }
        }

        static void TryBuy(string item, int cost, Player p)
        {
            if (p.coins < cost)
            {
                Console.WriteLine("Apologies. You cannot afford this. Maybe consider getting good.");
                Console.ReadKey();
            }
            else
            {
                if (item == "potion")
                    p.potion++;
                else if (item == "armour")
                    p.armourValue++;
                else if (item == "weapon")
                    p.weaponValue++;
                else if (item == "difficulty mod")
                    p.mods++;

                p.coins -= cost;
            }
        }
    }
}
