using System.Threading.Tasks;
using Database;
using MyCS.InputModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Services;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;

namespace MyCS.Controllers
{

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
            if (csv == null)
            {
                return this.RedirectToAction("Bulk", "CreditScore");
            }
            Dictionary<ICollection<Account>, ICollection<string>> dictionary = new Dictionary<ICollection<Account>, ICollection<string>>();
            try
            {
                dictionary = await this.seedService.SeedRecords(csv, ".\\wwwroot\\UploadedFiles\\");
                if (dictionary.First().Value.Count != 0)
                {
                    throw new Exception("An error occured.");
                }
                if (!string.IsNullOrWhiteSpace(Request.Form["Json"]))
                {
                    return this.Ok(new
                    {
                        dictionary.First().Key
                    });
                }
                else if (!string.IsNullOrWhiteSpace(Request.Form["CSV"]))
                {
                    var filePath = this.seedService.UpdatedCsvFile(dictionary.First().Key, $"{Directory.GetCurrentDirectory()}\\wwwroot\\Results\\{ DateTime.Now.ToString("yyyyMMddHHmmss") }_results_{csv.FileName}");
                    var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
                    return File(bytes, MediaTypeNames.Application.Octet, Path.GetFileName(filePath));
                }

                return this.NotFound();
            }
            catch (Exception ex)
            {
                dictionary.First().Value.Add(ex.Message);
                return this.BadRequest(new
                {
                    dictionary.First().Value
                });
            }
        }

        [HttpPost("manual/calculate")]
        public async Task<IActionResult> CalculateScoreManual([FromForm] ManualInputModel input)
        {
            var res = await this.creditScoreService.CreateRecordFromManualInput(input);

            if (this.ModelState.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(Request.Form["Json"]))
                {
                    return this.Ok(res.First().Key);
                }
                else if (!string.IsNullOrWhiteSpace(Request.Form["CSV"]))
                {
                    var filePath = this.seedService.UpdatedCsvFile(new List<Account>(){ res.First().Key }, $"{Directory.GetCurrentDirectory()}\\wwwroot\\Results\\{ DateTime.Now.ToString("yyyyMMddHHmmss") }_results_single.csv");
                    var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
                    return File(bytes, MediaTypeNames.Application.Octet, Path.GetFileName(filePath));
                }

                return this.NotFound();
            }

            return this.BadRequest(this.ModelState.Root.Errors.Select(x => x.ErrorMessage));
        }
    }
}
