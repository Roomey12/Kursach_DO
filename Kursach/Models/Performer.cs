using System;
using System.Collections.Generic;
using System.Text;

namespace Kursach.Models
{
    public class Performer
    {
        public string Name { get; set; }

        public Performer(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"Performer: Name = {Name};";
        }
    }
}
