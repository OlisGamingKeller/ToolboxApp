using System;
using System.Collections.Generic;
using System.Text;
using ToolboxApp;
using ToolboxApp.Calculator;
using ToolboxApp.Game;
using ToolboxApp.Hello;
using ToolboxApp.Leet;
using ToolboxApp.PasswordGenerator;
using ToolboxApp.PasswordManager;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

var tools = new List<ITool>
{
    new HelloTool(),
    new CalculatorTool(),
    new LeetTool(),
    new PasswordGeneratorTool(),
    new PasswordManagerTool(),
    new GameTool(),
    // spaeter: new CalculatorTool(), new LeetTranslatorTool(), ...
};

var app = new ToolboxApplication(tools);
app.Run();
