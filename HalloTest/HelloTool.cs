using System;

namespace ToolboxApp.Hello;

 public class HelloTool : ITool
{
    public string Name => "Hallo-Tool (Test)";

    public void Run()
    {
        Console.WriteLine("Hallo aus dem Tool!");
        Console.WriteLine("Enter zum Zurücksetzen...");
        Console.ReadLine();
    }
}