using InternetBanking.Application.Enums;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternetBanking.Presentation.Controllers
{
    [Authorize(Roles = nameof(UserType.Customer))]
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
