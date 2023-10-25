using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame1
{
    public class Encounters
    {
        static Random rand = new Random();

        // Encounters generic

        //Encounter
        public static void FirstEncounter()
        {
            Program.Print("You throw open the door and grab a rusty metal sword while charging towards your captor.");
            Program.Print("He turns...");
            Console.ReadKey();
            Combat(false, "Human Rogue", 2, 4);

        }

        public static void BasicFightEncounter()
        {
            Console.Clear();
            Program.Print("You turn the corner and there you see a foe...");
            Console.ReadKey();
            Combat(true, "", 0, 0);
        }

        public static void WizardEncounter()
        {
            Console.Clear();
            Program.Print("Suddenly, you see a long man with a pearlescent white beard, reading his evil tome.");
            if (IsChristmas())
            {
                Combat(false, "Scary Sorcerer Santa", 3, 5);
            }
            else
            {
                Combat(false, "Dark Wizard Jeff", 4, 2);
            }
        }

        public static void PuzzleEncounter()
        {
            Console.Clear();
            Program.Print("You make your way down the hall. You see a floor covered in runes.");
            List<char> chars = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            List<int> positions = new List<int>();
            char c = chars[Program.rand.Next(0, 10)];
            chars.Remove(c);

            for (int y = 0; y < 4; y++)
            {
                int position = Program.rand.Next(0, 4);
                positions.Add(position);
                for (int x = 0; x < 4; x++)
                {
                    if (x == position)
                        Console.Write(c);
                    else
                        Console.Write(chars[Program.rand.Next(0, 8)]);
                }
                Console.Write("\n");
            }
            Program.Print("Choose your path... (Type the position of the rune you want to stand on [left to right] not the number)");
            Program.Print("Alternatively, press E to turn around and return to the shop...");

            for (int i = 0; i < 4; i++)
            {
                while (true)
                {
                    string form = Console.ReadLine();
                    if (int.TryParse(form, out int input) && input < 5 && input > 0)
                    {
                        if (positions[i] == input - 1)
                        {
                            break;
                        }
                        else
                        {
                            if (Program.currentPlayer.currentClass == Player.PlayerClass.Ninja)
                            {
                                int damage = Program.rand.Next(0, 2);
                                Program.Print($"Spikes emerge from the ground you stand on. Your ninja skills help avoid the worst of it. You take {damage} damage!!");
                                Program.currentPlayer.health -= damage;
                            }
                            else
                            {
                                Program.Print("Spikes emerge from the ground you stand on. You take 2 damage!!");
                                Program.currentPlayer.health -= 2;
                            }
                            Console.ReadKey();
                            Program.Print("Choose your path... (Type the position of the rune you want to stand on [left to right] not the number)");
                            Program.Print("Alternatively, press E to turn around and return to the shop...");
                            if (Program.currentPlayer.health <= 0)
                            {
                                //Death code
                                //soundplayer.Stop();
                                Program.Print($"You have been bleeding heavily. It's too much. You take your final breath, and you wait for the heavens to take you away...");
                                Console.ReadKey();
                                Environment.Exit(0);
                            }
                        }
                    }
                    else
                    {
                        if (form.ToLower() == "r")
                        {
                            Shop.LoadShop(Program.currentPlayer);
                        }
                        else
                        {
                            Program.Print("Invalid input: Please input 1, 2, 3, or 4.");
                        }
                    }
                }
                Program.Print("You have successfully navigated the puzzle. Congratulations.");
                int r = Program.currentPlayer.GetCoins();
                int x = Program.currentPlayer.GetXp();
                Program.Print($"At the other side of the puzzle you see a basket with some treasure.\nYou have gained {r} gold coins!\nYou have gained {x} XP!");
                Program.currentPlayer.coins += c;
                Program.currentPlayer.xp += x;
                if (Program.currentPlayer.CanLevelUp())
                {
                    Program.currentPlayer.LevelUp();
                }
                Console.ReadKey();
                Shop.LoadShop(Program.currentPlayer);
            }
        }

        //Encounter tools

        public static void RandomEncounter()
        {
            switch (rand.Next(0, 3))
            {
                case 0:
                    BasicFightEncounter();
                    break;
                case 1:
                    WizardEncounter();
                    break;
                case 2:
                    PuzzleEncounter();
                    break;
            }
        }


        public static void Combat(bool random, string name, int power, int health)
        {
            SoundPlayer soundplayer = new SoundPlayer($"sounds{Path.DirectorySeparatorChar}battle.wav");
            soundplayer.PlayLooping();
            string n = "";
            int p = 0;
            int h = 0;

            if (random)
            {
                n = GetName();
                p = Program.currentPlayer.GetPower();
                h = Program.currentPlayer.GetHealth();
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
                    Program.PrintForEncounter($"You surge towards your attacker, ready to strike! As you lunge, the {n} strikes you!");
                    int damage = p - Program.currentPlayer.armourValue;
                    if (damage < 0)
                    {
                        damage = 0;
                    }
                    int attack = rand.Next(0, Program.currentPlayer.weaponValue) + rand.Next(1, 4) + (Program.currentPlayer.currentClass == Player.PlayerClass.Berserker ? rand.Next(1, 4) : 0);
                    Program.currentPlayer.health -= damage;
                    h -= attack;

                    Program.PrintForEncounter($"You lose {damage} health and deal {attack} damage.");


                }
                else if (input.ToLower() == "d" || input.ToLower() == "defend")
                {
                    //Defend
                    Program.PrintForEncounter($"As the {n} prepares to strike, you ready your sword in a defensive stance.");
                    int damage = (p / 4) - Program.currentPlayer.armourValue;
                    if (damage < 0)
                    {
                        damage = 0;
                    }
                    int attack = rand.Next(0, Program.currentPlayer.weaponValue) / 2;
                    Program.currentPlayer.health -= damage;
                    h -= attack;

                    Program.PrintForEncounter($"You lose {damage} health and deal {attack} damage.");
                }
                else if (input.ToLower() == "r" || input.ToLower() == "run")
                {
                    //Run
                    if (Program.currentPlayer.currentClass != Player.PlayerClass.Ninja && rand.Next(0, 2) == 0)
                    {
                        Program.PrintForEncounter($"As you sprint away from the {n}, it strike catches you in the back, sending you sprawling onto the ground.");
                        int damage = p - Program.currentPlayer.armourValue;
                        if (damage < 0)
                        {
                            damage = 0;
                        }
                        Program.currentPlayer.health -= damage;
                        Program.PrintForEncounter($"You lose {damage} health and are unable to escape.");
                    }
                    else
                    {
                        Program.PrintForEncounter($"You use your ninja prowess to evade the {n}, and you successfully escape.");
                        Console.ReadKey();
                        soundplayer.Stop();
                        Shop.LoadShop(Program.currentPlayer);
                    }
                }
                else if (input.ToLower() == "h" || input.ToLower() == "heal")
                {
                    //Heal
                    if (Program.currentPlayer.potion == 0)
                    {
                        Program.PrintForEncounter("As you desperately grasp for a potion in your bag, all that you feel are empty vials.");
                        int damage = p - Program.currentPlayer.armourValue;
                        if (damage < 0)
                        {
                            damage = 0;
                        }
                        Program.currentPlayer.health -= damage;
                        Program.PrintForEncounter($"The {n} strikes you with a mighty blow, and you lose {damage} health.");
                    }
                    else
                    {
                        Program.PrintForEncounter($"You reach into your bag and pull out a bright-coloured vial. You take a long drink.");
                        int potionValue = 5 + (Program.currentPlayer.currentClass == Player.PlayerClass.Druid ? 3 : 0);
                        Program.PrintForEncounter($"You gain {potionValue} health.");
                        Program.currentPlayer.health += potionValue;
                        if (Program.currentPlayer.health > Program.currentPlayer.GetMaxHealth())
                        {
                            Program.currentPlayer.health = Program.currentPlayer.GetMaxHealth();
                        }
                        Program.currentPlayer.potion--;
                        Program.PrintForEncounter($"As you were healing, the {n} advanced and struck!");
                        int damage = (p / 2) - Program.currentPlayer.armourValue;
                        if (damage < 0)
                        {
                            damage = 0;
                        }
                        Program.PrintForEncounter($"You lose {damage} health.");
                    }
                }
                if (Program.currentPlayer.health <= 0)
                {
                    //Death code
                    soundplayer.Stop();
                    Program.Print($"As the {n} stands over you, he utters these words: \"Get good, scrub!\". You have been defeated by the mighty {n}");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                Console.ReadKey();
            }
            int c = Program.currentPlayer.GetCoins();
            int x = Program.currentPlayer.GetXp();
            soundplayer.Stop();
            Program.Print($"As you stand over the body of the {n}, its body dissolves into {c} gold coins!\nYou have gained {x} XP!");
            Program.currentPlayer.coins += c;
            Program.currentPlayer.xp += x;
            if (Program.currentPlayer.CanLevelUp())
            {
                Program.currentPlayer.LevelUp();
            }

            Console.ReadKey();
        }

        public static string GetName()
        {

            int rando = rand.Next(0, 5);

            string[] foes;

            if (IsChristmas())
            {
                foes = new string[] { "Reindeer", "Evil Elf", "Snowmanhunter", "Corrupted Sleigh", "Mischievious Yeti" };
            }
            else
            {

                foes = new string[] { "Pirate", "Saibaman", "Zealot", "Temperamental Elf", "Human Rogue" };
            }
            return foes[rando];
        }

        public static bool IsChristmas()
        {
            DateTime time = DateTime.Now;
            if (time.Month == 12)
            {
                return true;
            }
            return false;
        }
    }
}
