namespace WebApiTupac.Services
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(IFormFile file);
        Task<string> UploadPdfAsync(IFormFile file);
    }
}
