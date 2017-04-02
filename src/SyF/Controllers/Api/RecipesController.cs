using Microsoft.AspNetCore.Mvc;
using SyF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyF.Controllers.Api
{
    [Route("api/recipes")]
    public class RecipesController : Controller
    {
        private ISyFRepository _repository;

        public RecipesController(ISyFRepository repository)
        {
            _repository = repository;

        }

        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(_repository.GetAllRecipes());
        }

        [HttpPost("")]
        public IActionResult Post([FromBody] Recipe samfaina)
        {
            return Ok(true);
        }
    }
}
