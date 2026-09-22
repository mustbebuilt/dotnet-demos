using System.Collections.Generic;

namespace lab1
{
    public class Student
    {
        private string studentId;
        private string name;
        private List<Module> modules;

        public Student(string id, string name)
        {
            studentId = id;
            this.name = name;
            modules = new List<Module>();
        }

        public void enroll(Module module)
        {
            if (!modules.Contains(module))
            {
                modules.Add(module);
                module.addStudent(this);
            }
        }

        public List<Module> getModules()
        {
            return modules;
        }

        public override string ToString()
        {
            return studentId + " - " + name;
        }
    }
}