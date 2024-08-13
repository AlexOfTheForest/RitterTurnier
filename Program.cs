using System;
using System.Collections.Generic;
using System.Text;

namespace RitterTurnier
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(" _____                                                                                                                           _____ \r\n( ___ )                                                                                                                         ( ___ )\r\n |   |~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|   | \r\n |   |  _______   __    __      __                                ________                               __                      |   | \r\n |   | |       \\ |  \\  |  \\    |  \\                              |        \\                             |  \\                     |   | \r\n |   | | $$$$$$$\\ \\$$ _| $$_  _| $$_     ______    ______         \\$$$$$$$$__    __   ______   _______   \\$$  ______    ______   |   | \r\n |   | | $$__| $$|  \\|   $$ \\|   $$ \\   /      \\  /      \\          | $$  |  \\  |  \\ /      \\ |       \\ |  \\ /      \\  /      \\  |   | \r\n |   | | $$    $$| $$ \\$$$$$$ \\$$$$$$  |  $$$$$$\\|  $$$$$$\\         | $$  | $$  | $$|  $$$$$$\\| $$$$$$$\\| $$|  $$$$$$\\|  $$$$$$\\ |   | \r\n |   | | $$$$$$$\\| $$  | $$ __ | $$ __ | $$    $$| $$   \\$$         | $$  | $$  | $$| $$   \\$$| $$  | $$| $$| $$    $$| $$   \\$$ |   | \r\n |   | | $$  | $$| $$  | $$|  \\| $$|  \\| $$$$$$$$| $$               | $$  | $$__/ $$| $$      | $$  | $$| $$| $$$$$$$$| $$       |   | \r\n |   | | $$  | $$| $$   \\$$  $$ \\$$  $$ \\$$     \\| $$               | $$   \\$$    $$| $$      | $$  | $$| $$ \\$$     \\| $$       |   | \r\n |   |  \\$$   \\$$ \\$$    \\$$$$   \\$$$$   \\$$$$$$$ \\$$                \\$$    \\$$$$$$  \\$$       \\$$   \\$$ \\$$  \\$$$$$$$ \\$$       |   | \r\n |___|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|___| \r\n(_____)                                                                                                                         (_____)");

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();
            Console.WriteLine();
            List<Knight> knights = new List<Knight>();
            int numberOfKnights = GetNumberOfKnights(knights);
            InitializeKnights(numberOfKnights, knights);

            Random random = new Random();

            // Turnier
            while (knights.Count > 1)
            {
                Console.WriteLine($"╔═══════════════════════════════════╗");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\n{knights.Count} Ritter sind bereit zu kämpfen! ⚔️ ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"\n╚═══════════════════════════════════╝");

                List<Knight> winners = new List<Knight>();

                for (int i = 0; i < knights.Count; i += 2)
                {
                    if (i + 1 < knights.Count)
                    {
                        Knight knight1 = knights[i];
                        Knight knight2 = knights[i + 1];
                        Knight winner = SimulateBattle(knight1, knight2, random);

                        winners.Add(winner);
                        Console.WriteLine("╔═════════════════════════════════╗");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\n{knight1.Name} vs {knight2.Name} -> Gewinner: {winner.Name} (Lvl: {winner.Lvl}) 🏆 ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"\n╚═════════════════════════════════╝");
                    }
                    else
                    {
                        winners.Add(knights[i]);
                        Console.WriteLine($"{knights[i].Name} kommt automatisch weiter.");
                    }
                }

                knights = winners;
            }

            var originalColor = Console.ForegroundColor;

            // white
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("╔═══════════════════════════════════════════════════╗");

            // yellow
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nDer Gewinner ist ");

            // yellow
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(knights[0].Name);

            // yellow
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($" und wird zum König gekrönt {knights[0].Lvl}! 👑");

            // white
            Console.ForegroundColor = originalColor;
            Console.WriteLine();

            // white
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n╚═══════════════════════════════════════════════════╝");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static int GetNumberOfKnights(List<Knight> knights)
        {
            Console.Write("Möchten Sie selbst spielen (Y/N)? ");

            bool isSelfPlayer = Console.ReadLine().Equals("Y", StringComparison.InvariantCultureIgnoreCase);
            int numberOfKnights = 0;

            if (isSelfPlayer)
            {
                Console.Write("\n\nGeben Sie Ihre Name ein ");
                string name = Console.ReadLine();
                PlayerKnight selfPlayerKnight = new PlayerKnight(0, name, 0, 100, true);
                knights.Add(selfPlayerKnight);
                numberOfKnights++;
            }

            Console.Write("\n\nWie viele Ritter wollen kämpfen? ⚔️ ");
            numberOfKnights += int.Parse(Console.ReadLine());

            while (numberOfKnights < 2)
            {
                Console.WriteLine("Mindestens 2 Ritter müssen teilnehmen.");
                Console.Write("Wie viele Ritter wollen kämpfen? ⚔️ ");
                numberOfKnights = int.Parse(Console.ReadLine());
            }

            return numberOfKnights;
        }

        public static void InitializeKnights(int numberOfKnights, List<Knight> knights)
        {
            int knightsToAdd = numberOfKnights - knights.Count;

            for (int i = 0; i < knightsToAdd; i++)
            {
                Console.Write($"\n\nGeben Sie den Namen von Ritter {i + 1} ein ", Console.ForegroundColor);
                Console.ForegroundColor = ConsoleColor.Green;
                string name = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                knights.Add(new Knight(i + 1, name, 0, 100, false));
            }
        }

        static Knight SimulateBattle(Knight knight1, Knight knight2, Random random)
        {
            Console.WriteLine($"\nKampf zwischen {knight1.Name} und {knight2.Name} beginnt!");
            knight1.Health = 100;
            knight2.Health = 100;

            while (knight1.Health > 0 && knight2.Health > 0)
            {
                if (knight1.IsSelfPlayerKnight || knight2.IsSelfPlayerKnight)
                {
                    Knight selfKnight = knight1.IsSelfPlayerKnight ? knight1 : knight2;
                    Knight opponentKnight = knight1.IsSelfPlayerKnight ? knight2 : knight1;

                    // Spielerzug
                    Console.WriteLine($"\n{selfKnight.Name}, es ist dein Zug! (Level: {selfKnight.Lvl})");
                    Console.WriteLine("1. Angreifen");
                    Console.WriteLine("2. Heilen");
                    Console.Write("Wähle eine Option (1/2): ");
                    string choice = Console.ReadLine();

                    if (choice == "1")
                    {
                        // Angriff 
                        int damage = random.Next(30, 60) + selfKnight.Lvl;
                        opponentKnight.Health -= damage;
                        Console.WriteLine($"{selfKnight.Name} Fügt {opponentKnight.Name} {damage} Schaden zu. ({opponentKnight.Name} Gesundheit ❤️ {opponentKnight.Health})");
                    }
                    else if (choice == "2")
                    {
                        // HHeal
                        int healAmount = random.Next(10, 21);
                        selfKnight.Health += healAmount;
                        Console.WriteLine($"{selfKnight.Name} Heilt sich um {healAmount} Leben. ({selfKnight.Name} Gesundheit 💚 {selfKnight.Health})");
                    }

                    if (opponentKnight.Health > 0)
                    {
                        int damageToSelf = random.Next(30, 60) + opponentKnight.Lvl;
                        selfKnight.Health -= damageToSelf;
                        Console.WriteLine($"{opponentKnight.Name} Fügt {selfKnight.Name} {damageToSelf} Schaden zu. ({selfKnight.Name} Gesundheit ❤️ {selfKnight.Health})");
                    }
                }
                else
                {
                    // Sim
                    if (random.Next(100) < 35)
                    {
                        ApplyRandomEvent(knight1, knight2, random);
                    }

                    int damageToKnight2 = random.Next(30, 60) + knight1.Lvl;
                    int damageToKnight1 = random.Next(30, 60) + knight2.Lvl;

                    knight2.Health -= damageToKnight2;
                    knight1.Health -= damageToKnight1;

                    var originalColor = Console.ForegroundColor;

                    Console.Write($"{knight1.Name} Fügt {knight2.Name} {damageToKnight2} Schaden zu. ({knight2.Name} Gesundheit ❤️ ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(knight2.Health);
                    Console.ForegroundColor = originalColor; // white
                    Console.WriteLine(")");

                    Console.Write($"{knight2.Name} Fügt {knight1.Name} {damageToKnight1} Schaden zu. ({knight1.Name} Gesundheit ❤️ ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(knight1.Health);
                    Console.ForegroundColor = originalColor; // white
                    Console.WriteLine(")");

                    System.Threading.Thread.Sleep(1000);
                }

                if (knight1.Health <= 0 && knight2.Health <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("\nBeide Ritter sind gefallen, es gibt keinen Gewinner in dieser Runde. 💀 ");
                    Console.ForegroundColor = ConsoleColor.White;
                    return random.Next(2) == 0 ? knight1 : knight2;
                }
            }

            Knight winner = knight1.Health > 0 ? knight1 : knight2;
            winner.Lvl += 1;
            Console.WriteLine($"{winner.Name} gewinnt den Kampf mit {winner.Health} Gesundheit übrig!");
            

            return winner;
        }

        static void ApplyRandomEvent(Knight knight1, Knight knight2, Random random)
        {
            // Events
            string[] events = { "heal", "Kritikal", "Nicht Getrofen" };
            string selectedEvent = events[random.Next(events.Length)];

            switch (selectedEvent)
            {
                case "heal":
                    Knight knightToHeal = random.Next(2) == 0 ? knight1 : knight2;
                    int healAmount = random.Next(10, 21);
                    knightToHeal.Health += healAmount;

                    // white
                    var originalColor = Console.ForegroundColor;

                    // healing green
                    Console.Write($"{knightToHeal.Name} Heilt sich um ");
                    Console.ForegroundColor = ConsoleColor.Green; // green
                    Console.Write(healAmount);
                    Console.ForegroundColor = originalColor; // white
                    Console.Write($" Leben. ({knightToHeal.Name} Gesundheit 💚 ");

                    // leben green
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(knightToHeal.Health);
                    Console.ForegroundColor = originalColor; // white
                    Console.WriteLine(")");

                    break;

                case "Kritikal":
                    Knight knightToCritical = random.Next(2) == 0 ? knight1 : knight2;
                    int criticalDamage = random.Next(10, 21);


                    // Save the original color

                    if (knightToCritical == knight1)
                    {
                        knight2.Health -= criticalDamage;

                        Console.Write($"{knight1.Name} Landet einen kritischen Treffer auf ");
                        Console.ForegroundColor = ConsoleColor.DarkYellow; // Red for damage
                        Console.Write($"{knight2.Name}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($" und verursacht ");
                        Console.ForegroundColor = ConsoleColor.DarkYellow; // Yellow for critical damage
                        Console.Write($"{criticalDamage}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($" zusätzlichen Schaden 🗡️ ({knight2.Name} Gesundheit ❤️ ");

                        Console.ForegroundColor = ConsoleColor.DarkYellow; // Red for health
                        Console.Write($"{knight2.Health}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(")");
                    }
                    else
                    {
                        knight1.Health -= criticalDamage;

                        Console.Write($"{knight2.Name} Landet einen kritischen Treffer auf ");
                        Console.ForegroundColor = ConsoleColor.DarkYellow; // Red for damage
                        Console.Write($"{knight1.Name}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($" und verursacht ");
                        Console.ForegroundColor = ConsoleColor.DarkYellow; // Yellow for critical damage
                        Console.Write($"{criticalDamage}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($" zusätzlichen Schaden 🗡️ ({knight1.Name} Gesundheit ❤️ ");

                        Console.ForegroundColor = ConsoleColor.DarkYellow; // Red for health
                        Console.Write($"{knight1.Health}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(")");
                    }

                    break;

                case "Nicht Getrofen":
                    Knight knightToMiss = random.Next(2) == 0 ? knight1 : knight2;
                    Console.Write($"{knight1.Name}");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($" Verfehlt seinen Angriff 🤣");
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }
    }

    public class Knight
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Lvl { get; set; }
        public int Health { get; set; }
        public bool IsSelfPlayerKnight { get; set; }

        public Knight(int id, string name, int lvl, int health, bool isSelfPlayerKnight)
        {
            Id = id;
            Name = name;
            Lvl = lvl;
            Health = health;
            IsSelfPlayerKnight = isSelfPlayerKnight;
        }
    }

    public class PlayerKnight : Knight
    {
        public PlayerKnight(int id, string name, int lvl, double health, bool isSelfPlayerKnight) : base(id, name, lvl, 100, isSelfPlayerKnight) { }
    }
}
