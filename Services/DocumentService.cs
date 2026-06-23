namespace ExpenseTracker.Services
{
    public class DocumentService
    {
        private readonly string _uploadsFolder;
        private readonly string[] _permittedExtensions = [".jpg", ".jpeg", ".png", ".pdf", ".docx"];
        private readonly int maxSizeLimit = 5_000_000;

        public DocumentService()
        {
            _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

        }

        public async Task<string?> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                // throw new ArgumentException("No file was uploaded.");
                return null;
            }
            if (file.Length > maxSizeLimit)
            {
                // throw new ArgumentException("Maximum size is 5MB");
                return null;
            }
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!_permittedExtensions.Contains(extension))
            {
                // throw new ArgumentException("Invalid file type");
                return null;
            }
            if (!Directory.Exists(_uploadsFolder))
            {
                Directory.CreateDirectory(_uploadsFolder);
            }
            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(_uploadsFolder, uniqueFileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return uniqueFileName;
        }
    }
}
