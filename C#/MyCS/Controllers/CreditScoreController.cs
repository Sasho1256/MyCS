using Microsoft.AspNetCore.Mvc;

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
    }
}
