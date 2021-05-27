using System.Collections.Generic;

namespace Kursach.Interfaces
{
    public interface IAlgorithm
    {
        public List<List<int>> Handle(List<List<int>> data);
    }
}