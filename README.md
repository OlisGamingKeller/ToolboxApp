# ToolboxApp

ToolboxApp is a modular console application written in C#.

I am building it as part of my retraining as a software developer (`Fachinformatiker fuer Anwendungsentwicklung`) and using it as a place to practice clean structure, simple architecture, and adding new features step by step.

Instead of creating one large app, the idea is to collect smaller tools inside one shared toolbox. Some parts are already working, others are still work in progress.

## Current Status

Right now, the project contains:

- a working calculator
- a small hello/test tool
- a LEET translator in progress
- placeholders for a password generator, password manager, and a game

This is still a learning project, so the codebase is evolving as I learn and refactor.

## Features

### Calculator

The calculator is the most complete tool at the moment.

It currently supports:

- addition
- subtraction
- multiplication
- division
- validation against division by zero
- input with either `,` or `.` as decimal separator
- formatted output rounded for display while keeping `double` precision internally

Implementation notes:

- operations are stored as objects in a list
- each operation carries its own calculation logic
- the calculator menu is generated dynamically from the available operations
- the calculation logic is separated from the console UI
- validation is handled through a `TryExecute`-style approach

### LEET Translator

The LEET tool is currently work in progress.

At the moment it can:

- read text from console input
- read text from supported files (`.txt`, `.md`)
- normalize input to uppercase
- try to detect whether the text looks like plain text or LEET
- translate in both directions
- ask the user to confirm or invert the detected direction
- save translated output to a supported file
- show a small info/rules page loaded from a markdown file

The current implementation is also split into smaller responsibilities:

- `LeetTool` handles menu flow and console interaction
- `LeetTranslator` contains the translation and detection logic
- `LeetFileService` handles file input/output and file type checks

### Other Tools

The following tools already exist in the menu, but are currently placeholders:

- Password Generator
- Password Manager
- Game / Text Adventure
- Hello Tool

## Project Structure

The application uses a simple modular structure:

- `Program.cs` creates the available tools and starts the app
- `ToolboxApp.cs` contains the main menu and tool selection logic
- `ITool.cs` defines the shared interface for all tools
- each tool lives in its own folder
- helper classes can live alongside a tool when logic grows beyond a single file

All tools implement the same interface:

```csharp
public interface ITool
{
    string Name { get; }
    void Run();
}
```

That makes it easy to add a new tool without changing the overall application design.

## Tech Stack

- C#
- .NET 10
- console application architecture
- nullable reference types enabled

## Running The Project

You can start the application with:

```bash
dotnet run
```

## Line Endings

The repository uses a `.gitattributes` file to keep line endings consistent across files and platforms.

- C# / .NET project files use `CRLF`
- Markdown and plain text files use `LF`

## Why This Project Exists

This project is mainly a learning playground where I can practice:

- structuring a console application
- separating responsibilities between UI and logic
- working with interfaces and modular design
- handling input validation
- extending an existing codebase without rewriting everything

## Notes

- The project is still in active development.
- Some tool names and console output are currently in German.
- The codebase is being refactored gradually while new features are added.
