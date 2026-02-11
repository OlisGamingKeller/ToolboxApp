using System;

namespace ToolboxApp.PasswordManager;

public class PasswordManagerTool : ITool
{
    public string Name => "Passwortmanager (WiP)";

    public void Run()
    {
        Console.WriteLine("Achtung Baustelle");
        Console.WriteLine("Hier entsteht ein Passwortmanager");
        Console.ReadLine();
    }
}