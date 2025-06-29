using Microsoft.AspNetCore.Mvc;

namespace GeoTipsBackend.Controllers
{
    public class MapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
