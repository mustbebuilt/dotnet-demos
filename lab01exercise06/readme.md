# Lab 01 - Exercise 06: Online Shopping System (Comprehensive OOP)

## Overview
This exercise integrates all core Object-Oriented Programming concepts into a unified **Online Shopping System**, featuring **composition**, **associations**, **interfaces**, **collections**, and **defensive error handling**.

---

## Key Concepts Explained

### 1. Composition (`Order` $\rightarrow$ `OrderItem`)
Composition is a strong "has-a" relationship where the child object's lifecycle is bound to the parent object:
- An `Order` **composes** `OrderItem`s (`Order 1 *-- 1..* OrderItem`).
- An `OrderItem` only exists as part of a specific `Order`.
- The `Order` encapsulates methods like `AddItem(Product, quantity)` and `CalculateTotal()`.

### 2. Association (`Customer` $\rightarrow$ `Order` & `OrderItem` $\rightarrow$ `Product`)
Association is a looser relationship where objects interact or maintain references:
- A `Customer` places and holds a history of `Order`s (`Customer 1 --> 0..* Order`).
- An `OrderItem` references an existing `Product` (`OrderItem --> 1 Product`).

### 3. Interface Abstraction (`PaymentMethod`)
- The `Order` and `Customer` classes process transactions via the `PaymentMethod` interface (`CreditCardPayment`, `PayPalPayment`), decoupling domain order logic from specific payment gateways.

### 4. Defensive Programming & Error Handling
Robust applications validate state before performing operations:
- Guarding against invalid parameters (`ArgumentException` for negative prices or quantities $< 1$).
- Preventing illegal state transitions (`InvalidOperationException` when checking out an empty order, adding items to a paid order, or double-paying).

```csharp
public void ProcessPayment(PaymentMethod paymentMethod)
{
    if (items.Count == 0)
        throw new InvalidOperationException("Cannot checkout an empty order.");
    if (isPaid)
        throw new InvalidOperationException($"Order {orderNumber} has already been paid.");
    
    paymentMethod.Pay(CalculateTotal());
    this.isPaid = true;
}
```

---

## Code Walkthrough

- **`Product`**: Represents items in the catalog (`productId`, `name`, `price`).
- **`OrderItem`**: Encapsulates a product reference and purchase quantity, computing line subtotal.
- **`Order`**: Composes items, computes the total sum, executes payment via `PaymentMethod`, and prints itemized receipts.
- **`Customer`**: Owns customer details and an order history list.
- **`PaymentMethod`**: Interface with `CreditCardPayment` and `PayPalPayment` implementations.
- **`Program.Main()`**:
  - Sets up a catalog and customer base.
  - Demonstrates end-to-end checkout with credit card and PayPal.
  - Tests error handling (negative quantities, empty order checkout, double payments).
  - Outputs itemized receipts and full customer order histories.

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
dotnet run --project lab01exercise06
```
