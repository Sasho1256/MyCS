namespace MyCS.Controllers
{
    using System.Threading.Tasks;
    using Database;
    using InputModels;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Microsoft.AspNetCore.Http;
    using System.Linq;

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
            var dictionary = await this.seedService.SeedRecords(csv);
            if (dictionary.First().Value.Count != 0)
            {
                return RedirectToAction("Error", "Home", new { exceptions = dictionary.First().Value });
            }

            //TODO: FIX THIS EXCEPTION - TypeLoadException: Could not load type 'Microsoft.AspNetCore.Http.Internal.FormFile' from assembly 'Microsoft.AspNetCore.Http, Version=5.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60'.
            var file = this.seedService.UpdatedCSVFile(dictionary.First().Key, $"{this.Request.Scheme}://{this.Request.Host}/UploadedFiles/{csv.FileName}");

            return this.Ok(new
            {
                fileUrl = $"{this.Request.Scheme}://{this.Request.Host}/UploadedFiles/{csv.FileName}" ,
                dictionary
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
