using System;

class Program
{
    static void Main(string[] args)
    {
        string response = "yes";

        while (response.ToLower() == "yes" || response.ToLower() == "y")
        {
            Random randomGenerator = new Random();
            int magicNumber = randomGenerator.Next(1, 100);

            int guess = -1;

            while (guess != magicNumber)
            {
                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine());

                if (magicNumber > guess)
                {
                    Console.WriteLine("Higher");
                }
                else if (magicNumber < guess)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                }
            }

            Console.Write("Do you want to play again? (yes/no): ");
            response = Console.ReadLine();
        }

        Console.WriteLine("Thanks for playing. Goodbye!");
    }
}