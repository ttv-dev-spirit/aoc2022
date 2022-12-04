#nullable enable

using System;
using System.Text.RegularExpressions;

namespace aoc2022
{
    internal sealed class Day4 : Day
    {
        private readonly struct Pair
        {
            public readonly int Left;
            public readonly int Right;

            public Pair(int left, int right)
            {
                Left = left;
                Right = right;
            }
        }
        
        private const string PATTERN = @"(\d+)-(\d+),(\d+)-(\d+)";
        
        public override void CalculatePart1(string[] input)
        {
            var rx = new Regex(PATTERN);
            var result = 0;
            foreach (string line in input)
            {
                (Pair first, Pair second) = GetPairs(line, rx);
                if (PairsContainOneOther(ref first, ref second))
                {
                    result++;
                }
            }

            Console.WriteLine(result);
        }

        public override void CalculatePart2(string[] input)
        {
            var rx = new Regex(PATTERN);
            var result = 0;
            foreach (string line in input)
            {
                (Pair first, Pair second) = GetPairs(line, rx);
                if (PairsOverlap(ref first, ref second))
                {
                    result++;
                }
            }

            Console.WriteLine(result);
        }

        private (Pair, Pair) GetPairs(string line, Regex rx)
        {
            Match match = rx.Match(line);
            int firstLeft = int.Parse(match.Groups[1].Value);
            int firstRight = int.Parse(match.Groups[2].Value);
            int secondLeft = int.Parse(match.Groups[3].Value);
            int secondRight = int.Parse(match.Groups[4].Value);
            return (new Pair(firstLeft, firstRight), new Pair(secondLeft, secondRight));
        }

        private bool PairsContainOneOther(ref Pair first, ref Pair second)
        {
            return (first.Left <= second.Left && first.Right >= second.Right) ||
                   (second.Left <= first.Left && second.Right >= first.Right);
        }

        private bool PairsOverlap(ref Pair first, ref Pair second)
        {
            if (PairsContainOneOther(ref first, ref second))
            {
                return true;
            }

            return (first.Left <= second.Left && first.Right >= second.Left) ||
                   (first.Left <= second.Right && first.Right >= second.Right);
        }
    }
}