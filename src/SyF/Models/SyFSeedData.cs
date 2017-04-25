using Microsoft.AspNetCore.Identity;
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
        private UserManager<SyFUser> _userManager;

        public SyFSeedData(SyFContext context, UserManager<SyFUser> userManager)
        {
            _context = context;
            _userManager = userManager;


        }

        public async Task SeedData()
        {
            if ( await _userManager.FindByEmailAsync("Alarru@syf.com") == null) 
            {

                var user = new SyFUser() //as is an Entity we can craft it up now
                {
                    UserName = "alarru",
                    Email = "Alarru@syf.com"
                    
                };

                await _userManager.CreateAsync(user, "P@ssw0rd!");


            }

            if(!_context.Recipes.Any())/*add any qual. test*/
            {
                var teriyaki = new Recipe()
                {
                    Name = "Teriyaki Sauce",
                    DateCreated = DateTime.UtcNow,
                    UserName = "alarru",
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
                    UserName = "alarru",
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
