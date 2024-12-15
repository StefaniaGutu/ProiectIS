namespace EvaluationSpaceAPI.Services.Reports
{
    public interface IReportService
    {

        Task<string> UploadZipToDolos(IFormFile zip, string name, string programmingLanguage);
    }
}
