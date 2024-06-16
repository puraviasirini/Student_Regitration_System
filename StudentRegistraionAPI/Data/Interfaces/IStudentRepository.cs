using StudentRegistraionAPI.Models;
using System.Threading.Tasks;

namespace StudentRegistraionAPI.Data.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllStudents();
        Task<int> AddStudent(Student student);
        Task<int> EditStudent(Student student);
        Task<bool> DeleteStudent(int studentId);
        Task<Student> GetStudentDetailsById(int studentId);
        Task<string> GetProfileImageUrl(int studentId);
    }
}
