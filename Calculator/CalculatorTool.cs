using System;
using System.Buffers;
using System.Linq.Expressions;

namespace ToolboxApp.Calculator;

public class CalculatorTool : ITool
{
    public string Name => "Taschenrechner (WiP)";

    private readonly List<Operation> _operations;

    public CalculatorTool()
    {
        _operations = new List<Operation>
        {
            new Operation("Addieren", (a,b) => (true, a + b, null)),
            new Operation("Subtrahieren", (a,b) => (true, a - b, null)),
            new Operation("Multiplizieren", (a,b) => (true, a * b, null)),
            new Operation("Dividieren", (a,b) =>
            {
                if (Math.Abs(b) < 1e-12)
                    return (false, 0 , "Division durch 0 ist nicht möglich.");
                return (true, a/b, null);
            })
        };
    }

    public void Run()
    {
        /*Console.WriteLine("Achtung Baustelle");
        Console.WriteLine("Hier entsteht ein Taschenrechner");
        Console.ReadLine();
        */
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Rechner ===");

            for (int i = 0; i < _operations.Count; i++)
            {
                Console.WriteLine($"{i+1}) {_operations[i].Name}");
            }

            Console.WriteLine("0) Zurück");
            Console.Write("Auswahl: ");

            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("Bitte eine Menünummer eingeben. Enter...");
                Console.ReadLine();
                continue;
            }
            if (choice == 0) return;

            int index =  choice -1;

            if (index < 0 || index >= _operations.Count)
            {
                Console.WriteLine("Ungültige Auswahl. Enter...");
                Console.ReadLine();
                continue;
            }

            Console.Clear();
            
            var operation = _operations[index];

            double a = ReadDouble("Zahl 1: ");
            double b = ReadDouble("Zahl 2: ");

            var (success, result, error) = operation.TryExecute(a,b);

            if (success)
            {
                Console.WriteLine($"Ergebnis: {result}");
            }
            else
            {
                Console.WriteLine(error ?? "Rechnung nicht möglich.");
            }

            Console.WriteLine("Enter zum Fortfahren...");
            Console.ReadLine();
            
        }
    }

    private double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                continue;
            
            input = input.Replace(',','.');

            if (double.TryParse(input, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double value))
            {
                return value;
            }
        }

    }

    private class Operation
    {
        public string Name { get; }
        public Func<double, double, (bool success, double result, string? error)> _impl;

        public Operation(string name, Func<double, double, (bool, double, string?)> impl)
        {
            Name =  name;
            _impl = impl;
        }

        public (bool success, double result, string? error) TryExecute(double a, double b) => _impl(a,b);
    }
}