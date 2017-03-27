using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyF.Models
{
    public class SyFSeedData
    {
        private SyFContext _context;

        public SyFSeedData(SyFContext context)
        {
            _context = context;


        }

        public async Task SeedData()
        {
            if(!_context.Recipes.Any(/*add any qual. test*/))
            {
                var teriyaki = new Recipe()
                {
                    Name = "Teriyaki Sauce",
                    DateCreated = DateTime.UtcNow,
                    UserName = "", //TODO AUTH
                    Ingredients = new List<Ingredient>()
                    {

                    }
                };

                _context.Recipes.Add(teriyaki);
                _context.Ingredients.AddRange(teriyaki.Ingredients);
            }
        }
    }
}
