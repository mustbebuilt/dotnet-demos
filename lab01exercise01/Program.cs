using System;

public class Book
{
    private string title;
    private string author;
    private string isbn;

    public Book(string title, string author, string isbn)
    {
        this.title = title;
        this.author = author;
        this.isbn = isbn;
    }

    public string GetTitle()
    {
        return title;
    }

    public string GetAuthor()
    {
        return author;
    }

    public string GetIsbn()
    {
        return isbn;
    }

    public void DisplayInfo()
    {
        Console.WriteLine("=== Book Details ===");
        Console.WriteLine($"Title : {title}");
        Console.WriteLine($"Author: {author}");
        Console.WriteLine($"ISBN  : {isbn}");
    }
}

public class Program
{
    public static void Main()
    {
        Book book = new Book(
            "1984",
            "George Orwell",
            "9780451524935"
        );
        book.DisplayInfo();
    }
}