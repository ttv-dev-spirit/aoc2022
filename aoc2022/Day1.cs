#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc2022
{
    internal sealed class Day1 : Day
    {
        public override void CalculatePart1(string[] input)
        {
            var sums = CalculateCaloriesSums(input);
            Console.WriteLine(sums.Max());
        }

        public override void CalculatePart2(string[] input)
        {
            var sums = CalculateCaloriesSums(input);
            var result = sums.OrderByDescending(x => x).Take(3).Sum();
            Console.WriteLine(result);
        }

        private List<int> CalculateCaloriesSums(string[] input)
        {
            var result = new List<int>{0};
            var index = 0;
            foreach (string line in input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    result.Add(0);
                    index++;
                    continue;
                }

                int value = int.Parse(line);
                result[index] += value;
            }

            return result;
        }
    }
}