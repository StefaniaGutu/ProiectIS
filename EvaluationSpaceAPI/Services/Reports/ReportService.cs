using Microsoft.DotNet.MSIdentity.Shared;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace EvaluationSpaceAPI.Services.Reports
{
    public class ReportService : IReportService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReportService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> UploadZipToDolos(IFormFile zip, string name, string programmingLanguage)
        {
            if (zip == null || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(programmingLanguage))
            {
                throw new ArgumentException("Missing required parameters for the upload.");
            }

            // Prepare the HttpClient instance
            var client = _httpClientFactory.CreateClient();

            // Create a MultipartFormDataContent to send the file and form data
            var formData = new MultipartFormDataContent
            {
                // Add the file content (zipfile)
                { new StreamContent(zip.OpenReadStream()) { Headers = { ContentType = new MediaTypeHeaderValue("application/octet-stream") } }, "dataset[zipfile]", zip.FileName },
                
                // Add other form data fields
                { new StringContent(name), "dataset[name]" },
                { new StringContent(programmingLanguage), "dataset[programming_language]" }
            };

            try
            {
                string reportIdResult = null;
                // Send the POST request to localhost:3000/reports
                var response = await client.PostAsync("http://localhost:3000/reports", formData);

                // Ensure successful response
                response.EnsureSuccessStatusCode();

                // Read and return the response content from the external server
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var jsonDoc = JsonDocument.Parse(jsonResponse);

                // Assuming the field you're looking for is called "status"
                if (jsonDoc.RootElement.TryGetProperty("id", out var reportId))
                {
                    // Extract the value of the "status" field
                    reportIdResult = reportId.GetString(); // You can adjust based on the type (e.g., GetInt32() if it's an integer)
                }
                return reportIdResult;
            }
            catch (HttpRequestException ex)
            {
                // Handle errors related to HTTP request failures (e.g., network issues)
                throw new InvalidOperationException("Error while sending request to external service", ex);
            }
        }
    }
}
