# Lab 01 - Exercise 01: Classes, Objects & Encapsulation

## Overview
This exercise introduces the fundamentals of Object-Oriented Programming (OOP) in C# by modeling a `Book` class and instantiating it from an entry-point `Program`.

---

## Key Concepts Explained

### 1. Encapsulation & Information Hiding
Encapsulation bundles data (fields) and the operations on that data (methods) inside a single class, keeping internal state protected from unauthorized external modification:
- Fields (`title`, `author`, `isbn`) are marked `private` so they cannot be altered directly from outside the class.
- Getter methods (`GetTitle()`, `GetAuthor()`, `GetIsbn()`) provide controlled, read-only access.

### 2. Constructors
A constructor (`public Book(...)`) is a special method called when a new instance of a class is created. It initializes the object's initial state using the `this` keyword to distinguish between instance fields and parameter names.

### 3. Object Behavior
The `DisplayInfo()` method encapsulates the presentation logic for a book instance, utilizing string interpolation (`$"..."`) for clean formatting.

---

## Code Walkthrough

```csharp
public class Book
{
    // Private fields (encapsulation)
    private string title;
    private string author;
    private string isbn;

    // Constructor to initialize state
    public Book(string title, string author, string isbn)
    {
        this.title = title;
        this.author = author;
        this.isbn = isbn;
    }

    // Public accessors (getters)
    public string GetTitle() => title;
    public string GetAuthor() => author;
    public string GetIsbn() => isbn;

    // Behavior
    public void DisplayInfo()
    {
        Console.WriteLine("=== Book Details ===");
        Console.WriteLine($"Title : {title}");
        Console.WriteLine($"Author: {author}");
        Console.WriteLine($"ISBN  : {isbn}");
    }
}
```

---

## Diagrams

- **Class Diagram:** [ClassDiagram.puml](ClassDiagram.puml)
- **Sequence Diagram:** [SequenceDiagram.puml](SequenceDiagram.puml)

---

## How to Run

```bash
# From within this directory
dotnet build
dotnet run

# Or from the repository root
dotnet run --project lab01exercise01
```
