#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc2022;

internal sealed class Day9 : Day
{
    private sealed class Point
    {
        public int X;
        public int Y;

        public override bool Equals(object obj)
        {
            if (obj is not Point point)
            {
                return false;
            }

            return point.X == X && point.Y == X;
        }

        public bool Near(Point point)
        {
            return Math.Abs(X - point.X) <= 1 && Math.Abs(Y - point.Y) <= 1;
        }

        public void MoveTowards(Point point)
        {
            int xMove = Math.Sign(point.X - X);
            int yMove = Math.Sign(point.Y - Y);
            X += xMove;
            Y += yMove;
        }

        public void Move(string direction)
        {
            switch (direction)
            {
                case "L":
                    X--;
                    break;
                case "R":
                    X++;
                    break;
                case "U":
                    Y++;
                    break;
                case "D":
                    Y--;
                    break;
            }
        }

        public override string ToString() => $"({X} {Y})";
    }

    private const string COMMAND_PATTERN = @"(\w) (\d+)";
    private const int NOTS_COUNT = 10;

    public override void CalculatePart1(string[] input)
    {
        var visited = new List<Point>();
        Point start = new Point();
        Point head = new Point();
        Point tail = new Point();
        var commandRx = new Regex(COMMAND_PATTERN);
        foreach (string line in input)
        {
            Match match = commandRx.Match(line);
            string? direction = match.Groups[1].Value;
            int numberOfSteps = int.Parse(match.Groups[2].Value);
            for (var i = 0; i < numberOfSteps; i++)
            {
                head.Move(direction);
                if (!tail.Near(head))
                {
                    tail.MoveTowards(head);
                    Console.WriteLine(tail.ToString());
                    if (!visited.Any(point => point.X == tail.X && point.Y == tail.Y))
                    {
                        visited.Add(new Point() { X = tail.X, Y = tail.Y });
                    }
                }
                else
                {
                    Console.WriteLine("near");
                }
            }
        }

        var result = visited.Count();
        if (!visited.Any(point => point.X == 0 && point.Y == 0))
        {
            result++;
        }

        Console.WriteLine(result);
    }

    public override void CalculatePart2(string[] input)
    {
        var rope = new List<Point>();
        var visited = new List<Point>();
        for (var i = 0; i < NOTS_COUNT; i++)
        {
            rope.Add(new Point());
        }

        var commandRx = new Regex(COMMAND_PATTERN);
        foreach (string line in input)
        {
            Match match = commandRx.Match(line);
            string? direction = match.Groups[1].Value;
            int numberOfSteps = int.Parse(match.Groups[2].Value);
            for (var i = 0; i < numberOfSteps; i++)
            {
                rope[0].Move(direction);
                for (var notIndex = 1; notIndex < NOTS_COUNT; notIndex++)
                {
                    if (!rope[notIndex].Near(rope[notIndex - 1]))
                    {
                        rope[notIndex].MoveTowards(rope[notIndex - 1]);
                        if(notIndex==NOTS_COUNT-1 && !visited.Any(point => point.X == rope[notIndex].X && point.Y == rope[notIndex].Y))
                        {
                            visited.Add(new Point() { X = rope[notIndex].X, Y = rope[notIndex].Y });
                        }
                    }
                }
            }
        }

        var result = visited.Count();
        if (!visited.Any(point => point.X == 0 && point.Y == 0))
        {
            result++;
        }

        Console.WriteLine(result);
    }
}