using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Данное приложение демонстрирует реализацию алгоритма 
/// поиска в ширину из книги "Грокаем алгоритмы"
/// </summary>

namespace MyBFS
{
    class Program
    {
        /// <summary>
        /// Метод инициализации графа связей между людьми
        /// Данный граф предстваляет друзей каждого человека
        /// </summary>
        /// <returns>Проинициализированный граф</returns>
        static Dictionary<string, List<string>> CreateGrahp()
        {
            Dictionary<string, List<string>> graph = null;
            try
            {
                graph = new Dictionary<string, List<string>>()
                {
                    ["Me"] = new List<string>() { "Alice", "Bob", "Claire" },
                    ["Bob"] = new List<string>() { "Anuj", "Peggy" },
                    ["Alice"] = new List<string>() { "Peggy" },
                    ["Claire"] = new List<string>() { "Thom", "Jonny" },
                    ["Anuj"] = new List<string>(),
                    ["Peggy"] = new List<string>(),
                    ["Thom"] = new List<string>(),
                    ["Jonny"] = new List<string>()
                };
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            return graph;
        }

        /// <summary>
        /// Метод добавления друзей человека в очередь
        /// </summary>
        /// <param name="myQueue">Искомая очередь</param>
        /// <param name="persons">Список друзей для добавления</param>
        static void PushEnqueue(Queue<string> myQueue, List<string> persons)
        {
            foreach (var person in persons)
                myQueue.Enqueue(person);
        }

        /// <summary>
        /// Самая простая проверка имени
        /// </summary>
        /// <param name="person">Имя человека</param>
        /// <returns>Индикатор успеха проверки</returns>
        static bool CheckPerson(string person, string searchingPerson)
        {
            if (person == searchingPerson)
                return true;
            return false;
        }

        static void Main(string[] args)
        {
            do
            {
                Console.Clear();

                // Словарь, представляющий граф.
                Dictionary<string, List<string>> graph = CreateGrahp();
                // Очередь людей на проверку имени.
                Queue<string> myQueue = new Queue<string>();
                // Список уже проверенных людей.
                List<string> checkedPeople = new List<string>();

                Console.WriteLine("Введите имя для поиска:");
                string searchingPerson = Console.ReadLine();

                PushEnqueue(myQueue, graph["Me"]);
                bool isFound = false;
                while (myQueue.Count != 0)
                {
                    string checkingPerson = myQueue.Dequeue();
                    if (CheckPerson(checkingPerson, searchingPerson))
                    {
                        Console.WriteLine($"Человек был найден: {checkingPerson}.");
                        isFound = true;
                        break;
                    }
                    else
                    {
                        PushEnqueue(myQueue, graph[checkingPerson]);
                        checkedPeople.Add(checkingPerson);
                    }
                }
                if (!isFound)
                    Console.WriteLine($"Человек с именем {searchingPerson} не был найден!!!");
                Console.WriteLine("Для выхода нажмите ESC...");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
