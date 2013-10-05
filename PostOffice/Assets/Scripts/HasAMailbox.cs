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
        letters.Add(letter);
    }

    public bool On(string pattern, Action<string[]> handler)
    {
        foreach (string letter in letters)
        {
            MatchCollection matches = Regex.Matches(letter, pattern);

            if (matches.Count > 0)
            {
                if (matches[0].Value == letter)
                {
                    List<string> groups = new List<string>();

                    foreach (Group group in matches[0].Groups)
                    {
                        groups.Add(group.Value);
                    }

                    groups.RemoveAt(0);

                    handler(groups.ToArray());

                    return true;
                }
            }
        }

        return false;
    }
}
