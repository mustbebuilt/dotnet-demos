using System;
using System.Collections.Generic;

public class Module
{
    private string code;
    private string title;
    private List<Student> students;

    public Module(string code, string title)
    {
        this.code = code;
        this.title = title;
        this.students = new List<Student>();
    }

    public string GetCode()
    {
        return code;
    }

    public string GetTitle()
    {
        return title;
    }

    public void AddStudent(Student student)
    {
        if (student != null && !students.Contains(student))
        {
            students.Add(student);
            student.Enroll(this);
        }
    }

    // Support camelCase from specification
    public void addStudent(Student student) => AddStudent(student);

    public List<Student> GetStudents()
    {
        return students;
    }

    // Support camelCase from specification
    public List<Student> getStudents() => GetStudents();

    public override string ToString()
    {
        return $"{code} - {title}";
    }
}

public class Student
{
    private string studentId;
    private string name;
    private List<Module> modules;

    public Student(string id, string name)
    {
        this.studentId = id;
        this.name = name;
        this.modules = new List<Module>();
    }

    public string GetStudentId()
    {
        return studentId;
    }

    public string GetName()
    {
        return name;
    }

    public void Enroll(Module module)
    {
        if (module != null && !modules.Contains(module))
        {
            modules.Add(module);
            module.AddStudent(this);
        }
    }

    // Support camelCase from specification
    public void enroll(Module module) => Enroll(module);

    public List<Module> GetModules()
    {
        return modules;
    }

    // Support camelCase from specification
    public List<Module> getModules() => GetModules();

    public override string ToString()
    {
        return $"{studentId}: {name}";
    }

    // Support camelCase from specification
    public string toString() => ToString();
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("==============================================");
        Console.WriteLine(" University Enrollment System (Exercise 2)");
        Console.WriteLine("==============================================\n");

        // 1. Create Modules (at least 2)
        Module cs101 = new Module("CS101", "Introduction to Programming");
        Module cs102 = new Module("CS102", "Data Structures & Algorithms");
        Module cs103 = new Module("CS103", "Web Application Development");

        List<Module> allModules = new List<Module> { cs101, cs102, cs103 };

        // 2. Create Students (at least 3)
        Student alice = new Student("S101", "Alice Smith");
        Student bob = new Student("S102", "Bob Jones");
        Student charlie = new Student("S103", "Charlie Brown");
        Student diana = new Student("S104", "Diana Prince");

        List<Student> allStudents = new List<Student> { alice, bob, charlie, diana };

        // 3. Perform Enrollments
        alice.Enroll(cs101);
        alice.Enroll(cs102);

        bob.Enroll(cs101);
        bob.Enroll(cs103);

        charlie.Enroll(cs102);
        charlie.Enroll(cs103);

        diana.Enroll(cs101);
        diana.Enroll(cs102);
        diana.Enroll(cs103);

        // 4. Listing all courses/modules taken by each student
        Console.WriteLine("--- Students and Their Enrolled Modules ---");
        foreach (Student student in allStudents)
        {
            Console.WriteLine($"\nStudent: {student}");
            List<Module> enrolled = student.GetModules();
            if (enrolled.Count == 0)
            {
                Console.WriteLine("  (No modules enrolled)");
            }
            else
            {
                foreach (Module module in enrolled)
                {
                    Console.WriteLine($"  -> {module}");
                }
            }
        }

        Console.WriteLine("\n----------------------------------------------\n");

        // 5. Listing all students enrolled in each course/module
        Console.WriteLine("--- Modules and Their Enrolled Students ---");
        foreach (Module module in allModules)
        {
            Console.WriteLine($"\nModule: {module}");
            List<Student> enrolledStudents = module.GetStudents();
            if (enrolledStudents.Count == 0)
            {
                Console.WriteLine("  (No students enrolled)");
            }
            else
            {
                foreach (Student student in enrolledStudents)
                {
                    Console.WriteLine($"  -> {student}");
                }
            }
        }

        Console.WriteLine("\n==============================================");
    }
}
