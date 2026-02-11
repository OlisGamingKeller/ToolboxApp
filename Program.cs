using System;
using System.Collections.Generic;
using ToolboxApp;
using ToolboxApp.Calculator;
using ToolboxApp.Game;
using ToolboxApp.Hello;
using ToolboxApp.Leet;
using ToolboxApp.PasswordGenerator;
using ToolboxApp.PasswordManager;


var tools = new List<ITool>
{
    new HelloTool(),
    new CalculatorTool(),
    new LeetTool(),
    new PasswordGeneratorTool(),
    new PasswordManagerTool(),
    new GameTool(),
    // später: new CalculatorTool(), new LeetTranslatorTool(), ...
};

var app = new ToolboxApplication(tools);
app.Run();
