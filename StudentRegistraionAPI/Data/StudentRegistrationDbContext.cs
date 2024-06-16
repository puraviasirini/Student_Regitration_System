using Microsoft.EntityFrameworkCore;
using StudentRegistraionAPI.Models;

namespace StudentRegistraionAPI.Data
{
    public class StudentRegistrationDbContext : DbContext
    {
        public StudentRegistrationDbContext(DbContextOptions options) : base(options)
        {
           
        }
        public DbSet<Student> Students { get; set; }
    }
}
