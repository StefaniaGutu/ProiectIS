using CsvHelper;
using EvaluationSpaceAPI.DTOs;
using Microsoft.DotNet.MSIdentity.Shared;
using System.Formats.Asn1;
using System.Globalization;
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

        public async Task<string> GetReportJson(string reportId)
        {
            var url = $"http://localhost:3000/reports/{reportId}";

            try
            {
                // Create an HttpClient instance
                var client = _httpClientFactory.CreateClient();

                // Send a GET request to fetch the report
                var response = await client.GetAsync(url);

                // Ensure the response is successful
                response.EnsureSuccessStatusCode();

                // Read and return the JSON response content
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return jsonResponse; // Return the JSON response as a string
            }
            catch (HttpRequestException ex)
            {
                // Log and throw an error if the HTTP request fails
                throw new InvalidOperationException("Error while retrieving the report", ex);
            }
        }

        public async Task<SimilarityDTO[]> GetReportSimilarityCSV(string reportId)
        {
            var url = $"http://localhost:3000/reports/{reportId}/data/pairs.csv";

            try
            {
                // Create an HttpClient instance
                var client = _httpClientFactory.CreateClient();
                Thread.Sleep(10000);
                // Send a GET request to fetch the CSV file
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode(); // Ensures that the response is successful

                // Read the CSV content as a string
                var csvContent = await response.Content.ReadAsStringAsync();
                var csvReader = new CsvReader(new StringReader(csvContent), CultureInfo.InvariantCulture);

                // Read the CSV file records
                var records = csvReader.GetRecords<dynamic>().ToList();

                List<SimilarityDTO> result = new List<SimilarityDTO>();

                foreach (var record in records)
                {
                    // Assuming the field names in the CSV are 'leftFilePath' and 'similarity'
                    var leftFilePath = record.leftFilePath;
                    var rightFilePath = record.rightFilePath;
                    var similarity = record.similarity;

                    // Extract just the file name from the full path
                    var leftfileName = Path.GetFileName(leftFilePath);
                    var rightfileName = Path.GetFileName(rightFilePath);

                    var x = new SimilarityDTO()
                    {
                        LeftFileName = leftfileName,
                        RightFileName = rightfileName,
                        Similarity = Math.Round(Double.Parse(similarity)*100, 2) 
                    };
                    result.Add(x);
                }

                return result.ToArray();
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException("Error while retrieving CSV data", ex);
            }
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
