namespace ToolboxApp.Leet;

public class LeetFileService
{
    public string? ReadTextFromFile(string path)
    {
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

}
