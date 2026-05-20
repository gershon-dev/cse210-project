// Journal.cs
// Manages the complete collection of journal entries and handles
// saving / loading them to / from a properly-formatted CSV file.

using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    // ── Member variables ─────────────────────────────────────────────────────
    private List<Entry> _entries;

    // ── Constructor ──────────────────────────────────────────────────────────
    public Journal()
    {
        _entries = new List<Entry>();
    }

    // ── Properties ───────────────────────────────────────────────────────────
    public int Count => _entries.Count;

    // ── Core operations ───────────────────────────────────────────────────────
    /// <summary>Adds a new entry to the journal.</summary>
    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    /// <summary>Displays every entry to the console, newest first.</summary>
    public void DisplayAll()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("  (No entries yet — start writing!)");
            return;
        }

        // EXCEEDS REQUIREMENTS: show entries newest-first
        for (int i = _entries.Count - 1; i >= 0; i--)
        {
            Console.WriteLine(new string('─', 60));
            Console.WriteLine(_entries[i]);
        }
        Console.WriteLine(new string('─', 60));
        Console.WriteLine($"  Total entries: {_entries.Count}");
    }

    // ── Persistence (CSV – EXCEEDS REQUIREMENTS) ──────────────────────────────
    /// <summary>
    /// Saves all entries to a CSV file with a proper header row.
    /// Fields containing commas or quotes are correctly escaped per RFC 4180.
    /// </summary>
    public void SaveToFile(string filename)
    {
        // Ensure .csv extension for Excel compatibility
        if (!filename.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            filename += ".csv";

        using StreamWriter writer = new StreamWriter(filename);
        // Write header
        writer.WriteLine("Date,Prompt,Response,Mood");

        foreach (Entry entry in _entries)
            writer.WriteLine(entry.ToCsvLine());

        Console.WriteLine($"  ✓ Saved {_entries.Count} entries to \"{filename}\"");
    }

    /// <summary>
    /// Loads entries from a CSV file, replacing any current entries.
    /// Skips the header row and any malformed lines gracefully.
    /// </summary>
    public void LoadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            // Also try appending .csv before giving up
            if (File.Exists(filename + ".csv"))
                filename += ".csv";
            else
            {
                Console.WriteLine($"  ✗ File not found: \"{filename}\"");
                return;
            }
        }

        string[] lines = File.ReadAllLines(filename);
        _entries.Clear();
        int loaded = 0, skipped = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            // Skip the header row
            if (i == 0 && line.StartsWith("Date,")) continue;

            Entry entry = Entry.FromCsvLine(line);
            if (entry != null)
            {
                _entries.Add(entry);
                loaded++;
            }
            else
            {
                skipped++;
            }
        }

        Console.WriteLine($"  ✓ Loaded {loaded} entries from \"{filename}\"" +
                          (skipped > 0 ? $" ({skipped} malformed lines skipped)" : ""));
    }

    // ── EXCEEDS REQUIREMENTS: search entries ──────────────────────────────────
    /// <summary>Prints every entry whose response or prompt contains the keyword.</summary>
    public void SearchEntries(string keyword)
    {
        keyword = keyword.ToLower();
        int count = 0;

        foreach (Entry entry in _entries)
        {
            if (entry.Response.ToLower().Contains(keyword) ||
                entry.Prompt.ToLower().Contains(keyword))
            {
                Console.WriteLine(new string('─', 60));
                Console.WriteLine(entry);
                count++;
            }
        }

        if (count == 0)
            Console.WriteLine($"  No entries found containing \"{keyword}\".");
        else
        {
            Console.WriteLine(new string('─', 60));
            Console.WriteLine($"  Found {count} matching entries.");
        }
    }
}
