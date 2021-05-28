using System;
using System.Collections.Generic;
using System.Linq;
using Kursach.Algorithms;
using Kursach.Interfaces;

namespace Kursach
{
    class Program
    {
        static void Output(List<List<int>> matrix)
        {
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix.First().Count; j++)
                {
                    Console.Write($"{matrix[i][j]} ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        static int GetEff(List<List<int>> Data, List<List<int>> Dist)
        {
            var eff = 0;

            foreach (var ints in Dist)
            {
                eff += Data[ints[0]][ints[1]];
            }

            return eff;
        }

        static void Main(string[] args)
        {
            IAlgorithm simon = new ElMansouryAlgorithm();
            IAlgorithm greedy = new LutsenkoAlgorithm();

            const int matricesNum = 40;

            const int workers = 7;
            const int workNum = 18;
            const int maxValue = 3;
            
            var rand = new Random();

            var effMatrices = new List<List<List<int>>>();

            for (int z = 0; z < matricesNum; z++)
            {
                var data = new List<List<int>>();

                for (int i = 0; i < workers; i++)
                {
                    data.Add(new int[workNum].ToList());
                }

                for (int i = 0; i < workers; i++)
                {
                    for (int j = 0; j < workNum; j++)
                    {
                        data[i][j] = rand.Next(0, maxValue);
                    }
                }

                effMatrices.Add(data);
            }
            
            effMatrices.ForEach(e =>
            {
                var result = GetEff(e, simon.Handle(e));
                var luts = GetEff(e, greedy.Handle(e));
                
                Console.WriteLine($"Simon: {result}\t Luts: {luts}\t Diff: {result - luts}");
                Console.WriteLine();
                
                //Console.WriteLine(result.OrderBy(e => e[1])
                //    .Aggregate("", (current, ints) =>
                //        current + $"Работник {ints[0] + 1} - работа {ints[1] + 1}\n"));
            });
        }
    }
}