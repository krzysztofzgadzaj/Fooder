using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Fooder.Helpers
{
    public static class FileManager
    {
        public static async Task<byte[]> SerializeFormFileAsync(IFormFile file)
        {
            var isFileInvalid = file == null || file.Length == 0;

            if (isFileInvalid)
            {
                return Array.Empty<byte>();
            }
            
            await using var stream = file.OpenReadStream();
            await using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            var serializedFile = memoryStream.ToArray();
            
            return serializedFile;
        }
    }
}
