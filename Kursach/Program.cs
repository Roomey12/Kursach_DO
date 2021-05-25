using Kursach.Algorithms;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kursach
{
    class Program
    {
        public static void OutputResult(List<List<int>> data)
        {
            Console.WriteLine("Worker\tTask");

            foreach (var v in data.OrderBy(e => e[0]))
            {
                Console.WriteLine($"{v[0] + 1}\t{v[1] + 1}");
            }

        }
        static void Main(string[] args)
        {

            // ARRANGE
            const int workers = 5;
            const int workNum = 7;
            const int maxValue = 10;
            var testData = new List<List<int>>();

            var bar = new List<List<int>>
            {
                new() {1, 3, 5, 1, 8, 4},
                new() {2, 2, 2, 6, 4, 3},
                new() {3, 6, 1, 8, 1, 7},
                new() {4, 4, 6, 3, 3, 2},
                new() {5, 1, 3, 4, 9, 5}

            };

            for (int i = 0; i < workers; i++)
            {
                testData.Add(new int[workNum].ToList());
            }

            var rand = new Random();
            for (int i = 0; i < workers; i++)
            {
                for (int j = 0; j < workNum; j++)
                {
                    testData[i][j] = rand.Next(0, maxValue);
                }
            }

            var algorithms = new List<IAlgorithm>
            {
                new GreedyAlgorithm(), new ElMansouryAlgorithm(), new LutsenkoAlgorithm(),
            };

            foreach (var algorithm in algorithms)
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                var dataCopy = bar.Select(x => x.ToList()).ToList();
                Console.WriteLine(algorithm);
                OutputResult(algorithm.Handle(dataCopy));
                stopWatch.Stop();
                Console.WriteLine($"Milliseconds: {stopWatch.ElapsedMilliseconds}");
                Console.WriteLine($"\n{new string('-', 40)}");
            }
        }
    }
}