#nullable enable
using System;
using System.Text;

namespace aoc2022
{
    internal sealed class Day8 : Day
    {
        public override void CalculatePart1(string[] input)
        {
            int width = input[0].Length;
            int height = input.Length;
            var visionMap = new bool[width, height];
            var heightMap = new int[width, height];
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    int treeHeight = int.Parse(input[y].Substring(x, 1));
                    heightMap[x, y] = treeHeight;
                }
            }

            var result = 0;
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    if (VisibleDown(heightMap, width, height, x, y) ||
                        VisibleTop(heightMap, width, height, x, y) ||
                        VisibleLeft(heightMap, width, height, x, y) ||
                        VisibleRight(heightMap, width, height, x, y)
                       )
                    {
                        result++;
                        visionMap[x, y] = true;
                    }
                }
            }

            Console.WriteLine(result);
            // Console.WriteLine();
            // PrintHeightMap(heightMap, width, height);
            Console.WriteLine();
            PrintVisionMap(visionMap, width, height);
        }

        public override void CalculatePart2(string[] input)
        {
            int width = input[0].Length;
            int height = input.Length;
            var heightMap = new int[width, height];
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    int treeHeight = int.Parse(input[y].Substring(x, 1));
                    heightMap[x, y] = treeHeight;
                }
            }

            long result = 0;
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var down = TreesVisibleDown(heightMap, width, height, x, y);
                    var left = TreesVisibleLeft(heightMap, width, height, x, y);
                    var right = TreesVisibleRight(heightMap, width, height, x, y);
                    var top = TreesVisibleTop(heightMap, width, height, x, y);
                    long score = top * down * left * right;
                    if (score > result)
                    {
                        result = score;
                    }
                }
            }

            Console.WriteLine(result);
        }

        private bool VisibleLeft(int[,] heightMap, int width, int height, int x, int y)
        {
            if (x == 0)
            {
                return true;
            }

            for (var i = x - 1; i >= 0; i--)
            {
                if (heightMap[x, y] <= heightMap[i, y])
                {
                    return false;
                }
            }

            return true;
        }

        private int TreesVisibleLeft(int[,] heightMap, int width, int height, int x, int y)
        {
            if (x == 0)
            {
                return 0;
            }

            var result = 0;

            for (var i = x - 1; i >= 0; i--)
            {
                result++;
                if (heightMap[x, y] <= heightMap[i, y])
                {
                    return result;
                }
            }

            return result;
        }

        private bool VisibleRight(int[,] heightMap, int width, int height, int x, int y)
        {
            if (x == width - 1)
            {
                return true;
            }

            for (var i = x + 1; i < width; i++)
            {
                if (heightMap[x, y] <= heightMap[i, y])
                {
                    return false;
                }
            }

            return true;
        }

        private bool VisibleTop(int[,] heightMap, int width, int height, int x, int y)
        {
            if (y == height - 1)
            {
                return true;
            }

            for (var i = y + 1; i < height; i++)
            {
                if (heightMap[x, y] <= heightMap[x, i])
                {
                    return false;
                }
            }

            return true;
        }

        private bool VisibleDown(int[,] heightMap, int width, int height, int x, int y)
        {
            if (y == 0)
            {
                return true;
            }

            for (var i = y - 1; i >= 0; i--)
            {
                if (heightMap[x, y] <= heightMap[x, i])
                {
                    return false;
                }
            }

            return true;
        }

        private int TreesVisibleRight(int[,] heightMap, int width, int height, int x, int y)
        {
            if (x == width - 1)
            {
                return 0;
            }

            var result = 0;

            for (var i = x + 1; i < width; i++)
            {
                result++;
                if (heightMap[x, y] <= heightMap[i, y])
                {
                    return result;
                }
            }

            return result;
        }

        private int TreesVisibleTop(int[,] heightMap, int width, int height, int x, int y)
        {
            if (y == height - 1)
            {
                return 0;
            }

            var result = 0;

            for (var i = y + 1; i < height; i++)
            {
                result++;
                if (heightMap[x, y] <= heightMap[x, i])
                {
                    return result;
                }
            }

            return result;
        }

        private int TreesVisibleDown(int[,] heightMap, int width, int height, int x, int y)
        {
            if (y == 0)
            {
                return 0;
            }

            var result = 0;

            for (var i = y - 1; i >= 0; i--)
            {
                result++;
                if (heightMap[x, y] <= heightMap[x, i])
                {
                    return result;
                }
            }

            return result;
        }

        private void PrintVisionMap(bool[,] visionMap, int width, int height)
        {
            var sb = new StringBuilder();
            for (var y = 0; y < height; y++)
            {
                sb.Clear();
                for (var x = 0; x < width; x++)
                {
                    sb.Append(visionMap[x, y] ? "1" : "0");
                }

                Console.WriteLine(sb);
            }
        }

        private void PrintHeightMap(int[,] heightMap, int width, int height)
        {
            var sb = new StringBuilder();
            for (var y = 0; y < height; y++)
            {
                sb.Clear();
                for (var x = 0; x < width; x++)
                {
                    sb.Append(heightMap[x, y]);
                }

                Console.WriteLine(sb);
            }
        }
    }
}