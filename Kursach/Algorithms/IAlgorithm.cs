using System.Collections.Generic;

namespace Kursach.Algorithms
{
    public interface IAlgorithm
    {
        public List<List<int>> Handle(List<List<int>> data);
    }
}