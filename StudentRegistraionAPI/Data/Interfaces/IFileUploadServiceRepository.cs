namespace StudentRegistraionAPI.Data.Interfaces
{
    public interface IFileUploadServiceRepository
    {
        Task<string> UploadProfileImage(IFormFile image);

    }
}
