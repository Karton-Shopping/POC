using System.Net.Http.Headers;

namespace Intoxalock.Regular.POC.Web.Services
{
    public class RegulaOcrService
    {
        private readonly string _apiUrl = "https://api.regulaforensics.com/v1/ocr";
        private readonly string _apiKey = "API_KEY";

        private readonly HttpClient _httpClient;

        public RegulaOcrService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        }


        public async Task<string> PerformOcrAsync(string imageFilePath)
        {
            var imageBytes = await System.IO.File.ReadAllBytesAsync(imageFilePath);

            using (var content = new MultipartFormDataContent())
            {
                var imageContent = new ByteArrayContent(imageBytes);
                imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

                content.Add(imageContent, "file", "document.jpg");

                var response = await _httpClient.PostAsync(_apiUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var result = await response.Content.ReadAsStringAsync();
                return result;
            }

        }
    }
}
