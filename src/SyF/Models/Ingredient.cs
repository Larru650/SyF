using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SyF.Models
{
    public class Ingredient
    {
        public int IngredientID { get; set; }
        public string Name { get; set; }
        public int DisplayIndex { get; set; } //order
        public double Quantity { get; set; }
        public string Recipe { get; set; }

    }
}