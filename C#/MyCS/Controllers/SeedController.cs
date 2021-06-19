using Microsoft.AspNetCore.Mvc;
using Services;
using System.IO;
using System.Threading.Tasks;
using MyCS.InputModels;
using Microsoft.AspNetCore.Http;

namespace MyCS.Controllers
{
    public class SeedController : Controller
    {
        private ISeedService seeder;

        public SeedController(ISeedService seeder)
        {
            this.seeder = seeder;
        }
        public IActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        //public IActionResult SeedRecords()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> SeedRecords(IFormFile file)
        //{
        //    await seeder.SeedRecords(new CsvFile() { File = file });
        //    return this.Redirect("/");
        //}
    }
}
