# ToolboxApp (C# Learning Project)

## 📌 Overview

ToolboxApp is a modular console-based application written in C#.  
It is part of my learning journey during my retraining as a software developer (Fachinformatiker für Anwendungsentwicklung).

The goal of this project is not just to “make it work”, but to practice:

- Clean structure
- Separation of concerns
- Extensibility
- Nullable reference types
- Delegates and functional patterns
- Thoughtful architecture decisions

The application is designed as a toolbox containing multiple independent tools.

---

## 🧰 Current Tools

### 🧮 Calculator
A modular calculator with the following features:

- Addition
- Subtraction
- Multiplication
- Division (with validation against division by zero)
- Configurable result formatting (rounded to 10 decimal places)
- Internal double precision with display formatting separated from calculation

### Design Decisions

- Operations are stored in a list of `Operation` objects.
- Each operation contains:
  - A display name
  - A delegate representing the calculation logic
  - Built-in validation via a `TryExecute` pattern
- The calculator UI does not use switch-statements for operations.
- Calculation logic is decoupled from presentation logic.
- Results are formatted separately from internal precision.

---

## 🏗 Architecture

The application follows a simple modular structure:

- `Program.cs` → Entry point
- `ITool` → Interface defining a toolbox tool
- `ToolboxApp` → Main menu and tool selection
- Individual tool folders (Calculator, Leet, PasswordManager, etc.)

Each tool implements:

```csharp
public interface ITool
{
    string Name { get; }
    void Run();
}
