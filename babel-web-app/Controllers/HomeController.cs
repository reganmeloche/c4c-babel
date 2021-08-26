using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

using babel_web_app.Models;

namespace babel_web_app.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHtmlLocalizer<HomeController> _localizer;

        public HomeController(IHtmlLocalizer<HomeController> localizer) {
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            ViewBag.PageTitle = _localizer["home_title"];
            return View();
        }

        public IActionResult CultureManagement(string culture, string returnUrl) {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName, 
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(10) });

            return LocalRedirect(returnUrl);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        { 
            ViewBag.PageTitle = _localizer["error_title"];
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
