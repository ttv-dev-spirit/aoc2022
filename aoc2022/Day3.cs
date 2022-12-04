#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2022
{
    internal sealed class Day3 : Day
    {
        private const int NUMBER_OF_ELVES_IN_GROUP = 3;
        
        public override void CalculatePart1(string[] input)
        {
            var result = 0;
            foreach (string line in input)
            {
                (char[] first, char[] second) = GetCompartments(line);
                var c = FindCommonChar(first, second);
                result += GetCharCost(c);
            }

            Console.WriteLine(result);
        }

        public override void CalculatePart2(string[] input)
        {
            int numberOfGroups = input.Length / NUMBER_OF_ELVES_IN_GROUP;
            var result = 0;
            for (var i = 0; i < numberOfGroups; i++)
            {
                var distinctChars = DistinctChars(input[i * NUMBER_OF_ELVES_IN_GROUP]);
                distinctChars = distinctChars.Intersect(DistinctChars(input[i * NUMBER_OF_ELVES_IN_GROUP + 1])).ToList();
                distinctChars = distinctChars.Intersect(DistinctChars(input[i * NUMBER_OF_ELVES_IN_GROUP + 2])).ToList();
                if (distinctChars.Count != 1)
                {
                    Console.WriteLine($"Wrong result chars {string.Join("",distinctChars)}");
                    return;
                }
                result += GetCharCost(distinctChars[0]);
            }
            Console.WriteLine(result);
        }

        private (char[], char[]) GetCompartments(string line)
        {
            int compartmentSize = line.Length / 2;
            var first = new char[compartmentSize];
            var second = new char[compartmentSize];
            for (var i = 0; i < compartmentSize; i++)
            {
                first[i] = line[i];
                second[i] = line[i + compartmentSize];
            }

            return (first, second);
        }

        private char FindCommonChar(char[] first, char[] second)
        {
            foreach (char c in first)
            {
                if (second.Contains(c))
                {
                    return c;
                }
            }

            throw new Exception($"No commons chars in {string.Join("", first)} and {string.Join("", second)}");
        }

        private int GetCharCost(char c)
        {
            if (c >= 'a' && c <= 'z')
            {
                return c - 'a' + 1;
            }

            return c - 'A' + 27;
        }

        private List<char> DistinctChars(string line)
        {
            return line.Distinct().ToList();
        }
    }
}