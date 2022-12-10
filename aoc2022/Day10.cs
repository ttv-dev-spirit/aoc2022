#nullable enable
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace aoc2022
{
    internal sealed class Day10 : Day
    {
        private abstract class Command
        {
            protected readonly Regex _rx;

            public int Delay { get; protected set; }

            protected Command(string pattern, int delay)
            {
                _rx = new Regex(pattern);
                Delay = delay;
            }

            public bool IsMatch(string line) => _rx.IsMatch(line);
            public abstract void Execute(string line, ref int register, ref int cycles, ref long result);

            public abstract void ExecuteAndDraw(string line, ref int register, ref int cycles, Day10 day10);
        }

        private sealed class AddX : Command
        {
            private const string PATTERN = @"addx (-*\d+)";
            private const int DELAY = 2;

            public AddX() : base(PATTERN, DELAY)
            {
            }

            public override void Execute(string line, ref int register, ref int cycles, ref long result)
            {
                int value = int.Parse(_rx.Match(line).Groups[1].Value);
                for (var i = 0; i < Delay; i++)
                {
                    cycles++;
                    if (i == DELAY - 1)
                    {
                        // register += value;
                    }

                    // Console.WriteLine($"Addx {value}, step {i}, Cycle {cycles}, Register {register}, Result {register*cycles}");
                    if (cycles-STEP_SHIFT >= 0 && (cycles-STEP_SHIFT) % STEP == 0)
                    {
                        Console.WriteLine($"Cycle {cycles}, Register {register}, Result {register*cycles}");
                        result += register * cycles;
                    }
                }

                register += value;
            }
            public override void ExecuteAndDraw(string line, ref int register, ref int cycles, Day10 day10)
            {
                int value = int.Parse(_rx.Match(line).Groups[1].Value);
                for (var i = 0; i < Delay; i++)
                {
                    day10.DrawPixelOnMap(cycles, register);
                    cycles++;
                }

                register += value;
            }
        }

        private sealed class Noop : Command
        {
            private const string PATTERN = @"noop";
            private const int DELAY = 1;

            public Noop() : base(PATTERN, DELAY)
            {
            }

            public override void Execute(string line, ref int register, ref int cycles, ref long result)
            {
                for (var i = 0; i < Delay; i++)
                {
                    cycles++;

                    // Console.WriteLine($"Noop step {i}Cycle {cycles}, Register {register}, Result {register*cycles}");
                    if (cycles-STEP_SHIFT >= 0 && (cycles-STEP_SHIFT) % STEP == 0)
                    {
                        Console.WriteLine($"Cycle {cycles}, Result {register*cycles}");
                        result += register * cycles;
                    }
                }
            }
            
            public override void ExecuteAndDraw(string line, ref int register, ref int cycles, Day10 day10)
            {
                for (var i = 0; i < Delay; i++)
                {
                    day10.DrawPixelOnMap(cycles, register);
                    cycles++;
                }
            }
        }

        private const int STEP = 40;
        private const int STEP_SHIFT = 20;
        private const int SPRITE_RADIUS = 1;
        private const int WIDTH = 40;
        private const int HEIGHT = 6;

        private bool[,] _map = new bool[WIDTH, HEIGHT]; 

        public override void CalculatePart1(string[] input)
        {
            var commands = new List<Command>()
            {
                new Noop(),
                new AddX()
            };

            var register = 1;
            var cycles = 0;
            long result = 0;
            foreach (string line in input)
            {
                foreach (Command command in commands)
                {
                    if (!command.IsMatch(line))
                    {
                        continue;
                    }

                    command.Execute(line, ref register, ref cycles, ref result);
                }
            }

            Console.WriteLine(result);
        }

        public override void CalculatePart2(string[] input)
        {
            var commands = new List<Command>()
            {
                new Noop(),
                new AddX()
            };

            var register = 1;
            var cycles = 0;
            long result = 0;
            foreach (string line in input)
            {
                foreach (Command command in commands)
                {
                    if (!command.IsMatch(line))
                    {
                        continue;
                    }

                    command.ExecuteAndDraw(line, ref register, ref cycles, this);
                }
            }
            PrintMap();
        }


        private bool DrawPixel(int x, int register)
        {
            return Math.Abs(x-register)<=SPRITE_RADIUS;
        }

        public void DrawPixelOnMap(int cycle, int register)
        {
            int c = cycle % (WIDTH * HEIGHT);
            int y = c/ (WIDTH);
            int x = c % WIDTH;
            if (DrawPixel(x, register))
            {
                _map[x, y] = true;
            }
        }

        private void PrintMap()
        {
            var sb = new StringBuilder();
            for (var y = 0; y < HEIGHT; y++)
            {
                sb.Clear();
                for (var x = 0; x < WIDTH; x++)
                {
                    sb.Append(_map[x, y] ? "#" : ".");
                }
                Console.WriteLine(sb);
            }
        }
    }
}