using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SyF.Models;
using Microsoft.Extensions.Logging;

namespace SyF.Controllers
{
    public class AppController : Controller
    {
        private IConfigurationRoot _config;
        private SyFContext _context;
        private ILogger<AppController> _logger;

        public AppController(IConfigurationRoot config, SyFContext context, ILogger<AppController> logger)
        {
            _config = config;
            _context = context;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var data = _context.Recipes.ToList();
            return View(data);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
