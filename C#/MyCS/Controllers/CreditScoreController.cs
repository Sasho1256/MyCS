using Microsoft.AspNetCore.Mvc;
using MyCS.InputModels;

namespace MyCS.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Services;

    public class CreditScoreController : Controller
    {
        private ISeedService seeder;
        private readonly ICreditScoreService scoreService;

        public CreditScoreController(ISeedService seeder, ICreditScoreService scoreService)
        {
            this.seeder = seeder;
            this.scoreService = scoreService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Manual()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Manual(ManualInputModel input)
        {
            this.scoreService.CreateRecordFromManualInput(input);
            return View();
        }

        [HttpGet]
        public IActionResult Bulk()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Bulk(IFormFile file)
        {
            try
            {
                await seeder.SeedRecords(new CsvFile() { File = file });
            }
            catch (Exception e)
            {
                return this.RedirectToAction("Error", "Home", new { exceptionMessage = e.Message});
            }
            return this.Redirect("/");
        }
    }
}
