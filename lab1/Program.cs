using System;

namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("Select an exercise:");
                Console.WriteLine("1. Book class");
                Console.WriteLine("2. Student and Module");
                Console.WriteLine("3. Library Management System");
                Console.WriteLine("0. Exit");
                Console.Write("Choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        runBookExercise();
                        break;
                    case "2":
                        runStudentModuleExercise();
                        break;
                    case "3":
                        runLibraryExercise();
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select 1, 2, 3, or 0.");
                        break;
                }
            }
        }

        private static void runBookExercise()
        {
            Book book = new Book("The Hobbit", "J.R.R. Tolkien", "9780261102217");

            Console.WriteLine();
            Console.WriteLine("Book exercise:");
            book.displayInfo();

            book.setTitle("The Lord of the Rings");

            Console.WriteLine();
            Console.WriteLine("After updating the title:");
            book.displayInfo();
        }

        private static void runStudentModuleExercise()
        {
            Module programming = new Module("COM1001", "Programming");
            Module softwareEngineering = new Module("COM2002", "Software Engineering");

            Console.WriteLine();
            Console.WriteLine("Enter details for three students:");
            Student alice = createStudent(1);
            Student bob = createStudent(2);
            Student charlie = createStudent(3);

            alice.enroll(programming);
            alice.enroll(softwareEngineering);
            bob.enroll(programming);
            charlie.enroll(softwareEngineering);
            charlie.enroll(programming);

            Console.WriteLine();
            Console.WriteLine("Modules taken by each student:");
            printStudentModules(alice);
            printStudentModules(bob);
            printStudentModules(charlie);

            Console.WriteLine();
            Console.WriteLine("Students enrolled in each module:");
            printModuleStudents(programming);
            printModuleStudents(softwareEngineering);
        }

        private static Student createStudent(int studentNumber)
        {
            Console.Write("Student " + studentNumber + " ID: ");
            string studentId = readRequiredValue();

            Console.Write("Student " + studentNumber + " name: ");
            string name = readRequiredValue();

            return new Student(studentId, name);
        }

        private static string readRequiredValue()
        {
            string value = Console.ReadLine();

            while (String.IsNullOrWhiteSpace(value))
            {
                Console.Write("Value cannot be blank. Please try again: ");
                value = Console.ReadLine();
            }

            return value;
        }

        private static void runLibraryExercise()
        {
            Library library = new Library("City Library");
            Member alice = new Member("M001", "Alice");
            Member bob = new Member("M002", "Bob");

            library.registerMember(alice);
            library.registerMember(bob);

            Book cleanCode = new Book("Clean Code", "9780132350884");
            Book designPatterns = new Book("Design Patterns", "9780201633610");
            library.addBook(cleanCode);
            library.addBook(designPatterns);

            Console.WriteLine();
            Console.WriteLine("Library exercise - borrowing books:");
            library.borrowBook("9780132350884", alice);
            library.borrowBook("9780132350884", bob);
            library.borrowBook("9780201633610", bob);

            Console.WriteLine();
            Console.WriteLine("Returning a book:");
            library.returnBook("9780132350884");
            library.borrowBook("9780132350884", bob);
        }

        private static void printStudentModules(Student student)
        {
            Console.WriteLine(student + ":");

            foreach (Module module in student.getModules())
            {
                Console.WriteLine("  " + module);
            }
        }

        private static void printModuleStudents(Module module)
        {
            Console.WriteLine(module + ":");

            foreach (Student student in module.getStudents())
            {
                Console.WriteLine("  " + student);
            }
        }
    }
}
