using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyF.Models
{
    public interface ISyFRepository
    {
        IEnumerable<Recipe> GetAllRecipes(); //for recipe controller
        Recipe GetRecipe(string recipeName); //for ingredient controller
        void AddRecipe(Recipe recipe); 
        void AddIngredient(string recipeName, Ingredient newIngredient);
        Task<bool> SaveChangesAsync();
    }
}