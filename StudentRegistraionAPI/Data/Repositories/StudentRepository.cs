using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StudentRegistraionAPI.Data.Interfaces;
using StudentRegistraionAPI.Models;
using System.Threading.Tasks;

namespace StudentRegistraionAPI.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        public readonly StudentRegistrationDbContext _studentRegistrationDbContext;
        private readonly IWebHostEnvironment _environment;

        public StudentRepository(StudentRegistrationDbContext studentRegistrationDbContext, IWebHostEnvironment environment)
        {
            _studentRegistrationDbContext = studentRegistrationDbContext;
            _environment = environment;
        }

        public async Task<int> AddStudent(Student studentRequest)
        {
            if (string.IsNullOrWhiteSpace(studentRequest.FirstName))
            {
                throw new ArgumentException("FirstName cannot be empty.");
            }

            try
            {
                var parameters = new[]
                    {
                        new SqlParameter("@firstName", studentRequest.FirstName),
                        new SqlParameter("@lastName", studentRequest.LastName),
                        new SqlParameter("@mobile", studentRequest.Mobile),
                        new SqlParameter("@email", studentRequest.Email),
                        new SqlParameter("@nic", studentRequest.NIC),
                        new SqlParameter("@dateOfBirth", studentRequest.DateOfBirth),
                        new SqlParameter("@address", studentRequest.Address),
                        new SqlParameter("@profileImageUrl", studentRequest.ProfileImageUrl)
                    };

                var result = await _studentRegistrationDbContext.Database.ExecuteSqlRawAsync("EXEC InsertStudent @firstName, @lastName, @mobile,@email,@nic,@dateOfBirth,@address,@profileImageUrl", parameters);

                await _studentRegistrationDbContext.SaveChangesAsync();
                return result;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Failed to add student to the database.", ex);
            }

        }

        public async Task<bool> DeleteStudent(int studentId)
        {
            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@studentId", studentId)
                };

                var result = await _studentRegistrationDbContext.Database.ExecuteSqlRawAsync("EXEC DeleteStudentById @studentId", parameters);

                return true;

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete student from the database.", ex);
            }
        }

        public async Task<int> EditStudent(Student studentRequest)
        {
            try
            {
                var parameters = new[]
            {
                        new SqlParameter("@studentId", studentRequest.StudentId),
                        new SqlParameter("@firstName", studentRequest.FirstName),
                        new SqlParameter("@lastName", studentRequest.LastName),
                        new SqlParameter("@mobile", studentRequest.Mobile),
                        new SqlParameter("@email", studentRequest.Email),
                        new SqlParameter("@nic", studentRequest.NIC),
                        new SqlParameter("@dateOfBirth", studentRequest.DateOfBirth),
                        new SqlParameter("@address", studentRequest.Address),
                        new SqlParameter("@profileImageUrl", studentRequest.ProfileImageUrl)
                    };

                var result = await _studentRegistrationDbContext.Database.ExecuteSqlRawAsync("EXEC UpdateStudent @studentId, @firstName, @lastName, @mobile, @email, @nic, @dateOfBirth, @address, @profileImageUrl", parameters);

                await _studentRegistrationDbContext.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update student.", ex);
            }

        }

        public async Task<List<Student>> GetAllStudents()
        {
            try
            {
                var result = await _studentRegistrationDbContext.Students.FromSqlRaw("GetAllStudents").ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load students.", ex);
            }
        }

        public async Task<Student> GetStudentDetailsById(int studentId)
        {
            try
            {
                var result = await _studentRegistrationDbContext.Students.FromSqlInterpolated($"EXECUTE GetStudentDetailsById {studentId}").AsNoTracking().ToListAsync();


                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load student.", ex);
            }
        }

        private string GetFilePath(string fileName)
        {
            return this._environment.WebRootPath + fileName;
        }

        public async Task<string> GetProfileImageUrl(int studentId)
        {
            var student = await this.GetStudentDetailsById(studentId);
            if (student != null && !string.IsNullOrEmpty(student.ProfileImageUrl))
            {
                string fileName  =  student.ProfileImageUrl;
                string imagePath = GetFilePath(fileName);
                string profImgUrl = imagePath;
                return imagePath;
            }
            else
            {
                return "no-profile-image.jpg"; 
            }
        }
    }
}
