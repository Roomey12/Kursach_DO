using System;
using System.Collections.Generic;
using System.Linq;
using Kursach.Helpers;

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
        }
    }
}