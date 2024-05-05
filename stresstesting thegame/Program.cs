using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace stresstesting_thegame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Random rnd = new Random();

                Console.WriteLine("    __//\r\n    /.__.\\\r\n    \\ \\/ /\r\n '__/    \\\r\n  \\-      )\r\n   \\_____/\r\n_____|_|____\r\n     \" \"");

                Console.WriteLine("[1] Start Game");
                Console.WriteLine("[2] Exit");

                char option = Console.ReadKey().KeyChar;
                Console.Clear();

                if (option == '1')
                {

                    int playerHealth = 100;
                    int potions = rnd.Next(3, 5);
                    int maxLevels = 5;
                    int specialMove = rnd.Next(1, 3);
                    int poisonDuration = 0;
                    int burnDuration = 0;
                    int paralyzeDuration = 0;

                    for (int level = 1; level <= maxLevels; level++)
                    {
                        int enemyHealth = 20 + (level * 10);
                        int baseEnemyDamage = rnd.Next(3, 6);

                        bool isAssassin = level == 1;
                        bool isSlime = level == 2;
                        bool isElemental = level == 3;
                        bool isDragon = level == 4;
                        bool isChickelle = level == maxLevels;

                        if (playerHealth <= 0)
                        {
                            Console.WriteLine("Game over. Thanks for playing!");
                            return;
                        }

                        if (isAssassin)
                        {
                            Console.WriteLine("\nA Bandit appears!");

                            
                            enemyHealth = 20;

                        }
                        else if (isSlime)
                        {
                            Console.WriteLine("A Slime appears!");
                            enemyHealth = 40;

                            if (rnd.Next(1, 3) == 1)
                            {
                                Console.WriteLine("The slime poisoned you!");
                                poisonDuration = 3;
                            }
                        }
                        else if (isElemental)
                        {
                            Console.WriteLine("An Elemental appears!");
                            enemyHealth = 50;

                            if (rnd.Next(1, 3) == 1)
                            {
                                Console.WriteLine("The elemental paralyzed you!");
                                paralyzeDuration = 3;
                            }
                        }
                        else if (isDragon)
                        {
                            Console.WriteLine("A Dragon appears!");
                            enemyHealth = 60;
                            Console.WriteLine($"The Dragon attacks.");
                            if (rnd.Next(1, 1) <= 3)
                            {
                                Console.WriteLine("The dragon burned you!");
                                burnDuration = 5;
                            }
                            if (rnd.Next(1, 1) <= 3)
                            {
                                Console.WriteLine("The dragon paralyzed you!");
                                paralyzeDuration = 2;
                            }
                        }
                        else if (isChickelle)
                        {
                            Console.WriteLine("Chickelle, the Legendary Chicken, appears!");
                            enemyHealth = 1;

                        }

                        while (playerHealth > 0 && enemyHealth > 0)
                        {
                            Console.WriteLine($"\nLevel {level}");
                            Console.WriteLine($"Player health: {playerHealth}");
                            Console.WriteLine($"Enemy health: {enemyHealth}");
                            Console.WriteLine("\n[0] Attack\n[1] Heal\n[2] Special \n[3] Flee");

                            // Apply status effects
                            if (poisonDuration > 0)
                            {
                                Console.WriteLine("\nYou're poisoned! (-2 health)");
                                playerHealth -= 2;
                                poisonDuration--;
                            }
                            if (burnDuration > 0)
                            {
                                Console.WriteLine("\nYou're burning! (-3 health)");
                                playerHealth -= 3;
                                burnDuration--;
                            }
                            if (paralyzeDuration > 0)
                            {
                                Console.WriteLine("\nYou're paralyzed! (Cannot attack)");
                                paralyzeDuration--;
                            }

                            char option2 = Console.ReadKey().KeyChar;
                            Console.Clear();

                            if (option2 == '0')
                            {
                                if (paralyzeDuration == 0)
                                {
                                    int playerDamage = rnd.Next(1, 7);
                                    bool playerMissed = rnd.Next(1, 11) == 1;
                                    bool enemyMissed = rnd.Next(1, 11) == 1;

                                    if (playerMissed)
                                    {
                                        Console.WriteLine("You missed");
                                    }
                                    else
                                    {
                                        if (isChickelle && rnd.Next(1, 11) <= 9) // 90% dodge chance for Chickelle
                                        {
                                            Console.WriteLine("Chickelle dodged your attack!");
                                        }
                                        else
                                        {
                                            enemyHealth -= playerDamage;
                                            Console.WriteLine($"You attacked for {playerDamage} damage");
                                        }
                                    }

                                    if (enemyMissed)
                                    {
                                        Console.WriteLine("The enemy missed");
                                    }
                                    else
                                    {
                                        int actualEnemyDamage = baseEnemyDamage + rnd.Next(-2, 3);
                                        playerHealth -= actualEnemyDamage;
                                        Console.WriteLine($"The enemy attacked for {actualEnemyDamage} damage");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("You took damage! You can't attack.");
                                    int actualEnemyDamage = baseEnemyDamage + rnd.Next(-2, 3);
                                    playerHealth -= actualEnemyDamage;
                                }
                            }
                            else if (option2 == '1')
                            {
                                if (potions > 0)
                                {
                                    int heal = new Random().Next(0, 30);
                                    Console.WriteLine($"You healed for {heal} health");
                                    playerHealth += heal;
                                    Console.WriteLine($"The enemy attacked for {baseEnemyDamage} damage");
                                    playerHealth -= baseEnemyDamage;

                                    if (playerHealth > 100)
                                    {
                                        playerHealth = 100;
                                    }

                                    potions--;
                                }
                                else
                                {
                                    Console.WriteLine("You've run out of healing potions");
                                }
                            }
                            else if (option == '3')
                            {
                                Console.WriteLine("You fled the battle!");
                                break;
                            }
                            else if (option2 == '2')
                            {
                                if (specialMove > 0)
                                {
                                    int playerDamage = new Random().Next(10, 20);
                                    int specialDamage = playerDamage * 2;
                                    enemyHealth -= specialDamage;
                                    Console.WriteLine($"You unleashed Dragon's Fury for {specialDamage} damage!");
                                    specialMove--;
                                }
                                else
                                {
                                    Console.WriteLine("You've already used your special move!");
                                }
                            }
                            if (enemyHealth <= 0)
                            {
                                Console.WriteLine("\nYou defeated the enemy!");
                                Console.WriteLine("\nContinue to the next level? (Y/N)");
                                while (true)
                                {
                                    char continueOption = Console.ReadKey().KeyChar;
                                    if (continueOption == 'Y' || continueOption == 'y')
                                    {
                                        break;
                                    }
                                    else if (continueOption == 'N' || continueOption == 'n')
                                    {
                                        Console.WriteLine("\nGame over. Thanks for playing!");
                                        return;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nInvalid input. Pleasechoose (Y/N):");
                                    }
                                }
                                Console.Clear();
                            }
                        }
                    }
                    if (playerHealth > 0)
                    {
                        Console.WriteLine("Congratulations, You Win!");
                    }
                    break;
                }
                else if (option == '2')
                {
                    Console.WriteLine("Goodbye!");
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please choose (1/2):");
                }

            }
            
        }
    }
}


