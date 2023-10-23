﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame1
{
    internal class Encounters
    {
        static Random rand = new Random();

        // Encounters generic

        //Encounter
        public static void FirstEncounter()
        {
            Console.WriteLine("You throw open the door and grab a rusty metal sword while charging towards your captor.");
            Console.WriteLine("He turns...");
            Console.ReadKey();
            Combat(false, "Human Rogue", 2, 4);

        }


        //Encounter tools

        public static void Combat(bool random, string name, int power, int health)
        {
            string n = "";
            int p = 0;
            int h = 0;

            if (random)
            {

            }
            else
            {
                n = name; p = power; h = health;

            }

            while (h > 0)
            {
                Console.Clear();
                Console.WriteLine(n);
                Console.WriteLine($"{p}/{h}");
                Console.WriteLine("==========================");
                Console.WriteLine(@"|    (A)ttack (D)efend   |");
                Console.WriteLine(@"|       (R)un (H)eal     |");
                Console.WriteLine("==========================");
                Console.WriteLine(@$"|  Potions: {Program.currentPlayer.potion} Health: {Program.currentPlayer.health} |");
                string input = Console.ReadLine();

                if (input.ToLower() == "a" || input.ToLower() == "attack")
                {
                    //Attack
                    Console.WriteLine($"You surge towards your attacker, ready to strike! As you pass, the {n} strikes you!");
                    int damage = p - Program.currentPlayer.armourValue;
                    if (damage < 0)
                    {
                        damage = 0;
                    }
                    int attack = rand.Next(0, Program.currentPlayer.weaponValue) + rand.Next(1, 4);
                    Program.currentPlayer.health -= damage;
                    h -= attack;

                    Console.WriteLine($"You lose {damage} health and deal {attack} damage.");


                }
                else if (input.ToLower() == "d" || input.ToLower() == "defend")
                {
                    //Defend
                    Console.WriteLine($"As the {n} prepares to strike, you ready your sword in a defensive stance.");
                    int damage = (p / 4) - Program.currentPlayer.armourValue;
                    if (damage < 0)
                    {
                        damage = 0;
                    }
                    int attack = rand.Next(0, Program.currentPlayer.weaponValue) / 2;
                    Program.currentPlayer.health -= damage;
                    h -= attack;

                    Console.WriteLine($"You lose {damage} health and deal {attack} damage.");
                }
                else if (input.ToLower() == "r" || input.ToLower() == "run")
                {
                    if (rand.Next(0, 2) == 0)
                    {
                        Console.WriteLine($"As you sprint away from the {n}, it strike catches you in the back, sending you sprawling onto the ground.");
                        int damage = p - Program.currentPlayer.armourValue;
                        if (damage < 0)
                        {
                            damage = 0;
                        }
                        Console.WriteLine($"You lose {damage} health and are unable to escape.");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine($"You use your ninja prowess to evade the {n}, and you successfully escape.");
                        Console.ReadKey();
                        // got to store
                    }
                }
                else if (input.ToLower() == "h" || input.ToLower() == "heal")
                {
                    //Heal
                    if (Program.currentPlayer.potion == 0)
                    {
                        Console.WriteLine("As you desperately grasp for a potion in your bag, all that you feel are empty vials.");
                        int damage = p - Program.currentPlayer.armourValue;
                        if (damage < 0)
                        {
                            damage = 0;
                        }
                        Console.WriteLine($"The {n} strikes you with a mighty blow, and you lose {damage} health.");
                    }
                    else
                    {
                        Console.WriteLine($"You reach into your bag and pull out a bright-coloured vial. You take a long drink.");
                        int potionValue = 5;
                        Console.WriteLine($"You gain {potionValue} health.");
                        Program.currentPlayer.health += potionValue;
                        Console.WriteLine($"As you were healing, the {n} advanced and struck!");
                        int damage = (p / 2) - Program.currentPlayer.armourValue;
                        if (damage < 0)
                        {
                            damage = 0;
                        }
                        Console.WriteLine($"You lose {damage} health.");
                    }
                    Console.ReadKey();
                }
                Console.ReadKey();
            }
        }
    }
}