namespace Application.Contracts.Infrastructure.Repositories
{
    public interface IImageUploadRepository
    {
        Task<string> Upload(string base64Image, string publicId);
        Task<string> Update(string base64Image, string publicId);
        Task<bool> Delete(string publicId);
    }
}
