using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your grade percentage? ");
        int grade = int.Parse(Console.ReadLine());

        // Step 1: Determine letter grade
        string letter;

        if (grade >= 90)
            letter = "A";
        else if (grade >= 80)
            letter = "B";
        else if (grade >= 70)
            letter = "C";
        else if (grade >= 60)
            letter = "D";
        else
            letter = "F";

        // Step 2: Determine sign using last digit
        int lastDigit = grade % 10;
        string sign;

        if (lastDigit >= 7)
            sign = "+";
        else if (lastDigit < 3)
            sign = "-";
        else
            sign = "";

        // Step 3: Fix special cases
        if (letter == "A" && sign == "+")
            sign = "";   // No A+, only A or A-

        if (letter == "F")
            sign = "";   

        // Step 4: Print grade and sign together
        Console.WriteLine($"Your grade is: {letter}{sign}");

        // Step 5: Pass or fail message
        if (grade >= 70)
            Console.WriteLine("You passed! Congratulations!");
        else
            Console.WriteLine("You did not pass, keep working hard for next time!");
    }
}