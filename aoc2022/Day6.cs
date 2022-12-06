#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2022;

internal sealed class Day6 : Day
{
    private const int NUMBER_OF_DISTINCT_CHARS_IN_PACKET_START = 4;
    private const int NUMBER_OF_DISTINCT_CHARS_IN_MESSAGE_START = 14;
    
    public override void CalculatePart1(string[] input)
    {
        foreach (string line in input)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            int result = FindFirstPositionWithDistinctChars(line, NUMBER_OF_DISTINCT_CHARS_IN_PACKET_START);
            Console.WriteLine(result);
        }
    }

    public override void CalculatePart2(string[] input)
    {
        foreach (string line in input)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            int result = FindFirstPositionWithDistinctChars(line, NUMBER_OF_DISTINCT_CHARS_IN_MESSAGE_START);
            Console.WriteLine(result);
        }
    }

    private int FindFirstPositionWithDistinctChars(string line, int numberOfDistinctChars)
    {
        var buffer = line.Substring(0, numberOfDistinctChars).ToList();
        for (var i = numberOfDistinctChars; i < line.Length; i++)
        {
            if (IsDistinctChars(buffer))
            {
                return i;
            }

            buffer.RemoveAt(0);
            buffer.Add(line[i]);
        }

        return IsDistinctChars(buffer) ? line.Length : -1;
    }

    private bool IsDistinctChars(IReadOnlyCollection<char> chars) => chars.Distinct().Count() == chars.Count;
}