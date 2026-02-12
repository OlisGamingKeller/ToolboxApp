using System;

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
    public void Run()
    {
        Console.WriteLine("Achtung Baustelle");
        Console.WriteLine("Hier entsteht ein LEET-Speech-Übersetzer");
        Console.ReadLine();
    }
}