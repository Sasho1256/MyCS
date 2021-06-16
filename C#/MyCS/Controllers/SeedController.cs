using Microsoft.AspNetCore.Mvc;
using Services;
using System.IO;

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

        public IActionResult SeedRecords() 
        {
            seeder.SeedRecords();
            return this.Ok();
        }
    }
}
