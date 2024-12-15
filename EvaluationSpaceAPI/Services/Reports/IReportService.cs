namespace EvaluationSpaceAPI.Services.Reports
{
    public interface IReportService
    {

        Task<string> UploadZipToDolos(IFormFile zip, string name, string programmingLanguage);
        Task<string> GetReportJson(string reportId);
        Task<string> GetReportSimilarityCSV(string reportId);
    }
}
