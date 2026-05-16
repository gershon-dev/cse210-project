using System;
using System.Data.SqlTypes;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("what is the percentage of grade or score? ");
        string letter = Console.ReadLine();
        int grade = int.Parse(letter);

        


        if(grade >= 90)
            letter = "A";    
        else if(grade >= 80)
            letter = "B";
        else if(grade >= 70)
            letter = "C";
        else if(grade >= 60)
            letter = "D";
        else if(grade < 70)
            letter = "F";

        
        int lastDigit = grade % 10;
        string sign;
        
        if(lastDigit >= 7)
            sign = "+";
        if(lastDigit < 3)
            sign = "-";
        else
            sign ="";
        
        if (letter == "A" && sign == "+")
            sign = "";   // No A+, only A or A-

        if (letter == "F")
            sign = "";   // No F+ or F-, only F

        
        {
           Console.WriteLine($"Your grade is: {letter}{sign}");
        }

            if(grade >= 70)
            {
                Console.Write("You have pass! congratulation");
            }
            else if(grade < 70)
            {
                
                Console.WriteLine("You failed, please kindly retake the course again and you can make it");
            }
        else 
        {
            Console.WriteLine("please kindly check and enter the percentage of your grade");
        }
   
    }
}