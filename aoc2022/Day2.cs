#nullable enable
using System;
using System.Collections.Generic;

namespace aoc2022
{
    internal sealed class Day2 : Day
    {
        private enum Shape
        {
            Rock,
            Paper,
            Scissors
        }
        
        private const int DRAW_COST = 3;
        private const int WIN_COST = 6;
        private const int LOSS_COST = 0;

        private const int ENEMY_POSITION = 0;
        private const int YOUR_POSITION = 2;

        private readonly Dictionary<char, Shape> _enemyShapes = new()
        {
            { 'A', Shape.Rock },
            { 'B', Shape.Paper },
            { 'C', Shape.Scissors }
        };
        
        private readonly Dictionary<char, Shape> _yourShapes = new()
        {
            { 'X', Shape.Rock },
            { 'Y', Shape.Paper },
            { 'Z', Shape.Scissors }
        };

        public override void CalculatePart1(string[] input)
        {
            var result = 0;
            foreach (string line in input)
            {
                char enemyShape = line[ENEMY_POSITION];
                char yourShape = line[YOUR_POSITION];
                result += RoundCost(_enemyShapes[enemyShape], _yourShapes[yourShape]);
            }
            Console.WriteLine(result);
        }

        public override void CalculatePart2(string[] input)
        {
            var result = 0;
            foreach (string line in input)
            {
                char enemyShape = line[ENEMY_POSITION];                
                char guide = line[YOUR_POSITION];
                Shape yourShape = GetShapeFromGuide(_enemyShapes[enemyShape], guide);
                result += RoundCost(_enemyShapes[enemyShape], yourShape);
            }
            Console.WriteLine(result);
        }

        private static int ShapeCost(Shape shape) => (int)shape + 1;

        private int RoundCost(Shape enemyShape, Shape yourShape)
        {
            return RoundResult(enemyShape, yourShape) + ShapeCost(yourShape);
        }

        private static int RoundResult(Shape enemyShape, Shape yourShape)
        {
            if (yourShape == enemyShape)
            {
                return DRAW_COST;
            }

            return yourShape switch
            {
                Shape.Rock => enemyShape == Shape.Scissors ? WIN_COST : LOSS_COST,
                Shape.Paper => enemyShape == Shape.Rock ? WIN_COST : LOSS_COST,
                Shape.Scissors => enemyShape == Shape.Paper ? WIN_COST : LOSS_COST,
                _ => throw new Exception("Spock?!")
            };
        }

        private Shape ShapeToDraw(Shape enemyShape) => enemyShape;
        private Shape ShapeToWin(Shape enemyShape) => (Shape)(((int)enemyShape + 1) % 3);
        private Shape ShapeToLoose(Shape enemyShape)
        {
            if (enemyShape == 0)
                return Shape.Scissors;
            return (Shape)((int)enemyShape - 1);
        }

        private Shape GetShapeFromGuide(Shape enemyShape, char guide)
        {
            return guide switch
            {
                'X' => ShapeToLoose(enemyShape),
                'Y' => ShapeToDraw(enemyShape),
                'Z' => ShapeToWin(enemyShape),
                _ => throw new Exception("Spock?!")
            };
        }
    }
}