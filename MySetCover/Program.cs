using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Данное приложение является реализацией задачи
/// о покрытии множеств из книги "Грокаем алгоритмы"
/// </summary>

namespace MySetCover
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                // Множество всех штатов.
                HashSet<string> neededStates = new HashSet<string>() { "mt", "wa", "or", "id",
                    "nv", "ut", "ca", "az"};

                // Таблица станций и покрытий.
                var stations = new Dictionary<string, HashSet<string>>()
                {
                    ["kone"] = new HashSet<string> { "id", "nv", "ut" },
                    ["ktwo"] = new HashSet<string> { "wa", "id", "mt" },
                    ["kthree"] = new HashSet<string> { "or", "nv", "ca" },
                    ["kfour"] = new HashSet<string> { "nv", "ut"},
                    ["kfive"] = new HashSet<string> { "ca", "az"}
                };

                // Окончательное множество станций.
                HashSet<string> finalStates = new HashSet<string>();

                while (neededStates.Count > 0)
                {
                    string bestStation = null;
                    HashSet<string> coveredBestStation = new HashSet<string>();

                    // Определяем станцию с самым лучшим покрытием.
                    foreach (string station in stations.Keys)
                    {
                        var stationStates = stations[station];
                        var covered = new HashSet<string>(neededStates.Intersect(stationStates));
                        if (covered.Count() > coveredBestStation.Count())
                        {
                            bestStation = station;
                            coveredBestStation = covered;
                        }
                    }
                    // Исключаем покрытые самой лучшей станцией штаты.
                    neededStates.ExceptWith(coveredBestStation);
                    finalStates.Add(bestStation);
                }

                foreach(var station in finalStates)
                    Console.WriteLine(station);

                Console.WriteLine("Для выхода нажмите ESC...");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
