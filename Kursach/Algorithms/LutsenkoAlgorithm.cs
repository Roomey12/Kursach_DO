using System;
using System.Collections.Generic;
using System.Linq;

namespace Kursach.Algorithms
{
    public class LutsenkoAlgorithm : IAlgorithm
    {
        public List<List<int>> Handle(List<List<int>> data)
        {
            var dataCopy = data.Select(x => x.ToList()).ToList();

            var finalResult = Hungarian(data);

            // --------- получить задачи, для которых не найдены исполнители --------
            var tasksNumbers = new List<int>();

            for (int i = 0; i < (data.Sum(x => x.Count) / data.Count); i++)
            {
                tasksNumbers.Add(i);
            }

            var assinedTasks = new List<int>();

            for (int i = 0; i < finalResult.Count; i++)
            {
                assinedTasks.Add(finalResult[i][1]);
            }

            var unassignedTasks = tasksNumbers.Except(assinedTasks).ToList();
            // -----------------------------------------------------------------------

            // найти исполнителей для оставшихся задач
            do
            {
                // ------------- подготовить матрицу с новыми значениями ----------------
                var intermediateData = new List<List<int>>();

                for (int i = 0; i < dataCopy.Count; i++)
                {
                    var helpList = new List<int>();
                    for (int j = 0; j < (dataCopy.Sum(x => x.Count) / data.Count); j++)
                    {
                        helpList.Add(0);
                    }
                    intermediateData.Add(helpList);
                }

                for (int i = 0; i < (dataCopy.Sum(x => x.Count) / data.Count); i++)
                {
                    if (unassignedTasks.Contains(i))
                    {
                        for (int j = 0; j < dataCopy.Count; j++)
                        {
                            intermediateData[j][i] = dataCopy[j][i];
                        }
                    }
                }
                // -----------------------------------------------------------------------

                // найти исполнителей для оставшихся задач
                var result2 = Hungarian(intermediateData);

                // добавить новые результаты в конечный результат
                for (int i = 0; i < result2.Count; i++)
                {
                    if (unassignedTasks.Contains(result2[i][1]))
                    {
                        finalResult.Add(result2[i]);
                    }
                }

                // ------------------- найти задачи без исполнителей ----------------------
                assinedTasks.Clear();

                for (int i = 0; i < finalResult.Count; i++)
                {
                    assinedTasks.Add(finalResult[i][1]);
                }

                unassignedTasks = tasksNumbers.Except(assinedTasks).ToList();
                // -----------------------------------------------------------------------

            } while (unassignedTasks.Count > 0); // если остались задачи без исполнителя

            return finalResult;
        }

        private List<List<int>> Hungarian(List<List<int>> matrix)
        {
            try
            {
                List<int> maxStrVals = matrix.Select(str => str.Max()).ToList();

                for (int i = 0; i < matrix.Count; i++)
                {
                    for (int j = 0; j < (matrix.Sum(x => x.Count) / matrix.Count); j++)
                    {
                        matrix[i][j] = maxStrVals[i] - matrix[i][j];
                    }
                }

                int height = matrix.Count, width = matrix.Sum(x => x.Count) / height;

                List<int> u = new List<int>(height);
                List<int> v = new List<int>(width);

                for (int a = 0; a < height; a++)
                {
                    u.Add(0);
                }

                for (int a = 0; a < width; a++)
                {
                    v.Add(0);
                }

                List<int> markIndices = new List<int>(width);
                for (int a = 0; a < width; a++)
                {
                    markIndices.Add(-1);
                }

                int count = 0;
                for (int i = 0; i < height; i++)
                {
                    List<int> links = new List<int>(width);
                    List<int> mins = new List<int>(width);
                    List<int> visited = new List<int>(width);

                    for (int a = 0; a < width; a++)
                    {
                        links.Add(-1);
                        mins.Add(int.MaxValue);
                        visited.Add(0);
                    }

                    int markedI = i, markedJ = -1, j = 0;
                    while (markedI != -1)
                    {
                        j = -1;
                        for (int j1 = 0; j1 < width; j1++)
                        {
                            if (visited[j1] != 1)
                            {
                                if (matrix[markedI][j1] - u[markedI] - v[j1] < mins[j1])
                                {
                                    mins[j1] = matrix[markedI][j1] - u[markedI] - v[j1];
                                    links[j1] = markedJ;
                                }
                                if (j == -1 || mins[j1] < mins[j])
                                    j = j1;
                            }
                        }

                        int delta = mins[j];
                        for (int j1 = 0; j1 < width; j1++)
                        {
                            if (visited[j1] == 1)
                            {
                                u[markIndices[j1]] += delta;
                                v[j1] -= delta;
                            }
                            else
                            {
                                mins[j1] -= delta;
                            }
                        }
                        u[i] += delta;

                        visited[j] = 1;
                        markedJ = j;
                        markedI = markIndices[j];
                        count++;
                    }

                    for (; links[j] != -1; j = links[j])
                    {
                        markIndices[j] = markIndices[links[j]];
                    }
                    markIndices[j] = i;
                }

                List<List<int>> result = new List<List<int>>();

                for (int j = 0; j < width; j++)
                {
                    if (markIndices[j] != -1)
                    {
                        result.Add(new List<int>() { markIndices[j], j });
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}