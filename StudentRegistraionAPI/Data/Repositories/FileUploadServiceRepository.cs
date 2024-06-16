using StudentRegistraionAPI.Data.Interfaces;

namespace StudentRegistraionAPI.Data.Repositories
{
    public class FileUploadServiceRepository : IFileUploadServiceRepository
    {
        private readonly string _uploadsFolder;
        private readonly IStudentRepository _studentRepository;

        public FileUploadServiceRepository(IStudentRepository studentRepository)
        {
            _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            _studentRepository = studentRepository;
        }

        public async Task<string> UploadProfileImage(IFormFile image)
        {
            try
            {
                if (image == null || image.Length == 0)
                {
                    throw new ArgumentException("No file uploaded.");
                }

                if (!Directory.Exists(_uploadsFolder))
                {
                    Directory.CreateDirectory(_uploadsFolder);
                }

                var fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(image.FileName)}";
                var filePath = Path.Combine(_uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                var relativePath = Path.Combine("uploads", fileName).Replace("\\", "/");
                return relativePath;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to upload file.", ex);
            }
        }
        
    }
}
