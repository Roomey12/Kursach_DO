using System;
using System.Collections.Generic;
using System.Text;
using Kursach.Models;

namespace Kursach.Helpers
{
    public static class Data
    {
        public static List<Efficiency> GetData(int performersCount, int tasksCount, out List<Performer> performers, out List<Task> tasks)
        {
            performers = new List<Performer>();
            for (int i = 1; i <= performersCount; i++)
            {
                performers.Add(new Performer($"P{i}"));
            }

            tasks = new List<Task>();
            for (int i = 1; i <= tasksCount; i++)
            {
                tasks.Add(new Task($"T{i}"));
            }

            var random = new Random();
            var efficiencies = new List<Efficiency>();
            for (int i = 0; i < performersCount; i++)
            {
                for (int j = 0; j < tasksCount; j++)
                {
                    efficiencies.Add(new Efficiency(performers[i], tasks[j], random.Next(0, 10)));
                }
            }

            return efficiencies;
        }
    }
}
