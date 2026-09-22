using System;

namespace lab1
{
    public class Book
    {
        private string title;
        private string author;
        private string isbn;
        private bool available;

        public Book(string title, string isbn)
        {
            this.title = title;
            author = "Unknown";
            this.isbn = isbn;
            available = true;
        }

        public Book(string title, string author, string isbn)
        {
            this.title = title;
            this.author = author;
            this.isbn = isbn;
            available = true;
        }

        public string getTitle()
        {
            return title;
        }

        public string getAuthor()
        {
            return author;
        }

        public string getIsbn()
        {
            return isbn;
        }

        public void setTitle(string title)
        {
            this.title = title;
        }

        public void setAuthor(string author)
        {
            this.author = author;
        }

        public void setIsbn(string isbn)
        {
            this.isbn = isbn;
        }

        public bool isAvailable()
        {
            return available;
        }

        public void borrow()
        {
            if (available)
            {
                available = false;
            }
        }

        public void returnBook()
        {
            available = true;
        }

        public void displayInfo()
        {
            Console.WriteLine("Title: " + title);
            Console.WriteLine("Author: " + author);
            Console.WriteLine("ISBN: " + isbn);
        }
    }
}