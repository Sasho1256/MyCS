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
    using System;
    using System.Net.Mime;

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

            try
            {
                var filePath = this.seedService.UpdatedCSVFile(dictionary.First().Key, $".\\wwwroot\\Results\\{ DateTime.Now.ToString("yyyyMMddHHmmss") }_results_{csv.FileName}");
                return this.File(filePath, MediaTypeNames.Text.Plain);
                //return this.Ok(new
                //{
                //    fileUrl = $"{this.Request.Scheme}://{this.Request.Host}/UploadedFiles/{csv.FileName}",
                //    dictionary
                //});
            }
            catch (Exception ex)
            {
                dictionary.First().Value.Add(ex.Message);
                return RedirectToAction("Error", "Home", new { exceptions = dictionary.First().Value });
            }
        }

        [HttpPost("manual/calculate")]
        public async Task<IActionResult> CalculateScoreManual([FromForm] ManualInputModel input)
        {
            var res = await this.creditScoreService.CreateRecordFromManualInput(input);
            return this.Ok(res);
        }
    }
}
