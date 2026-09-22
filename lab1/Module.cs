using System.Collections.Generic;

namespace lab1
{
    public class Module
    {
        private string code;
        private string title;
        private List<Student> students;

        public Module(string code, string title)
        {
            this.code = code;
            this.title = title;
            students = new List<Student>();
        }

        public void addStudent(Student student)
        {
            if (!students.Contains(student))
            {
                students.Add(student);

                if (!student.getModules().Contains(this))
                {
                    student.getModules().Add(this);
                }
            }
        }

        public List<Student> getStudents()
        {
            return students;
        }

        public override string ToString()
        {
            return code + " - " + title;
        }
    }
}