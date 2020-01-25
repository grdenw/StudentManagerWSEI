using NUnit.Framework;
using StudentManager.Core.Models;
using StudentManager.Core.Repositories.Abstractions;
using StudentManager.Core.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManager.Tests.Core
{
    [TestFixture]
    public class StudentCsvRepositoryTests
    {
        private readonly IStudentRepository studentRepository;
        private readonly string path;

        public StudentCsvRepositoryTests()
        {
            path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Students.csv");

            studentRepository = new StudentCsvRepository(path);
        }

        [SetUp]
        public void SetUp()
        {
            File.AppendAllLines(path, new List<string>
            {
                "1;StudentA;StudentA;20",
                "2;StudentB;StudentB;20",
                "3;StudentC;StudentC;20"
            });
        }

        [Test]
        public async Task AddStudent_AddedStudentIsInStudentsFile()
        {
            var student = new Student(4, "StudentD", "StudentD", 20);

            studentRepository.Add(student);

            var students = await studentRepository.GetAll();

            Assert.IsTrue(students.Any(s => s.Id == student.Id));
        }

        [Test]
        public async Task EditStudent_EditedStudentIsInStudentsFile()
        {
            var student = new Student(3, "StudentD", "StudentD", 20);

            studentRepository.Edit(3, student);

            var students = await studentRepository.GetAll();

            Assert.IsTrue(students.FirstOrDefault(s => s.Id == 3)?.Id == student.Id);
        }

        [Test]
        public async Task DeleteStudent_DeletedStudentIsNotInStudentsFile()
        {
            studentRepository.Delete(3);

            var students = await studentRepository.GetAll();

            Assert.IsTrue(students.All(s => s.Id != 3));
        }

        [Test]
        public async Task GetAllStudents_StudentsListCountIsProper()
        {
            var students = await studentRepository.GetAll();

            Assert.IsTrue(students.Count() == 3);
        }

        [Test]
        public async Task GenerateIdForNewStudent_NewStudentIdIsProper()
        {
            var id = await studentRepository.GenerateId();

            Assert.AreEqual(4, id);
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(path);
        }
    }
}
