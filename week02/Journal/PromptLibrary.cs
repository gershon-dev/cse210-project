// PromptLibrary.cs
// Manages a curated list of reflective journaling prompts and
// picks one at random, guaranteeing the same prompt is never
// repeated twice in a row (EXCEEDS REQUIREMENTS).

using System;
using System.Collections.Generic;

public class PromptLibrary
{
    // ── Member variables ─────────────────────────────────────────────────────
    private List<string> _prompts;
    private Random       _random;
    private int          _lastIndex;   // avoids repeating the same prompt twice in a row

    // ── Constructor ──────────────────────────────────────────────────────────
    public PromptLibrary()
    {
        _random    = new Random();
        _lastIndex = -1;

        // At least 5 required + several extras for variety
        _prompts = new List<string>
        {
            // Required-style prompts
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?",
            "How did I see the hand of God in my life today?",

            // Original prompts (added to exceed requirements)
            "What is one thing I learned today that I didn't know yesterday?",
            "What small act of kindness did I witness or perform today?",
            "What song, quote, or idea kept running through my mind today?",
            "What is something I'm grateful for that I often take for granted?",
            "What challenged me today, and how did I handle it?",
            "Who made me laugh today, and what was the moment?",
            "What do I wish I had said — or not said — today?",
            "What goal feels closer today than it did yesterday?",
            "If today had a title like a book chapter, what would it be?",
            "What would I tell my future self about today?"
        };
    }

    // ── Public methods ────────────────────────────────────────────────────────
    /// <summary>
    /// Returns a random prompt, guaranteed to differ from the last one shown.
    /// </summary>
    public string GetRandomPrompt()
    {
        if (_prompts.Count == 1) return _prompts[0];

        int index;
        do
        {
            index = _random.Next(_prompts.Count);
        } while (index == _lastIndex);

        _lastIndex = index;
        return _prompts[index];
    }

    /// <summary>Returns the total number of prompts in the library.</summary>
    public int PromptCount => _prompts.Count;
}
