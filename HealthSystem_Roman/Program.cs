using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HealthSystem_Roman
{
    internal class Program
    {
        static string studioName = "TEAM VOID";
        static string gameName = "Knight Tales";
        static int health = 100;
        static string healthStatus;
        static int shield = 100;
        static int lives = 3;
        static int xp = 0;
        static int level = 1;

        static void Main(string[] args)
        {
            UnitTestHealthSystem();

            Reset();
            Console.WriteLine("{0, 10}", studioName);
            Console.WriteLine("{0, 12}", gameName);
            Console.WriteLine();
            ShowHUD();
            TakeDamage(130);
            ShowHUD();
            Heal(10);
            ShowHUD();
            IncreaseXP(350);
            ShowHUD();
            RegenerateShield(50);
            ShowHUD();
            TakeDamage(100);
            ShowHUD();
            TakeDamage(50);
            ShowHUD();
            TakeDamage(20);
            ShowHUD();
            TakeDamage(250);
            ShowHUD();
        }

        static void TakeDamage(int damage)
        {   
            if(damage < 0)
            {
                Console.WriteLine("Value Range Error");
                return;
            }

            Console.WriteLine($"You took {damage} damage.");
            Console.WriteLine();

            if (shield > 0)
            {
                int damageToShield = Math.Min(shield, damage);
                shield -= damageToShield;
                damage -= damageToShield;

                if (shield == 0)
                {
                    Console.WriteLine("Your shield broke!");
                    Console.WriteLine();
                }
            }

            if (damage > 0)
            {
                health -= damage;
            }

            if (health < 0)
            {
                health = 0;
            }

            if (lives >= 99)
            {
                lives = 99;
            }

            if (health <= 0)
            {
                Console.WriteLine($"Remaining Health: {health}, Shield: {shield}");
                Console.WriteLine();
                Console.WriteLine("Enemy has Defeated you.");
                Console.WriteLine();
                Revive();
            }
            else
            {
                Console.WriteLine($"Remaining Health: {health}, Shield: {shield}");
            }
            Console.WriteLine();
        }

        static int Heal(int hp)
        {
            if (hp < 0)
            {
                Console.WriteLine("Value Range Error");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"You healed for {hp} health");
                health = Math.Min(health + hp, 100);
                Console.WriteLine();
            }
            return health;
        }

        static int RegenerateShield(int hp)
        {
            if (hp < 0)
            {
                Console.WriteLine("Value Range Error");
            }
            else
            {
                Console.WriteLine($"You regenerated your shield for {hp} points");
                shield = Math.Min(shield + hp, 100);
                Console.WriteLine();
            }
            return shield;
        }

        static void Revive()
        {   
            if(health <= 0 && lives > 0)
            {
                lives--;
                Console.WriteLine($"You are revived! You have {lives} lives left.");
                health = 100;
                shield = 100;
            }
            else if (health <= 0 && lives == 0)
            {
                Console.WriteLine("No more lives left. You lost!");
                Console.ReadKey(true);
                Environment.Exit(0);
            }
        }

        static void HealthCheck()
        {
            if (health <= 10)
            {
                healthStatus = "Imminent Danger";
            }
            else if (health <= 50)
            {
                healthStatus = "Badly Hurt";
            }
            else if (health <= 75)
            {
                healthStatus = "Hurt";
            }
            else if (health < 100)
            {
                healthStatus = "Healthy";
            }
            else
            {
                healthStatus = "Perfect Health";
            }
        }

        static void IncreaseXP(int exp)
        {
            if (exp < 0)
            {
                Console.WriteLine("Value Range Error");
                Console.WriteLine();
                return;
            }

            xp += exp;

            Console.WriteLine($"You gain {exp} XP!");
            Console.WriteLine();

            while (xp > level * 100)
            {
                xp -= level * 100;
                level++;
                Console.WriteLine($"You leveled up to level {level}!");
                Console.WriteLine();
            }
        }

        static void ShowHUD()
        {
            HealthCheck();
            
            if (health < 0)
            {
                health = 0;
            }

            if (lives >= 99)
            {
                lives = 99;
            }

            Console.WriteLine($"Health: {health}");
            Console.WriteLine($"Shield: {shield}");
            Console.WriteLine($"Lives: {lives}");
            Console.WriteLine($"XP: {xp}");
            Console.WriteLine($"Level: {level}");
            Console.WriteLine($"Health Status: {healthStatus}");
            Console.WriteLine();

            Console.ReadKey(true);
            Console.Clear();
        }

        static void Reset()
        {
            health = 100;
            shield = 100;
            lives = 3;
            xp = 0;
            level = 1;
        }









        static void UnitTestHealthSystem()
        {
            Debug.WriteLine("Unit testing Health System started...");

            // TakeDamage()

            // TakeDamage() - only shield
            shield = 100;
            health = 100;
            lives = 3;
            TakeDamage(10);
            Debug.Assert(shield == 90);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // TakeDamage() - shield and health
            shield = 10;
            health = 100;
            lives = 3;
            TakeDamage(50);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 60);
            Debug.Assert(lives == 3);

            // TakeDamage() - only health
            shield = 0;
            health = 50;
            lives = 3;
            TakeDamage(10);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 40);
            Debug.Assert(lives == 3);

            // TakeDamage() - health and lives
            shield = 0;
            health = 10;
            lives = 3;
            TakeDamage(25);
            Debug.Assert(shield == 100);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 2);

            // TakeDamage() - shield, health, and lives
            shield = 5;
            health = 100;
            lives = 3;
            TakeDamage(110);
            Debug.Assert(shield == 100);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 2);

            // TakeDamage() - negative input
            shield = 50;
            health = 50;
            lives = 3;
            TakeDamage(-10);
            Debug.Assert(shield == 50);
            Debug.Assert(health == 50);
            Debug.Assert(lives == 3);

            // Heal()

            // Heal() - normal
            shield = 0;
            health = 90;
            lives = 3;
            Heal(5);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 95);
            Debug.Assert(lives == 3);

            // Heal() - already max health
            shield = 90;
            health = 100;
            lives = 3;
            Heal(5);
            Debug.Assert(shield == 90);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // Heal() - negative input
            shield = 50;
            health = 50;
            lives = 3;
            Heal(-10);
            Debug.Assert(shield == 50);
            Debug.Assert(health == 50);
            Debug.Assert(lives == 3);

            // RegenerateShield()

            // RegenerateShield() - normal
            shield = 50;
            health = 100;
            lives = 3;
            RegenerateShield(10);
            Debug.Assert(shield == 60);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // RegenerateShield() - already max shield
            shield = 100;
            health = 100;
            lives = 3;
            RegenerateShield(10);
            Debug.Assert(shield == 100);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // RegenerateShield() - negative input
            shield = 50;
            health = 50;
            lives = 3;
            RegenerateShield(-10);
            Debug.Assert(shield == 50);
            Debug.Assert(health == 50);
            Debug.Assert(lives == 3);

            // Revive()

            // Revive()
            shield = 0;
            health = 0;
            lives = 2;
            Revive();
            Debug.Assert(shield == 100);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 1);

            Debug.WriteLine("Unit testing Health System completed.");
            Console.Clear();
        }


    }
}
