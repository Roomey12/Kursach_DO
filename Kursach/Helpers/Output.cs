using System;
using System.Collections.Generic;
using System.Text;
using Kursach.Models;

namespace Kursach.Helpers
{
    public class Output
    {
        public void OutputList<T>(List<T> list)
        {
            Console.WriteLine();
            foreach (var snus in list)
            {
                Console.WriteLine(snus);
            }
            Console.WriteLine();
        }
    }
}
