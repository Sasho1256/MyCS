using Microsoft.AspNetCore.Mvc;
using Services;
using System.IO;
using System.Threading.Tasks;

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

        public async Task<IActionResult> SeedRecords() 
        {
            await seeder.SeedRecords();
            return this.Redirect("/");
        }
    }
}
