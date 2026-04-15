namespace ToolboxApp.Calculator;

/*
CalculatorTool ist ein Toolbox-Tool mit eigenem Untermenue.
Design: Rechenoperationen werden als Liste von Operation-Objekten gespeichert.
        Jede Operation enthaelt Name + Berechnungslogik (Delegate) + eigene Validierung (TryExecute),
        sodass Run() keine switch/if-Kaskaden braucht und leicht erweiterbar bleibt.
*/
public class CalculatorTool : ITool
{
    public string Name => "Taschenrechner";

    /*
    Enthaelt alle verfuegbaren Rechenoperationen.
    readonly: Die Liste wird im Konstruktor einmal aufgebaut und danach nur noch gelesen.
    Neue Operationen lassen sich hinzufuegen, ohne Run() anfassen zu muessen.
    */
    private readonly List<Operation> _operations;

    /*
    Initialisiert die Operationen.
    Jede Operation liefert ein Tuple zurueck: (success, result, error).
    Bei Fehlern, z. B. Division durch 0, wird success=false und eine Fehlermeldung gesetzt.
    */
    public CalculatorTool()
    {
        _operations = new List<Operation>
        {
            new Operation("Addieren", (a, b) => (true, a + b, null)),
            new Operation("Subtrahieren", (a, b) => (true, a - b, null)),
            new Operation("Multiplizieren", (a, b) => (true, a * b, null)),
            new Operation("Dividieren", (a, b) =>
            {
                if (Math.Abs(b) < 1e-12)
                {
                    return (false, 0, "Division durch 0 ist nicht moeglich.");
                }

                return (true, a / b, null);
            })
        };
    }

    public void Run()
    {
        while (true)
        {
            // 1) Menue anzeigen, dynamisch aus _operations generiert
            Console.Clear();
            Console.WriteLine("=== Rechner ===");

            for (int i = 0; i < _operations.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {_operations[i].Name}");
            }

            Console.WriteLine("0) Zurueck");
            Console.Write("Auswahl: ");

            // 2) Menueauswahl einlesen und validieren
            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("Bitte geben Sie eine Menunummer ein. Enter...");
                Console.ReadLine();
                continue;
            }
            if (choice == 0) return;

            // 3) Ausgewaehlte Operation bestimmen, Menuepunkt zu Listenindex
            int index = choice - 1;

            if (index < 0 || index >= _operations.Count)
            {
                Console.WriteLine("Ungueltige Auswahl. Enter...");
                Console.ReadLine();
                continue;
            }

            Console.Clear();

            var operation = _operations[index];

            // 4) Zwei Zahlen einlesen, Komma oder Punkt ist erlaubt
            double a = ReadDouble("Zahl 1: ");
            double b = ReadDouble("Zahl 2: ");

            // 5) Operation ausfuehren, jede Operation prueft selbst ihre Gueltigkeit
            var (success, result, error) = operation.TryExecute(a, b);

            // 6) Ausgabe, Ergebnis formatiert oder Fehlertext bei Misserfolg
            if (success)
            {
                Console.WriteLine($"Ergebnis: {FormatResult(result)}");
            }
            else
            {
                Console.WriteLine(error ?? "Rechnung nicht moeglich.");
            }

            Console.WriteLine("Enter zum Fortfahren...");
            Console.ReadLine();
        }
    }

    /*
    Liest eine Zahl als double ein.
    Akzeptiert sowohl "1,7" als auch "1.7" durch Normalisierung auf '.' und InvariantCulture.
    Die Schleife laeuft so lange, bis eine gueltige Zahl eingegeben wurde.
    */
    private double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                continue;
            }

            input = input.Replace(',', '.');

            if (double.TryParse(input, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double value))
            {
                return value;
            }
        }
    }

    /*
    Formatiert das Ergebnis fuer die Anzeige wie bei einem Taschenrechner:
    intern double-Praezision, aber fuer die Anzeige auf 10 Stellen gerundet,
    ohne unnoetige Nachkommastellen.
    */
    private string FormatResult(double value)
    {
        double rounded = Math.Round(value, 10);
        return rounded.ToString("0.##########");
    }

    /*
    Hilfsklasse nur fuer CalculatorTool:
    Kapselt Name + Implementierung der Berechnung.
    _impl ist ein Delegate, sodass jede Operation ihre Logik und Validierung selbst mitbringt.
    TryExecute gibt ein Tuple zurueck: success/result/error, aehnlich wie TryParse.
    */
    private class Operation
    {
        public string Name { get; }
        private readonly Func<double, double, (bool success, double result, string? error)> _impl;

        public Operation(string name, Func<double, double, (bool, double, string?)> impl)
        {
            Name = name;
            _impl = impl;
        }

        public (bool success, double result, string? error) TryExecute(double a, double b) => _impl(a, b);
    }
}
