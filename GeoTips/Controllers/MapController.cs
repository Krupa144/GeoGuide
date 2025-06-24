using Microsoft.AspNetCore.Mvc;

namespace GeoTips.Controllers
{
    public class MapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
