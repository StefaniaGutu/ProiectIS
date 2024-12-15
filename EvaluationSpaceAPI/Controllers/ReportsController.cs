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
            var x = await this._reportService.UploadZipToDolos(zip, name, programmingLanguage);
            return Ok(x);
        }

    }
}
