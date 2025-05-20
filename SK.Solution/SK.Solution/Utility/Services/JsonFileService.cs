using System.Text.Json;

namespace SK.Solution.Utility.Services
{
    public class JsonFileService
    {
        private readonly IWebHostEnvironment _env;

        public JsonFileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<List<T>> ReadJsonFileAsync<T>(string relativePath)
        {
            var fullPath = Path.Combine(_env.WebRootPath, relativePath);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"File not found: {fullPath}");

            var json = await File.ReadAllTextAsync(fullPath);
            return JsonSerializer.Deserialize<List<T>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new();
        }
    }

}
