using EvaluationSpaceAPI.Services.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationSpaceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            this._reportService = reportService;
        }

        [HttpPost]
        [Authorize]
        [Route("UpoloadZip")]
        public async Task<IActionResult> UpoloadZip(IFormFile zip, [FromForm] string name, [FromForm] string programmingLanguage)
        {
            var reportId = await this._reportService.UploadZipToDolos(zip, name, programmingLanguage);
            return Ok(reportId);
        }

        [HttpGet]
        [Authorize]
        [Route("GetReport/{reportId}")]
        public async Task<IActionResult> GetReport([FromRoute] string reportId)
        {
            var report = await this._reportService.GetReportJson(reportId);
            return Ok(report);
        }

        [HttpGet]
        [Authorize]
        [Route("GetReportSimilarity/{reportId}")]
        public async Task<IActionResult> GetReportSimilarity([FromRoute] string reportId)
        {
            var similarityCSV = await this._reportService.GetReportSimilarityCSV(reportId);
            return Ok(similarityCSV);
        }
    }
}
