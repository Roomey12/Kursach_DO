using System;
using System.Collections.Generic;
using Kursach.Helpers;
using Kursach.Models;

namespace Kursach
{
    class Program
    {
        static void Main(string[] args)
        {
            var performers = new List<Performer>();
            var tasks = new List<Task>();
            var efficiencies = Data.GetData(5, 7, out performers, out tasks);

            var output = new Output();
            output.OutputList(performers);
            output.OutputList(tasks);
            output.OutputList(efficiencies);
        }
    }
}
