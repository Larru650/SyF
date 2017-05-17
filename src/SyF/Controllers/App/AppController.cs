using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SyF.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace SyF.Controllers
{
    public class AppController : Controller
    {
        private IConfigurationRoot _config;
        private ILogger<AppController> _logger;
        private ISyFRepository _repository;

        public AppController(IConfigurationRoot config, ISyFRepository repository, ILogger<AppController> logger)
        {
            _config = config;
            _logger = logger;
            _repository = repository;
        }

        [Authorize]
        public IActionResult Index()
        {
            try
            {
                var data = _repository.GetAllRecipes();
                return View(data);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Could not get recipe from Database : {ex.Message}");
                return Redirect("/error");
            }
        
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize]
        public IActionResult Recipes()
        {
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
