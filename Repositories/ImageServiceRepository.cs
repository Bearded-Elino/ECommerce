using System;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ValeShop.interfaces;
using ValeShop.ImageUpload;

namespace ValeShop.Repositories
{
    public class ImageServiceRepository : IImageRepository
    {
        private readonly Cloudinary _cloudinary;
        private readonly ILogger<ImageServiceRepository> _logger;

        public ImageServiceRepository(Cloudinary cloudinary, ILogger<ImageServiceRepository> logger)
        {
            _cloudinary = cloudinary;
            _logger = logger;
        }


        public async Task<CustomImageUploadResult> UploadImagesAsync(IFormFile file)
        {
            var uploadResult = new CustomImageUploadResult();
            try
            {
                if (file.Length > 0)
                {
                    await using var stream = file.OpenReadStream();
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, file.OpenReadStream())
                    };
                    var result = await _cloudinary.UploadAsync(uploadParams);

                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        uploadResult.PublicId = result.PublicId;
                        _logger.LogInformation("SecureUrl type: {type}, value: {value}", result.SecureUrl.GetType(),
                            result.SecureUrl);
                        uploadResult.Url = result.SecureUrl.ToString();
                    }
                    else
                    {
                        _logger.LogError("Image upload failed with status code: {statusCode}", result.StatusCode);
                    }
                }
                else
                {
                    _logger.LogError("File length is zero");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during image upload");
            }

            return uploadResult;
        }

    }


}