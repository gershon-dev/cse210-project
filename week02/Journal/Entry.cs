// Entry.cs
// Represents a single journal entry with a date, prompt, and the user's response.

using System;

public class Entry
{
    // ── Member variables ─────────────────────────────────────────────────────
    private string _date;
    private string _prompt;
    private string _response;
    private string _mood;   // EXCEEDS REQUIREMENTS: also tracks the user's mood (1-5 scale)

    // ── Constructor ──────────────────────────────────────────────────────────
    public Entry(string date, string prompt, string response, string mood = "")
    {
        _date     = date;
        _prompt   = prompt;
        _response = response;
        _mood     = mood;
    }

    // ── Properties ───────────────────────────────────────────────────────────
    public string Date     => _date;
    public string Prompt   => _prompt;
    public string Response => _response;
    public string Mood     => _mood;

    // ── Display ──────────────────────────────────────────────────────────────
    /// <summary>Returns a formatted multi-line string for console display.</summary>
    public override string ToString()
    {
        string moodLine = string.IsNullOrEmpty(_mood)
            ? ""
            : $"\n  Mood: {MoodLabel(_mood)}";

        return $"Date: {_date}{moodLine}\n" +
               $"  Prompt: {_prompt}\n" +
               $"  Response: {_response}";
    }

    // ── CSV serialisation (EXCEEDS REQUIREMENTS) ─────────────────────────────
    /// <summary>
    /// Serialises this entry to a single CSV line, correctly escaping any
    /// commas or double-quotes that appear inside a field value.
    /// </summary>
    public string ToCsvLine()
    {
        return $"{CsvEscape(_date)},{CsvEscape(_prompt)},{CsvEscape(_response)},{CsvEscape(_mood)}";
    }

    /// <summary>
    /// Reconstructs an Entry from a CSV line produced by ToCsvLine().
    /// Returns null if the line cannot be parsed.
    /// </summary>
    public static Entry FromCsvLine(string line)
    {
        string[] fields = CsvSplit(line);
        if (fields.Length < 3) return null;
        string mood = fields.Length >= 4 ? fields[3] : "";
        return new Entry(fields[0], fields[1], fields[2], mood);
    }

    // Private helpers 
    private static string CsvEscape(string value)
    {
        // RFC 4180: if the value contains a comma, double-quote, or newline,
        // wrap it in double-quotes and escape any existing double-quotes by doubling them.
        if (value.Contains(',') || value.Contains('"') || value.Contains('\n'))
            return $"\"{value.Replace("\"", "\"\"")}\"";
        return value;
    }

    /// Splits one CSV line respecting quoted fields
    private static string[] CsvSplit(string line)
    {
        var fields = new System.Collections.Generic.List<string>();
        bool inQuotes = false;
        var current  = new System.Text.StringBuilder();

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];
            if (inQuotes)
            {
                if (c == '"')
                {
                    // Peek at next character to detect escaped double-quote ("")
                    if (i + 1 < line.Length && line[i + 1] == '"')
                    {
                        current.Append('"');
                        i++; // skip the second quote
                    }
                    else
                    {
                        inQuotes = false;
                    }
                }
                else
                {
                    current.Append(c);
                }
            }
            else
            {
                if (c == '"')
                {
                    inQuotes = true;
                }
                else if (c == ',')
                {
                    fields.Add(current.ToString());
                    current.Clear();
                }
                else
                {
                    current.Append(c);
                }
            }
        }
        fields.Add(current.ToString());
        return fields.ToArray();
    }

    private static string MoodLabel(string mood)
    {
        return mood switch
        {
            "1" => "😞 1 – Rough day",
            "2" => "😕 2 – Below average",
            "3" => "😐 3 – Okay",
            "4" => "🙂 4 – Good",
            "5" => "😄 5 – Great day!",
            _   => mood
        };
    }
}
