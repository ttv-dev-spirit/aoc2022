﻿#nullable enable
using System.IO;

namespace aoc2022
{
    internal static class Program
    {
        private const string DATA_PATH = @"C:\StreamProjects\aoc2022\aoc2022\data";
        
        public static void Main(string[] args)
        {
            new Day3().CalculatePart2(GetInput("day3_input"));
        }

        private static string[] GetInput(string fileName)
        {
            return File.ReadAllLines($"{DATA_PATH}\\{fileName}");
        }
    }
}