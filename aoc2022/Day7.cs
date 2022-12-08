#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace aoc2022
{
    internal sealed class Day7 : Day
    {
        private readonly struct File
        {
            public readonly string FileName;
            public readonly long Size;

            public File(string fileName, long size)
            {
                FileName = fileName;
                Size = size;
            }
        }

        private class Node
        {
            public readonly Node? ParentNode;
            public readonly string Name;
            public readonly List<Node> ChildNodes = new();
            public readonly List<File> Files = new();
            public long Size;

            public Node(Node? parentNode, string name)
            {
                ParentNode = parentNode;
                Name = name;
            }
        }

        private const string TRAVERSE_PATTERN = @"\$ cd (\w+)";
        private const string UP_COMMAND = @"$ cd ..";
        private const string LIST_COMMAND = @"$ ls";

        private const int THRESHOLD = 100000;
        private const long MAX_SPACE = 70000000;
        private const long SPACE_NEEDED = 30000000;

        public override void CalculatePart1(string[] input)
        {
            var rootNode = ParseTree(input);
            CalculateSize(rootNode);
            long result=0;
            CalculateSum(rootNode, ref result);
            Console.WriteLine(result);
        }

        public override void CalculatePart2(string[] input)
        {
            var rootNode = ParseTree(input);
            CalculateSize(rootNode);
            long spaceUsed = rootNode.Size;
            long spaceLeft = MAX_SPACE - spaceUsed;
            long spaceToClear = SPACE_NEEDED - spaceLeft;
            var potentialNodes = new List<Node>();
            GetPotentialNodes(rootNode, ref potentialNodes, spaceToClear);
            long size = potentialNodes.OrderBy(node => node.Size).First().Size;
            Console.WriteLine(size);
        }

        private void GetPotentialNodes(Node node, ref List<Node> potentialNodes, long spaceToClear)
        {
            if (node.Size >= spaceToClear)
            {
                potentialNodes.Add(node);
            }
            foreach (Node nodeChildNode in node.ChildNodes)
            {
                GetPotentialNodes(nodeChildNode, ref potentialNodes, spaceToClear);
            }
        }

        private Node ParseTree(string[] input)
        {
            var rootNode = new Node(null, "/");
            Node? currentNode = rootNode;
            var traverseRx = new Regex(TRAVERSE_PATTERN);
            for (var i = 1; i < input.Length; i++)
            {
                if (string.Equals(input[i], UP_COMMAND))
                {
                    currentNode = currentNode.ParentNode;
                    continue;
                }

                if (traverseRx.IsMatch(input[i]))
                {
                    Match match = traverseRx.Match(input[i]);
                    string? name = match.Groups[1].Value;
                    Node? node = currentNode.ChildNodes.First(node => string.Equals(name, node.Name));
                    currentNode = node;
                    continue;
                }

                if (string.Equals(LIST_COMMAND, input[i]))
                {
                    i++;
                    int nextLine = ReadNodeData(input, i, ref currentNode);
                    i = nextLine - 1;
                }
            }

            return rootNode;
        }

        private int ReadNodeData(string[] input, int index, ref Node currentNode)
        {
            string dirPattern = @"dir (\w+)";
            string filePattern = @"(\d+) (\S+)";
            var dirRx = new Regex(dirPattern);
            var fileRx = new Regex(filePattern);
            
            while (input[index][0]!='$')
            {
                if (dirRx.IsMatch(input[index]))
                {
                    var match = dirRx.Match(input[index]);
                    var nodeName = match.Groups[1].Value;
                    if (!currentNode.ChildNodes.Any(node => string.Equals(nodeName, node.Name)))
                    {
                        currentNode.ChildNodes.Add(new Node(currentNode, nodeName));
                    }
                }
                if (fileRx.IsMatch(input[index]))
                {
                    var match = fileRx.Match(input[index]);
                    var fileName = match.Groups[2].Value;
                    var fileSize = long.Parse(match.Groups[1].Value);
                    if (!currentNode.Files.Any(file => string.Equals(fileName, file.FileName)))
                    {
                        currentNode.Files.Add(new File(fileName, fileSize));
                    }
                }

                index++;
                if (index >= input.Length)
                {
                    return index;
                }
            }

            return index;
        }

        private void CalculateSize(Node node)
        {
            if (node.ChildNodes.Count == 0)
            {
                node.Size = node.Files.Sum(file => file.Size);
            }
            foreach (Node nodeChildNode in node.ChildNodes)
            {
                CalculateSize(nodeChildNode);
            }

            node.Size = node.ChildNodes.Sum(child => child.Size) + node.Files.Sum(file => file.Size);
        }

        private void CalculateSum(Node node, ref long sum)
        {
            if (node.Size <= THRESHOLD)
            {
                sum += node.Size;
            }

            foreach (Node nodeChildNode in node.ChildNodes)
            {
                CalculateSum(nodeChildNode, ref sum);
            }
        }

    }
}