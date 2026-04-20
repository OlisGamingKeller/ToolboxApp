using System;

namespace ToolboxApp.PasswordGenerator;

public class PasswordGeneratorTool : ITool
{
    public string Name => "Passwortgenerator (WiP)";

    public void Run()
    {
        Console.WriteLine("Achtung Baustelle");
        Console.WriteLine("Hier entsteht ein Passwortgenerator");
        Console.WriteLine("Zurueck. Enter...");
        Console.ReadLine();
    }
}