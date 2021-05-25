using System.Collections.Generic;
using System.Linq;

namespace Kursach.Algorithms
{
    public class GreedyAlgorithm : IAlgorithm
    {
        private int _workAmount;
        private int _workersNum;

        private List<List<int>> FindOptimalDistribution(List<List<int>> efficiencyMatrix)
        {
            var efficiency = 0;
            List<KeyValuePair<int, int>> indexes = new List<KeyValuePair<int, int>>();

            for (int i = 0; i < _workAmount; i++)
            {
                var max = 0;
                int J = 0;

                for (int j = 0; j < _workersNum; j++)
                {
                    if (indexes.Count < _workersNum)
                    {
                        if (efficiencyMatrix[j][i] > max && !indexes.Exists(e => e.Key == j))
                        {
                            J = j;
                            max = efficiencyMatrix[j][i];
                        }
                    }
                    else
                    {
                        if (efficiencyMatrix[j][i] > max)
                        {
                            J = j;
                            max = efficiencyMatrix[j][i];
                        }

                    }
                }

                indexes.Add(new KeyValuePair<int, int>(J, i));
                efficiency += max;
            }

            //for (int i = 0; i < _workersNum; i++)
            //{
            //    Console.Write($"Worker {i + 1}: ");
            //    for (int j = 0; j < _workAmount; j++)
            //    {
            //        if (indexes.Contains(new KeyValuePair<int, int>(i, j)))
            //        {
            //            Console.ForegroundColor = ConsoleColor.Green;
            //        }

            //        Console.Write($"{efficiencyMatrix[i][j]} ");
            //        Console.ResetColor();
            //    }

            //    Console.WriteLine();
            //}

            //Console.WriteLine(new string('-', 40));
            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine($"MAX EFFICIENCY: {efficiency}");
            //Console.ResetColor();
            //Console.WriteLine(new string('-', 40));
            return indexes.Select(t => new List<int> { t.Key, t.Value }).ToList();

        }

        public List<List<int>> Handle(List<List<int>> data)
        {

            _workAmount = data.First().Count;
            _workersNum = data.Count;
            return FindOptimalDistribution(data);
        }
    }
}