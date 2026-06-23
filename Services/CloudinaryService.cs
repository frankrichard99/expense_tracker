using CloudinaryDotNet;
using ExpenseTracker.Models;
using Microsoft.Extensions.Options;
using CloudinaryDotNet.Actions;

namespace ExpenseTracker.Services
{
    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        private readonly string[] _permittedExtensions = [".jpg", ".jpeg", ".png", "webp"];
        private readonly int maxSizeLimit = 5_000_000;
        public CloudinaryService(IOptions<CloudinarySettings> settings)
        {
            var account = new Account
            (
                settings.Value.CloudName,
                settings.Value.ApiKey,
                settings.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<string?> UploadFile(IFormFile file) { 

            if (file.Length <= 0)
            {
                return null;
            }
            if (file.Length > maxSizeLimit)
            {
                return null;
            }
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!_permittedExtensions.Contains(extension))
            {
                return null;
            }

            await using var stream = file.OpenReadStream();

            bool isImage = extension is ".jpg" or ".jpeg" or ".png";

            RawUploadParams uploadParams = isImage
            ? new ImageUploadParams
            {
                 File = new FileDescription(file.FileName, stream),
                 Folder = "ExpenseTracker_Receipts",
                 Transformation = new Transformation().Quality("auto").FetchFormat("auto")
            }
            : new RawUploadParams
            {
                 File = new FileDescription(file.FileName, stream),
                 Folder = "ExpenseTracker_Receipts"
             };

           

            var result = await _cloudinary.UploadAsync(uploadParams);

            if (result.Error != null)
            {
                Console.WriteLine($"[Cloudinary Upload Error]: {result.Error.Message}");
                return null;
            }

            return result.SecureUrl?.AbsoluteUri;

        }




    }
}
