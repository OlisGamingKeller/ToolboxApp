using System.Text;

namespace ToolboxApp.Leet;

public class LeetTranslator
{
    private readonly Dictionary<char, char> _plainToLeet;
    private readonly Dictionary<char, char> _leetToPlain;

    public LeetTranslator()
    {
        _plainToLeet = new Dictionary<char, char>
        {
            { 'A', '4' },
            { 'E', '3' },
            { 'I', '1' },
            { 'O', '0' },
            { 'S', '5' },
            { 'T', '7' }
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
    public string NormalizeInput(string input)
    {
        return input.ToUpper();
    }

    // Erkennen
    public bool IsLikelyLeet(string input)
    {
        foreach (char c in input)
        {
            if (_leetToPlain.ContainsKey(c))            //.ContainsKey(c) prüft Zeichen auf Übereinstimmung im Dictionary
            {
                return true;
            }
        }

        return false;
    }

    // Übersetzen
    public string Translate(string input, bool useLeetToPlain)
    {
        Dictionary<char, char> dictionary;

        if (useLeetToPlain)
        {
            dictionary = _leetToPlain;
        }
        else
        {
            dictionary = _plainToLeet;
        }

        StringBuilder builder = new StringBuilder();

        foreach (char c in input)
        {
            if (dictionary.TryGetValue(c, out char translatedChar))         // Prüft Zeichen auf Überinstimmung und tauscht gegebenenfalls aus
            {
                builder.Append(translatedChar);
            }
            else
            {
                builder.Append(c);
            }
        }

        return builder.ToString();
    }
}
