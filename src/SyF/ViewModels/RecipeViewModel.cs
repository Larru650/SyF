using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyF.ViewModels
{
    public class RecipeViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Name { get; set; }
        public string FromPage { get; set; }
        public string ToPage { get; set; }
        public int Calories { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public string Image { get; set; }
        public string Url { get; set; }
        public ICollection<IngredientViewModel> Ingredients { get; set; }
    }
}
