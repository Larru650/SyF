using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyF.Models
{
    public class Recipe  //we need to create 2 models (entities): Recipes and Ingredients
    {
        public int Id { get; set; }
        public string Name { get; set; }  //recipe name   
        public DateTime DateCreated { get; set; }
        public string UserName { get; set; }  //we can get recipes for individual users


        public ICollection<Ingredient> Ingredients { get; set; } //as we will want to add and remove stops we can't use a IEnumerable (as is read only)

    }
}
