namespace backend.Application.Contracts.Infrastructure.Repositories
{
    public interface IImageRepository
    {
        Task<string> Upload(string base64Image, string publicId, bool backgroundRemoval = false);
        Task<string> Update(string base64Image, string publicId, bool backgroundRemoval = false);
        Task<bool> Delete(string publicId);
    }
}
