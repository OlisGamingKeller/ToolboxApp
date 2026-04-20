namespace ToolboxApp.Leet;

public class LeetFileService
{
    // Text aus Datei einlesen
    public string? ReadTextFromFile(string path)
    {
        string extension = Path.GetExtension(path).ToLowerInvariant();

        if (extension != ".txt" && extension != ".md")
        {
            return null;
        }
        
        if (!File.Exists(path))
        {
            return null;
        }

        try
        {
            string text = File.ReadAllText(path);

            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            return text;
        }
        catch
        {
            return null;
        }
    }

    // Ergebnis in Textdatei speichern
    public bool SaveTextToFile(string path, string text)
    {
        string extension = Path.GetExtension(path).ToLowerInvariant();
        if (extension != ".txt" && extension != ".md")
        {
            return false;
        }

        string? directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrWhiteSpace(directory) && !Directory.Exists(directory))
        {
            return false;
        }
        
        try
        {
            File.WriteAllText(path, text);
            return true;
        }
        catch
        {
            return false;
        }
    }

}
