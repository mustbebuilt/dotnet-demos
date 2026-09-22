using System;
using System.Collections.Generic;

namespace lab1
{
    public class Library
    {
        private string name;
        private List<Book> books;
        private List<Member> members;
        private Dictionary<string, Member> borrowers;

        public Library(string name)
        {
            this.name = name;
            books = new List<Book>();
            members = new List<Member>();
            borrowers = new Dictionary<string, Member>();
        }

        public void addBook(Book book)
        {
            books.Add(book);
        }

        public void registerMember(Member member)
        {
            members.Add(member);
        }

        public void borrowBook(string isbn, Member member)
        {
            Book book = findBook(isbn);

            if (book == null)
            {
                Console.WriteLine("Book with ISBN " + isbn + " was not found.");
                return;
            }

            if (!book.isAvailable())
            {
                Console.WriteLine("Book " + book.getTitle() + " is unavailable.");
                return;
            }

            book.borrow();
            borrowers[isbn] = member;
            Console.WriteLine(member + " borrowed " + book.getTitle() + ".");
        }

        public void returnBook(string isbn)
        {
            Book book = findBook(isbn);

            if (book == null)
            {
                Console.WriteLine("Book with ISBN " + isbn + " was not found.");
                return;
            }

            if (book.isAvailable())
            {
                Console.WriteLine("Book " + book.getTitle() + " is already available.");
                return;
            }

            book.returnBook();
            borrowers.Remove(isbn);
            Console.WriteLine(book.getTitle() + " was returned.");
        }

        private Book findBook(string isbn)
        {
            foreach (Book book in books)
            {
                if (book.getIsbn() == isbn)
                {
                    return book;
                }
            }

            return null;
        }
    }
}