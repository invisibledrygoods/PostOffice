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
        letters.Add(Regex.Replace(letter, @"\s+", " ").ToLower());
    }

    /// <summary>
    /// Checks your mail.
    /// </summary>
    /// <param name="pattern">Letter to look for. Case and whitespace insensitive. Use __ as a fill in the blank for floats.</param>
    /// <param name="handler">Function to call if a letter is found.</param>
    /// <returns></returns>
    public bool On(string pattern, Action<float[]> handler)
    {
        pattern = Regex.Replace(pattern, @"\s+", " ").ToLower();

        foreach (string letter in letters)
        {
            MatchCollection matches = Regex.Matches(letter, pattern.Replace("__", @"-?[0-9]+(?:\.[0-9]+)?"));

            if (matches.Count > 0)
            {
                if (matches[0].Value == letter)
                {
                    List<float> args = new List<float>();

                    foreach (Match match in Regex.Matches(Regex.Replace(matches[0].Value, " ", "  "), @"(?:^| )(-?[0-9]+(?:\.[0-9]+)?)(?:$| )"))
                    {
                        string value = match.Value.Trim();
                        args.Add(float.Parse(value));
                    }

                    handler(args.ToArray());

                    return true;
                }
            }
        }

        return false;
    }
}
