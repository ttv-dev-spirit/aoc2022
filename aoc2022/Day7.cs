#nullable enable
using System.Collections.Generic;

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
            public readonly List<long> Files = new();

            public Node(Node? parentNode, string Name)
            {
                ParentNode = parentNode;
            }
        }

        private const string _traversePattern = @"$ cd (/w+)
$ ls";

        private const string _upCommand = @"$ cd ..";
        private const string _listCommand = @"$ ls";

        private Node _rootNode = null!;
        private Node _currentNode = null!;

        public override void CalculatePart1(string[] input)
        {
            _rootNode = new Node(null, "/");
            _currentNode = _rootNode;
            for (var i = 1; i < input.Length; i++)
            {

            }
        }

        public override void CalculatePart2(string[] input)
        {
        }
    }
}