using ElMansoury;

using Kursach.Helpers;

using System;

namespace Kursach
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = Data.Get3by4Matrix();
            Output.Data(data);

            var result = LutsenkoAlgorithm.Handle(data);
            Output.Result(result);


            Console.WriteLine(new string('-', 40));
            var x = new ElMansouryAlgorithm(9, 7);
            x.FillData(8);
            x.GenerateAllMatrices();
            Console.WriteLine(new string('-', 40));
            x.FindOptimalDistribution();
        }
    }
}