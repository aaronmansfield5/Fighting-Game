using System;
using System.Collections.Generic;
using System.Linq;

namespace fighting_game
{
    class Program
    {
        static void Main(string[] args)
        {
        fightDragon:
            {
                int myHealth = new Random().Next(65, 135);

                Dictionary<string, int> dragon = new Dictionary<string, int>(); // The key is a string and the value is an int <key, value>
                dragon.Add("Health", new Random().Next(65, 135));
                dragon.Add("Attack", new Random().Next(35, 75));
                int health = dragon["Health"];
                int attack = dragon["Attack"];
                Console.WriteLine("The dragon's health is {0} and it's attack damage is {1}!", health, attack);
                Console.WriteLine("Your health is {0}.", myHealth);

                string[] attacks = { "Ground Slam", "Flaming Fist", "Arcane Spray", "Lightning Strikes", "Whirlwind" };
                int[] attackdamage = { 25, 15, 20, 35, 25 };

                Dictionary<string, int> playerAttacks = new Dictionary<string, int>();

                for (int i = 0; i < 3; i++)
                {
                    int random = new Random().Next(1, attacks.Length);
                    bool conditional = Convert.ToBoolean(chooseAttack(attacks, playerAttacks, random, attackdamage));
                }

                string[] attackArray = playerAttacks.Keys.ToArray();
                string attStr = "";
                for (int i = 0; i < attackArray.Length; i++)
                {
                    attStr += string.Concat((i + 1) + ". " + attackArray[i] + "\n");
                }

                Console.Write("Which attack would you like to use?\n{0}", attStr);

                while (true)
                {
                    string input = Console.ReadLine();
                    int selection;
                    if (int.TryParse(input, out selection))
                    {
                        if (selection <= attackArray.Length)
                        {
                            Console.WriteLine("Using {0} to attack the dragon!", attackArray[selection - 1]);
                            System.Threading.Thread.Sleep(2000);
                            if ((health - playerAttacks[attackArray[selection - 1]]) <= 0)
                            {
                                health = 0;
                                Console.WriteLine("Wow, you killed the beast! Would you like to play again?");
                                string choice = Console.ReadLine();
                                if (choice.ToLower().Contains("y"))
                                {
                                    Console.WriteLine("Great, loading new game!");
                                    System.Threading.Thread.Sleep(1000);
                                    Console.Clear();
                                    goto fightDragon;
                                }
                                else
                                {
                                    Console.WriteLine("Oh well, I hope you had fun!");
                                    System.Threading.Thread.Sleep(1000);
                                    break;
                                }
                            }
                            else
                            {
                                health -= playerAttacks[attackArray[selection - 1]];
                                Console.WriteLine("You hit the dragon for {0}. It has {1} health remaining!", playerAttacks[attackArray[selection - 1]], health);
                                int chance = new Random().Next(1, 3);
                                if (chance == 1)
                                {
                                    if (myHealth - attack <= 0)
                                    {
                                        myHealth -= attack;
                                        Console.WriteLine("Oh no, the dragon fought back and it was fatal, you died! Would you like to play again?");
                                        string choice = Console.ReadLine();
                                        if (choice.ToLower().Contains("y"))
                                        {
                                            Console.WriteLine("Great, loading new game!");
                                            System.Threading.Thread.Sleep(1000);
                                            Console.Clear();
                                            goto fightDragon;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Oh well, I hope you had fun!");
                                            System.Threading.Thread.Sleep(1000);
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        myHealth -= attack;
                                        Console.WriteLine("Oh no, the dragon is fighting back! It knocked you down by {0} HP to {1} health!", attack, myHealth);
                                        System.Threading.Thread.Sleep(3000);
                                    }
                                }
                                System.Threading.Thread.Sleep(2000);
                                Console.Clear();
                                Console.Write("Which attack would you like to use?\n{0}", attStr);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("That selection is incorrect!");
                        System.Threading.Thread.Sleep(2000);
                        Console.Clear();
                        Console.Write("Which attack would you like to use?\n{0}", attStr);
                    }
                }
            }
        }

        static object chooseAttack(string[] attacks, Dictionary<string, int> playerAttacks, int random, int[] attackdamage)
        {
            if (playerAttacks.ContainsKey(attacks[random]))
            {
                random = new Random().Next(1, attacks.Length);
                Convert.ToBoolean(chooseAttack(attacks, playerAttacks, random, attackdamage));
                return true;

            }
            else
            {
                playerAttacks.Add(attacks[random], attackdamage[random]);
                return true;
            }
        }
    }
}