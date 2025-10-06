using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace HealthSystem_Roman
{
    internal class Program
    {
        static int health = 100;
        static string healthStatus;
        static int shield = 100;
        static int lives = 3;

        static void Main(string[] args)
        {
            UnitTestHealthSystem();


        }

        static void TakeDamage(int damage)
        {   
            if(damage < 0)
            {
                Console.WriteLine("Value Range Error");
            }
            else
            {
                Console.WriteLine($"You took {damage} damage.");
                shield -= damage;
                if (shield <= 0)
                {
                    Console.WriteLine("Your shield broke!");
                    health += shield;
                    shield = 0;
                }
                else if (health <= 0)
                {
                    health = 0;
                    Console.WriteLine("Enemy has defeated you.");
                }
            }

            Revive();
        }

        static int Heal(int hp)
        {
            if (hp < 0)
            {
                Console.WriteLine("Value Range Error");
            }
            else
            {
                Console.WriteLine($"You healed for {hp} health");
                health += hp;
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
                Console.WriteLine($"You regenerated your shield to {hp} points");
                shield += hp;
            }
            return shield;
        }

        static void Revive()
        {
            if(health <= 0 )
            {
                Console.WriteLine($"You are revived! You have {lives} lives left.");
                health = 100;
                shield = 100;
                lives--;
            }
        }

        static void ShowHUD()
        {
            Console.WriteLine("");
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
            Debug.Assert(shield == 0);
            Debug.Assert(health == 0);
            Debug.Assert(lives == 3);

            // TakeDamage() - shield, health, and lives
            shield = 5;
            health = 100;
            lives = 3;
            TakeDamage(110);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 0);
            Debug.Assert(lives == 3);

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
            //Console.Clear();
        }


    }
}
