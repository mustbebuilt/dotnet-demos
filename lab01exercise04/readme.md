# Lab 01 - Exercise 04: Inheritance & Polymorphism

## Overview
This exercise models an **Employee Payroll Hierarchy** demonstrating **class inheritance**, **abstract classes**, and **runtime polymorphism** in C#.

---

## Key Concepts Explained

### 1. Class Inheritance (`IS-A` Relationship)
Inheritance allows derived classes to inherit fields, properties, and methods from a base class:
- `SalariedEmployee` **is an** `Employee`.
- `HourlyEmployee` **is an** `Employee`.
- Common state (`employeeId`, `name`) and logic reside in the `Employee` base class. Subclasses invoke the base constructor using `: base(employeeId, name)`.

### 2. Abstract Classes & Abstract Methods
- `public abstract class Employee`: Marked `abstract` so it cannot be instantiated directly; it serves as a common template.
- `public abstract double CalculatePay()`: An abstract method that declares the contract without providing an implementation. Every non-abstract derived class **must** provide its own implementation using the `override` keyword.

### 3. Dynamic (Runtime) Polymorphism
Polymorphism allows objects of different derived types to be treated uniformly through their base type reference:
- A single collection `List<Employee>` holds both `SalariedEmployee` and `HourlyEmployee` objects.
- During a loop, calling `emp.CalculatePay()` dynamically invokes the appropriate overridden method at runtime without needing type checks or `switch` statements.

```csharp
List<Employee> employees = new List<Employee>
{
    new SalariedEmployee("E001", "Alice", 84000.00),
    new HourlyEmployee("E002", "Bob", 25.50, 40.0)
};

foreach (Employee emp in employees)
{
    // Polymorphic invocation:
    double pay = emp.CalculatePay();
}
```

---

## Code Walkthrough

- **`Employee`**: Abstract base class with `employeeId`, `name`, and abstract `CalculatePay()`.
- **`SalariedEmployee`**: Overrides `CalculatePay()` to return monthly salary (`annualSalary / 12.0`).
- **`HourlyEmployee`**: Overrides `CalculatePay()` to return `hourlyRate * hoursWorked`.
- **`Program.Main()`**: Iterates through the polymorphic employee list, computes period pay, formats output in UK Sterling (`£`), and calculates total organization payroll.

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
dotnet run --project lab01exercise04
```
