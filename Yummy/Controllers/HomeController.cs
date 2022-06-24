using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Yummy.DAL;
using Yummy.Models;
using Yummy.ViewModels;

namespace Yummy.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context { get; }
        

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM home = new HomeVM
            {
                Products = _context.Products.ToList()
            };
            return View(home);
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
