using UnityEngine;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using Require;

public class HasAMailbox : MonoBehaviour
{
    List<string> letters = new List<String>();

    public void Send(string letter)
    {
        letters.Add(Regex.Replace(letter, @"[^a-zA-Z0-9.-]+", " ").ToLower());
    }

    /// <summary>
    /// Checks your mail.
    /// </summary>
    /// <param name="pattern">Letter to look for. Case and whitespace insensitive. Use __ as a fill in the blank for floats.</param>
    /// <param name="handler">Function to call if a letter is found.</param>
    /// <returns></returns>
    public bool On(string pattern, Action<float[]> handler)
    {
        pattern = Regex.Replace(pattern, @"[^a-zA-Z0-9.-_]+", " ").ToLower();

        string found = null;

        foreach (string letter in letters)
        {
            if (Regex.Replace(letter, @"-?[0-9]+(?:\.[0-9]+)?", "__") == pattern)
            {
                List<float> args = new List<float>();

                foreach (Match match in Regex.Matches(Regex.Replace(letter, " ", "  "), @"(?:^| )(-?[0-9]+(?:\.[0-9]+)?)(?:$| )"))
                {
                    string value = match.Value.Trim();
                    args.Add(float.Parse(value));
                }

                handler(args.ToArray());

                found = letter;
                break;
            }
        }

        if (found != null)
        {
            letters.Remove(found);
            return true;
        }

        return false;
    }
}
