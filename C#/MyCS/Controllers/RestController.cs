namespace MyCS.Controllers
{
    using System.Threading.Tasks;
    using Database;
    using InputModels;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Microsoft.AspNetCore.Http;

    [ApiController]
    public class RestController : ControllerBase
    {
        private readonly ISeedService seedService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ICreditScoreService creditScoreService;

        public RestController(ISeedService seedService, MyCsDbContext context, IWebHostEnvironment webHostEnvironment, ICreditScoreService creditScoreService)
        {
            this.seedService = seedService;
            this.webHostEnvironment = webHostEnvironment;
            this.creditScoreService = creditScoreService;
        }

        [HttpPost("csv/calculate")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CalculateScoreCsv([FromForm(Name = "file")] IFormFile csv)
        {
            var errors = await this.seedService.SeedRecords(csv);
            return this.Ok(new
            {
                fileUrl = $"{this.Request.Scheme}://{this.Request.Host}/UploadedFiles/{csv.FileName}" ,
                errors
            });
        }

        [HttpPost("manual/calculate")]
        public async Task<IActionResult> CalculateScoreManual([FromForm] ManualInputModel input)
        {
            var res = await this.creditScoreService.CreateRecordFromManualInput(input);
            return this.Ok(res);
        }
    }
}
