# MyTodo

A small C# / .NET 9 todo manager exploring clean separation between domain, use cases, and UI. Console front end, JSON or SQLite storage.

Quick study, the todos are an excuse. What I actually wanted was to see how far the domain could be pushed away from the thing displaying it, and where that stops being worth the effort.

## Three projects, arrows pointing one way

```
(View/Infrastructure)
MyTodo.CLI     ->     MyTodo (Domain)
               ->     Config (Tools)
MyTodo.CLI constructs the entire application and links usecases with views and infrastructure
```
## Goals
- Command history with undo and redos
- MVVM, MVC and MVP Layers
### Frontend types 
- Spectre CLI
- Unity
- WPF/Windows Forms
