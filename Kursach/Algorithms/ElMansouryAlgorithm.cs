using Kursach.Interfaces;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Kursach.Algorithms
{

    public class ElMansouryAlgorithm : IAlgorithm
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

        private List<List<int>> Data { get; set; }
        private List<List<List<int>>> PermutationMatrices { get; set; } = new();
        private int WorkAmount { get; set; }
        private int WorkersNum { get; set; }

        private List<List<int>> Start()
        {
            var jobs = new List<int>();
            var finalResult = new List<List<int>>();

            do
            {
                int shift;

                var distributionMatrix = FindOptimalDistribution(Data, out shift);
                
                if(!distributionMatrix.Any()) break;
                var tempResult = findResultList(distributionMatrix, shift);

                ReSortJobs(finalResult, tempResult);
                jobs = finalResult.Select(e => e[1]).ToList();

                ReplaceFoundedJobsWithZeros(distributionMatrix, shift);
            } while (jobs.Count != WorkAmount);


            return finalResult;
        }

        private void ReSortJobs(List<List<int>> oldJobsAndWorkers, List<List<int>> newJobsAndWorkes)
        {
            // rabochiy | rabota

            foreach (var newJobsAndWorker in newJobsAndWorkes)
            {
                //if (oldJobsAndWorkers.Exists(e => e[0] == newJobsAndWorker[0]))
                //{
                //    oldJobsAndWorkers.Add(newJobsAndWorker);

                //    var oldAssigment = oldJobsAndWorkers.First(e => e[0] == newJobsAndWorker[0]);

                //    if (oldAssigment[1] > newJobsAndWorker[1])
                //    {
                //        oldJobsAndWorkers.Remove(oldAssigment);
                //        oldJobsAndWorkers.Add(newJobsAndWorker);
                //    }
                //}
                //else
                //{
                //    oldJobsAndWorkers.Add(newJobsAndWorker);
                //}

                if (!oldJobsAndWorkers.Exists(e => e[1] == newJobsAndWorker[1]))
                {
                    oldJobsAndWorkers.Add(newJobsAndWorker);
                }
            }
        }

        private void ReplaceFoundedJobsWithZeros(List<List<int>> distributionMatrix, int shift)
        {
            if (shift == -1)
                shift = 0;

            var dataCopy2 = Data.Select(x => x.ToList()).ToList();

            var jobs = new List<int>();

            for (var i = 0; i < WorkersNum; i++)
            {
                var x = 0;

                for (var j = 0; j < WorkAmount; j++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;

                    if (j >= shift && x < WorkersNum)
                    {
                        x++;

                        if (distributionMatrix[i][j - shift] == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }

                        dataCopy2[i][j] = 0;
                        jobs.Add(j);
                    }

                    //Console.Write(Data[i][j] + " ");
                    Console.ResetColor();
                }

                //Console.WriteLine();
            }

            Data = dataCopy2;
        }
        
        private List<List<int>> findResultList(List<List<int>> distMatrix, int shift)
        {
            var result = new List<List<int>>();

            for (int i = 0; i < WorkersNum; i++)
            {
                for (int j = 0; j < WorkersNum; j++)
                {
                    if (distMatrix[i][j] == 1)
                    {
                        result.Add(new List<int>
                        {
                            i, j + shift
                        });
                    }
                }
            }

            return result;
        }

        private List<List<int>> FindOptimalDistribution(List<List<int>> Data, out int shift)
        {
            //Console.WriteLine("efficiency");
            //Output(Data);

            var maxDistributionEfficiency = 0;
            List<List<int>> optimalDistribution = new List<List<int>>();

            int difference = WorkAmount - WorkersNum;
            shift = -1;

            while (difference >= 0)
            {
                foreach (List<List<int>> permutationMatrix in PermutationMatrices)
                {
                    var currentEfficiency = 0;

                    for (var i = 0; i < WorkersNum; i++)
                    {
                        for (var j = 0; j < WorkersNum; j++)
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
                }

                difference--;
            }

            return optimalDistribution;
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
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            WorkAmount = data.First().Count;
            WorkersNum = data.Count;
            GenerateAllMatrices();
            Data = data;

            return Start();
        }
    }
}