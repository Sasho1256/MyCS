using Microsoft.AspNetCore.Mvc;
using MyCS.InputModels;

namespace MyCS.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Services;

    public class CreditScoreController : Controller
    {
        private ISeedService seeder;
        public CreditScoreController(ISeedService seeder)
        {
            this.seeder = seeder;
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
            await seeder.SeedRecords(new CsvFile() { File = file });
            return this.Redirect("/");
        }
    }
}
