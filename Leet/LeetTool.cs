namespace ToolboxApp.Leet;

/* Idee:
LeetTool ist ein Toolbox-Tool mit eigenem Untermenü
Es dient als Übersetzer von LEET-Speech in beide Richtungen (Klartext <-> LEET)
Es soll automatisch erkennen, welche Form vorliegt. Man sollte das Ergebnis in einer .txt speichern können
und auch aus einer solchen einlesen können.
*/
public class LeetTool : ITool
{
    public string Name => "LEET-Speech-Übersetzer (WiP)";

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
        Console.WriteLine("Geben Sie einen zu übersetzenden Text ein");
        string? textInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(textInput))
        {
            Console.WriteLine("Bitte einen Text eingeben. Enter...");
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
            Console.WriteLine("Bitte einen gueltigen Pfad eingeben. Enter...");
            Console.ReadLine();
            return null;
        }

        string? textInput = _fileService.ReadTextFromFile(pathInput);

        if (textInput == null)
        {
            Console.WriteLine("Datei konnte nicht gelesen werden oder enthaelt keinen verwertbaren Text. Enter...");
            Console.ReadLine();
            return null;
        }

        return textInput;
    }

    // Text verarbeiten
    private void ProcessText(string textInput)
    {
        Console.Clear();

        //Normalisieren
        string normalizedInput = _translator.NormalizeInput(textInput);

        //Erkennen
        bool isLeet = _translator.IsLikelyLeet(normalizedInput);
        if (isLeet)
        {
            Console.WriteLine("Der Text wurde als Leet-Text erkannt");
        }
        else
        {
            Console.WriteLine("Der Text wurde als Plain-Text erkannt");
        }

        //User-Abfrage zur Absischerung & Dictionary festlegen
        Console.WriteLine("Ist das korrekt?");
        Console.WriteLine("1) Ja, stimmt. Bitte übersetzen.");
        Console.WriteLine("2) Nein, bitte umkehren und übersetzen.");
        Console.WriteLine("3) Ich bin mir nicht sicher. Übersetze mit dem ermittelten Ergebnis.");
        Console.WriteLine("0) Ich möchte den Text neu eingeben. Zurück.");
        Console.Write("Auswahl: ");

        string? inputTextcheck = Console.ReadLine();
        if (!int.TryParse(inputTextcheck, out int choiceText))
        {
            Console.WriteLine("Bitte eine Menünummer eingeben. Enter...");
            Console.ReadLine();
            return;
        }
        if (choiceText < 0 || choiceText > 3)
        {
            Console.WriteLine("Ungültige Auswahl. Enter...");
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

        //Übersetzen
        string result = _translator.Translate(normalizedInput, useLeetToPlain);

        Console.WriteLine($"Eingabe erkannt als: {(isLeet ? "Leet-Text" : "Plain-Text")}");
        Console.WriteLine($"Übersetzungsrichtung: {(useLeetToPlain ? "Leet -> Plain" : "Plain -> Leet")}");
        Console.WriteLine($"Ergebnis: \n{result}");
        Console.ReadLine();
    }

    public void Run()
    {
        while(true)
        {
            // Menü anzeigen
            Console.Clear();
            Console.WriteLine("Achtung Baustelle");
            Console.WriteLine("=== Leet-Übersetzer ===");
            Console.WriteLine("1) Text eingeben und übersetzen");
            Console.WriteLine("2) Text aus Datei einlesen und übersetzen");
            Console.WriteLine("0) Zurück");
            Console.Write("Auswahl: ");

            // Menüauswahl einlesen + validieren
            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int choice))
            {
                Console.WriteLine("Bitte eine Menünummer eingeben. Enter...");
                Console.ReadLine();
                continue;
            }
            if (choice < 0 || choice > 2)
            {
                Console.WriteLine("Ungültige Auswahl. Enter...");
                Console.ReadLine();
                continue;
            }
            if (choice == 0) return;
            // Menüpunkt 1 - Texteingabe einlesen, normalisieren, erkennen und übersetzen
            if (choice == 1)
            {
                string? textInput = ReadTextFromConsole();
                if (textInput == null) continue;
                ProcessText(textInput);              
            }
            // Menüpunkt 2 - Datei einlesen, Text validieren, normalisieren, erkennen und übersetzen
            else if (choice == 2)
            {
                string? textInput = ReadTextFromFile();
                if (textInput == null) continue;
                ProcessText(textInput);
            }
        }
    }
}