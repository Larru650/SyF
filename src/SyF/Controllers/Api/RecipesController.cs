using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SyF.Models;
using SyF.Services;
using SyF.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyF.Controllers.Api
{
    [Authorize]
    public class RecipesController : Controller
    {
        private ILogger<RecipesController> _logger;
        private ISyFRepository _repository;
        private EdamamService _recipeService;

        public RecipesController(ISyFRepository repository, ILogger<RecipesController> logger, EdamamService recipeService)
        {
            _repository = repository;
            _logger = logger; //so we can log the exception error
            _recipeService = recipeService; //add edamam service into the recipescontroller
        }

        [HttpGet("api/recipes")]
        public IActionResult Get(string recipeName)
        {
            try
            {
                var recipe = _repository.GetRecipesByUser(this.User.Identity.Name);
                return Ok(Mapper.Map<IEnumerable<RecipeViewModel>>(recipe).ToList());
            }
            catch (Exception ex)
            {
                //TODO Logging 
                _logger.LogError($"Failed to get all recipes: {ex}");

                return BadRequest("Error occurred");
            }

        }

        [HttpPost("api/recipes")]
        public async Task<IActionResult> Post([FromBody] RecipeViewModel theRecipe)
        {


            if (ModelState.IsValid)
            {
                var newRecipe = Mapper.Map<Recipe>(theRecipe);

                newRecipe.UserName = User.Identity.Name;

                _repository.AddRecipe(newRecipe);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/recipes/{theRecipe.Name}", Mapper.Map<RecipeViewModel>(newRecipe));

                }
            }

            return BadRequest("Bad data");

        }

        [Authorize]
        [HttpPost("api/recipes/newRecipe")]//try a get. new recipe changed to bind it with angular
        public async Task<IActionResult> GetRecipeEdamam(string newRecipe, [FromBody]RecipeViewModel vm) //we pass the vm with 3 props from body: recipe name(q=), frompage,topage)
        {

            try
            {
                //validation
                if (ModelState.IsValid)
                {
                    var temporaryRecipe = Mapper.Map<Recipe>(vm);


                    //TODO lookup edamam database recipe by ingredients

                    //var result = await _recipeService.EdamamAsync(temporaryRecipe.Name, temporaryRecipe.FromPage, temporaryRecipe.ToPage); //will look up into edamam object response and find the recipe name by the ingredient name

                    var result = await _recipeService.EdamamAsync(temporaryRecipe.Name, temporaryRecipe.Calories); //will look up into edamam object response and find the recipe name by the ingredient name


                    if (!result.Success)
                    {
                        _logger.LogError(result.Message);
                    }

                    else
                    {

                        temporaryRecipe.Name = result.RecipeLabel; //we manually map the result properties from edamam service to the model 
                        temporaryRecipe.Calories = result.Calories;
                        _repository.AddRecipe(temporaryRecipe);

                        if (await _repository.SaveChangesAsync())
                        {

                            return Created($"api/recipes/{temporaryRecipe.Name}",
                                           Mapper.Map<RecipeViewModel>(temporaryRecipe)); //this class allows us to return both URI and Object
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




       
        [HttpGet("api/recipes/{newRecipe}")]//try a get. new recipe changed to bind it with angular
        public async Task<IActionResult> GetRecipesForAngular([FromRoute]string newRecipe) //we pass the vm with 3 props from body: recipe name(q=), frompage,topage)
        {
            
            
            //TODO lookup edamam database recipe by ingredients

            //var result = await _recipeService.EdamamAsync(temporaryRecipe.Name, temporaryRecipe.FromPage, temporaryRecipe.ToPage); //will look up into edamam object response and find the recipe name by the ingredient name

            var result = await _recipeService.EdamamAsync(newRecipe); //will look up into edamam object response and find the recipe name by the ingredient name

            newRecipe = result;

            return Ok(newRecipe); //this class allows us to return both URI and Object



        }
    }
}
