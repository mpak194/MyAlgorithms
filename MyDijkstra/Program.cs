using System;
using System.Collections.Generic;

/// <summary>
/// Данное приложение демонстрирует реализацию алгоритма 
/// Дейкстры из книги "Грокаем алгоритмы"
/// </summary>

namespace MyDijkstra
{
    class Program
    {
        /// <summary>
        /// Метод генерации графа связей между точками с их весами
        /// </summary>
        /// <returns>Сгенерированный граф</returns>
        static Dictionary<string, Dictionary<string, int>> CreateGraph()
        {
            return new Dictionary<string, Dictionary<string, int>>()
            {
                ["Start"] = new Dictionary<string, int>()
                {
                    ["A"] = 6,
                    ["B"] = 2
                },
                ["A"] = new Dictionary<string, int>()
                {
                    ["Finish"] = 1
                },
                ["B"] = new Dictionary<string, int>()
                {
                    ["A"] = 3,
                    ["Finish"] = 5
                },
                ["Finish"] = new Dictionary<string, int>() { }
            };
        }

        /// <summary>
        /// Метод генерации словаря, описывающего стоимости
        /// времени перехода от начала
        /// </summary>
        /// <returns>Сгенерированный словарь</returns>
        static Dictionary<string, int> CreateCosts()
        {
            return new Dictionary<string, int>()
            {
                ["A"] = 6,
                ["B"] = 2,
                ["Finish"] = int.MaxValue
            };
        }

        /// <summary>
        /// Метод генерации словаря родителей вершин графа
        /// </summary>
        /// <returns>Сгенерированный словарь</returns>
        static Dictionary<string, string> CreateParents()
        {
            return new Dictionary<string, string>()
            {
                ["A"] = "Start",
                ["B"] = "Start",
                ["Finish"] = null
            };
        }

        /// <summary>
        /// Метод поиска вершины с наименьшим весом
        /// </summary>
        /// <param name="costs">Словарь вершин с весами</param>
        /// <param name="processed">Список уже проверенных вершин</param>
        /// <returns>Вершина с минимальным весом</returns>
        static string FindLowestCost(Dictionary<string, int> costs, List<string> processed)
        {
            int lowestCost = int.MaxValue;
            string lowestCostNode = null;
            foreach (string node in costs.Keys)
            {
                int cost = costs[node];
                if (cost < lowestCost && !processed.Contains(node))
                {
                    lowestCost = cost;
                    lowestCostNode = node;
                }
            }
            return lowestCostNode;
        }

        /// <summary>
        /// Метод вывода найденного пути
        /// </summary>
        /// <param name="parents">Словарь родителей вершин</param>
        static void DisplayWay(Dictionary<string, string> parents)
        {
            string search = "Start";
            Console.Write(search);
            for (int i = 0; i < parents.Count; i++)
            {
                foreach (var item in parents.Keys)
                {
                    if (parents[item] == search)
                    {
                        Console.Write($" -> {item}");
                        search = item;
                    }
                }
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            do
            {
                var graph = CreateGraph();
                var costs = CreateCosts();
                var parents = CreateParents();

                List<string> processed = new List<string>();

                string node = FindLowestCost(costs, processed);
                while (node != null)
                {
                    int cost = costs[node];
                    // Определяем соседей вершины с минимальным весом.
                    Dictionary<string, int> neighbors = graph[node];
                    foreach (var item in neighbors.Keys)
                    {
                        int newCost = cost + neighbors[item];
                        if (costs[item] > newCost)
                        {
                            costs[item] = newCost;
                            parents[item] = node;
                        }
                    }
                    processed.Add(node);
                    node = FindLowestCost(costs, processed);
                }

                DisplayWay(parents);

                Console.WriteLine("Для выхода нажмите ESC...");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
