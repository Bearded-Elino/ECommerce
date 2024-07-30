using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using ValeShop.ImageUpload;

namespace ValeShop.interfaces
{
    public interface IImageRepository
    {
        Task<CustomImageUploadResult> UploadImagesAsync(IFormFile file);
    }
}