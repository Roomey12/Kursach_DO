using System;
using System.Collections.Generic;
using System.Linq;

namespace Kursach.Helpers
{
    public static class Output
    {
        public static void Data(List<List<int>> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < (data.Sum(x => x.Count) / data.Count); j++)
                {
                    Console.Write(data[i][j] + "\t");
                }
                Console.WriteLine();
            }
        }

        public static void Result(List<List<int>> result)
        {
            Console.WriteLine("\nWorker\tTask");
            for (int i = 0; i < result.Count; i++)
            {
                for (int j = 0; j < (result.Sum(x => x.Count) / result.Count); j++)
                {
                    Console.Write(result[i][j] + 1 + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}
