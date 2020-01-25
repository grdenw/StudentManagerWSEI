using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using StudentManager.Core.Models;
using StudentManager.Core.Repositories.Abstractions;

namespace StudentManager.Core.Repositories.Implementations
{
    public class StudentCsvRepository : IStudentRepository
    {
        private readonly string path;

        public StudentCsvRepository(string path)
        {
            this.path = path;
        }

        public void Add(Student student)
        {
            File.AppendAllText(path
                    , $"{Convert.ToString(student.Id)};{student.Name};{student.Surname};{student.Age}{Environment.NewLine}");
        }

        public void Delete(int studentId)
        {
            var students = GetAll().Result.ToList();

            var studentToDelete = students.Single(s => s.Id == studentId);

            students.Remove(studentToDelete);

            AddStudentsToFile(students);
        }

        public void Edit(int studentId, Student student)
        {
            var students = GetAll().Result.ToList();

            var studentToEdit = students.FindIndex(s => s.Id == studentId);

            students[studentToEdit] = student;

            AddStudentsToFile(students);
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            if (!File.Exists(path)) return Enumerable.Empty<Student>();

            IEnumerable<string> lines = await File.ReadAllLinesAsync(path);

            var students = new List<Student>();

            if (!lines.Any()) return Enumerable.Empty<Student>();

            foreach (var line in lines)
            {
                var studentItems = line.Split(';');
                students.Add(new Student(Convert.ToInt32(studentItems[0]), studentItems[1], studentItems[2], Convert.ToInt32(studentItems[3])
                ));
            }

            return students;
        }

        public async Task<int> GenerateId()
        {
            var students = await GetAll();

            var studentsList = students.ToList();

            if (!studentsList.Any())
            {
                return 1;
            }

            return studentsList.Last().Id + 1;
        }

        private void AddStudentsToFile(IEnumerable<Student> students)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            foreach (var student in students)
            {
                Add(student);
            }
        }
    }
}
