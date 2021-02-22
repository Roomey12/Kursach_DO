using System;
using System.Collections.Generic;
using System.Text;

namespace Kursach.Models
{
    public class Efficiency
    {
        public Performer Performer { get; set; }
        public Task Task { get; set; }
        public int Weight { get; set; }

        public Efficiency(Performer performer, Task task, int weight)
        {
            Performer = performer;
            Task = task;
            Weight = weight;
        }

        public override string ToString()
        {
            return $"Efficiency: Performer = {Performer.Name}; Task = {Task.Name}; Weight = {Weight};";
        }
    }
}
