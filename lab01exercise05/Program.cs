using System;
using System.Collections.Generic;

// 1. Interface definition
public interface PaymentMethod
{
    void Pay(double amount);

    // Support camelCase from specification
    void pay(double amount) => Pay(amount);
}

// 2. Concrete Implementation: Credit Card
public class CreditCardPayment : PaymentMethod
{
    private string cardNumber;
    private string cardHolder;

    public CreditCardPayment(string cardNumber, string cardHolder)
    {
        this.cardNumber = cardNumber;
        this.cardHolder = cardHolder;
    }

    public void Pay(double amount)
    {
        string maskedNumber = cardNumber.Length >= 4
            ? $"****-****-****-{cardNumber.Substring(cardNumber.Length - 4)}"
            : cardNumber;
        Console.WriteLine($"[Credit Card]   Paid £{amount:N2} using card {maskedNumber} (Cardholder: {cardHolder})");
    }

    public void pay(double amount) => Pay(amount);
}

// 3. Concrete Implementation: PayPal
public class PayPalPayment : PaymentMethod
{
    private string email;

    public PayPalPayment(string email)
    {
        this.email = email;
    }

    public void Pay(double amount)
    {
        Console.WriteLine($"[PayPal]        Paid £{amount:N2} using PayPal account ({email})");
    }

    public void pay(double amount) => Pay(amount);
}

// 4. Concrete Implementation: Bank Transfer
public class BankTransferPayment : PaymentMethod
{
    private string sortCode;
    private string accountNumber;

    public BankTransferPayment(string sortCode, string accountNumber)
    {
        this.sortCode = sortCode;
        this.accountNumber = accountNumber;
    }

    public void Pay(double amount)
    {
        string maskedAccount = accountNumber.Length >= 4
            ? $"****{accountNumber.Substring(accountNumber.Length - 4)}"
            : accountNumber;
        Console.WriteLine($"[Bank Transfer] Paid £{amount:N2} via Sort Code: {sortCode}, Account: {maskedAccount}");
    }

    public void pay(double amount) => Pay(amount);
}

// 5. Service consuming the interface (demonstrates loose coupling & extensibility)
public class CheckoutService
{
    public void ProcessPayment(PaymentMethod method, double amount)
    {
        if (method == null)
        {
            throw new ArgumentNullException(nameof(method), "Payment method cannot be null.");
        }
        if (amount <= 0)
        {
            throw new ArgumentException("Payment amount must be greater than zero.", nameof(amount));
        }

        Console.Write("Processing checkout... ");
        method.Pay(amount);
    }

    // Support camelCase from specification
    public void processPayment(PaymentMethod method, double amount) => ProcessPayment(method, amount);
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("==========================================================");
        Console.WriteLine(" Payment Processing System - Interfaces (Exercise 5)");
        Console.WriteLine("==========================================================\n");

        CheckoutService checkout = new CheckoutService();

        // 1. Instantiate concrete payment methods
        PaymentMethod card = new CreditCardPayment("4532758912345678", "Alice Johnson");
        PaymentMethod paypal = new PayPalPayment("bob.smith@example.co.uk");
        PaymentMethod bank = new BankTransferPayment("20-00-00", "12345678");

        // 2. Demonstration via CheckoutService (Loose Coupling)
        Console.WriteLine("--- Section 1: Invocations via CheckoutService ---");
        checkout.ProcessPayment(card, 149.99);
        checkout.ProcessPayment(paypal, 45.50);
        checkout.ProcessPayment(bank, 1250.00);

        // 3. Demonstration using a Collection of Interface references
        Console.WriteLine("\n--- Section 2: Batch Processing via Interface Collection ---");
        List<(PaymentMethod Method, double Amount)> pendingTransactions = new List<(PaymentMethod, double)>
        {
            (card, 29.95),
            (paypal, 89.00),
            (bank, 500.00),
            (new CreditCardPayment("5412751234569876", "Charlie Davis"), 74.20)
        };

        double totalProcessed = 0;
        foreach (var transaction in pendingTransactions)
        {
            transaction.Method.Pay(transaction.Amount);
            totalProcessed += transaction.Amount;
        }

        Console.WriteLine(new string('-', 58));
        Console.WriteLine($"{"Total Processed:",-44} £{totalProcessed:N2}");
        Console.WriteLine("==========================================================");
    }
}
