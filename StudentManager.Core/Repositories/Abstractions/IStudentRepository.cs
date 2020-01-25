using System.Collections.Generic;
using System.Threading.Tasks;
using StudentManager.Core.Models;

namespace StudentManager.Core.Repositories.Abstractions
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAll();
        void Add(Student student);
        void Edit(int studentId, Student student);
        void Delete(int studentId);
        Task<int> GenerateId();
    }
}
