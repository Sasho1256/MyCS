using Microsoft.AspNetCore.Mvc;
using MyCS.InputModels;

namespace MyCS.Controllers
{
    public class CreditScoreController : Controller
    {
        public CreditScoreController()
        {

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

        public IActionResult Bulk()
        {
            return View();
        }
    }
}
