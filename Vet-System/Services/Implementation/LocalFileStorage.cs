using Vet_System.Services.Interfaces;

namespace Vet_System.Services.Implementation
{
    public class LocalFileStorage : IFileStorage
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LocalFileStorage(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
        }
        public Task Delete(string container, string? path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return Task.CompletedTask;
            }
            var fileName = Path.GetFileName(path);
            var filePath = Path.Combine(env.WebRootPath, container, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            return Task.CompletedTask;

        }

        public async Task<string> Store(string container, IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var folder = Path.Combine(env.WebRootPath, container);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string path = Path.Combine(folder, fileName);
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                var content = ms.ToArray();
                await File.WriteAllBytesAsync(path, content);
            }
            var request = httpContextAccessor.HttpContext!.Request!;
            var url = $"{request.Scheme}://{request.Host}";
            var urlFile = Path.Combine(url, container, fileName).Replace("\\", "/");
            return urlFile;
        }
    }
}
