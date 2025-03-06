using Microsoft.AspNetCore.Http;

namespace Vet_Infrastructure.Services.Interfaces
{
    public interface IFileStorage
    {
        Task<string> Store(string container, IFormFile file);
        Task Delete(string container, string? path);
        async Task<string> Edit(string container, IFormFile file, string? path)
        {
            await Delete(container, path);
            return await Store(container, file);
        }
    }
}
