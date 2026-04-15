using System;
using System.Collections.Generic;

namespace ToolboxApp;

public class ToolboxApplication
{
    private readonly List<ITool> _tools;

    public ToolboxApplication(List<ITool> tools)
    {
        _tools = tools;
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Toolbox ===");

            for (int i = 0; i < _tools.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {_tools[i].Name}");
            }

            Console.WriteLine("0) Beenden");
            Console.Write("Auswahl: ");

            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("Bitte eine Menunummer eingeben. Enter...");
                Console.ReadLine();
                continue;
            }

            if (choice == 0)
            {
                Console.Clear();
                return;
            }

            int index = choice - 1;

            if (index < 0 || index >= _tools.Count)
            {
                Console.WriteLine("Ungueltige Auswahl. Enter...");
                Console.ReadLine();
                continue;
            }

            Console.Clear();
            _tools[index].Run();
        }
    }
}
