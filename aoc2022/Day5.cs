#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc2022
{
    internal sealed class Day5 : Day
    {
        private readonly struct Command
        {
            public readonly int Quantity;
            public readonly int From;
            public readonly int To;

            public Command(int quantity, int from, int to)
            {
                Quantity = quantity;
                From = from;
                To = to;
            }
        }

        private const string CRATE_PATTERN = @"[(\w)]";
        private const string COMMAND_PATTERN = @"move (\d+) from (\d+) to (\d+)";

        private readonly List<List<char>> _storageMap = new();

        public override void CalculatePart1(string[] input)
        {
            CalculateCrates(input, ExecuteCommandOneByOne);
        }

        public override void CalculatePart2(string[] input)
        {
            CalculateCrates(input, ExecuteCommandAllAtOnce);
        }

        private void CalculateCrates(string[] input, Action<Command> commandExecuter)
        {
            var crateLines = new List<string>();
            var commands = new List<Command>();
            var isCommands = false;
            var commandRx = new Regex(COMMAND_PATTERN);
            var crateRx = new Regex(CRATE_PATTERN);
            for (var index = 0; index < input.Length; index++)
            {
                string line = input[index];
                if (isCommands)
                {
                    commands.Add(ParseCommand(line, commandRx));
                    continue;
                }

                if (int.TryParse(line.Substring(1, 1), out _))
                {
                    isCommands = true;
                    index++;
                    continue;
                }

                crateLines.Insert(0, line);
            }

            foreach (string crateLine in crateLines)
            {
                ParseCratesLine(crateLine, crateRx);
            }

            foreach (Command command in commands)
            {
                commandExecuter(command);
            }

            PrintTopCrates();
        }

        private void PrintTopCrates()
        {
            var result = new List<char>();
            foreach (List<char> crates in _storageMap)
            {
                if (crates.Count == 0)
                {
                    result.Add(' ');
                }
                else
                {
                    result.Add(crates.Last());
                }
            }

            Console.WriteLine(string.Join("", result));
        }

        private void ParseCratesLine(string line, Regex rx)
        {
            MatchCollection matches = rx.Matches(line);
            foreach (Match match in matches)
            {
                int index = (match.Index - 1) / 4;
                char crate = match.Value[0];
                if (_storageMap.Count < index + 1)
                {
                    for (int i = _storageMap.Count; i < index + 1; i++)
                    {
                        _storageMap.Add(new List<char>());
                    }
                }

                _storageMap[index].Add(crate);
            }
        }

        private Command ParseCommand(string line, Regex rx)
        {
            Match match = rx.Match(line);
            int quantity = int.Parse(match.Groups[1].Value);
            int from = int.Parse(match.Groups[2].Value) - 1;
            int to = int.Parse(match.Groups[3].Value) - 1;
            return new Command(quantity, from, to);
        }

        private void ExecuteCommandOneByOne(Command command)
        {
            for (var i = 0; i < command.Quantity; i++)
            {
                int lastIndex = _storageMap[command.From].Count - 1;
                char crate = _storageMap[command.From][lastIndex];
                _storageMap[command.From].RemoveAt(lastIndex);
                _storageMap[command.To].Add(crate);
            }
        }

        private void ExecuteCommandAllAtOnce(Command command)
        {
            int firstCrateIndex = _storageMap[command.From].Count - command.Quantity;
            var movedCrates = _storageMap[command.From].GetRange(firstCrateIndex, command.Quantity);
            _storageMap[command.From].RemoveRange(firstCrateIndex, command.Quantity);
            _storageMap[command.To].AddRange(movedCrates);
        }
    }
}