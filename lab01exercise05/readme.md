# Lab 01 - Exercise 05: Interfaces & Extensibility

## Overview
This exercise models a **Payment Processing System** illustrating how **interfaces** enable loose coupling, multiple implementations, and architectural extensibility.

---

## Key Concepts Explained

### 1. Interfaces as Contracts
An interface (`PaymentMethod`) defines a contract specifying **what** operations must be supported (`void Pay(double amount)`), without dictating **how** they must be implemented:
- Unlike classes, interfaces contain no stored state (fields).
- A class declares that it implements an interface using `: PaymentMethod`.

### 2. Multiple Implementations & Loose Coupling
Three distinct payment mechanisms implement the same `PaymentMethod` interface:
1. `CreditCardPayment`: Validates card details, masks sensitive digits, and prints credit card confirmation.
2. `PayPalPayment`: Validates email and prints PayPal transaction info.
3. `BankTransferPayment`: Masks bank account numbers and processes via Sort Code.

### 3. Open-Closed Principle (OCP) & Dependency Inversion (DIP)
- The `CheckoutService` depends on the **abstraction** (`PaymentMethod`), rather than any concrete class (`CreditCardPayment` or `PayPalPayment`):

```csharp
public class CheckoutService
{
    public void ProcessPayment(PaymentMethod method, double amount)
    {
        // High-level service does not care WHICH payment method is used
        method.Pay(amount);
    }
}
```
- **Extensibility**: You can add a new payment method (e.g. `ApplePayPayment`, `CryptoPayment`) at any time **without modifying a single line of `CheckoutService`**.

---

## Code Walkthrough

- **`PaymentMethod`**: The interface defining `Pay(double amount)`.
- **`CreditCardPayment` / `PayPalPayment` / `BankTransferPayment`**: Concrete implementations of the interface.
- **`CheckoutService`**: Processes payments through the interface reference.
- **`Program.Main()`**:
  1. Section 1 demonstrates single-payment processing through `CheckoutService`.
  2. Section 2 demonstrates batch processing over a collection of interface references `List<(PaymentMethod, double)>`.

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
dotnet run --project lab01exercise05
```
