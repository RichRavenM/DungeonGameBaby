using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonGame1
{
    [Serializable]
    public class Player
    {

        public string name;
        public int id;
        public int coins = 0;
        public int level = 1;
        public int xp = 1;
        public int health = 10;
        public int damage = 1;
        public int armourValue = 0;
        public int potion = 5;
        public int weaponValue = 1;

        public int mods = 0;

        public enum PlayerClass { Ninja, Berserker, Druid };
        public PlayerClass currentClass = PlayerClass.Berserker;

        public int GetHealth()
        {
            int upper = (2 * mods + 5);
            int lower = (mods + 2);
            return Program.rand.Next(lower, upper);
        }

        public int GetPower()
        {
            int upper = (2 * mods + 2);
            int lower = (mods + 1);
            return Program.rand.Next(lower, upper);
        }

        public int GetCoins()
        {
            int upper = (15 * mods + 50);
            int lower = (10 * mods + 10);
            return Program.rand.Next(lower, upper);
        }

        public int GetXp()
        {
            int upper = (20 * mods + 50);
            int lower = (15 * mods + 10);
            return Program.rand.Next(lower, upper);
        }

        public int GetLevelUpValue()
        {
            double total = 0;

            for (int i = 1; i < level; i++)
            {
                total += Math.Floor(i + 300 * Math.Pow(2, i / 7.0));
            }

            return (int)Math.Floor(total / 4);
        }

        public bool CanLevelUp()
        {
            if (xp >= GetLevelUpValue())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void LevelUp()
        {
            while (CanLevelUp())
            {
                level++;
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Program.PrintForLevelUp($"Congrats, you are now level {level}!");
            Console.ResetColor();
        }
    }
}

