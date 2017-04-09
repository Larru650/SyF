using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SyF.Models;
using SyF.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyF.Controllers.Api
{
    [Route("api/recipes")]
    public class RecipesController : Controller
    {
        private ILogger<RecipesController> _logger;
        private ISyFRepository _repository;

        public RecipesController(ISyFRepository repository, ILogger<RecipesController> logger)
        {
            _repository = repository;
            _logger = logger; //so we can log the exception error

        }

        [HttpGet("")]
        public IActionResult Get(string recipeName)
        {
            try
            {
                var recipe = _repository.GetAllRecipes();
                return Ok(Mapper.Map<IEnumerable<RecipeViewModel>>(recipe).ToList());
            }
            catch (Exception ex)
            {
                //TODO Logging 
                _logger.LogError($"Failed to get all recipes: {ex}");

                return BadRequest("Error occurred");
            }
            
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] RecipeViewModel theRecipe)
        {
            

            if (ModelState.IsValid)
            {
                var newRecipe = Mapper.Map<Recipe>(theRecipe);
                //TODO AddRecipe 
                _repository.AddRecipe(newRecipe);

                if(await _repository.SaveChangesAsync())
                {
                    return Created($"api/recipes/{theRecipe.Name}", Mapper.Map<RecipeViewModel>(newRecipe));

                }
            }

            return BadRequest("Bad data");

        }
    }
}
