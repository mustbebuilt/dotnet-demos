# Lab 01 - Exercise 03: Aggregation & Object Collaboration

## Overview
This exercise models a **Library Management System** demonstrating **aggregation** and **object collaboration** across three classes: `Library`, `Book`, and `Member`.

---

## Key Concepts Explained

### 1. Aggregation (`HAS-A` Relationship)
Aggregation represents a whole-part relationship where the parts can exist independently of the whole:
- A `Library` aggregates `Book`s and `Member`s (`Library o-- Book`, `Library o-- Member`).
- If the `Library` ceases to exist, the `Book` and `Member` objects can still exist in memory.

### 2. Object Collaboration
Classes work together by invoking methods on each other to fulfill business requirements:
- The `Library` acts as a coordinator / facade.
- When `Library.BorrowBook(isbn, member)` is invoked:
  1. It validates that the `Member` is registered.
  2. It locates the `Book` matching the given ISBN using `.Find()`.
  3. It queries `book.IsAvailable()`.
  4. It delegates state change to `book.Borrow()`.

### 3. State Management & Defensive Validation
- The `Book` class guards its internal state: `Borrow()` throws an exception if the book is already unavailable, and `ReturnBook()` throws an exception if it is already available.
- The `Library` performs domain checks and outputs clear user feedback for successful and failed operations.

---

## Code Walkthrough

```csharp
// Book manages availability state
public class Book
{
    private string title;
    private string isbn;
    private bool available;

    public void Borrow()
    {
        if (!available) throw new InvalidOperationException("Unavailable");
        available = false;
    }

    public void ReturnBook()
    {
        if (available) throw new InvalidOperationException("Cannot return: book is not currently checked out");
        available = true;
    }
}

// Library coordinates borrowing and returning
public class Library
{
    private List<Book> books;
    private List<Member> members;

    public void BorrowBook(string isbn, Member member)
    {
        if (!members.Contains(member)) return;
        Book? book = books.Find(b => b.GetIsbn() == isbn);
        if (book != null && book.IsAvailable())
        {
            book.Borrow();
        }
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
dotnet run --project lab01exercise03
```
