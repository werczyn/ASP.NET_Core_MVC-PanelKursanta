using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IPBProjekt.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using IPBProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace IPBProjekt.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppData _context;

        public HomeController(AppData context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([Bind("Login,Haslo")] Uzytkownik uzytkownik)
        {
            var tmpUzytkownik = await _context.Uzytkownicy.SingleOrDefaultAsync(u => u.Login == uzytkownik.Login && u.Haslo == uzytkownik.Haslo) ?? new Uzytkownik();
            ClaimsIdentity identity = null;
            bool isAuthenticated = false;

            if (tmpUzytkownik.Grupa == "Kursant")
            {
                identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, uzytkownik.Login),
                    new Claim(ClaimTypes.Role, "Kursant")
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                isAuthenticated = true;
            }
            if (tmpUzytkownik.Grupa == "WydzialKomunikacji")
            {
                identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, uzytkownik.Login),
                    new Claim(ClaimTypes.Role, "WydzialKomunikacji")
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                isAuthenticated = true;
            }

            if (isAuthenticated)
            {
                var principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                if(principal.IsInRole("Kursant"))
                {
                    return Redirect("/Kursant/Details/"+ tmpUzytkownik.IdOsoba);
                }
                else if (principal.IsInRole("WydzialKomunikacji"))
                {
                    return Redirect("/WydzialKomunikacji/Details/"+tmpUzytkownik.NumerWydzialu);
                }
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
