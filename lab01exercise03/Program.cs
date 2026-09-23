using System;
using System.Collections.Generic;

public class Member
{
    private string memberId;
    private string name;

    public Member(string memberId, string name)
    {
        this.memberId = memberId;
        this.name = name;
    }

    public string GetMemberId() => memberId;
    public string GetName() => name;

    public override string ToString()
    {
        return $"Member [{memberId}] {name}";
    }
}

public class Book
{
    private string title;
    private string isbn;
    private bool available;

    public Book(string title, string isbn)
    {
        this.title = title;
        this.isbn = isbn;
        this.available = true;
    }

    public string GetTitle() => title;
    public string GetIsbn() => isbn;
    public bool IsAvailable() => available;

    public void Borrow()
    {
        if (!available)
        {
            throw new InvalidOperationException($"Book '{title}' is currently unavailable.");
        }
        available = false;
    }

    // Support camelCase from specification
    public void borrow() => Borrow();

    public void ReturnBook()
    {
        if (available)
        {
            throw new InvalidOperationException($"Book '{title}' is already marked as available.");
        }
        available = true;
    }

    // Support camelCase from specification
    public void returnBook() => ReturnBook();

    public override string ToString()
    {
        string status = available ? "Available" : "Borrowed";
        return $"'{title}' (ISBN: {isbn}) - [{status}]";
    }
}

public class Library
{
    private string name;
    private List<Book> books;
    private List<Member> members;

    public Library(string name)
    {
        this.name = name;
        this.books = new List<Book>();
        this.members = new List<Member>();
    }

    public string GetName() => name;

    public void AddBook(Book book)
    {
        if (book != null && !books.Contains(book))
        {
            books.Add(book);
            Console.WriteLine($"[+] Book added to library: {book.GetTitle()} (ISBN: {book.GetIsbn()})");
        }
    }

    // Support camelCase from specification
    public void addBook(Book book) => AddBook(book);

    public void RegisterMember(Member member)
    {
        if (member != null && !members.Contains(member))
        {
            members.Add(member);
            Console.WriteLine($"[+] Member registered: {member.GetName()} (ID: {member.GetMemberId()})");
        }
    }

    // Support camelCase from specification
    public void registerMember(Member member) => RegisterMember(member);

    public void BorrowBook(string isbn, Member member)
    {
        if (!members.Contains(member))
        {
            Console.WriteLine($"[!] Error: Member '{member?.GetName() ?? "Unknown"}' is not registered with this library.");
            return;
        }

        Book? book = books.Find(b => b.GetIsbn() == isbn);
        if (book == null)
        {
            Console.WriteLine($"[!] Error: Book with ISBN '{isbn}' not found in library.");
            return;
        }

        if (!book.IsAvailable())
        {
            Console.WriteLine($"[X] Borrow Failed: '{book.GetTitle()}' is currently unavailable (already borrowed).");
            return;
        }

        book.Borrow();
        Console.WriteLine($"[✓] Borrowed: '{book.GetTitle()}' successfully borrowed by {member.GetName()}.");
    }

    // Support camelCase from specification
    public void borrowBook(string isbn, Member member) => BorrowBook(isbn, member);

    public void ReturnBook(string isbn)
    {
        Book? book = books.Find(b => b.GetIsbn() == isbn);
        if (book == null)
        {
            Console.WriteLine($"[!] Error: Book with ISBN '{isbn}' not found in library.");
            return;
        }

        if (book.IsAvailable())
        {
            Console.WriteLine($"[!] Notice: '{book.GetTitle()}' is already in the library (not currently borrowed).");
            return;
        }

        book.ReturnBook();
        Console.WriteLine($"[✓] Returned: '{book.GetTitle()}' has been returned to the library.");
    }

    // Support camelCase from specification
    public void returnBook(string isbn) => ReturnBook(isbn);

    public void DisplayInventory()
    {
        Console.WriteLine($"\n--- {name} Inventory ---");
        foreach (Book book in books)
        {
            Console.WriteLine($"  * {book}");
        }
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("==================================================");
        Console.WriteLine(" Library Management System (Exercise 3)");
        Console.WriteLine("==================================================\n");

        Library library = new Library("City Central Library");

        // 1. Registering Members
        Console.WriteLine("1. REGISTERING MEMBERS");
        Console.WriteLine("--------------------------------------------------");
        Member member1 = new Member("M001", "Alice Johnson");
        Member member2 = new Member("M002", "Bob Smith");
        Member member3 = new Member("M003", "Charlie Davis");

        library.RegisterMember(member1);
        library.RegisterMember(member2);
        library.RegisterMember(member3);

        // 2. Adding Books
        Console.WriteLine("\n2. ADDING BOOKS");
        Console.WriteLine("--------------------------------------------------");
        Book book1 = new Book("Clean Code", "9780132350884");
        Book book2 = new Book("Design Patterns", "9780201633610");
        Book book3 = new Book("The Pragmatic Programmer", "9780135957059");

        library.AddBook(book1);
        library.AddBook(book2);
        library.AddBook(book3);

        library.DisplayInventory();

        // 3. Borrowing Books
        Console.WriteLine("\n3. BORROWING BOOKS");
        Console.WriteLine("--------------------------------------------------");
        // Successful borrow
        library.BorrowBook("9780132350884", member1); // Alice borrows Clean Code
        library.BorrowBook("9780201633610", member2); // Bob borrows Design Patterns

        // Attempt to borrow an already borrowed book (Prevent borrowing)
        Console.WriteLine("\nAttempting to borrow an unavailable book:");
        library.BorrowBook("9780132350884", member3); // Charlie attempts Clean Code

        library.DisplayInventory();

        // 4. Returning Books
        Console.WriteLine("\n4. RETURNING BOOKS");
        Console.WriteLine("--------------------------------------------------");
        library.ReturnBook("9780132350884"); // Clean Code is returned

        library.DisplayInventory();

        // Charlie can now borrow Clean Code after return
        Console.WriteLine("\nCharlie borrows 'Clean Code' now that it is available:");
        library.BorrowBook("9780132350884", member3);

        library.DisplayInventory();

        Console.WriteLine("\n==================================================");
    }
}
