namespace ToolboxApp.Leet;

/* Idee:
LeetTool ist ein Toolbox-Tool mit eigenem Untermenue.
Es dient als Uebersetzer von LEET-Speech in beide Richtungen (Klartext <-> LEET).
Es soll automatisch erkennen, welche Form vorliegt.
Man soll das Ergebnis in einer Datei speichern und aus einer Datei einlesen koennen.
*/
public class LeetTool : ITool
{
    public string Name => "LEET-Speech-Uebersetzer (WiP)";

    private readonly LeetTranslator _translator;
    private readonly LeetFileService _fileService;

    // Konstruktor
    public LeetTool()
    {
        _translator = new LeetTranslator();
        _fileService = new LeetFileService();
    }

    // Einlesen aus Konsoleneingabe
    private string? ReadTextFromConsole()
    {
        Console.Clear();
        Console.WriteLine("Geben Sie einen zu uebersetzenden Text ein.");
        string? textInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(textInput))
        {
            Console.WriteLine("Bitte geben Sie einen Text ein. Enter...");
            Console.ReadLine();
            return null;
        }
        return textInput;
    }

    // Einlesen aus Datei
    private string? ReadTextFromFile()
    {
        Console.Clear();
        Console.WriteLine("Geben Sie den Dateipfad ein:");
        string? pathInput = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(pathInput))
        {
            Console.WriteLine("Bitte geben Sie einen gueltigen Pfad ein. Enter...");
            Console.ReadLine();
            return null;
        }

        string? textInput = _fileService.ReadTextFromFile(pathInput);

        if (textInput == null)
        {
            Console.WriteLine("Die Datei konnte nicht gelesen werden, ist leer oder das Format wird nicht unterstuetzt. Enter...");
            Console.ReadLine();
            return null;
        }

        return textInput;
    }

    // Speicherabfrage und Speichern
    private void AskToSaveResult(string result)
    {
        Console.WriteLine("1) Ergebnis speichern");
        Console.WriteLine("0) Zurueck");
        Console.Write("Auswahl: ");
        string? inputSave = Console.ReadLine();
        if (!int.TryParse(inputSave, out int choiceSave))
        {
            Console.WriteLine("Bitte geben Sie eine Menunummer ein. Enter...");
            Console.ReadLine();
            return;
        }
        if (choiceSave < 0 || choiceSave > 1)
        {
            Console.WriteLine("Ungueltige Auswahl. Enter...");
            Console.ReadLine();
            return;
        }
        if (choiceSave == 0)
        {
            return;
        }

        Console.Clear();
        Console.WriteLine("Geben Sie einen Pfad zum Speichern an:");
        string? path = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(path))
        {
            Console.WriteLine("Bitte geben Sie einen gueltigen Pfad ein. Enter...");
            Console.ReadLine();
            return;
        }

        bool saved = _fileService.SaveTextToFile(path, result);

        if (saved)
        {
            Console.WriteLine("Die Datei wurde erfolgreich gespeichert. Enter...");
        }
        else
        {
            Console.WriteLine("Die Datei konnte nicht gespeichert werden. Enter...");
        }
        Console.ReadLine();
    }

    // Text verarbeiten
    private void ProcessText(string textInput)
    {
        Console.Clear();

        // Normalisieren
        string normalizedInput = _translator.NormalizeInput(textInput);

        // Erkennen
        bool isLeet = _translator.IsLikelyLeet(normalizedInput);
        if (isLeet)
        {
            Console.WriteLine("Der Text wurde als Leet-Text erkannt.");
        }
        else
        {
            Console.WriteLine("Der Text wurde als Plain-Text erkannt.");
        }

        // User-Abfrage zur Absicherung und Uebersetzungsrichtung festlegen
        Console.WriteLine("Ist das korrekt?");
        Console.WriteLine("1) Ja, stimmt. Bitte uebersetzen.");
        Console.WriteLine("2) Nein, bitte umkehren und uebersetzen.");
        Console.WriteLine("3) Ich bin mir nicht sicher. Uebersetze mit dem ermittelten Ergebnis.");
        Console.WriteLine("0) Ich moechte den Text neu eingeben. Zurueck.");
        Console.Write("Auswahl: ");

        string? inputTextcheck = Console.ReadLine();
        if (!int.TryParse(inputTextcheck, out int choiceText))
        {
            Console.WriteLine("Bitte geben Sie eine Menunummer ein. Enter...");
            Console.ReadLine();
            return;
        }
        if (choiceText < 0 || choiceText > 3)
        {
            Console.WriteLine("Ungueltige Auswahl. Enter...");
            Console.ReadLine();
            return;
        }
        if (choiceText == 0) return;

        bool useLeetToPlain = isLeet;
        if (choiceText == 2)
        {
            useLeetToPlain = !isLeet;
        }
        Console.Clear();

        // Uebersetzen
        string result = _translator.Translate(normalizedInput, useLeetToPlain);

        Console.WriteLine($"Eingabe erkannt als: {(isLeet ? "Leet-Text" : "Plain-Text")}");
        Console.WriteLine($"Uebersetzungsrichtung: {(useLeetToPlain ? "Leet -> Plain" : "Plain -> Leet")}");
        Console.WriteLine($"Ergebnis: \n{result}");
        AskToSaveResult(result);
    }

    // Informationen und Regeln fuer das Tool
    private void ShowInfo()
    {
        Console.Clear();

        string infoPath = "Leet/LeetInfo.md";
        string? infoText = _fileService.ReadTextFromFile(infoPath);

        if (infoText == null)
        {
            Console.WriteLine("Die Info-Datei konnte nicht gelesen werden.");
        }
        else
        {
            Console.WriteLine(infoText);
        }

        Console.WriteLine();
        Console.WriteLine("Zurueck. Enter...");
        Console.ReadLine();
    }

    // Ablauf
    public void Run()
    {
        while (true)
        {
            // Menue anzeigen
            Console.Clear();
            Console.WriteLine("Achtung Baustelle");
            Console.WriteLine("=== Leet-Uebersetzer ===");
            Console.WriteLine("1) Text eingeben und uebersetzen");
            Console.WriteLine("2) Text aus Datei einlesen und uebersetzen");
            Console.WriteLine("3) Info & Regeln");
            Console.WriteLine("0) Zurueck");
            Console.Write("Auswahl: ");

            // Menueauswahl einlesen und validieren
            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("Bitte geben Sie eine Menunummer ein. Enter...");
                Console.ReadLine();
                continue;
            }
            if (choice < 0 || choice > 3)
            {
                Console.WriteLine("Ungueltige Auswahl. Enter...");
                Console.ReadLine();
                continue;
            }
            if (choice == 0) return;

            // Menuepunkt 1 - Texteingabe einlesen, normalisieren, erkennen und uebersetzen
            if (choice == 1)
            {
                string? textInput = ReadTextFromConsole();
                if (textInput == null) continue;
                ProcessText(textInput);
            }
            // Menuepunkt 2 - Datei einlesen, Text validieren, normalisieren, erkennen und uebersetzen
            else if (choice == 2)
            {
                string? textInput = ReadTextFromFile();
                if (textInput == null) continue;
                ProcessText(textInput);
            }
            else if (choice == 3)
            {
                ShowInfo();
            }
        }
    }
}
