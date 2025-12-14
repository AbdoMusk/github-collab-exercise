using System;
using System.Collections.Generic;

class Game
{
    static Player player = new Player();
    static List<Enemy> enemies = new List<Enemy>();
    static bool running = true;

    static void Main(string[] args)
    {
        Console.WriteLine("My Simple Game");
        Console.WriteLine("==============\n");

        enemies.Add(new Enemy("Goblin"));
        enemies.Add(new Enemy("Skeleton"));

        while(running)
        {
            Console.WriteLine("\nWhat do you want to do?");
            Console.WriteLine("1. Move Left");
            Console.WriteLine("2. Move Right");
            Console.WriteLine("3. Jump");
            Console.WriteLine("4. Check Enemies");
            Console.WriteLine("5. Quit");
            Console.Write("\nEnter choice: ");

            string input = Console.ReadLine();

            switch(input)
            {
                case "1":
                    player.Move(-1);
                    break;
                case "2":
                    player.Move(1);
                    break;
                case "3":
                    player.Jump();
                    player.PlayJumpAnimation();
                    break;
                case "4":
                    UpdateEnemies();
                    break;
                case "5":
                    running = false;
                    break;
                default:
                    Console.WriteLine("invalid option try again");
                    break;
            }
        }

        Console.WriteLine("Thanks for playing!");
    }

    // checks what all the enemies are doing
    static void UpdateEnemies()
    {
        Console.WriteLine("\n--- Enemy Status ---");
        foreach(var enemy in enemies)
        {
            enemy.Update();
        }
    }
}
