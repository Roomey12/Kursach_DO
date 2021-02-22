using System;
using System.Collections.Generic;
using System.Text;

namespace Kursach.Models
{
    public class Task
    {
        public string Name { get; set; }

        public Task(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"Task: Name = {Name};";
        }
    }
}
