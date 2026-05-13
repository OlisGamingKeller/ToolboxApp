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

    // Zeichen zählen
    private (int spaceCount, int relevantCount, int leetCount, int plainCount) CountTextCharacteristics(string input)
    {
        int spaceCount = 0;
        int relevantCount = 0;
        int leetCount = 0;
        int plainCount = 0;

        foreach (char c in input)
        {
            if (c == ' ')
            {
                spaceCount++;
            }
            if (char.IsLetterOrDigit(c))
            {
                relevantCount++;
                if (_leetToPlain.ContainsKey(c))
                {
                    leetCount++;
                }
                if (_plainToLeet.ContainsKey(c))
                {
                    plainCount++;
                }
            }
        }

        return (spaceCount, relevantCount, leetCount, plainCount);
    }

    // Erkennen
    public bool IsLikelyLeet(string input)
    {
        var (spaceCount, relevantCount, leetCount, plainCount) = CountTextCharacteristics(input);
        
        if (relevantCount == 0)
        {
            return false;
        }

        if (plainCount > leetCount)
        {
            return false;
        }

        if (spaceCount >= 2)
        {
            double ratio = (double)leetCount / relevantCount;
            return ratio >= 0.25;
            
        }

        else
        {
            if (input.Length >= 5)
            {
                if (leetCount >=2)
                {
                    double ratio = (double)leetCount / relevantCount;
                    return ratio >= 0.25;
                }
            }
        }

        return false;
    }

    // Uebersetzen
    public string Translate(string input, bool useLeetToPlain)              //useLeetToPlain wird in LeetTool von IsLikelyLeet übergegeben
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

        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];
            if (useLeetToPlain)
            {
                if (char.IsDigit(c))
                {
                    if (i < input.Length - 1 && char.IsDigit(input[i+1]))
                    {
                        // TODO: Ganze Ziffernfolge bestimmen und im Kontext entscheiden, ob Zahl oder Leet

                    }
                }
            }

            if (dictionary.TryGetValue(c, out char translatedChar))         // Sucht das Zeichen im Dictionary und ersetzt es bei Treffer
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
