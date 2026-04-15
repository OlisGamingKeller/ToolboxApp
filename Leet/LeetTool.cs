using System;
using System.Text;

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

    private readonly Dictionary<char,char> _plainToLeet;
    private readonly Dictionary<char,char> _leetToPlain;

    // Konstruktor
    public LeetTool()
    {
        _plainToLeet = new Dictionary<char,char>
        {
            {'A','4'},
            {'E','3'},
            {'I','1'},
            {'O','0'},
            {'S','5'},
            {'T','7'}
        };
        _leetToPlain = new Dictionary<char, char>
        {
            { '4', 'A' },
            { '3', 'E' },
            { '1', 'I' },
            { '0', 'O' },
            { '5', 'S' },
            { '7', 'T' }
        };
    }

    // Normalisieren
    private string NormalizeInput(string input)
    {
        return input.ToUpper();
    }

    // Erkennen
    private bool IsLikelyLeet(string input)
    {
        foreach (char c in input)
        {
            if (_leetToPlain.ContainsKey(c))         //ContainsKey(c) prüft ob Zeichen in Dictionary
            {
                return true;
            }
        }
        return false;
    }

    // Übersetzen
    private string Translate(string input, Dictionary<char, char> dictionary)
    {
        StringBuilder builder = new StringBuilder();
        foreach (char c in input)
        {
            if (dictionary.TryGetValue(c, out char translatedChar))      //Prüft, ob Zeichen als Leet-Key vorkommt
            {
                builder.Append(translatedChar);                         //Übersetzten Wert anhängen
            }
            else
            {
                builder.Append(c);                                      //Originalzeichen anhängen
            }
        }
        return builder.ToString();
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
        Console.WriteLine("Geben Sie den Dateipfad ein:");                                      //Dateipfad eingeben
        string? pathInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(pathInput))
        {
            Console.WriteLine("Bitte einen gültigen Pfad eingeben. Enter...");
            Console.ReadLine();
            return null;
        }
        if (!File.Exists(pathInput))                                                            //Existiert die Datei
        {
            Console.WriteLine("Die Datei existiert nicht oder der Pfad ist falsch. Enter...");
            Console.ReadLine();
            return null;
        }
        // Kein Text in Datei & kann Datei überhaupt gelesen werden
        try
        {
            string? textInput = File.ReadAllText(pathInput);
            if (string.IsNullOrWhiteSpace(textInput))
            {
                Console.WriteLine("Die Datei enthält keinen verwertbaren Text. Enter...");
                Console.ReadLine();
                return null;
            }
            return textInput;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Datei konnte nicht gelesen werden. Enter...");
            Console.WriteLine(ex.Message);
            Console.ReadLine();
            return null;
        }
    }

    // Text verarbeiten
    private void ProcessText(string textInput)
    {
        Console.Clear();

        //Normalisieren
        string normalizedInput = NormalizeInput(textInput);
        //Erkennen
        bool isLeet = IsLikelyLeet(normalizedInput);
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
        string result;
        if (useLeetToPlain)
        {
            result = Translate(normalizedInput, _leetToPlain);
        }
        else
        {
            result = Translate(normalizedInput, _plainToLeet);
        }
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