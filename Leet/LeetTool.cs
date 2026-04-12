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
            if(_leetToPlain.ContainsKey(c))         //ContainsKey(c) prüft ob Zeichen in Dictionary
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
            if(dictionary.TryGetValue(c, out char translatedChar))      //Prüft, ob Zeichen als Leet-Key vorkommt
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

    public void Run()
    {
        Console.WriteLine("Achtung Baustelle");
        Console.WriteLine("Hier entsteht ein LEET-Speech-Übersetzer");
        Console.ReadLine();
    }
}