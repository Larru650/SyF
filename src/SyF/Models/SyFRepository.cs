using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyF.Models
{
    public class SyFRepository : ISyFRepository
    {
        private SyFContext _context;
        private ILogger<SyFRepository> _logger;

        public SyFRepository(SyFContext context, ILogger<SyFRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddIngredient(string recipeName, Ingredient newIngredient)
        {

            var recipe = GetRecipe(recipeName);
            if(recipe != null)
            {
                recipe.Ingredients.Add(newIngredient);//set foreign key
                _context.Ingredients.Add(newIngredient); //add it as new object

            }
        }

        public void AddRecipe(Recipe recipe)
        {
            _context.Add(recipe); //we push it into the context as a new oject
        }

     

        public IEnumerable<Recipe> GetAllRecipes()
        {
            _logger.LogInformation("Getting recipes from the database");
            return _context.Recipes.ToList();
        }


        public IEnumerable<Recipe> GetRecipesByUser(string name)
        {
            return _context.Recipes.Where(t => t.UserName == name).ToList();
        }


        public Recipe GetRecipe(string recipeName)
        {
            
            _logger.LogInformation("Getting recipe by recipe name from the database");
            return _context.Recipes
                .Include(r => r.Ingredients)
                .Where(r => r.Name == recipeName)
            .FirstOrDefault();   //we need to return the first recipe from the database that matches the recipe name we are sending in
            
        }

       

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0; //if no rows are affected (it returns 0) we don't need to save any changes.
        }
    }
}
