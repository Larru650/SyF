using System.Collections.Generic;

namespace SyF.Models
{
    public interface ISyFRepository
    {
        IEnumerable<Recipe> GetAllRecipes();
    }
}