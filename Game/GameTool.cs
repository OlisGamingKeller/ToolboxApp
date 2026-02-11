using System;

namespace ToolboxApp.Game;

public class GameTool : ITool
{
    public string Name => "Spiel (WiP)";

    public void Run()
    {
        Console.WriteLine("Achtung Baustelle");
        Console.WriteLine("Hier entsteht ein Text-Adventure");
        Console.ReadLine();
    }
}