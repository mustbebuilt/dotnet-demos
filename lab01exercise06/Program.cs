using System;
using System.Collections.Generic;

// ==========================================
// 1. Interfaces & Payment Implementations
// ==========================================
public interface PaymentMethod
{
    void Pay(double amount);

    // Support camelCase from specification
    void pay(double amount) => Pay(amount);
}

public class CreditCardPayment : PaymentMethod
{
    private string cardNumber;
    private string cardHolder;

    public CreditCardPayment(string cardNumber, string cardHolder)
    {
        if (string.IsNullOrWhiteSpace(cardNumber))
            throw new ArgumentException("Card number cannot be empty.", nameof(cardNumber));
        if (string.IsNullOrWhiteSpace(cardHolder))
            throw new ArgumentException("Cardholder name cannot be empty.", nameof(cardHolder));

        this.cardNumber = cardNumber;
        this.cardHolder = cardHolder;
    }

    public void Pay(double amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Payment amount must be greater than zero.", nameof(amount));

        string masked = cardNumber.Length >= 4
            ? $"****-****-****-{cardNumber.Substring(cardNumber.Length - 4)}"
            : cardNumber;
        Console.WriteLine($"  💳 [Credit Card] Paid £{amount:N2} using card {masked} (Holder: {cardHolder})");
    }

    public void pay(double amount) => Pay(amount);
}

public class PayPalPayment : PaymentMethod
{
    private string email;

    public PayPalPayment(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new ArgumentException("A valid email address is required.", nameof(email));

        this.email = email;
    }

    public void Pay(double amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Payment amount must be greater than zero.", nameof(amount));

        Console.WriteLine($"  🅿️ [PayPal]      Paid £{amount:N2} using account ({email})");
    }

    public void pay(double amount) => Pay(amount);
}

// ==========================================
// 2. Product Class
// ==========================================
public class Product
{
    private string productId;
    private string name;
    private double price;

    public Product(string productId, string name, double price)
    {
        if (string.IsNullOrWhiteSpace(productId))
            throw new ArgumentException("Product ID cannot be empty.", nameof(productId));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name cannot be empty.", nameof(name));
        if (price < 0)
            throw new ArgumentException("Price cannot be negative.", nameof(price));

        this.productId = productId;
        this.name = name;
        this.price = price;
    }

    // Overload constructor matching diagram: Product(name : String, price : double)
    public Product(string name, double price)
        : this(Guid.NewGuid().ToString().Substring(0, 6).ToUpper(), name, price)
    {
    }

    public string GetProductId() => productId;
    public string GetName() => name;
    public double GetPrice() => price;

    public override string ToString()
    {
        return $"[{productId}] {name} - £{price:N2}";
    }
}

// ==========================================
// 3. OrderItem Class (Part of Order Composition)
// ==========================================
public class OrderItem
{
    private Product product;
    private int quantity;

    public OrderItem(Product product, int quantity)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product), "Product cannot be null.");
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be at least 1.", nameof(quantity));

        this.product = product;
        this.quantity = quantity;
    }

    public Product GetProduct() => product;
    public int GetQuantity() => quantity;

    public double GetSubtotal()
    {
        return product.GetPrice() * quantity;
    }

    public override string ToString()
    {
        return $"{product.GetName(),-24} x {quantity,2} @ £{product.GetPrice(),7:N2} = £{GetSubtotal(),8:N2}";
    }
}

// ==========================================
// 4. Order Class (Composition of OrderItems)
// ==========================================
public class Order
{
    private string orderNumber;
    private List<OrderItem> items;
    private bool isPaid;
    private DateTime createdAt;

    public Order(string orderNumber)
    {
        if (string.IsNullOrWhiteSpace(orderNumber))
            throw new ArgumentException("Order number cannot be empty.", nameof(orderNumber));

        this.orderNumber = orderNumber;
        this.items = new List<OrderItem>();
        this.isPaid = false;
        this.createdAt = DateTime.Now;
    }

    public string GetOrderNumber() => orderNumber;
    public bool IsPaid() => isPaid;
    public DateTime GetCreatedAt() => createdAt;
    public List<OrderItem> GetItems() => new List<OrderItem>(items);

    public void AddItem(Product product, int quantity)
    {
        if (isPaid)
            throw new InvalidOperationException("Cannot add items to an already paid order.");

        OrderItem item = new OrderItem(product, quantity);
        items.Add(item);
    }

    public double CalculateTotal()
    {
        double total = 0;
        foreach (OrderItem item in items)
        {
            total += item.GetSubtotal();
        }
        return total;
    }

    // Support camelCase from specification
    public double calculateTotal() => CalculateTotal();

    public void ProcessPayment(PaymentMethod paymentMethod)
    {
        if (items.Count == 0)
            throw new InvalidOperationException("Cannot checkout an empty order.");
        if (isPaid)
            throw new InvalidOperationException($"Order {orderNumber} has already been paid.");
        if (paymentMethod == null)
            throw new ArgumentNullException(nameof(paymentMethod), "A payment method must be provided.");

        double total = CalculateTotal();
        Console.WriteLine($"\n[Order #{orderNumber}] Processing total payment of £{total:N2}...");
        paymentMethod.Pay(total);
        this.isPaid = true;
        Console.WriteLine($"[Order #{orderNumber}] Status: PAID ✓");
    }

    public void PrintReceipt()
    {
        Console.WriteLine($"\n------------------------------------------------------------");
        Console.WriteLine($" RECEIPT - Order #{orderNumber} | Date: {createdAt:yyyy-MM-dd HH:mm}");
        Console.WriteLine($" Status: {(isPaid ? "PAID ✓" : "UNPAID ✗")}");
        Console.WriteLine($"------------------------------------------------------------");
        foreach (OrderItem item in items)
        {
            Console.WriteLine($"  {item}");
        }
        Console.WriteLine($"------------------------------------------------------------");
        Console.WriteLine($" {"TOTAL:",-39} £{CalculateTotal(),8:N2}");
        Console.WriteLine($"------------------------------------------------------------");
    }
}

// ==========================================
// 5. Customer Class (Association with Orders)
// ==========================================
public class Customer
{
    private string customerId;
    private string name;
    private List<Order> orders;

    public Customer(string customerId, string name)
    {
        if (string.IsNullOrWhiteSpace(customerId))
            throw new ArgumentException("Customer ID cannot be empty.", nameof(customerId));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Customer name cannot be empty.", nameof(name));

        this.customerId = customerId;
        this.name = name;
        this.orders = new List<Order>();
    }

    public string GetCustomerId() => customerId;
    public string GetName() => name;
    public List<Order> GetOrders() => new List<Order>(orders);

    public void PlaceOrder(Order order, PaymentMethod paymentMethod)
    {
        if (order == null)
            throw new ArgumentNullException(nameof(order), "Order cannot be null.");
        if (paymentMethod == null)
            throw new ArgumentNullException(nameof(paymentMethod), "Payment method cannot be null.");

        Console.WriteLine($"\n>>> Customer '{name}' is placing Order #{order.GetOrderNumber()}...");
        order.ProcessPayment(paymentMethod);
        orders.Add(order);
    }

    // Support camelCase from specification
    public void placeOrder(Order order, PaymentMethod paymentMethod) => PlaceOrder(order, paymentMethod);

    public void DisplayOrderHistory()
    {
        Console.WriteLine($"\n============================================================");
        Console.WriteLine($" Order History for Customer: {name} (ID: {customerId})");
        Console.WriteLine($" Total Orders Placed: {orders.Count}");
        Console.WriteLine($"============================================================");
        if (orders.Count == 0)
        {
            Console.WriteLine(" No orders placed yet.");
            return;
        }

        foreach (Order order in orders)
        {
            order.PrintReceipt();
        }
    }
}

// ==========================================
// 6. Main Program & Demonstration
// ==========================================
public class Program
{
    public static void Main()
    {
        Console.WriteLine("============================================================");
        Console.WriteLine("  Online Shopping System (Exercise 6)");
        Console.WriteLine("============================================================\n");

        // 1. Create Product Catalog
        Product laptop = new Product("PROD01", "Dell XPS 15 Laptop", 1299.99);
        Product mouse = new Product("PROD02", "Logitech Wireless Mouse", 29.50);
        Product keyboard = new Product("PROD03", "Mechanical Keyboard", 84.99);
        Product monitor = new Product("PROD04", "4K Ultra HD Monitor", 349.00);

        Console.WriteLine("--- Product Catalog ---");
        Console.WriteLine($"• {laptop}");
        Console.WriteLine($"• {mouse}");
        Console.WriteLine($"• {keyboard}");
        Console.WriteLine($"• {monitor}");

        // 2. Create Customers
        Customer alice = new Customer("CUST01", "Alice Johnson");
        Customer bob = new Customer("CUST02", "Bob Smith");

        // 3. Customer 1 places an order with Credit Card
        Console.WriteLine("\n------------------------------------------------------------");
        Console.WriteLine(" SCENARIO 1: Alice places an order paid by Credit Card");
        Console.WriteLine("------------------------------------------------------------");
        Order aliceOrder = new Order("ORD-1001");
        aliceOrder.AddItem(laptop, 1);
        aliceOrder.AddItem(mouse, 2);
        aliceOrder.AddItem(keyboard, 1);

        PaymentMethod creditCard = new CreditCardPayment("4532758912345678", "Alice Johnson");
        alice.PlaceOrder(aliceOrder, creditCard);
        aliceOrder.PrintReceipt();

        // 4. Customer 2 places an order with PayPal
        Console.WriteLine("\n------------------------------------------------------------");
        Console.WriteLine(" SCENARIO 2: Bob places an order paid by PayPal");
        Console.WriteLine("------------------------------------------------------------");
        Order bobOrder = new Order("ORD-1002");
        bobOrder.AddItem(monitor, 2);
        bobOrder.AddItem(mouse, 1);

        PaymentMethod payPal = new PayPalPayment("bob.smith@example.co.uk");
        bob.PlaceOrder(bobOrder, payPal);
        bobOrder.PrintReceipt();

        // 5. Error Handling Demonstrations
        Console.WriteLine("\n------------------------------------------------------------");
        Console.WriteLine(" SCENARIO 3: Error Handling & System Validations");
        Console.WriteLine("------------------------------------------------------------");

        // Test A: Attempt to add items with invalid quantity
        try
        {
            Console.WriteLine("1. Testing invalid quantity (-1)...");
            Order badOrder = new Order("ORD-ERR1");
            badOrder.AddItem(laptop, -1);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"   [Expected Error Caught] {ex.Message}");
        }

        // Test B: Attempt to checkout an empty order
        try
        {
            Console.WriteLine("2. Testing checkout on empty order...");
            Order emptyOrder = new Order("ORD-EMPTY");
            emptyOrder.ProcessPayment(creditCard);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"   [Expected Error Caught] {ex.Message}");
        }

        // Test C: Attempt to pay an order twice
        try
        {
            Console.WriteLine("3. Testing paying an already-paid order...");
            aliceOrder.ProcessPayment(creditCard);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"   [Expected Error Caught] {ex.Message}");
        }

        // 6. Display Customer Order History
        alice.DisplayOrderHistory();
        bob.DisplayOrderHistory();

        Console.WriteLine("\n============================================================");
        Console.WriteLine("  Demonstration completed successfully.");
        Console.WriteLine("============================================================");
    }
}
