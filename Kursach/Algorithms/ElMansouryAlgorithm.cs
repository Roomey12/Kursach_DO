using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Kursach.Algorithms
{
    public class ElMansouryAlgorithm : IAlgorithm
    {
        private List<List<int>> _data { get; set; }
        private List<List<List<int>>> PermutationMatrices { get; set; } = new();
        private int WorkAmount { get; set; }
        private int WorkersNum { get; set; }

        private List<List<int>> Start()
        {
            GenerateAllMatrices();
            //Console.WriteLine(new string('-', 40));

            return FindOptimalDistribution();
        }

        private List<List<int>> FindOptimalDistribution()
        {
            var maxDistributionEfficiency = 0;
            List<List<int>> optimalDistribution = new List<List<int>>();
            //Console.ForegroundColor = ConsoleColor.Yellow;

            //Console.WriteLine("\nSTART FINDING OPTIMAL DISTRIBUTION\n");
            //Console.ResetColor();

            int difference = WorkAmount - WorkersNum;
            int shift = -1;
            double progress = 0;
            double part = difference != 0 ? 100 / (difference * (double)PermutationMatrices.Count) : 100 / (double)PermutationMatrices.Count;
            //Console.WriteLine(new string('-', 40));

            while (difference >= 0)
            {
                foreach (List<List<int>> permutationMatrix in PermutationMatrices)
                {
                    var currentEfficiency = 0;

                    for (var i = 0; i < WorkersNum; i++)
                    {
                        for (var j = 0; j < WorkersNum; j++)
                        {
                            currentEfficiency += _data[i][j + difference] * permutationMatrix[i][j];
                        }
                    }

                    if (maxDistributionEfficiency < currentEfficiency)
                    {
                        maxDistributionEfficiency = currentEfficiency;
                        optimalDistribution = permutationMatrix;
                        shift = difference;
                    }

                    progress += part;
                    //ConsoleUtility.WriteProgressBar(progress, true);
                }

                difference--;
            }


            var result = new List<List<int>>();

            for (int i = 0; i < WorkersNum; i++)
            {
                for (int j = 0; j < WorkersNum; j++)
                {
                    if (optimalDistribution[i][j] == 1)
                        result.Add(new List<int>
                        {
                            i, j
                        });
                }
            }


            //Console.WriteLine($"\n{new string('-', 40)}");

            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine($"MAX EFFICIENCY (1) = {maxDistributionEfficiency}");
            //Console.ResetColor();
            //Console.WriteLine($"{new string('-', 40)}\nOptimal distribution\n{new string('-', 40)}");

            //OutputMatrix(optimalDistribution);

            //Console.WriteLine(new string('-', 40));

            //OutputFinalDistribution(shift, optimalDistribution.AsReadOnly());

            var z = OutputFinalDistribution1(shift, optimalDistribution.AsReadOnly(), result);

            //Console.ForegroundColor = ConsoleColor.Red;

            //Console.WriteLine($"MAX EFFICIENCY (final) = {maxDistributionEfficiency + z}");
            //Console.ResetColor();

            //Console.WriteLine($"{new string('-', 40)}\n");
            return result;
        }

        private int OutputFinalDistribution1(int shift, ReadOnlyCollection<List<int>> optimalDistribution, List<List<int>> result)
        {
            var unUsedWork = _data;
            for (var i = 0; i < WorkersNum; i++)
            {
                var x = 0;

                for (var j = 0; j < WorkAmount; j++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;

                    if (j >= shift && x < WorkersNum)
                    {
                        x++;
                        Console.ForegroundColor = optimalDistribution[i][j - shift] == 1 ? ConsoleColor.Green : ConsoleColor.Gray;
                        unUsedWork[i][j] = 0;
                    }

                    Console.ResetColor();
                }
            }

            return FindRemaining(unUsedWork, result);
        }

        private int FindRemaining(IReadOnlyList<List<int>> efficiencyMatrix, List<List<int>> result) 
        {
            var efficiency = 0;
            List<KeyValuePair<int, int>> indexes = new List<KeyValuePair<int, int>>();

            for (int i = 0; i < WorkAmount; i++)
            {
                var max = efficiencyMatrix[0][i];
                int J = 0;

                for (int j = 1; j < WorkersNum; j++)
                {
                    if (efficiencyMatrix[j][i] > max)
                    {
                        J = j;
                        max = efficiencyMatrix[j][i];
                    }
                }

                if (efficiencyMatrix[J][i] != 0)
                {
                    result.Add(new List<int> { J, i });
                    indexes.Add(new KeyValuePair<int, int>(J, i));
                }

                efficiency += max;
            }

            //for (int i = 0; i < WorkersNum; i++)
            //{
            //    Console.Write($"Worker {i}: ");
            //    for (int j = 0; j < WorkAmount; j++)
            //    {
            //        Console.ForegroundColor = indexes.Exists(e => e.Key == i && e.Value == j) ? ConsoleColor.Green : ConsoleColor.DarkGray;
            //        Console.Write($"{efficiencyMatrix[i][j]} ");
            //        Console.ResetColor();
            //    }

            //    Console.WriteLine();
            //}

            return efficiency;
        }

        #region Zero Matrix generation

        private void GenerateAllMatrices()
        {
            var startMatrix = new List<List<int>>();

            for (var i = 0; i < WorkersNum; i++)
            {
                startMatrix.Add(new int[WorkersNum].ToList());
                for (var j = 0; j < WorkersNum; j++)
                {
                    startMatrix[i][j] = 0;
                    startMatrix[i][i] = 1;
                }
            }

            PermutationMatrices = Permute(startMatrix);
        }

        private List<List<List<int>>> Permute(List<List<int>> nums)
        {
            var list = new List<List<List<int>>>();
            return DoPermute(nums, 0, nums[0].Count - 1, list);
        }

        private List<List<List<int>>> DoPermute(List<List<int>> nums, int start, int end, List<List<List<int>>> list)
        {
            if (start == end)
            {
                list.Add(new List<List<int>>(nums));
            }
            else
            {
                for (var i = start; i <= end; i++)
                {
                    var x = nums[start];
                    var y = nums[i];

                    Swap(ref x, ref y);
                    nums[start] = x;
                    nums[i] = y;

                    DoPermute(nums, start + 1, end, list);

                    var a = nums[start];
                    var b = nums[i];

                    Swap(ref a, ref b);
                    nums[start] = a;
                    nums[i] = b;
                }
            }

            return list;
        }

        private void Swap(ref List<int> a, ref List<int> b)
        {
            var temp = a;
            a = b;
            b = temp;
        }

        #endregion

        public List<List<int>> Handle(List<List<int>> data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            WorkAmount = data.First().Count;
            WorkersNum = data.Count;
            _data = data;

            return Start();
        }
    }
}