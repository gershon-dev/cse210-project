// Program.cs
//
// BYU CSE 210 – Prove: Developer – Journal Program
//
//  EXCEEDS REQUIREMENTS – summary of enhancements 
// 1. CSV storage (RFC 4180 compliant): fields that contain commas or
//    double-quotes are correctly escaped, so the file opens cleanly in Excel.
// 2. Mood tracking: each entry also records the user's self-rated mood (1-5)
//    so they can spot patterns over time.
// 3. No-repeat prompts: PromptLibrary guarantees the same prompt is never
//    shown twice in a row.
// 4. Reverse-chronological display: newest entries appear at the top so the
//    most recent reflection is always seen first.
// 5. Keyword search: option 5 lets the user search all entries by keyword,
//    which addresses the common problem of forgetting what you wrote.
// 6. Auto .csv extension: SaveToFile appends ".csv" if not already present,
//    and LoadFromFile will try both with and without the extension.
// 7. Graceful error handling: bad filenames, missing files, and empty journals
//    are handled with clear messages rather than crashes.
// ─────────────────────────────────────────────────────────────────────────

using System;

class Program
{
    static void Main(string[] args)
    {
        Journal       journal = new Journal();
        PromptLibrary library = new PromptLibrary();

        Console.WriteLine("Welcome to the Journal Program");
        

        bool running = true;
        while (running)
        {
            PrintMenu();
            string choice = (Console.ReadLine() ?? "").Trim();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    WriteNewEntry(journal, library);
                    break;
                case "2":
                    DisplayJournal(journal);
                    break;
                case "3":
                    SaveJournal(journal);
                    break;
                case "4":
                    LoadJournal(journal);
                    break;
                case "5":
                    SearchJournal(journal);
                    break;
                case "6":
                    running = false;
                    Console.WriteLine("Keep reflecting — goodbye! 👋");
                    break;
                default:
                    Console.WriteLine("  Please enter a number from 1 to 6.");
                    break;
            }

            if (running) Console.WriteLine();
        }
    }

    //  Menu 
    static void PrintMenu()
    {
        Console.WriteLine("Please select one of the following choices:");
        Console.WriteLine("  1 - Write");
        Console.WriteLine("  2 - Display");
        Console.WriteLine("  3 - Save");
        Console.WriteLine("  4 - Load");
        Console.WriteLine("  5 - Search entries by keyword");   // EXCEEDS
        Console.WriteLine("  6 - Quit");
        Console.Write("> ");
    }

    //Option 1: Write a new entry 
    static void WriteNewEntry(Journal journal, PromptLibrary library)
    {
        string date   = DateTime.Now.ToString("yyyy-MM-dd");
        string prompt = library.GetRandomPrompt();

        Console.WriteLine($"Date: {date}");
        Console.WriteLine($"Prompt: {prompt}");
        Console.Write("> ");
        string response = (Console.ReadLine() ?? "").Trim();

        if (string.IsNullOrEmpty(response))
        {
            Console.WriteLine("  (Entry skipped — no response entered.)");
            return;
        }

        // EXCEEDS REQUIREMENTS: ask for mood rating
        string mood = AskMood();

        journal.AddEntry(new Entry(date, prompt, response, mood));
        Console.WriteLine("  ✓ Entry saved.");
    }

    /// Asks the user for a 1-5 mood rating; returns empty string if skipped.
    static string AskMood()
    {
        Console.Write("How would you rate today's mood? (1 = rough, 5 = great, Enter to skip): ");
        string input = (Console.ReadLine() ?? "").Trim();
        if (input == "1" || input == "2" || input == "3" || input == "4" || input == "5")
            return input;
        return "";
    }

    //Option 2: Display journal 
    static void DisplayJournal(Journal journal)
    {
        Console.WriteLine("Journal Entries");
        journal.DisplayAll();
    }

    // Option 3: Save to file
    static void SaveJournal(Journal journal)
    {
        Console.Write("What is the filename? ");
        string filename = (Console.ReadLine() ?? "").Trim();
        if (string.IsNullOrEmpty(filename))
        {
            Console.WriteLine("  (Save cancelled — no filename entered.)");
            return;
        }
        journal.SaveToFile(filename);
    }

    //Option 4: Load from file 
    static void LoadJournal(Journal journal)
    {
        Console.Write("What is the filename? ");
        string filename = (Console.ReadLine() ?? "").Trim();
        if (string.IsNullOrEmpty(filename))
        {
            Console.WriteLine("  (Load cancelled — no filename entered.)");
            return;
        }
        journal.LoadFromFile(filename);
    }

    //Option 5: Search entries (EXCEEDS REQUIREMENTS)
    static void SearchJournal(Journal journal)
    {
        Console.Write("Enter a keyword to search: ");
        string keyword = (Console.ReadLine() ?? "").Trim();
        if (string.IsNullOrEmpty(keyword))
        {
            Console.WriteLine("  (Search cancelled — no keyword entered.)");
            return;
        }
        journal.SearchEntries(keyword);
    }
}