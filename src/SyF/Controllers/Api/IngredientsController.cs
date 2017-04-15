using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SyF.Models;
using SyF.Services;
using SyF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyF.Controllers.Api
{
    [Route("api/recipes/{recipeName}/ingredients")]
    public class IngredientsController : Controller
    {//this will be an associative controller since Recipes has a relationship with Ingredients
        private ILogger<IngredientsController> _logger;
        private EdamamService _recipeService;
        private ISyFRepository _repository;

        public IngredientsController(ISyFRepository repository, ILogger<IngredientsController> logger, EdamamService recipeService) //we inject the dependencies we need
        {
            _repository = repository;
            _logger = logger;
            _recipeService = recipeService;
        }

        [HttpGet("")]
        public IActionResult Get(string recipeName)
        {
            try
            {
                //TODO get recipe name by ingredient


                var recipe = _repository.GetRecipe(recipeName);

              
                return Ok(Mapper.Map<IEnumerable<IngredientViewModel>>(recipe.Ingredients.OrderBy(i => i.DisplayIndex).ToList()));

            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to pull ingredient list from database {0}", ex);
            }

            return BadRequest("Failed to get ingredients");
        }

        [HttpPost("")]
        public async Task<IActionResult> Post(string recipeName,[FromBody]IngredientViewModel vm) 
        {

            try
            {
                //validation
                if (ModelState.IsValid)
                {
                    var newIngredient = Mapper.Map<Ingredient>(vm);
                    //TODO lookup edamam database recipe by ingredients

                    var result = await _recipeService.EdamamAsync(newIngredient.Name); //will look up into edamam object response and find the recipe name by the ingredient name

                    if (!result.Success)
                    {
                        _logger.LogError(result.Message);
                    }

                    else {

                     newIngredient.RecipeName = result.RecipeLabel;
                    _repository.AddIngredient(recipeName, newIngredient);

                    if (await _repository.SaveChangesAsync())
                    { 

                    return Created($"api/recipes/{recipeName}/{newIngredient.Name}", 
                                   Mapper.Map<IngredientViewModel>(newIngredient)); //this class allows us to return both URI and Object
                    }
                    }
                }

                //ingredientviewmodel --> ingredient (entity) ---> ingredientviewmodel. as we have added the reversed method into startup, we can do this now

            }
            catch (Exception ex)
            {

                _logger.LogError("could not save new ingredients {0}", ex);
            }

            return BadRequest("Failed to save new Ingredients");
        }
        
    }
}
