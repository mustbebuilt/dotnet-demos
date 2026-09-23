# Lab 01 - Exercise 02: Multiple Related Classes (Many-to-Many Association)

## Overview
This exercise models a university system where **Students** and **Modules** share a **many-to-many (`0..* <-> 0..*`) relationship**:
- A student can enroll in multiple modules.
- A module can have multiple enrolled students.

---

## Key Concepts Explained

### 1. Bidirectional Associations
When two entities know about each other, they maintain references to each other. In C#, this is represented using generic collections (`List<Module>` inside `Student`, and `List<Student>` inside `Module`).

### 2. Referential Integrity & Avoiding Infinite Recursion
To keep both sides synchronized without entering an infinite loop:
- When `student.Enroll(module)` is called, it checks if `module` is already enrolled. If not, it adds it and calls `module.AddStudent(this)`.
- Inside `module.AddStudent(student)`, it checks `!students.Contains(student)` before adding, breaking any potential infinite recursion loop.

```csharp
public void Enroll(Module module)
{
    if (module != null && !modules.Contains(module))
    {
        modules.Add(module);
        module.AddStudent(this); // Keeps both sides in sync
    }
}
```

### 3. Generic Collections (`List<T>`)
The .NET `List<T>` class from `System.Collections.Generic` dynamically manages variable-sized arrays of objects, providing type safety and methods like `.Add()`, `.Contains()`, and `.Count`.

---

## Code Walkthrough

- **`Student`**: Encapsulates `studentId`, `name`, and an internal `List<Module>`. Exposes `Enroll()` and `GetModules()`.
- **`Module`**: Encapsulates `code`, `title`, and an internal `List<Student>`. Exposes `AddStudent()` and `GetStudents()`.
- **`Program.Main()`**:
  1. Instantiates 3 modules and 4 students.
  2. Enrolls students into multiple modules.
  3. Iterates and displays both perspectives (courses per student, and students per course).

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
dotnet run --project lab01exercise02
```
