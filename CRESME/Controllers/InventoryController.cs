using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRESME.Controllers
{
    public class InventoryController : Controller
    {
        [Authorize]
        public IActionResult GetAll()
        {
            return View();
        }
    }
}
