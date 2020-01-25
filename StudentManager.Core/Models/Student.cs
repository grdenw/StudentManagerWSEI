namespace StudentManager.Core.Models
{
    public class Student
    {
        public Student(int id, string name, string surname, int age)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Age = age;
        }

        public int Id { get;  }
        public string Name { get; }
        public string Surname { get; }
        public int Age { get; }
    }
}
