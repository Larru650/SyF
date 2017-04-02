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

        public IEnumerable<Recipe> GetAllRecipes()
        {
            _logger.LogInformation("Getting recipes from the database");
            return _context.Recipes.ToList();
        }
    }
}
