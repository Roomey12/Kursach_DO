using System;
using System.Collections.Generic;
using System.Linq;

namespace ElMansoury
{
    public class ElMansouryAlgorithm
    {
        public List<List<int>> Data { get; private set; } = new();
        public List<List<List<int>>> PermutationMatrices { get; set; } = new();
        public int WorkAmount { get; set; }
        public int WorkersNum { get; set; }
        public ElMansouryAlgorithm(int workAmount, int workersNum)
        {
            if (workersNum > workAmount)
            {
                return;
            }

            WorkAmount = workAmount;
            WorkersNum = workersNum;

            for (int i = 0; i < workersNum; i++)
            {
                Data.Add(new int[workAmount].ToList());
            }
        }
        public void FillData(int maxValue)
        {
            var rand = new Random();
            for (int i = 0; i < WorkersNum; i++)
            {
                for (int j = 0; j < WorkAmount; j++)
                {
                    Data[i][j] = (int)rand.Next(0, (int)maxValue);
                }
            }

            OutputMatrix(Data);
            Console.WriteLine(new string('-', 40));
        }

        public void FillData(List<List<int>> data)
        {
            Data = data;
        }

        public void OutputMatrix(List<List<int>> Data)
        {
            for (int i = 0; i < Data.Count; i++)
            {
                Console.WriteLine($"Worker {i + 1}: {string.Join(" ", Data[i])}");

            }

        }

        public void GenerateAllMatrices()
        {
            var startMatrix = new List<List<int>>();

            for (int i = 0; i < WorkersNum; i++)
            {
                startMatrix.Add(new int[WorkersNum].ToList());
                for (int j = 0; j < WorkersNum; j++)
                {
                    startMatrix[i][j] = 0;
                    startMatrix[i][i] = 1;
                }
            }

            PermutationMatrices = Permute(startMatrix);
            //PrintResult(Permute(startMatrix));
        }

        public void FindOptimalDistribution()
        {
            int maxDistributionEfficiency = 0;
            List<List<int>> optimalDistribution = new();
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("\nSTART FINDING OPTIMAL DISTRIBUTION\n");
            Console.ResetColor();

            int difference = WorkAmount - WorkersNum;
            int shift = -1;
            double progress = 0;
            double part = difference != 0 ? (double)100 / ((double)difference * (double)PermutationMatrices.Count) : (double)100 / (double)PermutationMatrices.Count;
            Console.WriteLine(new string('-', 40));

            while (difference >= 0)
            {
                foreach (var permutationMatrix in PermutationMatrices)
                {
                    int currentEfficiency = 0;

                    for (int i = 0; i < WorkersNum; i++)
                    {
                        for (int j = 0; j < WorkersNum; j++)
                        {
                            currentEfficiency += Data[i][j + difference] * permutationMatrix[i][j];
                        }
                    }

                    if (maxDistributionEfficiency < currentEfficiency)
                    {
                        maxDistributionEfficiency = currentEfficiency;
                        optimalDistribution = permutationMatrix;
                        shift = difference;
                    }

                    progress += part;
                    ConsoleUtility.WriteProgressBar(progress, true);
                }

                difference--;
            }
            Console.WriteLine();

            Console.WriteLine(new string('-', 40));

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"MAX EFFICIENCY = {maxDistributionEfficiency}");
            Console.ResetColor();
            Console.WriteLine(new string('-', 40));

            Console.WriteLine("Optimal distribution");

            Console.WriteLine(new string('-', 40));

            OutputMatrix(optimalDistribution);

            Console.WriteLine(new string('-', 40));

            OutputFinalDistribution(shift, optimalDistribution);

            Console.WriteLine(new string('-', 40));


            Console.WriteLine();
        }

        public void OutputFinalDistribution(int shift, List<List<int>> optimalDistribution)
        {
            for (int i = 0; i < WorkersNum; i++)
            {
                var x = 0;
                Console.Write($"Worker {i + 1}: ");
                for (int j = 0; j < WorkAmount; j++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;

                    if (j >= shift && x < WorkersNum)
                    {
                        x++;
                        Console.ForegroundColor = optimalDistribution[i][j - shift] == 1 ? ConsoleColor.Green : ConsoleColor.Gray;
                    }


                    Console.Write(Data[i][j] + " ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        List<List<List<int>>> Permute(List<List<int>> nums)
        {
            var list = new List<List<List<int>>>();
            return DoPermute(nums, 0, nums[0].Count - 1, list);
        }

        List<List<List<int>>> DoPermute(List<List<int>> nums, int start, int end, List<List<List<int>>> list)
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

        void Swap(ref List<int> a, ref List<int> b)
        {
            var temp = a;
            a = b;
            b = temp;
        }
    }
    static class ConsoleUtility
    {
        const char _block = '■';
        const string _back = "\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b";
        public static void WriteProgressBar(double percent, bool update = false)
        {

            if (update)
            {
                Console.Write(_back);
            }

            Console.Write("[");
            var p = (double)((percent / 10f) + .5f);
            for (var i = 0; i < 10; ++i)
            {
                Console.Write(i >= p ? ' ' : _block);
            }
            Console.Write("] {0,2:##0}%", percent);
            Console.ResetColor();
        }
    }

}