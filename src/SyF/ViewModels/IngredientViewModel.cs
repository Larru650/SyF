using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyF.ViewModels
{
    public class IngredientViewModel
    {//the viewmodel will allow us to hide the id property so only the server knows about it

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        public int DisplayIndex { get; set; } //order
        public double Quantity { get; set; }
        public string RecipeName { get; set; } //we will have to convert this to foreign key

    }
}
