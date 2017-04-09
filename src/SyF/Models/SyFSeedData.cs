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
            if(!_context.Recipes.Any())/*add any qual. test*/
            {
                var teriyaki = new Recipe()
                {
                    Name = "Teriyaki Sauce",
                    DateCreated = DateTime.UtcNow,
                    UserName = "", //TODO AUTH
                    Ingredients = new List<Ingredient>()
                    {
                        new Ingredient() { Name = "Soy Sauce", DisplayIndex = 1241 , Quantity = 630  },
                        new Ingredient() { Name = "Honey", DisplayIndex = 1242 , Quantity = 640  },
                        new Ingredient() { Name = "Ginger", DisplayIndex = 12224 , Quantity = 660  }



                    }
                };

                _context.Recipes.Add(teriyaki);
                _context.Ingredients.AddRange(teriyaki.Ingredients);


                var kaleandBS = new Recipe()
                {
                    Name = "Kale and Butternut Squash",
                    DateCreated = DateTime.UtcNow,
                    UserName = "", //TODO AUTH  
                    Ingredients = new List<Ingredient>()
                    {
                        new Ingredient() { Name = "Kale", DisplayIndex = 1 , Quantity = 60  },
                        new Ingredient() { Name = "Butternut Squash", DisplayIndex = 2 , Quantity = 1  },
                        new Ingredient() { Name = "Raisins", DisplayIndex = 3 , Quantity = 10  },
                        new Ingredient() { Name = "Olive Oil", DisplayIndex = 4 , Quantity = 5  }
                        
                    }

                };

                _context.Recipes.Add(kaleandBS);
                _context.Ingredients.AddRange(kaleandBS.Ingredients);


                await _context.SaveChangesAsync();

                
            }
        }
    }
}
