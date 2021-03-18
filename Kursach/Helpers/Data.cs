using System;
using System.Collections.Generic;
using System.Text;

namespace Kursach.Helpers
{
    public static class Data
    {
        public static List<List<int>> Get3by4Matrix()
        {
            return new List<List<int>>()
            {
                    new List<int>() {2, 9, 6, 5},
                    new List<int>() {2, 3, 5, 7},
                    new List<int>() {5, 5, 6, 2}
            };
        }

        public static List<List<int>> Get3by5Matrix()
        {
            return new List<List<int>>() 
            {
                new List<int>() {2, 9, 7, 5, 4},
                new List<int>() {2, 3, 5, 7, 4},
                new List<int>() {5, 5, 6, 2, 8}
            };
        }
    }
}
